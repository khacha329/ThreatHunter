using M2MqttUnity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using M2MqttUnity.Examples;
using Microsoft.MixedReality.Toolkit.Experimental.SceneUnderstanding;

public class ButtonToggler : MonoBehaviour
{
    TextMeshPro buttonId;

    private void Start()
    {
        buttonId = gameObject.GetComponent<TextMeshPro>();
    }

    public void OnToggle()
    {
        ButtonsHandler.ButtonsToggle(GridManager.buttonsState, buttonId.text);
        //DemoSceneUnderstandingController.ToggleWorld(); // enables world mesh in augmented environment
        Debug.Log("Button Toggled");
    }
}
