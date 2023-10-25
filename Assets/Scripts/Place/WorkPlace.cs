using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkPlace : Place
{
    public List<Character> employees = new List<Character>();

    public void AddEmployee(Character character)
    {
        employees.Add(character);
    }

    public void RemoveEmployee(Character character)
    {
        employees.Remove(character);
    }

    private void Start()
    {
        placeType = PlaceType.Work;
        PlaceManager.Instance.AddPlace(this);
    }
}
