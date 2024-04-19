using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControllerHuman : AIController
{
    public Transform hidingWaypoint;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void ProcessInputs()
    {

    }

    public void DoHideState()
    {
        Hide();
    }

    public void Hide()
    {
        // TODO: Find nearest hide waypoint

        // TODO: Seek nearest hide waypoint
    }

    public void DoSilentState()
    {
        // TODO: If isInHidingSpace, do nothing

        // TODO: else use the silent ability
    }

}
