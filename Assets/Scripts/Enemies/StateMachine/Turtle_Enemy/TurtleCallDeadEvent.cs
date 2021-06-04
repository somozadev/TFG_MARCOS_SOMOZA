using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Turtle_Enemy;

public class TurtleCallDeadEvent : EnemyCallDeadEvent
{
    public override void PerfDead()
    {
        base.PerfDead();
        TurtleStateMachine stateMachine = GetComponent<TurtleStateMachine>();
        stateMachine.GetComponent<Rigidbody>().velocity = Vector3.zero;
        stateMachine.navAgent.enabled = false;
        GetComponent<TurtleStateMachine>().SetIsDieAnim(true);
    }
}
