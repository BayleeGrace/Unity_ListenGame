using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerYokai : PlayerController
{
    [HideInInspector] public PawnYokai pawnYokai;
    public KeyCode whisperKey;
    public override void Start()
    {
        // TODO: Make this Yokai Player the first in the players list in the GamaManager
    }

    public override void Update()
    {
        base.Update();
    }

    public override void ProcessInputs()
    {
        base.ProcessInputs();
        if (Input.GetKey(whisperKey))
        {
            pawnYokai.DoWhisper();
        }

        if (!Input.GetKey(whisperKey))
        {
            pawnYokai.noiseMaker.StopAbility1Sound();
            pawnYokai.moveSpeed = pawnYokai.defaultMoveSpeed;
        }
    }
}
