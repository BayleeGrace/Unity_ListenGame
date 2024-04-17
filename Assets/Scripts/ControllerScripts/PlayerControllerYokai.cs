using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerYokai : PlayerController
{
    [HideInInspector] public PawnYokai pawnYokai;
    public override void Start()
    {
        pawnYokai = transform.parent.GetComponent<PawnYokai>();
        // TODO: Make this Yokai Player the first in the players list in the GamaManager
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
