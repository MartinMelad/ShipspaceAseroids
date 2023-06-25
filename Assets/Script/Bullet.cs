using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > ScreenUtils.ScreenRight || transform.position.x < ScreenUtils.ScreenLeft ||
            transform.position.y > ScreenUtils.ScreenTop || transform.position.y < ScreenUtils.ScreenBottom)
            Destroy(gameObject);

    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

   
}
