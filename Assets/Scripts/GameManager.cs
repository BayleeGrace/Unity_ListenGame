using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager instance; // Variable to reference the GameManager
    private GameObject objectiveManager;
    #region Player Variables
    public List<Controller> players;
    public List<AIController> AIs;
    public GameObject yokaiPrefab;
    public GameObject[] humanPrefabs;
    public GameObject[] humanAIPrefabs;
    private GameObject[] playerPrefabs;
    public GameObject[] spawnPoints;
    [HideInInspector] public GameObject yokaiPlayer;
    [HideInInspector] public GameObject humanPlayer;
    [HideInInspector] public bool isMultiplayer = false;
    private bool isPlayerOneSpawned = false;
    #endregion Player Variables
    #endregion Variables
    
    private void Awake()
    {
        // Create a game object that holds this game manager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Don't destroy the first GameManeged created in the scene
        }
        // If there is a game object that holds the game instance already, destroy it
        else
        {
            Destroy(gameObject); 
        }

        foreach (var spawn in spawnPoints)
        {
            if (players.Count < 2)
            {
                if (isPlayerOneSpawned == false)
                {
                    SpawnPlayerOne(spawn);
                    isPlayerOneSpawned = true;
                }
                else if (isPlayerOneSpawned == true)
                {
                    if (isMultiplayer == true)
                    {
                        SpawnPlayerTwo(spawn);
                    }
                    else if (isMultiplayer == false)
                    {
                        SpawnAI(spawn);
                    }
                }
            }
        }
    }
    
    private void Start()
    {

    }

    private void Update()
    {
        
    }

    public void SpawnPlayerOne(GameObject spawnPoint)
    {
        // Get a random PLAYER CONTROLLER prefab
        GameObject newYokai = Instantiate(yokaiPrefab, spawnPoint.transform.position, Quaternion.identity) as GameObject;

        // Set this player to the first player in the sequence
        yokaiPlayer = newYokai.gameObject;
    }

    public void SpawnPlayerTwo(GameObject spawnPoint)
    {
        if (isMultiplayer == true)
        {
            // TODO: Get a random PLAYER CONTROLLER prefab
            GameObject newHuman = Instantiate(RandomHumanPrefab(), spawnPoint.transform.position, Quaternion.identity) as GameObject;

            // TODO: Set this player to the SECOND player in the sequence
            humanPlayer = newHuman.gameObject;

            yokaiPlayer.GetComponent<Pawn>().targetPlayer = humanPlayer;
            humanPlayer.GetComponent<Pawn>().targetPlayer = yokaiPlayer;
        }
    }

    public void SpawnAI(GameObject spawnPoint)
    {
        if (isMultiplayer == false)
        {
            // TODO: Spawn the random AI CONTROLLER prefab from above
            GameObject newHuman = Instantiate(RandomHumanAIPrefab(), spawnPoint.transform.position, Quaternion.identity) as GameObject;

            // TODO: Set this player to the SECOND player in the sequence
            humanPlayer = newHuman.gameObject;

        }
    }

    // TODO: Create a function that sets the random scene to the current gameplay scene upon starting the game

    // TODO: Create a function that grabs a random map/scene

    public GameObject RandomPlayerPrefab()
    {
        // pull random enemy obj's from the allotted array created by designers
        return playerPrefabs[Random.Range(0, playerPrefabs.Length)];
    }

    public GameObject RandomHumanPrefab()
    {
        // pull random enemy obj's from the allotted array created by designers
        return humanPrefabs[Random.Range(0, humanPrefabs.Length)];
    }

    public GameObject RandomHumanAIPrefab()
    {
        // pull random enemy obj's from the allotted array created by designers
        return humanAIPrefabs[Random.Range(0, humanAIPrefabs.Length)];
    }

    public GameObject RandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }

    // TODO: Create a function that sets each player spawn point randomly for each player in the scene

    // TODO: Create a function that grabs a random spawn point in the map

}
