using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpawnPoint : SpawnPoint
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.humanSpawn = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
