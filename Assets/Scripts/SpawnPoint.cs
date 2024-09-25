using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public void Awake()
    {
        if (GameManager.instance.spawnPoints.Count <= 1
        )
        {
            GameManager.instance.spawnPoints.Add(this.gameObject);
        }
    }

    public void OnDestroy()
    {
        GameManager.instance.spawnPoints.Remove(this.gameObject);
    }

}
