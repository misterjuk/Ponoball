using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Config;

public class Obstacle : MonoBehaviour
{
    public delegate void OnDestroy(GameObject gameObject);
    public static event OnDestroy ObstacleDestroyed;

    public ObstacleType Type;
    [SerializeField]
    public int ScorePerHit = 10;
    [SerializeField]
    private ParticleSystem particleSystem;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private int maxHealth = 3;
    [SerializeField]
    private int currentHealth;

    private Vector3 currentScale;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rb.AddForce(new Vector2(Random.Range(-0.2f,0.2f), Random.Range(-0.2f, 0.2f)), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       // particleSystem.Play();
    }
    public void MultiplyScale(float multiply)
    {
        transform.localScale *= multiply;
    }
    public void DecreaseHealth(int number)
    {
        if (currentHealth > 1)
        {
            currentHealth -= number;
        }
        else 
        {
            ObstacleDestroyed(this.gameObject);
            Destroy(gameObject);
        }
    }

}
