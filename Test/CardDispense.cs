using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDispense : MonoBehaviour
{

    [ContextMenu("Dispense")]
    public void DispenseCard()
    {
        uint lHcom;
        int iRet;

        // Replace the COM Port number with appropriate COM port string
        lHcom = CDDLL.DllClass.CommOpenWithBaut("COM9", 3);

        // If in case code proceed too fast causing card unable to dispense, try enable the following
        //Thread.Sleep(500);

        iRet = CDDLL.DllClass.CRT541_Eject(lHcom, byte.Parse("1"), byte.Parse("5"));

        iRet = CDDLL.DllClass.CommClose(lHcom);
    }
}
