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

    public override void Update()
    {
        base.Update();
    }

    public override void ProcessInputs()
    {
        base.ProcessInputs();
        if (Input.GetKey(ability1Key))
        {
            pawn.ActivateAbility1();
        }

        if (!Input.GetKey(ability1Key))
        {
            pawn.DeactivateAbility1();
        }

        if (Input.GetKeyDown(ability2Key))
        {
            pawn.ActivateAbility2();
        }

        if (Input.GetKeyDown(ability3Key))
        {
            pawn.ActivateAbility3();
        }
    }
}
