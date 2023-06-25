using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ship : MonoBehaviour
{
    [SerializeField] private GameObject prefabExplosion;

    [SerializeField] private GameObject prefabBullet;
    
    [SerializeField] private GameObject Fire;

    private GameObject fire;

    private Animator anim;

    private Vector3 shipPosition = new Vector3(0,0,0);

    private Vector2 thrustDirection = new Vector2(1,0);
    
    private float ThrustForce = 5f;
    
    private float rotateDegreesPerSecond = 180;
    
    private int health  = 3;
    
    private float _scor = 0;

    private Rigidbody2D rb2d;
    
    private bool flag = false;

    private Timer fireTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        fireTimer = gameObject.AddComponent<Timer>();
        fireTimer.Duration = .1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > ScreenUtils.ScreenRight || transform.position.x < ScreenUtils.ScreenLeft ||
            transform.position.y > ScreenUtils.ScreenTop || transform.position.y < ScreenUtils.ScreenBottom)
        {
            Vector2 position =gameObject.transform.position;
            float dis = Vector2.Distance(new Vector2(0, 0), position);
            position = dis * thrustDirection * -1;   
            gameObject.transform.position = position;
        }
        float rotationInput = Input.GetAxis("Rotate");
        if ( rotationInput != 0)
        {
            float rotationAmount = rotateDegreesPerSecond * Time.deltaTime;
            if (rotationInput < 0) {
                rotationAmount *= -1;
            }
            transform.Rotate(Vector3.forward, rotationAmount);
            rb2d.velocity = rb2d.velocity * .991f;
            float zRotation = transform.eulerAngles.z * Mathf.Deg2Rad;
            thrustDirection.x = Mathf.Cos(zRotation);
            thrustDirection.y = Mathf.Sin(zRotation);
        }

        if (Input.GetAxis("Thrust") <= 0 )
        {
            rb2d.velocity = rb2d.velocity * .99f;
            if (anim != null)
                anim.SetBool("Off", true);
        }
        if (Input.GetAxis("Fire") > 0 )
        {
            if (flag ==  false)
            {
                flag = true;
                fireTimer.Run();
            }

            if (flag && fireTimer.Finished)
            {
                fireTimer.Run();
                GameObject bullet;
                bullet = Instantiate(prefabBullet, transform.position, transform.rotation);
                bullet.GetComponent<Rigidbody2D>().AddForce(thrustDirection * 500);
            }
           
        }
        else
        {
            flag = false;
        }
    }
    
    void FixedUpdate()
    {
        if (Input.GetAxis("Thrust") > 0)
        {
            rb2d.AddForce(thrustDirection * ThrustForce);
            if ( anim == null)
            {
                fire = Instantiate(Fire, transform);
                anim = fire.GetComponent<Animator>();
            }
            anim.SetBool("Off", false);
        }
    }
    
    void OnBecameInvisible()
    {
        Vector2 position =gameObject.transform.position;
        float dis = Vector2.Distance(new Vector2(0, 0), position);
         position = dis * thrustDirection * -1;
        // if (position.x + colliderRadius < ScreenUtils.ScreenLeft ||
        //     position.x - colliderRadius > ScreenUtils.ScreenRight)
        // {
        //     position.x *= -1;
        // }
        // if (position.y - colliderRadius > ScreenUtils.ScreenTop ||
        //     position.y + colliderRadius < ScreenUtils.ScreenBottom)
        // {
        //     position.y *= -1;
        // }
        gameObject.transform.position = position;

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "C4")
        {
            Instantiate(prefabExplosion, transform.position, quaternion.identity);
            transform.position = shipPosition;
            _scor = 0;
        }
        else
        {
            _scor++;
            
        }   
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "nn")
        {
            health--;
            if (health <= 0)
            {
                Instantiate(prefabExplosion, transform.position, quaternion.identity);
                health = 3;
                transform.position = shipPosition;
            }
            Destroy(col.gameObject);
        }
    }
}
