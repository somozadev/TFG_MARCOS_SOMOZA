using UnityEngine;
using StateMachine.Orc_Enemy;

public class OrcCallDeadEvent : EnemyCallDeadEvent
{
    public override void PerfDead()
    {
        base.PerfDead();
        // OrcStateMachine stateMachine = GetComponent<OrcStateMachine>();
        // stateMachine.GetComponent<Rigidbody>().velocity = Vector3.zero;
        // stateMachine.navAgent.enabled = false;
        GetComponent<OrcStateMachine>().SetIsDieAnim(true);
    }
}
