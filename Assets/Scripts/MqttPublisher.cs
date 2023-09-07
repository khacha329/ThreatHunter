using UnityEngine;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity.Examples;
using M2MqttUnity;

public class MqttPublisher : MonoBehaviour
{
    public string publishTopic = "control/command";
    public float targetTime = 5.0f;
    // Update is called once per frame
    void Update()
    {        
        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f && ButtonsHandler.activeButtonId != null)
        {
            Publish(ButtonsHandler.activeButtonId);
        }
    }
    public void Publish(string messageToPublish)
    {
        targetTime = 5.0f;
        M2MqttUnityClient.client.Publish(publishTopic, System.Text.Encoding.UTF8.GetBytes(messageToPublish), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Debug.Log("Requesting signal strength of MAC address: " + messageToPublish);
    }
}

