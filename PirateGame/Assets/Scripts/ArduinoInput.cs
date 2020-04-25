using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using System.Collections;

public class ArduinoInput : MonoBehaviour
{

    public GameObject player;
    public bool controllerActive = true;
    public int commPort = 3;
    public float steerValue;
    public float cannonValue;
    string value;
    public float anchorButtonPressed;
    public float fireButtonPressed;
    public bool anchorButtonBool = false;

    private SerialPort serial = null;
    private bool connected = true;

    // Use this for initialization
    void Start()
    {
        ConnectToSerial();
    }

    void ConnectToSerial()
    {
        Debug.Log("Attempting Serial: " + commPort);

        // Read this: https://support.microsoft.com/en-us/help/115831/howto-specify-serial-ports-larger-than-com9
        serial = new SerialPort("\\\\.\\COM" + commPort, 9600);
        serial.ReadTimeout = 50;
        serial.Open();

    }

    // Update is called once per frame
    void Update()
    {
        if (controllerActive)
        {
            WriteToArduino("I");                // Ask for the positions
            value = ReadFromArduino(50); // read the positions

            if (value != null)                  // check to see if we got what we need
            {
                // EXPECTED VALUE FORMAT: "0-1023"
                string[] values = value.Split('-');     // split the values

                if (values.Length == 4)
                {
                    RemappingValues(values);
                }
            }
        }
    }

    void RemappingValues(String[] values)
    {
        steerValue = Remap(int.Parse(values[0]), 0, 1023, -5, 5);            // scale the input. this could be done on the Arduino as well.
        cannonValue = Remap(int.Parse(values[1]), 0, 1023, -5, 5);              // scale the input. this could be done on the Arduino as well.
        anchorButtonPressed = Remap(int.Parse(values[2]), 0, 1, 0, 1);
        fireButtonPressed = Remap(int.Parse(values[3]), 0, 1, 0, 1);

        if (anchorButtonPressed == 1)
        {
            anchorButtonBool = true;
        }
        else
        {
            anchorButtonBool = false;
        }
    }


    void WriteToArduino(string message)
    {
        serial.WriteLine(message);
        serial.BaseStream.Flush();
    }

    public string ReadFromArduino(int timeout = 0)
    {
        serial.ReadTimeout = timeout;
        try
        {
            return serial.ReadLine();
        }
        catch (TimeoutException e)
        {
            return null;
        }
    }

    // be sure to close the serial when the game ends.
    void OnDestroy()
    {
        Debug.Log("Exiting");
        serial.Close();
    }

    // https://forum.unity.com/threads/re-map-a-number-from-one-range-to-another.119437/
    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}