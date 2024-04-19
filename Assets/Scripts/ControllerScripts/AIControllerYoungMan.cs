using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControllerYoungMan : AIControllerHuman
{
    [HideInInspector] public Controller targetPlayerController;
    [HideInInspector] public bool objective1Complete = true;

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
            if (objective1Complete == false)
            {
                switch(currentState)
                {
                    case AIState.Patrol:
                        // Do Patrol state
                        // check for transitions
                        DoPatrolState();
                        if (pawn.IsDistanceLessThan(targetPlayer, fleeDistance))
                        {
                            ChangeState(AIState.Flee);
                        }
                        break;

                    case AIState.Flee:
                        DoFleeState();
                        if (targetPlayer != null)
                        {
                            if (!pawn.IsDistanceLessThan(targetPlayer, fleeDistance) && pawn.IsDistanceLessThan(targetPlayer, safeDistance))
                            {
                                ChangeState(AIState.Hide);
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

                    case AIState.Hide:
                        DoHideState();
                        // If the targetPlayer is outside of the safe distance then switch to Patrol State
                        if (!pawn.IsDistanceLessThan(targetPlayer, safeDistance))
                        {
                            ChangeState(AIState.Patrol);
                        }
                        break;

                    case AIState.Silent:
                        DoSilentState();
                        // If the targetPlayer is outside of the safe distance then change to patrol state
                        if (!pawn.IsDistanceLessThan(targetPlayer, safeDistance))
                        {
                            ChangeState(AIState.Patrol);
                        }

                        // Else if the targetPlayer is within fleeDistance, change to the flee state
                        else if (pawn.IsDistanceLessThan(targetPlayer, safeDistance))
                        {
                            ChangeState(AIState.Flee);
                        }
                        break;

                    case AIState.Chase:
                        DoChaseState();
                        // TODO: If the pawn is InLineOfSight && is within flee distance of targetPlayer, Change state to flee
                        if (IsInLineOfSight(targetPlayer) && !pawn.IsDistanceLessThan(targetPlayer, fleeDistance))
                        {
                            ChangeState(AIState.Flee);
                        }
                        break;
                }
            }
            else
            {
                switch(currentState)
                {
                    case AIState.Patrol:
                        // Do Patrol state
                        // check for transitions
                        DoPatrolState();
                        if (pawn.IsDistanceLessThan(targetPlayer, fleeDistance) || targetPlayerController.IsCanSee(pawn.gameObject))
                        {
                            ChangeState(AIState.Flee);
                        }
                        else if (pawn.IsDistanceLessThan(targetPlayer, fleeDistance) && !targetPlayerController.IsCanSee(pawn.gameObject))
                        {
                            ChangeState(AIState.Chase);
                        }
                        break;

                    case AIState.Flee:
                        DoFleeState();
                        if (targetPlayer != null)
                        {
                            if (!pawn.IsDistanceLessThan(targetPlayer, safeDistance))
                            {
                                ChangeState(AIState.Patrol);
                            }
                            else if (pawn.IsDistanceLessThan(targetPlayer, safeDistance) && !targetPlayerController.IsCanHear(pawn.gameObject))
                            {
                                ChangeState(AIState.Chase);
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
                        if (targetPlayerController.IsInLineOfSight(pawn.gameObject) && !pawn.IsDistanceLessThan(targetPlayer, fleeDistance))
                        {
                            ChangeState(AIState.Flee);
                        }
                        else if (!pawn.IsDistanceLessThan(targetPlayer, safeDistance))
                        {
                            ChangeState(AIState.Patrol);
                        }
                        break;
                }
            }
        }
    }
}
