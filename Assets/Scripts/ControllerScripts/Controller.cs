using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Controller : MonoBehaviour
{
    public Pawn pawn;
    [Header("Senses Variables")]
    public float fieldOfView; // Variable to hold the AI's field of view value
    public float maxViewDistance; // Variable to determine how far the enemy can see
    public GameObject lastHit; // Tracks what the AI is seeing in-game
    public Vector3 collision = Vector3.zero;
    public float hearingDistance;
    [HideInInspector] public bool isStunned = false;

    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        if (pawn.targetPlayer != null)
        {
            ProcessInputs();
        }
        CheckRaycast();
    }

    public virtual void ProcessInputs()
    {

    }
    #region Senses

    public virtual bool IsCanHear(GameObject targetPlayer)
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

    public virtual void CheckRaycast()
    {
        var ray = new Ray(pawn.transform.position, pawn.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            lastHit = hit.transform.gameObject;
            collision = hit.point;
        }
    }

    public virtual bool IsInLineOfSight(GameObject targetPlayer)
    {
        if (targetPlayer == lastHit)
        {
            return true;
        }
        else{
            return false;
        }
    }

    public virtual bool IsCanSee(GameObject targetPlayer)
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

    protected virtual bool IsHasTarget()
    {
        // return true, we have targetPlayer
        return (pawn.targetPlayer != null);
        // false, we dont have a targetPlayer
    }
    #endregion Senses
}
