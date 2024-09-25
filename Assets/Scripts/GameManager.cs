using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager instance; // Variable to reference the GameManager
    private GameObject objectiveManager;
    #region Player Variables
    public List<Controller> players;
    public List<AIController> AIs;
    public GameObject yokaiPrefab;
    public GameObject yokaiAIPrefab;
    public GameObject[] humanPrefabs;
    public GameObject[] humanAIPrefabs;
    private GameObject[] playerPrefabs;
    public List<GameObject> spawnPoints;
    public GameObject yokaiSpawn;
    public GameObject humanSpawn;
    [HideInInspector] public GameObject yokaiPlayer;
    [HideInInspector] public GameObject humanPlayer;
    private bool isPlayerOneSpawned = false;
    #endregion Player Variables

    #region UI Variables
    public GameObject pauseMenuStateObject;
    private bool pauseMenuActive = false;
    #endregion UI Variables

    #region Settings Variables
    [Header("Game Settings")]
    [HideInInspector] public bool isMultiplayer = false;
    [HideInInspector] public bool playerOneIsYokai = true;
    public float timeLimit;
    #endregion Settings Variables

    public MapGenerator mapGenerator;
    //[HideInInspector] public GameObject currentMap;
    public int currentMapInt;

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
    }
    
    private void Start()
    {

    }

    private void Update()
    {
        
    }

    /*public void DetermineSpawnPoints()
    {
        SpawnPoint[] newSpawnPoints = FindObjectsOfType<SpawnPoint>();

        foreach (var newSpawn in newSpawnPoints)
        {
            Debug.Log("Spawn recognized");
            spawnPoints.Add(newSpawn.gameObject);
        }
    }*/

    public void SpawnPlayers()
    {
        if (players.Count < 2)
        {
            foreach (var spawn in spawnPoints)
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

    public void SpawnPlayerOne(GameObject spawnPoint)
    {
        if (playerOneIsYokai == true)
        {
            // Get a random PLAYER CONTROLLER prefab
            GameObject newYokai = Instantiate(yokaiPrefab, spawnPoint.transform.position, Quaternion.identity) as GameObject;

            // Set this player to the first player in the sequence
            yokaiPlayer = newYokai.gameObject;
        }
        else 
        {
            // TODO: Get a random PLAYER CONTROLLER prefab
            GameObject newHuman = Instantiate(RandomHumanPrefab(), spawnPoint.transform.position, Quaternion.identity) as GameObject;

            // TODO: Set this player to the SECOND player in the sequence
            humanPlayer = newHuman.gameObject;
        }
        
    }

    public void SpawnPlayerTwo(GameObject spawnPoint)
    {
        if (isMultiplayer == true)
        {
            if (playerOneIsYokai == true)
            {
                // TODO: Get a random PLAYER CONTROLLER prefab
                GameObject newHuman = Instantiate(RandomHumanPrefab(), spawnPoint.transform.position, Quaternion.identity) as GameObject;

                // TODO: Set this player to the SECOND player in the sequence
                humanPlayer = newHuman.gameObject;
            }
            else
            {
                // Get a random PLAYER CONTROLLER prefab
                GameObject newYokai = Instantiate(yokaiAIPrefab, spawnPoint.transform.position, Quaternion.identity) as GameObject;

                // Set this player to the first player in the sequence
                yokaiPlayer = newYokai.gameObject;
            }

            yokaiPlayer.GetComponent<Pawn>().targetPlayer = humanPlayer;
            humanPlayer.GetComponent<Pawn>().targetPlayer = yokaiPlayer;
        }
    }

    public void SpawnAI(GameObject spawnPoint)
    {
        if (isMultiplayer == false)
        {
            if (playerOneIsYokai == true)
            {
                // TODO: Spawn the random AI CONTROLLER prefab from above
                GameObject newHuman = Instantiate(RandomHumanAIPrefab(), spawnPoint.transform.position, Quaternion.identity) as GameObject;

                // TODO: Set this player to the SECOND player in the sequence
                humanPlayer = newHuman.gameObject;
            }
            else 
            {
                // Get a random PLAYER CONTROLLER prefab
                GameObject newYokai = Instantiate(yokaiAIPrefab, spawnPoint.transform.position, Quaternion.identity) as GameObject;

                // Set this player to the first player in the sequence
                yokaiPlayer = newYokai.gameObject;
            }

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
        return spawnPoints[Random.Range(0, 1)];
    }

    #region Menu Management

    public void ActivateGameplay()
    {
        // TODO: Start the timer upon starting


            SceneManager.LoadSceneAsync(currentMapInt);
            //mapGenerator.SetMap();
            //currentMap = mapGenerator.newGeneratedMapGameObject;
            //DetermineSpawnPoints();
        /*else 
        {
            SceneManager.LoadSceneAsync("Garden");
        }*/
    }

    public void DeactivateGameplay()
    {
        spawnPoints.Clear();
        players.Clear();
        isPlayerOneSpawned = false;
    }

    public void ActivateGameOverScreen()
    {
        SceneManager.LoadSceneAsync("GameOver");
        DeactivateGameplay();
    }

    public void ActivateMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
        DeactivateGameplay();
    }

    public void TogglePauseMenu()
    {
        if (pauseMenuActive == false)
        {
            pauseMenuStateObject.SetActive(true);
            pauseMenuActive = true;
        }
        else 
        {
            pauseMenuStateObject.SetActive(false);
            pauseMenuActive = false;
        }
    }

    #endregion Menu Management

}
