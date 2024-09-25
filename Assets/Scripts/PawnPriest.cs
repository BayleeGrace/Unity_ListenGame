using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnPriest : PawnHuman
{
    public Controller targetPlayerController;

    public override void Start()
    {
        base.Start();
        if (targetPlayer != null)
        {
            // Grab the controller object
            targetPlayerController = targetPlayer.transform.GetChild(1).gameObject.GetComponent<Controller>();
        }
    }

    public override void ActivateAbility1()
    {
        base.ActivateAbility1();
    }
    public override void DeactivateAbility1()
    {
        base.DeactivateAbility1();
    }

    public override void ActivateAbility2()
    {
        base.ActivateAbility2();
    }

    public override void DeactivateAbility2()
    {
        base.DeactivateAbility2();
    }

    public override void ActivateAbility3()
    {
        base.ActivateAbility3();
        StunYokai();
    }

    public override void DeactivateAbility3()
    {
        base.DeactivateAbility3();
        targetPlayerController.isStunned = false;
    }

    public void StunYokai()
    {
        targetPlayerController.isStunned = true;
        // TODO: Start ability timer

        // TODO: reduce the # needed to stun the yokai again in the Objective
    }
    
}
