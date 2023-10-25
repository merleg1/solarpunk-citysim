using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToHobbyTask : MoveToPosTask
{
    public GoToHobbyTask(Character character) : base(GetClosestHobbyPlacePosition(character), character.GetNavMeshAgent())
    {
    }

    private static Vector3 GetClosestHobbyPlacePosition(Character character)
    {
        Place closestHobbyPlace = PlaceManager.Instance.GetClosestPlace(character, Place.PlaceType.Hobby);
        return closestHobbyPlace.transform.position;
    }
}
