using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Pursuit_Basic_Enemy
{
    public class DeathState : IState
    {
        public IState DoState(StateMachine stateMachine)
        {
            if (!stateMachine.animator.GetBool("isDie"))
                DoDeathState(stateMachine);
            return this;
        }

        private void DoDeathState(StateMachine stateMachine)
        {
            stateMachine.navAgent.isStopped = true;
            stateMachine.navAgent.velocity = Vector3.zero;
            stateMachine.enemy.SetNewDamageIndicator();
            stateMachine.enemy.ParticleDamaged();
            stateMachine.animator.SetTrigger("Die");
            stateMachine.animator.SetBool("isDie", true);
            stateMachine.enemy.conditions.isAttacking = false;
            stateMachine.enemy.conditions.isChasing = false;
            stateMachine.enemy.conditions.isRange = false;
            stateMachine.enemy.conditions.isHitten = false;
            stateMachine.enemy.conditions.isDead = true;
        }

    }
}