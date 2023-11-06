using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToWorkTask : MoveToPosTask
{
    public GoToWorkTask(Character character, TaskPriority taskPriority = TaskPriority.Medium) : base(GetWorkPosition(character), character.GetNavMeshAgent(), 10f, taskPriority)
    {
    }

    private static Vector3 GetWorkPosition(Character character)
    {
        Place workPlace = PlaceManager.Instance.GetWorkPlaceForCharacter(character);
        return workPlace.transform.position;
    }
}
