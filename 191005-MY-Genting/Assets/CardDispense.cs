using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDDLL;
using System.IO.Ports;
using UnityEngine.UI;
using System;

public class CardDispense : MonoBehaviour
{
    public string[] ports;
    public Dropdown PortName;

    void Start()
    {
        ports = SerialPort.GetPortNames();
        PortName.options.Clear();
        foreach (string cc in ports)
        {
            PortName.options.Add(new Dropdown.OptionData() { text = cc });
        }
        PortName.value = 1;
        PortName.value = 0;
    }

    void Update()
    {

    }

    public void SetPortName()
    {
        string nameport = PortName.options[PortName.value].text;
        PlayerPrefs.SetString("PortName_CardDispenser", nameport);
    }

    public void DispenseCard()
    {
        uint lHcom;
        int iRet;
        byte addrH = byte.Parse("1");
        byte addrL = byte.Parse("5");

        string card_port = PlayerPrefs.GetString("PortName_CardDispenser", "COM9");
        // Replace the COM Port number with appropriate COM port string
        lHcom = DllClass.CommOpenWithBaut(card_port, 3);

        // Reset machine
        iRet = DllClass.CRT541_Reset(lHcom, addrH, addrL);

        // If in case code proceed too fast causing card unable to dispense, try enable the following
        Thread.Sleep(1000);

        // Dispense Card
        iRet = DllClass.CRT541_Eject(lHcom, addrH, addrL);

        // Close the serial COM port
        iRet = DllClass.CommClose(lHcom);
    }
}
