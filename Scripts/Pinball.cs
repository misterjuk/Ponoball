using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Config;

public class Pinball : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float _force;
    [SerializeField]
    private float _defaultForce;
    [SerializeField]
    private float maxSpeed = 20.0f;
    [SerializeField]
    [Range(0.0f,1.0f)]
    private float k = 0.5f; //variable for lowering force when ball hit some object
    // Start is called before the first frame update
    public delegate void OnHit(int score);
    public static event OnHit AddScore;
    
    void Start()
    {
        _force = _defaultForce;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(GetComponent<Rigidbody2D>().velocity, maxSpeed);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D point = collision.GetContact(0);
        Obstacle obstacle = collision.gameObject.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            //collision.gameObject.GetComponent<Obstacle>
            obstacle.MultiplyScale(0.75f);
            obstacle.DecreaseHealth(1);
            switch (obstacle.Type)
            {
                case ObstacleType.Common:
                    break;
                case ObstacleType.Sticky:
                    _force *= 0.25f;
                    break;
                case ObstacleType.Bouncy:
                    _force = _defaultForce * 1.5f; ;
                    break;
            }        
            AddScore(obstacle.ScorePerHit);
        }
        rb.AddForce(point.normal * _force, ForceMode2D.Impulse);
        _force *= k;
    }
}
