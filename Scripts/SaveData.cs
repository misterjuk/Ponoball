using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int m_BestScore;

    public float m_AudioVolume;
    public float m_EffectsVolume;

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
    public void LoadFromJson(string a_Json)
    {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }
}

public interface ISaveable
{
    void PopulateSaveData(SaveData a_saveData);
    void LoadFromSaveData(SaveData a_saveData);
}