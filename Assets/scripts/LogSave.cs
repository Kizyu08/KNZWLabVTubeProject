using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public class LogSave : MonoBehaviour
{
    public void logSave(string txt)
    {
        StreamWriter sw;
        FileInfo fi;
        fi = new FileInfo(Application.dataPath + "/logfile.txt");
        sw = fi.AppendText();
        sw.WriteLine(txt);
        sw.Flush();
        sw.Close();
    }
}