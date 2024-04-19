using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    #region Movement Variables
    [Header("Movement")]
    public float defaultMoveSpeed;
    [HideInInspector] public float moveSpeed;
    public float turnSpeed;
    public Mover mover; // variable to hold our Mover component
    #endregion Movement Variables

    #region Sound Variables
    [Header("Sound")]
    public NoiseMaker noiseMaker;
    public float noiseMakerVolume;
    #endregion Sound Variables

    #region Ability Variables
    [Header("Abilities")]
    #region Cooldown Timers
    public float ability1Cooldown;
    public float ability2Cooldown;
    public float ability3Cooldown;
    private bool cooldownActive;
    private float cooldown;
    #endregion Cooldown Timers
    #region Ability Timers
    public float ability1Timer;
    public float ability2Timer;
    public float ability3Timer;
    #endregion Ability Timers
    #endregion Ability Variables
    public GameObject targetPlayer;

    public virtual void Start()
    {
        mover = GetComponent<Mover>();
        noiseMaker = GetComponent<NoiseMaker>();
        ResetVariables();
    }

    public virtual void Update()
    {
        
    }

    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();
    public abstract void RotateTowards(Vector3 targetPosition);
    public abstract void MakeNoise();
    public abstract void StopNoise();
    public virtual void StartAbilitiesTimer(float timer)
    {
        // TODO: Create a timer system based on the input given
        // This timer will track how long the ability will last
    }
    public virtual void StartAbilitiesCooldown(float timer)
    {
        // TODO: Create a timer system based on the input given
        // This timer will track how long until the ability can be used again
    }

    public abstract void SetMovementSpeed(float newSpeed);
    public abstract void ResetVariables();

    public virtual void ActivateAbility1()
    {
        StartAbilitiesTimer(ability1Timer);
        noiseMaker.PlayAbility1Sound();
    }
    public virtual void DeactivateAbility1()
    {
        noiseMaker.StopAbility1Sound();
    }
    public virtual void ActivateAbility2()
    {
        StartAbilitiesTimer(ability2Timer);
        noiseMaker.PlayAbility2Sound();
    }
    public virtual void DeactivateAbility2()
    {
        noiseMaker.StopAbility2Sound();
    }
    public virtual void ActivateAbility3()
    {
        StartAbilitiesTimer(ability3Timer);
        noiseMaker.PlayAbility3Sound();
    }
    public virtual void DeactivateAbility3()
    {
        noiseMaker.StopAbility3Sound();
    }

    public virtual void ActivatePassiveAbility()
    {
        noiseMaker.PlayPassiveAbilitySound();
    }
    public virtual void DeactivatePassiveAbility()
    {
        noiseMaker.StopPassiveAbilitySound();
    }

    public virtual bool IsDistanceLessThan(GameObject target, float distance)
    {
        // Compare transform distance of two pawns, the owner of this component and targetPlayer pawn
        if (Vector3.Distance(transform.position, target.transform.position) < distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
