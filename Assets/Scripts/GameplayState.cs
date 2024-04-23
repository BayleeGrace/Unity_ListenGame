using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        GameManager.instance.SpawnPlayers();
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
}
