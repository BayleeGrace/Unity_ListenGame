using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnYoungMan : PawnHuman
{
    [Header("Abilities")]
    public GameObject distractionPrefab;
    public Transform firepointTransform;
    public float throwSpeed;
    public float throwDistance;
    //public AudioClip distractAudioClip;
    private GameObject newDistraction;

    public override void Start()
    {
        base.Start();
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
        SpawnDistraction(distractionPrefab, throwSpeed, throwDistance);
    }

    public override void DeactivateAbility3()
    {
        base.DeactivateAbility3();
    }

    // TODO: Create a function that instantiates a sound in a random place
    public void SpawnDistraction(GameObject distractionPrefab, float fireForce, float lifespan)
    {
        // Instantiate our distraction
        newDistraction = Instantiate(distractionPrefab, firepointTransform.position, firepointTransform.rotation) as GameObject;

        // Get the rigidbody component
        Rigidbody rigidbody = newDistraction.GetComponent<Rigidbody>();

        if (rigidbody != null)
        {
            rigidbody.AddForce(firepointTransform.forward * fireForce);
        }
        Destroy(newDistraction, lifespan);
    }

    /*public void PlayDistractionSound()
    {
        if (newDistraction != null)
        {
            AudioSource.PlayClipAtPoint(distractAudioClip, newDistraction.transform.position);
        }
    }*/
}
