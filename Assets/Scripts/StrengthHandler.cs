using M2MqttUnity.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthHandler : MonoBehaviour
{

    [SerializeField] private Material myMaterial;



    // Start is called before the first frame update
    void Start()
    {
        var red = 239;
        var green = 133;
        var blue = 20;

        var newColor = new Color(red, green, blue);
        myMaterial.SetColor("_BaseColor", Color.black);
        myMaterial.SetColor("_WireColor", Color.white);

    }

    // Update is called once per frame
    void Update()
    {
        if(ButtonsHandler.activeButtonId != null)
        {
            if (M2MqttUnityTest.requestedStrength > -80 && M2MqttUnityTest.requestedStrength < -67) //weak signal
            {
                myMaterial.SetColor("_WireColor", Color.red);

            }
            else if (M2MqttUnityTest.requestedStrength >= -67 && M2MqttUnityTest.requestedStrength < -30) //medium signal
            {
                myMaterial.SetColor("_WireColor", Color.yellow);
            }
            else if (M2MqttUnityTest.requestedStrength >= -30)// Streong Signal
            {
                myMaterial.SetColor("_WireColor", Color.green);
            }
        }
        else 
        {
            myMaterial.SetColor("_BaseColor", Color.black);
            myMaterial.SetColor("_WireColor", Color.white);
        }
        
    }
}
