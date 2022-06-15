using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour, ISaveable
{
    private float audioVolume;
    private float effectsVolume;

    private void Start()
    {
        LoadJsonData(this);
    }
    public void SaveData()
    {
        SaveJsonData(this);
    }
    private static void SaveJsonData(Settings settings)
    {
        SaveData sd = new SaveData();
        settings.PopulateSaveData(sd);
        if (FileManager.FileManager.WriteToFile("SaveData.dat", sd.ToJson()))
        {
            Debug.Log("Save successful");
        }
    }
    public void PopulateSaveData(SaveData a_SaveData)
    {
        a_SaveData.m_AudioVolume  = audioVolume;
        a_SaveData.m_EffectsVolume = effectsVolume;
    }
    private static void LoadJsonData(Settings settings)
    {
        if (FileManager.FileManager.LoadFromFile("SaveData.dat", out var json))
        {
            SaveData sd = new SaveData();
            sd.LoadFromJson(json);

            settings.LoadFromSaveData(sd);
            Debug.Log("load complete");
        }
    }
    public void LoadFromSaveData(SaveData a_SaveData)
    {
        audioVolume = a_SaveData.m_AudioVolume;
        effectsVolume = a_SaveData.m_EffectsVolume;
    }
}
