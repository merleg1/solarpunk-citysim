using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour
{
    public enum PlaceType
    {
        Eat,
        Hobby,
        Home,
        Work
    }

    public PlaceType placeType;

    public string placeName;

    public string placeDescription;

    private void Start()
    {
        PlaceManager.Instance.AddPlace(this);
    }

}
