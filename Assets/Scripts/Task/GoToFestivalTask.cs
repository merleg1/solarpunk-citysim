using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToFestivalTask : MoveToPosTask
{
    public GoToFestivalTask(Character character, TaskPriority taskPriority = TaskPriority.High) : base(GetClosestFestivalPosition(character), character.GetNavMeshAgent(), 0f, taskPriority)
    {
    }

    private static Vector3 GetClosestFestivalPosition(Character character)
    {  
        Vector3 festivalPosition = PlaceManager.Instance.GetClosestPlace(character, Place.PlaceType.Festival).transform.position;
        return festivalPosition;
    }
}
