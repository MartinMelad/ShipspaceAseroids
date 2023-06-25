using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnShip : MonoBehaviour
{
    [SerializeField] private GameObject prefabShiop;

    private GameObject ship;

    private Vector3 position = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        ship = Instantiate(prefabShiop , position, Quaternion.identity);
    }

    // Update is called once per frame
    
    
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("32");
        Destroy(ship);
        ship = Instantiate(prefabShiop , position, Quaternion.identity);
    }
}
