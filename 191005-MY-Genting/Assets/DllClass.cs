using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace CDDLL
{
    public class DllClass
    {
        /*open comm by Baudrate
         * Baudrate=0,1,2,3,4,5;(1200,2400,4800,9600,19200,38400)
         * CommOpenWithBaut(3,3):com3 and baudrate is 9600
       */
        [DllImport("CRT_541.dll")]
        public static extern uint CommOpenWithBaut(string port, byte Baudrate);


        //close comm
        [DllImport("CRT_541.dll")]
        public static extern int CommClose(uint ComHandle);


        //Eject Card
        [DllImport("CRT_541.dll")]
        public static extern int CRT541_Eject(uint ComHandle, byte AddrH, byte AddrL);

        //Reset
        [DllImport("CRT_541.dll")]
        public static extern int CRT541_Reset(UInt32 ComHandle, byte AddrH, byte AddrL);
    }
}