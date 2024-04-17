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
    #endregion Cooldown Timers
    #endregion Ability Variables

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
    public abstract void StartAbilitiesTimer(float timer);

    public abstract void SetMovementSpeed(float newSpeed);
    public abstract void ResetVariables();

    public virtual void ActivateAbility1()
    {
        StartAbilitiesTimer(ability1Cooldown);
        noiseMaker.PlayAbility1Sound();
    }
    public virtual void DeactivateAbility1()
    {
        noiseMaker.StopAbility1Sound();
    }
    public virtual void ActivateAbility2()
    {
        StartAbilitiesTimer(ability2Cooldown);
        noiseMaker.PlayAbility2Sound();
    }
    public virtual void DeactivateAbility2()
    {
        noiseMaker.StopAbility2Sound();
    }
    public virtual void ActivateAbility3()
    {
        StartAbilitiesTimer(ability3Cooldown);
        noiseMaker.PlayAbility3Sound();
    }
    public virtual void DeactivateAbility3()
    {
        noiseMaker.StopAbility3Sound();
    }
}
