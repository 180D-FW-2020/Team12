using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// including the M2Mqtt Library
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;
using System;

public class MQTT_Sub : MonoBehaviour
{
    private MqttClient client;
    private string clientId;

    // Start is called before the first frame update
    void Start()
    {
        // create client instance 
        client = new MqttClient("mqtt.eclipse.org");

        // register a callback-function (we have to implement, see below) which is called by the library when a message was received
        client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

        // use a unique id as client id, each time we start the application
        clientId = Guid.NewGuid().ToString();

        client.Connect(clientId);
        // subscribe to the topic "ece180d/team12" with QoS 1
        client.Subscribe(new string[] {"ece180d/team12" }, new byte[] { 1 });

    }

    // Update is called once per frame
    void Update()
    {

    }

    void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        Debug.Log("Received: " + System.Text.Encoding.UTF8.GetString(e.Message));
    }

}
