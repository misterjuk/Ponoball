using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FileManager;

public class GameManager : MonoBehaviour, ISaveable
{
    [SerializeField]
    private int _score;
    private int _bestScore;
    [SerializeField]
    private int maxhealth;
    [SerializeField]
    private int health;

    public delegate void OnChange(int current);
    public static event OnChange UpdateScoreUI;
    public static event OnChange UpdateHealthUI;
    public static event OnChange GameOver;

    [Header("Spawner")]
    [SerializeField]
    private List<GameObject> gameObjectsToSpawn;
    private List<GameObject> currentWave = new List<GameObject>();
    [SerializeField]
    private GameObject ball;
    private GameObject currentball;
    [SerializeField]
    private List<GameObject> ballspawnpoints;

    [Header("Adds")]
    [SerializeField]
    private InterstitialAdExample interstitialAd;
    void Start()
    {
        LoadJsonData(this);

        Pinball.AddScore += UpdateScore;

        _score = 0;
        UpdateScoreUI(_score);
        health = maxhealth;
        SpawnWave();
        InstantiateBall();
        //SpawnWave
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentWave != null)
        {
            foreach (GameObject obstacle in currentWave)
            {
                if (obstacle != null)
                {
                    Vector2 direction = new Vector3(Random.Range(-4.0f,4.0f), Random.Range(2.0f, 4.0f), 0.0f) - obstacle.transform.position;
                    obstacle.GetComponent<Rigidbody2D>().AddForce(direction.normalized*0.5f, ForceMode2D.Force);
                    Debug.DrawLine(obstacle.transform.position, new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(2.0f, 4.0f), 0.0f));
                }
            }
        }
        if(Input.touchCount >= 1)
        {
            Debug.Log(Input.GetTouch(0).position);
        }
    }
    void UpdateScore(int score)
    {
        _score += score;
        UpdateScoreUI(_score);
    }
    private void SpawnWave()
    {
        for(int i = 0; i < Random.Range(3,6);i++)
        {
            GameObject go = Instantiate(gameObjectsToSpawn[(Random.Range(0, gameObjectsToSpawn.Count))],
                transform.position + new Vector3(Random.Range(-2.0f,2.0f),Random.Range(-2.0f,2.0f),0), new Quaternion());
            currentWave.Add(go);
        }
        //spawn wave
    }
    private void InstantiateBall()
    {
        currentball = Instantiate(ball, ballspawnpoints[Random.Range(0,ballspawnpoints.Count)].transform.position, new Quaternion());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        health--;
        UpdateHealthUI(health);
        if (health > 0)
        {
            Destroy(currentball);
            InstantiateBall();
        }
        else
        {
            if(_score >= _bestScore)
            {
                _bestScore = _score;
            }
            SaveJsonData(this);
            interstitialAd.ShowAd();
            GameOver(_score);        
            //save scores and load new scene
        }
    }
    public void GameRestart()
    {
        currentball = null;
        if(currentWave.Count != 0)
        {
            foreach(GameObject go in currentWave)
            {
                Destroy(go);
            }
            currentWave.Clear();
        }

        health = maxhealth;
        _score = 0;

        InstantiateBall();
        SpawnWave();
    }

    //SAVING AND LOADING 
    private static void SaveJsonData(GameManager gameManager)
    {
        SaveData sd = new SaveData();
        gameManager.PopulateSaveData(sd);
        if(FileManager.FileManager.WriteToFile("SaveData.dat", sd.ToJson()))
        {
            Debug.Log("Save successful");
        }        
    }
    public void PopulateSaveData(SaveData a_SaveData)
    {
        a_SaveData.m_BestScore = _bestScore;
    }
    private static void LoadJsonData(GameManager gameManager)
    {
        if (FileManager.FileManager.LoadFromFile("SaveData.dat", out var json))
        {
            SaveData sd = new SaveData();
            sd.LoadFromJson(json);

            gameManager.LoadFromSaveData(sd);
            Debug.Log("load complete");
        }
    }
    public void LoadFromSaveData(SaveData a_SaveData)
    {
        _bestScore = a_SaveData.m_BestScore;
    }
}



