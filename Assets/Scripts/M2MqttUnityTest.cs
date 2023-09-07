/*
The MIT License (MIT)

Copyright (c) 2018 Giovanni Paolo Vigano'

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;

/// <summary>
/// Examples for the M2MQTT library (https://github.com/eclipse/paho.mqtt.m2mqtt),
/// </summary>
namespace M2MqttUnity.Examples
{
    /// <summary>
    /// Script for testing M2MQTT with a Unity UI
    /// </summary>
    public class M2MqttUnityTest : M2MqttUnityClient
    {
        [Tooltip("Set this to true to perform a testing cycle automatically on startup")]
        public bool autoTest = false;
        [Header("Topics")]
        public string subscribeTopic = "wifi/signal";
        public string publishTopic = "control/command";

        public float targetTime = 5.0f;

        public static List<string> idList { get; set; } = new List<string>();
        public static int requestedStrength { get; set; }

        public void SetBrokerAddress(string brokerAddress)
        {        
                this.brokerAddress = brokerAddress;
        }

        public void SetBrokerPort(string brokerPort)
        {
                int.TryParse(brokerPort, out this.brokerPort);
        }

        public void SetEncrypted(bool isEncrypted)
        {
            this.isEncrypted = isEncrypted;
        }

        protected override void OnConnecting()
        {
            base.OnConnecting();
            Debug.Log("Connecting to broker on " + brokerAddress + ":" + brokerPort.ToString() + "...\n");
        }

        protected override void OnConnected()
        {
            base.OnConnected();
            Debug.Log("Connected to broker on " + brokerAddress + "\n");            
        }

        protected override void SubscribeTopics()
        {
            client.Subscribe(new string[] { subscribeTopic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }

        protected override void UnsubscribeTopics()
        {
            client.Unsubscribe(new string[] { subscribeTopic });
        }

        protected override void OnConnectionFailed(string errorMessage)
        {
            Debug.Log("CONNECTION FAILED! " + errorMessage);
        }

        protected override void OnDisconnected()
        {
            Debug.Log("Disconnected.");
        }

        protected override void OnConnectionLost()
        {
            Debug.Log("CONNECTION LOST!");
        }

        protected override void Start()
        {
            Debug.Log("Ready.");
            base.Start();
            Connect();
        }
        //protected override void Update()
        //{
        //    targetTime -= Time.deltaTime;

        //    if (targetTime <= 0.0f && ButtonsHandler.activeButtonId != null)
        //    {
        //        Publish(ButtonsHandler.activeButtonId);
        //    }
        //}
        //public void Publish(string messageToPublish)
        //{
        //    targetTime = 5.0f;
        //    client.Publish(publishTopic, System.Text.Encoding.UTF8.GetBytes(messageToPublish), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        //    Debug.Log("Requesting signal strength of MAC address: " + messageToPublish);
        //}

        protected override void DecodeMessage(string topic, byte[] message) // this receives addresses + strength
        {
            string msg = System.Text.Encoding.UTF8.GetString(message);
            //Debug.Log("Received: " + idList.Count);
            if (msg.Length == 17)
                StoreMessage(msg,1);
            else if (msg.Length < 4)
                StoreMessage(msg, 2);
            else
                Debug.Log(msg);               
        }

        private void StoreMessage(string eventMsg, int msgFlag)
        {
            if(msgFlag == 1)
                idList.Add(eventMsg);
            else
            {
                requestedStrength = int.Parse(eventMsg);
                Debug.Log(requestedStrength);
            }
                
        }

        private void OnDestroy()
        {
            Disconnect();
        }

        private void OnValidate()
        {
            if (autoTest)
            {
                autoConnect = true;
            }
        }
    }
}
