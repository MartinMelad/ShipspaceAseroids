using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private GameObject explosionPrefab;
    
    private int health = 5;

    private GameObject Player;
    
    private Rigidbody2D rb2d; 
    
    private Vector2 thrustDirection = new Vector2(1,0);

    private Vector3 newDirection;

    private float distance;

    private float disShip;

    private Timer FireTimer;
    
    bool flag = false;

    private float am = 0;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 enimeyPos = new Vector3(Random.Range(ScreenUtils.ScreenLeft + 2, ScreenUtils.ScreenRight - 2),
            ScreenUtils.ScreenTop, 0);
        transform.position = enimeyPos;
        
        rb2d = gameObject.GetComponent<Rigidbody2D>();

        gameObject.AddComponent<Timer>();

        FireTimer = gameObject.AddComponent<Timer>();
        FireTimer.Duration = 1f;
        Player = GameObject.FindWithTag("Ship");
        newDirection = (Player.transform.position - transform.position);
        FireTimer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        newDirection = (Player.transform.position - transform.position);
        
        float angle2 = Vector2.Angle(newDirection,new Vector2(1,0));
        if ((newDirection.x < 0 && newDirection.y < 0) || (newDirection.x > 0 && newDirection.y < 0))
            angle2 = 360 - angle2;
        transform.rotation = Quaternion.Euler(0, 0, angle2);

        if (Vector2.Distance(transform.position, Player.transform.position) <= 2.5)
            rb2d.velocity *= .99f;
        else
            rb2d.velocity = newDirection.normalized * 2;

        if (FireTimer.Finished)
        {
            if (Vector3.Distance(transform.position, Player.transform.position) < 4)
            {
                GameObject bullet;
                bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = (newDirection.normalized * 10);
                bullet.name = "nn";
                FireTimer.Run();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name != "nn")
        {
            health -= 5;
            if (health <= 0)
            {
                Instantiate(explosionPrefab, transform.position, quaternion.identity);
                health = 5;
                transform.position = Methodes.RandomPositionInScreen(Random.Range(0, 4));
            }
            Destroy(col.gameObject);
        }
        

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        health -= 5;
        if (health <= 0)
        {
            Instantiate(explosionPrefab, transform.position, quaternion.identity);
            health = 5;
            transform.position = Methodes.RandomPositionInScreen(Random.Range(0, 4));
        }
    }
    
    
}
