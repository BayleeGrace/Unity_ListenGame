using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnYokai : Pawn
{
    public float whisperMoveSpeed;
    public float bloodlustMoveSpeed;
    public float dashMoveSpeed;
    public float distanceToInteract;
    public float distanceToBloodlust;

    public override void Start()
    {
        base.Start();
        targetPlayer = GameManager.instance.humanPlayer;
    }

    public override void Update()
    {
        if (targetPlayer == null)
        {
            targetPlayer = GameManager.instance.humanPlayer;
        }
        if (targetPlayer != null)
        {
            if (IsDistanceLessThan(targetPlayer, distanceToBloodlust))
            {
                ActivatePassiveAbility();
            }
            else 
            {
                DeactivatePassiveAbility();
            }
        }
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

    public override void ActivateAbility1()
    {
        base.ActivateAbility1();
        DoWhisper();
        StartAbilitiesTimer(ability1Timer);
        noiseMaker.PlayAbility1Sound();
    }
    public override void DeactivateAbility1()
    {
        base.DeactivateAbility1();
        StopNoise();
        ResetVariables();
    }

    public override void ActivateAbility2()
    {
        base.ActivateAbility2();
        if (targetPlayer != null)
        {
            if (IsDistanceLessThan(targetPlayer, distanceToInteract))
            {
                // TODO: communicate with the PawnHuman and the Game Manager that the Yokai has won the game
                Debug.Log("Player captured, game over");
                GameManager.instance.ActivateGameOverScreen();
            }
        }
    }

    public override void DeactivateAbility2()
    {
        base.DeactivateAbility2();
    }

    public override void ActivateAbility3()
    {
        base.ActivateAbility3();
        DoDash();
    }

    public override void DeactivateAbility3()
    {
        base.DeactivateAbility3();
        ResetVariables();
    }

    public override void ActivatePassiveAbility()
    {
        base.ActivatePassiveAbility();
        DoBloodlust();
    }

    public override void DeactivatePassiveAbility()
    {
        base.DeactivatePassiveAbility();
        StopNoise();
        ResetVariables();
    }

    public void DoWhisper()
    {
        MakeNoise();

        // Slow movement speed by a set percentage
        if (moveSpeed > defaultMoveSpeed)
        {
            SetMovementSpeed((whisperMoveSpeed/moveSpeed) * bloodlustMoveSpeed);
        }
        else 
        {
            SetMovementSpeed(whisperMoveSpeed);
        }

        // TODO: Clear FOV for player

    }

    public void DoBloodlust()
    {
        MakeNoise();

        // Increase movement speed by a set speed (Bloodlust)
        SetMovementSpeed(bloodlustMoveSpeed);

        // TODO: Blur FOV for player even more

    }

    public void DoDash()
    {
        SetMovementSpeed(dashMoveSpeed);
    }
    
}
