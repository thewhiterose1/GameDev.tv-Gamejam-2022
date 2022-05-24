using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcSpawner : MonoBehaviour

{
    private float Timer;
    public float timeToSpawn = 10;
    public GameObject objectToSpawn;
    private float actualSpawnTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        actualSpawnTime = timeToSpawn * (20 / Time.time);
        Timer += Time.deltaTime;
        if (Timer > actualSpawnTime)
        {
            Instantiate(objectToSpawn);
            Timer = 0;
        }
    }   
}
