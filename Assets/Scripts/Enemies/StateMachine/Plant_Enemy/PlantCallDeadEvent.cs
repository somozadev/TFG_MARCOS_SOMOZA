using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Plant_Enemy;

public class PlantCallDeadEvent : EnemyCallDeadEvent
{
    public override void PerfDead()
    {
        base.PerfDead();
        PlantStateMachine stateMachine = GetComponent<PlantStateMachine>();
        stateMachine.GetComponent<Rigidbody>().velocity = Vector3.zero;
        stateMachine.navAgent.enabled = false;
        GetComponent<PlantStateMachine>().SetIsDieAnim(true);
    }
}
