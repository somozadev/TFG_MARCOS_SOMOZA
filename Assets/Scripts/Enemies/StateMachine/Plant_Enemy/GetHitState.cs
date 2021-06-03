using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.Plant_Enemy
{
    public class GetHitState : IState
    {
        public IState DoState(PlantStateMachine stateMachine)
        {
            DoGetHit(stateMachine);
            if (stateMachine.enemy.conditions.isIdle)
                return stateMachine.idleState;
            else if (stateMachine.enemy.conditions.isDead)
                return stateMachine.deathState;
            else //if (stateMachine.enemy.conditions.isShootingRange)
                return stateMachine.shootRangeState;
        }
        private void DoGetHit(PlantStateMachine stateMachine)
        {
            
            stateMachine.navAgent.velocity = Vector3.zero;
            stateMachine.SetTriggerGetHitAnim();
            stateMachine.enemy.SetNewDamageIndicator();
            stateMachine.enemy.ParticleDamaged();

            stateMachine.enemy.stats.CurrentHp -= stateMachine.enemy.cuantity;

            stateMachine.enemy.conditions.isHitten = false;

        }

    }
}