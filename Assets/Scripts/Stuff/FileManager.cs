using UnityEngine;
using System.IO;
using System;
using System.Xml.Serialization;

public static class FileManager
{
    private static string filePath = Application.dataPath + @"/Data/";


    private static void CreateFile(string fileName)
    {
        if(Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
        if(!File.Exists(filePath + fileName))
        {
            FileStream f = File.Create(filePath + fileName);
            f.Close();
        }
    }
    
    public static object OpenSaves<T>(string fileName)
    {
        CreateFile(fileName);
        object result = null;
        try
        {
            using(FileStream fs = File.Open(filePath + fileName, FileMode.OpenOrCreate))
            {
                var serializer = new XmlSerializer(typeof(T));
                result = serializer.Deserialize(fs);
            }
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message);
        }
        return result;
    }

    public static void SaveFile<T>(T saveData, string fileName)
    {
        try
        {
            using(StreamWriter sw = new StreamWriter(filePath + fileName))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(sw,saveData);
                sw.Flush(); //cleans buffer
            }

        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }


}