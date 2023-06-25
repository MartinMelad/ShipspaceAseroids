using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject prefabGreenRock;
    //[SerializeField] private GameObject prefabWhiteRock;
    [SerializeField] private GameObject prefabRedRock;

    private Timer spawneTimer;
    
    // const int SpawnBorderSize = 200;
    // int minSpawnX;
    // int maxSpawnX;
    // int minSpawnY;
    // int maxSpawnY;

    private const int MaxSpawnTries = 20;
    private float rockRaduse;
    private Vector2 min = new Vector2();
    private Vector2 max = new Vector2();
    void Start()
    {
        // minSpawnX = SpawnBorderSize;
        // maxSpawnX = Screen.width - SpawnBorderSize;
        // minSpawnY = SpawnBorderSize;
        // maxSpawnY = Screen.height - SpawnBorderSize;
        GameObject trie = Instantiate(prefabRedRock, Vector3.zero, Quaternion.identity);
        CircleCollider2D cc2d = trie.GetComponent<CircleCollider2D>();
        rockRaduse = cc2d.radius;
        Destroy(trie);
        spawneTimer = gameObject.AddComponent<Timer>();
        spawneTimer.Duration = .5f;
        spawneTimer.Run();
        Spawne();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameObject.FindGameObjectsWithTag("C4").Length < 10 && spawneTimer.Finished)
        {
            Spawne();
            spawneTimer.Duration = .5f;
            spawneTimer.Run();
        }

    }

    void Spawne()
    {
        GameObject Rock;
        int sprite = Random.Range(0, 2);
        int pos = Random.Range(0, 4);
        Vector3 position = Methodes.RandomPositionInScreen(pos);
        int spawnTries = MaxSpawnTries;
        SetMinMax(position);

        while (Physics2D.OverlapArea(min, max) != null && spawnTries != 0)
        {
            position = Methodes.RandomPositionInScreen(pos);
            SetMinMax(position);
            spawnTries --;
        }
        
        if (Physics2D.OverlapArea(min, max) == null)
        {
            if (sprite == 0)
            {
                Rock = Instantiate(prefabGreenRock, position, Quaternion.identity);
                Rock.GetComponent<Rock>().Initialize(pos);
            }
            else
            {
                Rock = Instantiate(prefabRedRock, position, Quaternion.identity);
                Rock.GetComponent<Rock>().Initialize(pos);
            }
        }
        
        // Vector3 Ship = new Vector3(0, 0, 0);
        // if (GameObject.FindWithTag("Ship"))
        //     Ship = GameObject.FindWithTag("Ship").transform.position;
        // int sprite = Random.Range(0, 2);
        // Vector3 rockPosition = new Vector3(Random.Range(minSpawnX, maxSpawnX), Random.Range(minSpawnY, maxSpawnY),
        //     -Camera.main.transform.position.z);
        // Vector3 worldLocation = Camera.main.ScreenToWorldPoint(rockPosition);
        // float mag = Mathf.Sqrt(Mathf.Pow(Ship.x - worldLocation.x, 2) + Mathf.Pow(Ship.y - worldLocation.y, 2));
        // SetMinMax(worldLocation);
        // int spawnTries = MaxSpawnTries;
        // while (Physics2D.OverlapArea(min, max) != null && spawnTries != 0)
        // {
        //     sprite = Random.Range(0, 2);
        //      rockPosition = new Vector3(Random.Range(minSpawnX, maxSpawnX), Random.Range(minSpawnY, maxSpawnY),
        //         -Camera.main.transform.position.z);
        //     worldLocation = Camera.main.ScreenToWorldPoint(rockPosition);
        //     SetMinMax(worldLocation);
        //     spawnTries--;
        // }
        // if (mag > 1 && sprite == 0 && Physics2D.OverlapArea(min,max) == null)
        // {
        //     GameObject Rock = Instantiate(prefabGreenRock, worldLocation, Quaternion.identity);
        // }
        // else
        // {
        //     if (mag > 1 && Physics2D.OverlapArea(min,max) == null)
        //     {
        //         GameObject Rock = Instantiate(prefabRedRock, worldLocation, Quaternion.identity);
        //     }
        // }

    }

    void SetMinMax(Vector3 location)
    {
        min.x = location.x - rockRaduse - 1;
        min.y = location.y - rockRaduse - 1;
        max.x = location.x + rockRaduse + 1;
        max.y = location.y + rockRaduse + 1;
    }
}