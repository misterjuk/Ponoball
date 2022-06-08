using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FileManager;

public class UIManager : MonoBehaviour, ISaveable
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private GameObject options;

    [Header("HealthBar")]
    [SerializeField]
    private List<GameObject> healthbarsprites;

    [Header("RestartWindow")]
    [SerializeField]
    private GameObject restartWindow;
    [SerializeField]
    private TextMeshProUGUI finalScoreText;
    [SerializeField]
    private TextMeshProUGUI bestScoreText;
    private int _bestScore;

    private void Start()
    {
        GameManager.UpdateScoreUI += SetScore;
        GameManager.UpdateHealthUI += UpdateHealthBar;
        GameManager.GameOver += SetGameOverWindow;

    }
    private void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }
    private void UpdateHealthBar(int currenthealth)
    {
        healthbarsprites[currenthealth].SetActive(false);
    }
    public void ToggleOptions()
    {
        if(options.activeSelf)
        {
            options.SetActive(!options.activeSelf);
        }
        else
        {
            options.SetActive(true);
        }
    }
    private void SetGameOverWindow(int finalscore)
    {
        restartWindow.SetActive(true);
        finalScoreText.text = "Score:" + finalscore.ToString();
        LoadJsonData(this);
        bestScoreText.text = "Best score:" + _bestScore.ToString();
    }
    public void ResetUI()
    {
        restartWindow.SetActive(false);
        foreach(GameObject sprite in healthbarsprites)
        {
            sprite.SetActive(true);
        }
        SetScore(0);
    }


    //SAVING AND LOADING 
    private static void SaveJsonData(UIManager UIManager)
    {
        SaveData sd = new SaveData();
        UIManager.PopulateSaveData(sd);
        if (FileManager.FileManager.WriteToFile("SaveData.dat", sd.ToJson()))
        {
            Debug.Log("Save successful");
        }
    }
    public void PopulateSaveData(SaveData a_SaveData)
    {
       
    }
    private static void LoadJsonData(UIManager UIManager)
    {
        if (FileManager.FileManager.LoadFromFile("SaveData.dat", out var json))
        {
            SaveData sd = new SaveData();
            sd.LoadFromJson(json);

            UIManager.LoadFromSaveData(sd);
            Debug.Log("load complete");
        }
    }
    public void LoadFromSaveData(SaveData a_SaveData)
    {
        _bestScore = a_SaveData.m_BestScore;
    }
}
