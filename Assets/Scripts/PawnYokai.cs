using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnYokai : Pawn
{
    public float whisperMoveSpeed;
    public float bloodlustMoveSpeed;

    public override void Start()
    {
        base.Start();
        ResetVariables();
    }

    public override void Update()
    {
        
    }

    public override void MoveForward()
    {
        mover.Move(transform.forward, moveSpeed);
    }
    public override void MoveBackward()
    {
        mover.Move(transform.forward, -moveSpeed);
    }
    public override void RotateClockwise()
    {
        mover.Rotate(turnSpeed);
    }
    public override void RotateCounterClockwise()
    {
        mover.Rotate(-turnSpeed);
    }
    public override void RotateTowards(Vector3 targetPosition)
    {
        // find the vector to the target
        Vector3 vectorToTarget = targetPosition - transform.position;
        // find the rotation to look down that vector
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);
        // Rotate closer to that vector, but not more than turn speed allows in a single frame
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
    
    
    public override void MakeNoise()
    {
        if(noiseMaker != null)
        {
            noiseMaker.volumeDistance = noiseMakerVolume;
        }
    }
    public override void StopNoise()
    {
        if(noiseMaker != null)
        {
            // Set the volume distance back to 0 (the human cannot hear the Yokai now)
            noiseMaker.volumeDistance = 0;
        }
    }

    public void DoWhisper()
    {
        MakeNoise();
        noiseMaker.PlayAbility1Sound();

        // TODO: Slow movement speed by a set percentage
        SetMovementSpeed(whisperMoveSpeed);

        // TODO: Clear FOV for player

        // TODO: Create a timer for this function

    }

    public override void StartAbilitiesTimer(float timer)
    {

    }

    public override void SetMovementSpeed(float newSpeed)
    {
        if (newSpeed > 0)
        {
            moveSpeed = newSpeed;
        }
    }

    public override void ResetVariables()
    {
        moveSpeed = defaultMoveSpeed;
    }
    
}
