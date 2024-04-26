using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YokaiSpawnPoint : SpawnPoint
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.yokaiSpawn = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
