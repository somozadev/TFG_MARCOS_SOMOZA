using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using StateMachine.Bat_Enemy;

public class BatCallDeadEvent : EnemyCallDeadEvent
{
    public override void PerfDead()
    {   
        base.PerfDead();
        GetComponent<BatStateMachine>().SetIsDieAnim(true);
    }

}
