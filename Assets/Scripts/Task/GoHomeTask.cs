using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHomeTask : MoveToPosTask
{
    public GoHomeTask(Character character) : base(GetHomePosition(character), character.GetNavMeshAgent(), 5f)
    {
    }

    private static Vector3 GetHomePosition(Character character)
    {
        Home home = PlaceManager.Instance.GetHomeForCharacter(character);
        return home.transform.position;
    }
}
