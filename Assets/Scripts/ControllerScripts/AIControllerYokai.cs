using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControllerYokai : AIController
{
    [HideInInspector] public Controller targetPlayerController;
    
    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (targetPlayer == null)
        {
            // Grab the controller object
            targetPlayer = pawn.targetPlayer;
            targetPlayerController = targetPlayer.transform.GetChild(1).gameObject.GetComponent<Controller>();
        }
    }

    public override void ProcessInputs()
    {
        if (targetPlayer != null)
        {
                switch(currentState)
                {
                    case AIState.Patrol:
                        // Do Patrol state
                        // check for transitions
                        DoPatrolState();
                        if (pawn.IsDistanceLessThan(targetPlayer, fleeDistance) && (targetPlayerController.IsCanSee(pawn.gameObject)))
                        {
                            ChangeState(AIState.Flee);
                        }
                        if (pawn.IsDistanceLessThan(targetPlayer, fleeDistance) && (!targetPlayerController.IsCanSee(pawn.gameObject)))
                        {
                            ChangeState(AIState.Chase);
                        }
                        break;

                    case AIState.Flee:
                        DoFleeState();
                        if (targetPlayer != null)
                        {
                            if (pawn.IsDistanceLessThan(targetPlayer, fleeDistance) && (!targetPlayerController.IsCanSee(pawn.gameObject)))
                            {
                                ChangeState(AIState.Chase);
                            }
                            else if (!pawn.IsDistanceLessThan(targetPlayer, safeDistance))
                            {
                                ChangeState(AIState.Patrol);
                            }
                        }
                        else
                        {
                            ChangeState(AIState.Patrol);
                        }
                        break;

                    case AIState.Chase:
                        DoChaseState();
                        // TODO: If the pawn is InLineOfSight && is within flee distance of targetPlayer, Change state to flee
                        if (targetPlayerController.IsCanSee(pawn.gameObject) && pawn.IsDistanceLessThan(targetPlayer, fleeDistance))
                        {
                            ChangeState(AIState.Flee);
                        }
                        if (pawn.IsDistanceLessThan(targetPlayer, (fleeDistance/2)) && IsInLineOfSight(targetPlayer))
                        {
                            ChangeState(AIState.Flee);
                        }
                        break;
                }
        }
    }
}
