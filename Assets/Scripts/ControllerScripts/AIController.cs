using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    public enum AIState { Idle, Patrol, Bloodlust, Whisper, Chase, Silent, Flee, Hide };
    [Header("Miscellaneous Variables")]
    public AIState currentState;
    private float lastStateChangeTime;
    private GameObject targetPlayer;
    public float fleeDistance;
    [Header("Waypoint Variables")]
    public List<Transform> patrolWaypoints;
    public Transform nearestWaypoint;
    public float waypointStopDistance;
    public bool firstWaypointWasReached = false;
    [Header("Senses Variables")]
    public float hearingDistance;
    public float fieldOfView; // Variable to hold the AI's field of view value
    public float maxViewDistance; // Variable to determine how far the enemy can see
    public GameObject lastHit; // Tracks what the AI is seeing in-game
    public Vector3 collision = Vector3.zero;

    public override void Start()
    {
        targetPlayer = pawn.targetPlayer;
        ChangeState(AIState.Patrol);
        currentState = AIState.Patrol;
    }

    public override void Update()
    {
        base.Update();
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        ProcessInputs();
        CheckRaycast();
    }

    public override void ProcessInputs()
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
                // Do Patrol state
                // check for transitions
                DoFleeState();
                if (!pawn.IsDistanceLessThan(targetPlayer, fleeDistance))
                {
                    ChangeState(AIState.Patrol);
                }
            break;
        }
    }

    public virtual void ChangeState(AIState newState)
    {
        currentState = newState;
        lastStateChangeTime = Time.time; // Tracks how long it takes to change states when ChangeState is called
    }

    public void DoIdleState()
    {

    }

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
                    float distanceToNewWaypoint = Vector3.Distance(patrolWaypoint.position, pawn.transform.position);
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

    public void DoChaseState()
    {
        currentState = AIState.Chase;
        // Seek our targetPlayer
        if (targetPlayer != null)
        {
        Seek(targetPlayer.transform.position);
        }
    }

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

    public void CheckRaycast()
    {
        var ray = new Ray(pawn.transform.position, pawn.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            lastHit = hit.transform.gameObject;
            collision = hit.point;
        }
    }

    public bool IsCanHear(GameObject targetPlayer)
    {
        // Get the targetPlayer's NoiseMaker
        NoiseMaker noiseMaker = targetPlayer.GetComponent<NoiseMaker>();
        // If they don't have one, they can't make noise, so return false
        if (noiseMaker == null) 
        {
            return false;
        }
        // If they are making 0 noise, they also can't be heard
        if (noiseMaker.volumeDistance <= 0) 
        {
            return false;
        }
        // If they are making noise, add the volumeDistance in the noisemaker to the hearingDistance of this AI
        float totalDistance = noiseMaker.volumeDistance + hearingDistance;
        // If the distance between our pawn and targetPlayer is closer than this...
        if (Vector3.Distance(pawn.transform.position, targetPlayer.transform.position) <= totalDistance) 
        {
            // ... then we can hear the targetPlayer
            return true;
        }
        else 
        {
            // Otherwise, we are too far away to hear them
            return false;
        }
    }

    public bool IsInLineOfSight(GameObject targetPlayer)
    {
        if (targetPlayer == lastHit)
        {
            return true;
        }
        else{
            return false;
        }
    }

    public bool IsCanSee(GameObject targetPlayer)
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

    protected bool IsHasTarget()
    {
        // return true, we have targetPlayer
        return (pawn.targetPlayer != null);
        // false, we dont have a targetPlayer
    }
    
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
