using UnityEngine;
using System.Collections;

public class ActivateAllDisplays : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("displays connected: " + Display.displays.Length);
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }

    private void Update()
    {

    }
}
