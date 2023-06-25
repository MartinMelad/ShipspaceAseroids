using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rock : MonoBehaviour
{
    [SerializeField] private GameObject prefabExplosion;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (gameObject.tag == "C4" || col.tag == "Bullet")
        {
            Destroy(gameObject);
            Instantiate(prefabExplosion, transform.position, Quaternion.identity);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (gameObject.tag == "C4" || col.collider.tag == "Bullet")
        {
            Destroy(gameObject);
            Instantiate(prefabExplosion, transform.position, Quaternion.identity);
        }
        else
        {
            Destroy(gameObject);
        }
    }
   
    public void Initialize(float dir)
    {
        const float MinImpulseForce = 1f;
        const float MaxImpulseForce = 2f;
        float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);
        Vector2 direction = new Vector2();
        
        if (dir == 0)
        {
            direction = Methodes.Direction(2);
        }
        else if (dir == 1)
        {
            direction = Methodes.Direction(0);
        }
        else if (dir == 2)
        {
            direction = Methodes.Direction(1);
        }
        else
        {
            direction = Methodes.Direction(3);
        }
        
        GetComponent<Rigidbody2D>().AddForce(direction * magnitude, ForceMode2D.Impulse);
    }


}
