using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHomeTask : MoveToPosTask
{
    public GoHomeTask(Character character, TaskPriority taskPriority = TaskPriority.Medium) : base(GetHomePosition(character), character.GetNavMeshAgent(), 0f, taskPriority)
    {
    }

    private static Vector3 GetHomePosition(Character character)
    {
        Home home = PlaceManager.Instance.GetHomeForCharacter(character);
        return home.transform.position;
    }
}
