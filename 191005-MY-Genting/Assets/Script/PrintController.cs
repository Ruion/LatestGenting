using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PrintController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Print(string a)
    {
        var proc = new System.Diagnostics.Process();
        proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        proc.StartInfo.Verb = "print";

/*        string init_path = Application.streamingAssetsPath + "/PDFtoPrinter.exe";
        string init_path2 = Application.streamingAssetsPath + "/" + a + ".pdf";
        Debug.Log(init_path);
        Debug.Log(init_path2);
        string[] DataPath1 = init_path.Split('/');
        string final_path = "";

        int tempnum = 0;
        int tempnum2 = 0;

        foreach (string i in DataPath1)
        {
            if (tempnum == DataPath1.Length - 1)
            {
                final_path += i;
            }
            else
            {
                final_path += i + @"\";
            }
            tempnum += 1;
        }
        string[] DataPath2 = init_path2.Split('/');
        string final_path2 = "";
        foreach (string i in DataPath2)
        {
            if (tempnum2 == DataPath2.Length - 1)
            {
                final_path2 += i;
            }
            else
            {
                final_path2 += i + @"\";
            }
            tempnum2 += 1;
        }
        Debug.Log(final_path);
        Debug.Log(final_path2);*/
        // Using PDFtoPrinter
        proc.StartInfo.FileName = @"C:\Unity_Print\PDFtoPrinter.exe";
        proc.StartInfo.Arguments = string.Format(@"c:\Unity_Print\" + a + ".pdf");

        proc.StartInfo.UseShellExecute = false;
        proc.StartInfo.CreateNoWindow = true;

        proc.Start();

        if (proc.HasExited == false)
        {
            proc.WaitForExit(3000);
        }

        proc.EnableRaisingEvents = true;

        proc.Close();
    }

    private static bool KillAdobe(string name)
    {
        foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcesses().Where(
            process => process.ProcessName.StartsWith(name)))
        {
            process.Kill();
            return true;
        }
        return false;
    }
}
