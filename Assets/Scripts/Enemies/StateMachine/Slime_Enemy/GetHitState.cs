using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Slime_Enemy
{
    public class GetHitState : IState
    {
        public IState DoState(SlimeStateMachine stateMachine)
        {
            DoGetHit(stateMachine);
            if (stateMachine.enemy.conditions.isWait)
                return stateMachine.waitState;
            else if (stateMachine.enemy.conditions.isAttackRange)
                return stateMachine.attackState;
            else if (stateMachine.enemy.conditions.isDead)
                return stateMachine.deathState;
            else
                return stateMachine.pursuitState;

        }

        private void DoGetHit(StateMachine stateMachine)
        {
            if (!GameManager.Instance.soundManager.isPlaying("SlimeGetHit"))
                GameManager.Instance.soundManager.Play("SlimeGetHit");
            stateMachine.navAgent.isStopped = true;
            stateMachine.navAgent.velocity = Vector3.zero;
            stateMachine.SetTriggerGetHitAnim();
            stateMachine.enemy.SetNewDamageIndicator();
            stateMachine.enemy.ParticleDamaged();

            stateMachine.enemy.stats.CurrentHp -= stateMachine.enemy.cuantity;
            stateMachine.enemy.hpIndicator.UpdateHp();

            stateMachine.enemy.conditions.isWait = true;
            stateMachine.enemy.conditions.isHitten = false;

        }
    }
}