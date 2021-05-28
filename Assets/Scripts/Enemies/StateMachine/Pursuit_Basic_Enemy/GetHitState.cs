using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Pursuit_Basic_Enemy
{
    public class GetHitState : IState
    {
        public IState DoState(StateMachine stateMachine)
        {
            DoGetHit(stateMachine);
            if (stateMachine.enemy.conditions.isWait)
                return stateMachine.waitState;
            else if (stateMachine.enemy.conditions.isRange)
                return stateMachine.attackState;
            else if (stateMachine.enemy.conditions.isDead)
                return stateMachine.deathState;
            else
                return stateMachine.pursuitState;

        }

        private void DoGetHit(StateMachine stateMachine)
        {
            stateMachine.navAgent.isStopped = true;
            stateMachine.navAgent.velocity = Vector3.zero;
            stateMachine.animator.SetTrigger("GetHit");
            stateMachine.enemy.SetNewDamageIndicator();
        
            stateMachine.enemy.stats.CurrentHp -= (int)stateMachine.enemy.cuantity;

            stateMachine.enemy.conditions.isWait = true;
            stateMachine.enemy.conditions.isHitten = false;

        }
    }
}