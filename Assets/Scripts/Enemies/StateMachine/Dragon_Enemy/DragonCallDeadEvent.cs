using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using StateMachine.Dragon_Enemy;

public class DragonCallDeadEvent : EnemyCallDeadEvent
{
    public override void PerfDead()
    {
        base.PerfDead();
        DragonStateMachine stateMachine = GetComponent<DragonStateMachine>();
        stateMachine.GetComponent<Rigidbody>().velocity = Vector3.zero;
        stateMachine.navAgent.enabled = false;
        GetComponent<DragonStateMachine>().SetIsDieAnim(true);
    }
}


