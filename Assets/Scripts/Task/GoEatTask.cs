using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoEatTask : MoveToPosTask
{
    public GoEatTask(Character character) : base(GetClosestEatPlacePosition(character), character.GetNavMeshAgent())
    {
    }

    private static Vector3 GetClosestEatPlacePosition(Character character)
    {
        Place closestEatPlace = PlaceManager.Instance.GetClosestPlace(character, Place.PlaceType.Eat);
        return closestEatPlace.transform.position;
    }
}
