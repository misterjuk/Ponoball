using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
namespace FileManager
{
    public class FileManager
    {
        public static bool WriteToFile(string a_FileName, string a_FileContents)
        {
        var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);
        try
        {
            File.WriteAllText(fullPath, a_FileContents);
            return true;
        }
        catch (Exception e)
        {
            Debug.Log($"Failed to write to {fullPath} with exception {e}");
        }
        return false;
        }

        public static bool LoadFromFile(string a_FileName, out string result)
        {
            var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);

            try
            {
                result = File.ReadAllText(fullPath);
                return true;
            }
            catch(Exception e)
            {
                Debug.Log($"Failed to write to {fullPath} with exception {e}");
                result = "";
                return false;
            }
        }
    }
}
