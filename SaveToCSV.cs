using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class SaveToCSV : MonoBehaviour
{
    // Stringbuilder will concatenate many strings into lines and parapgraphs  [ https://msdn.microsoft.com/en-us/library/system.text.stringbuilder(v=vs.110).aspx ]
    StringBuilder sb = new StringBuilder();
    string fileTitle;

    // Use this for initialization
    void Start()
    {
        sb.Length = 0;
        sb.AppendLine("Date, Hit Name, Hit Object, Is Changed");
    }

    // This will take 4 variables from another class and append them into 4 columns of data for each object
    public void Add(string date, string hitName, string hitobj, bool isChangedBool) {
        sb.AppendLine(String.Format("{0},{1},{2},{3}", date, hitName, hitobj, isChangedBool));
    }

    public void FileName(string fileName)
    {
        fileTitle = fileName;
    }

    public void Save()
    { 
        string filePath = getPath();
        Debug.Log("Saved to:" + filePath);

        // Writes stringbuilder to a fill     [ https://msdn.microsoft.com/en-us/library/system.io.streamwriter(v=vs.110).aspx ]
        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    // Following method is used to retrive the relative path as device platform
    private string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath +"/CSV/"+ fileTitle + ".csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath + fileTitle + ".csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+ fileTitle + ".csv";
#else
        return Application.dataPath + "/" + fileTitle + ".csv";
#endif
    }
}
