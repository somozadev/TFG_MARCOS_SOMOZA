using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Orc_Enemy
{
    public class DeathState : IState
    {
        public IState DoState(OrcStateMachine stateMachine)
        {
            if (!stateMachine.GetIsDieAnim())
                DoDeathState(stateMachine);
            return this;
        }

        private void DoDeathState(StateMachine stateMachine)
        {
            stateMachine.navAgent.isStopped = true;
            stateMachine.navAgent.velocity = Vector3.zero;
            stateMachine.enemy.SetNewDamageIndicator();
            stateMachine.enemy.ParticleDamaged();
            stateMachine.SetTriggerDieAnim();
            stateMachine.SetIsDieAnim(true);
            stateMachine.enemy.conditions.Reset();
            stateMachine.enemy.conditions.isDead = true;
        }


    }
}