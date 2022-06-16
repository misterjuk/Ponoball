using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, ISaveable
{
    // Start is called before the first frame update
    [SerializeField]
    private AudioSource audioSource;

    private float audioVolume;
    private float effectsVolume;

    void Start()
    {
        LoadJsonData(this);
        audioSource.volume = audioVolume;
        audioSource.playOnAwake = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopulateSaveData(SaveData a_SaveData)
    {

    }
    private static void LoadJsonData(AudioManager audioManager)
    {
        if (FileManager.FileManager.LoadFromFile("SaveData.dat", out var json))
        {
            SaveData sd = new SaveData();
            sd.LoadFromJson(json);

            audioManager.LoadFromSaveData(sd);
            Debug.Log("load complete");
        }
    }
    public void LoadFromSaveData(SaveData a_SaveData)
    {
        audioVolume = a_SaveData.m_AudioVolume;
        effectsVolume = a_SaveData.m_EffectsVolume;
    }
}
