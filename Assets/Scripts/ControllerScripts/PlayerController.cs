using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerController : Controller
{
    #region Locomotion Controlls
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode rotateClockwiseKey;
    public KeyCode rotateCounterClockwiseKey;
    private Mover mover;
    #endregion Locomotion Controlls
    // Variable to hold the pause key
    public KeyCode pauseKey;

    public void Awake()
    {
        // Check if we have a Game Manager
        if(GameManager.instance != null)
        {
            // Register this player with the Player List in the Game Manager
            GameManager.instance.players.Add(this);
        }
        // Run the Start() fx from the parent (base) class
        base.Start();
        
    }

    public override void Update()
    {
        // Process your keyboard inputs
        ProcessInputs();
        // Run the Update() fx from the parent class
        base.Update();
    }

    public override void ProcessInputs() // I'm not sure why we do this?
    {
        
        // Create a conditional that checks every frame (above) to see if the player has pressed X key within controller
        if (Input.GetKey(moveForwardKey))
        {
            pawn.MoveForward();
            if (pawn.noiseMaker.canMakeSound == true)
            {
                pawn.MakeNoise();
                pawn.mover.isMoving = true;
                pawn.noiseMaker.PlayMovingSound();
            }
        }

        // If moveForwardKey is not pressed, check if it's moveBackwardKey...etc.
        if (Input.GetKey(moveBackwardKey))
        {
            pawn.MoveBackward();
            if (pawn.noiseMaker.canMakeSound == true)
            {
                pawn.MakeNoise();
                pawn.mover.isMoving = true;
                pawn.noiseMaker.PlayMovingSound();
            }
        }

        if (Input.GetKey(rotateClockwiseKey))
        {
            pawn.RotateClockwise();
        }

        if (Input.GetKey(rotateCounterClockwiseKey))
        {
            pawn.RotateCounterClockwise();
        }

        // If the player is not moving, change the volume distance to 0
        if (!Input.GetKey(moveForwardKey) && !Input.GetKey(moveBackwardKey) && !Input.GetKey(rotateClockwiseKey) && !Input.GetKey(rotateCounterClockwiseKey))
        {
            pawn.StopNoise();
            pawn.mover.isMoving = false;
            pawn.noiseMaker.StopMovingSound();
            pawn.ResetVariables();
        }

        /*
        if (Input.GetKeyDown(pauseKey))
        {
            GameManager.instance.ActivatePauseMenuScreen();
        }*/
    }

    // Create a function that tells the Game Manager when this player has died, thus removing it from the players List
    // OnDestroy() is not a custom function in Unity C#. We don't have to write a separate command to call this function.
    public void OnDestroy()
    {
        /*
        // Instance tracking the destroyed player
        if (GameManager.instance.players != null)
        {
            GameManager.instance.players.Remove(this);
        }
        */
    }

}
