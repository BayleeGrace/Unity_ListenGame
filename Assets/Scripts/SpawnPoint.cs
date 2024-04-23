using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public void Awake()
    {
        GameManager.instance.spawnPoints.Add(this.gameObject);
    }

    public void OnDestroy()
    {
        GameManager.instance.spawnPoints.Remove(this.gameObject);
    }

}
