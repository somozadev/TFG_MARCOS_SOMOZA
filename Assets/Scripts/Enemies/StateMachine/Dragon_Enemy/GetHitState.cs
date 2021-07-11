using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Dragon_Enemy
{
    public class GetHitState : IState
    {
        public IState DoState(DragonStateMachine stateMachine)
        {
            DoGetHit(stateMachine);
            if (stateMachine.enemy.conditions.isDead)
                return stateMachine.deathState;
            else
                return stateMachine.pursuitState;

        }

        private void DoGetHit(DragonStateMachine stateMachine)
        {
            if (!GameManager.Instance.soundManager.isPlaying("DragonGetHit"))
                GameManager.Instance.soundManager.Play("DragonGetHit");
            stateMachine.navAgent.isStopped = true;
            stateMachine.navAgent.velocity = Vector3.zero;
            stateMachine.SetTriggerGetHitAnim();
            stateMachine.enemy.SetNewDamageIndicator();
            stateMachine.enemy.ParticleDamaged();

            stateMachine.enemy.stats.CurrentHp -= stateMachine.enemy.cuantity;

            stateMachine.enemy.conditions.isWait = true;
            stateMachine.enemy.conditions.isHitten = false;

        }
    }
}