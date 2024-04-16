using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerHuman : PlayerController
{
    [HideInInspector] public PawnHuman pawnHuman;
    public override void Start()
    {
        pawnHuman = transform.parent.GetComponent<PawnHuman>();
        // TODO: Make this Human Player the SECOND in the players list in the GamaManager
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void ProcessInputs()
    {
        base.ProcessInputs();
    }
}
