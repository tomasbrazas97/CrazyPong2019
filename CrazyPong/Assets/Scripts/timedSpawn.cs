using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timedSpawn : MonoBehaviour
{
    public GameObject spawnee;
    public bool stopSpawning = false;
    public float spawnTime = 1;
    public float spawnDelay = 1;
    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }


    public void SpawnObject()
    {
        Instantiate(spawnee, transform.position, transform.rotation);
        if (stopSpawning)
        {
            CancelInvoke("SpawnObject");
        }
    }
}
