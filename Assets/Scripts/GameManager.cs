using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager instance; // Variable to reference the GameManager
    private GameObject objectiveManager;
    #region Player Variables
    public List<PlayerController> players;
    //public List<AIController> AIs;
    public GameObject yokaiPrefab;
    public GameObject[] humanPrefabs;
    [HideInInspector] public bool isMultiplayer = false;
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
    }
    
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void SpawnPlayerOne()
    {
        // TODO: Get a random PLAYER CONTROLLER prefab

        // TODO: Spawn the random PLAYER CONTROLLER prefab from above

        // TODO: Set this player to the first player in the sequence
    }

    public void SpawnPlayerTwo()
    {
        if (isMultiplayer == true)
        {
            // TODO: Get a random PLAYER CONTROLLER prefab

            // TODO: Spawn the random PLAYER CONTROLLER prefab from above

            // TODO: Set this player to the SECOND player in the sequence

        }
    }

    public void SpawnAI()
    {
        if (isMultiplayer == false)
        {
            // TODO: Get a random AI CONTROLLER prefab

            // TODO: Spawn the random AI CONTROLLER prefab from above

        }
    }

    // TODO: Create a function that sets the random scene to the current gameplay scene upon starting the game

    // TODO: Create a function that grabs a random map/scene

    // TODO: Create a function that sets each player spawn point randomly for each player in the scene

    // TODO: Create a function that grabs a random spawn point in the map

}
