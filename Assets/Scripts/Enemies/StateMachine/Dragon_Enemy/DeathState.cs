using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Dragon_Enemy
{
    public class DeathState : IState
    {
        public IState DoState(DragonStateMachine stateMachine)
        {
            if (!stateMachine.GetIsDieAnim())
                DoDeathState(stateMachine);
            return this;
        }

        private void DoDeathState(DragonStateMachine stateMachine)
        {
            stateMachine.navAgent.isStopped = true;
            stateMachine.navAgent.velocity = Vector3.zero;
            stateMachine.enemy.SetNewDamageIndicator();
            stateMachine.enemy.ParticleDamaged();
            stateMachine.SetTriggerDieAnim();
            stateMachine.SetIsDieAnim(true);
            stateMachine.childrenCollider.enabled = true; 
            
            stateMachine.enemy.conditions.isAttacking = false;
            stateMachine.enemy.conditions.isChasing = false;
            stateMachine.enemy.conditions.isAttackRange = false;
            stateMachine.enemy.conditions.isHitten = false;
            stateMachine.enemy.conditions.isDead = true;
        }

    }
}