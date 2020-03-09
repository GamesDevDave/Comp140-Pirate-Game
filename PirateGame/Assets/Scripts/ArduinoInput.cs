using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.IO.Ports;
public class ArduinoInput : MonoBehaviour
{
    public int commPort = 6;
    public int steerValue;

    private SerialPort serial;

    // Use this for initialization
    void Start()
    {
        ConnectToSerial();
    }

    // Update is called once per frame
    void Update()
    {

        serial.ReadLine();
    }

    void ConnectToSerial()
        {
            Debug.Log("Attempting Serial: " + commPort);

            serial = new SerialPort("\\\\.\\COM" + commPort, 9600);
            serial.ReadTimeout = 50;
            serial.Open();
        }
}
    