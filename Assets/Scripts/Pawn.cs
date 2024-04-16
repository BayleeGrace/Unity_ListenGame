using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    [HideInInspector] public float moveSpeed;
    public float defaultMoveSpeed;
    public float turnSpeed;
    public Mover mover; // variable to hold our Mover component
    public NoiseMaker noiseMaker;
    public float noiseMakerVolume;

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
}
