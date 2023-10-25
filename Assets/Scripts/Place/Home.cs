using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : Place
{
    public List<Character> characters = new List<Character>();
    public int maxCharacters = 3;

    public void AddCharacter(Character character)
    {
        characters.Add(character);
    }

    public void RemoveCharacter(Character character)
    {
        characters.Remove(character);
    }

    private void Start()
    {
        placeType = PlaceType.Home;
        PlaceManager.Instance.AddPlace(this);
    }
}
