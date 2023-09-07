using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsHandler : MonoBehaviour
{
    public static string activeButtonId = null;
    public static void ButtonsToggle(Dictionary<string, bool> buttonsState, string buttonId)
    {
        if(activeButtonId == null)
        {
           
            buttonsState[buttonId] = true;
            activeButtonId = buttonId;
        }
        else if(activeButtonId == buttonId) {
            buttonsState[buttonId] = true;
            activeButtonId = null;
        }
        else
        {
            buttonsState[activeButtonId] = false;
            buttonsState[buttonId] = true;
            activeButtonId = buttonId;           
        }            
    }
}
