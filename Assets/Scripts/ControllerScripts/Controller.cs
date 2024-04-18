using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Controller : MonoBehaviour
{
    public Pawn pawn;

    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        ProcessInputs();
    }

    public virtual void ProcessInputs()
    {

    }
}
