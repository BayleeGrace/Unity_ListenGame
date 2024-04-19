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
        GameManager.instance.players[0] = this;
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
