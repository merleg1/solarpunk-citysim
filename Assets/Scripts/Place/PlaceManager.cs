using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceManager : MonoBehaviour
{

    public static PlaceManager Instance { get; private set; }

    private readonly List<Place> _places = new List<Place>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AddPlace(Place place)
    {
        _places.Add(place);
    }

    public void RemovePlace(Place place)
    {
        _places.Remove(place);
    }

    public Home GetHomeForCharacter(Character character)
    {
       foreach (Place place in _places)
        {
            if (place.placeType == Place.PlaceType.Home)
            {
                Home home = (Home)place;
                if (home.characters.Contains(character))
                {
                    return home;
                }
            }
        }
        return AddCharacterToHome(character);
    }

    public Home AddCharacterToHome(Character character)
    {
        foreach (Place place in _places)
        {
            if (place.placeType == Place.PlaceType.Home)
            {
                Home home = (Home)place;
                if (home.characters.Count < home.maxCharacters)
                {
                    home.AddCharacter(character);
                    return home;
                }
            }
        }
        return null;
    }

    public WorkPlace GetWorkPlaceForCharacter(Character character)
    {
        foreach (Place place in _places)
        {
            if (place.placeType == Place.PlaceType.Work)
            {
                WorkPlace workPlace = (WorkPlace)place;
                if (workPlace.employees.Contains(character))
                {
                    return workPlace;
                }
            }
        }
        return AddCharacterToWorkPlace(character);
    }

    public WorkPlace AddCharacterToWorkPlace(Character character)
    {
        Home home = GetHomeForCharacter(character);
        WorkPlace workPlace = GetClosestPlace(home.transform.position, Place.PlaceType.Work) as WorkPlace;
        if (workPlace != null)
        {
            workPlace.AddEmployee(character);
        }
        return workPlace;
    }

    public Place GetRandomPlace(Place.PlaceType placeType)
    {
        List<Place> places = new List<Place>();
        foreach (Place place in _places)
        {
            if (place.placeType == placeType)
            {
                places.Add(place);
            }
        }
        if (places.Count > 0)
        {
            return places[Random.Range(0, places.Count)];
        }
        return null;
    }

    public Place GetClosestPlace(Character character, Place.PlaceType placeType)
    {
        return GetClosestPlace(character.transform.position, placeType);
    }

    public Place GetClosestPlace(Vector3 position, Place.PlaceType placeType)
    {
        Place closestPlace = null;
        float closestDistance = float.MaxValue;

        foreach (Place place in _places)
        {
            if (place.placeType == placeType)
            {
                float distance = Vector3.Distance(position, place.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPlace = place;
                }
            }
        }

        return closestPlace;
    }
    
}
