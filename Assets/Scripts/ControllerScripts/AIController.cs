using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    public enum AIState { Idle, Patrol, Bloodlust, Whisper, Chase, Silent, Flee, Hide };
    [Header("Miscellaneous Variables")]
    public AIState currentState;
    private float lastStateChangeTime;
    [HideInInspector] public GameObject targetPlayer;
    public float fleeDistance;
    public float safeDistance;
    [Header("Waypoint Variables")]
    public PatrolWaypoint[] patrolWaypoints;
    [HideInInspector] public Transform nearestWaypoint;
    public float waypointStopDistance;
    private bool firstWaypointWasReached = false;

    public virtual void Awake()
    {
        patrolWaypoints = FindObjectsOfType<PatrolWaypoint>();
    }

    public override void Start()
    {
        ChangeState(AIState.Patrol);
        currentState = AIState.Patrol;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void ProcessInputs()
    {
        /*
        switch(currentState)
        {

        }
        */
    }

    public virtual void ChangeState(AIState newState)
    {
        currentState = newState;
        lastStateChangeTime = Time.time; // Tracks how long it takes to change states when ChangeState is called
    }

    #region Idle State
    public void DoIdleState()
    {
        // TODO: Play Idle animation
    }
    #endregion Idle State

    #region Patrol State
    public void DoPatrolState()
    {
        currentState = AIState.Patrol;
        if (patrolWaypoints != null)
        {
            FindNearestPatrolWaypoint();
            Seek(nearestWaypoint.position);
            if (pawn.IsDistanceLessThan(nearestWaypoint.gameObject, waypointStopDistance))
            {
                firstWaypointWasReached = true;
                Transform nextWaypoint = nearestWaypoint.GetComponent<PatrolWaypoint>().nextWaypoint.transform;
                nearestWaypoint = nextWaypoint;
            }
        }
        else
        {
            ChangeState(AIState.Idle);
        }
    }

    // Create a function to track what PatrolWaypoint is the closest to the current AIController
    public void FindNearestPatrolWaypoint()
    {
        if (firstWaypointWasReached == false)
        {
            // Assume the first waypoint in the array is the nearest
            nearestWaypoint = patrolWaypoints[0].transform;

            float distanceToNearestWaypoint = Vector3.Distance(nearestWaypoint.position, pawn.transform.position);

            // Compare the current waypoint's Vector distance to all other waypoint distances in the list
            foreach (var patrolWaypoint in patrolWaypoints)
            {
                if (patrolWaypoint.gameObject != nearestWaypoint.gameObject)
                {
                    // Find the distance between the AI and each patrolWaypoint
                    float distanceToNewWaypoint = Vector3.Distance(patrolWaypoint.transform.position, pawn.transform.position);
                    if (distanceToNewWaypoint < distanceToNearestWaypoint)
                    {
                        //distanceToNewWaypoint = distanceToNearestWaypoint;
                        nearestWaypoint = patrolWaypoint.transform;
                    }
                }
            }
        }
    }    

    protected void RestartPatrol()
    {
        // Reset the current array index back to 0
        FindNearestPatrolWaypoint();
    }

    #endregion Patrol State

    #region Chase State
    public void DoChaseState()
    {
        currentState = AIState.Chase;
        // Seek our targetPlayer
        if (targetPlayer != null)
        {
        Seek(targetPlayer.transform.position);
        }
    }
    #endregion Chase State

    #region Seek
    public void Seek(Vector3 targetPosition)
    {
        // Chase a GAMEOBJECT's position
        if (targetPlayer != null)
        {
            // Rotate towards the targetPlayer
            pawn.RotateTowards(targetPosition);
            // Move Forward towards the targetPlayer
            pawn.MoveForward();
        }
        else
        {
            //Seek(nearestWaypoint);
            ChangeState(AIState.Patrol);
        }
    }
    #endregion Seek

    #region Flee State
    public void DoFleeState()
    {
        currentState = AIState.Flee;
        // Find the Vector to our target
        Vector3 vectorToTarget = targetPlayer.transform.position - pawn.transform.position;
        // Find the Vector away from our target by multiplying by -1
        Vector3 vectorAwayFromTarget = -vectorToTarget;
        // Find the vector we would travel down in order to flee
        Vector3 fleeVector = vectorAwayFromTarget.normalized * fleeDistance;
        // Seek the point that is "fleeVector" away from our current position
        Seek(pawn.transform.position + fleeVector);
    }
    #endregion Flee State

    #region Target Player
    public void TargetPlayerOne()
    {
        // If the GameManager exists
        if(GameManager.instance != null)
        {
            if (GameManager.instance.players.Count > 0)
            {
                pawn.targetPlayer = GameManager.instance.players[0].pawn.gameObject;
            }
        }
    }

    public void TargetPlayerTwo()
    {
        // If the GameManager exists
        if(GameManager.instance != null)
        {
            if (GameManager.instance.players.Count > 0)
            {
                pawn.targetPlayer = GameManager.instance.players[1].pawn.gameObject;
            }
        }
    }
    #endregion Target Player

    #region Senses
    public override void CheckRaycast()
    {
        var ray = new Ray(pawn.transform.position, pawn.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            lastHit = hit.transform.gameObject;
            collision = hit.point;
        }
    }

    public override bool IsInLineOfSight(GameObject targetPlayer)
    {
        if (targetPlayer == lastHit)
        {
            return true;
        }
        else{
            return false;
        }
    }

    public override bool IsCanSee(GameObject targetPlayer)
    {
        // Find the vector from the agent to the targetPlayer
        Vector3 agentTotargetPlayerVector = targetPlayer.transform.position - transform.position;
        // Find the angle between the direction our agent is facing (forward in local space) and the vector to the targetPlayer.
        float angleTotargetPlayer = Vector3.Angle(agentTotargetPlayerVector, pawn.transform.forward);
        // if that angle is less than our field of view
        if (angleTotargetPlayer < fieldOfView) 
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    protected override bool IsHasTarget()
    {
        // return true, we have targetPlayer
        return (pawn.targetPlayer != null);
        // false, we dont have a targetPlayer
    }
    #endregion Senses
    
    // Removes this object from the enemies array in the Game Manager
    /*
    public void OnDestroy()
    {
        if (GameManager.instance.enemies != null)
        {
            // Deregister with the Game Manager
            GameManager.instance.enemies.Remove(this);
        }
    }
    */
}
