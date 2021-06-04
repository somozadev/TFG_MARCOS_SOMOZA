using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.Turtle_Enemy
{

    public class GetHitState : IState
    {
        public IState DoState(TurtleStateMachine stateMachine)
        {
            DoGetHit(stateMachine);
            if (stateMachine.enemy.conditions.isDead)
                return stateMachine.deathState;
            else
                return stateMachine.invincibleState;
        }
        private void DoGetHit(TurtleStateMachine stateMachine)
        {
            stateMachine.enemy.conditions.isIdle = false;
            stateMachine.enemy.conditions.isPatrol = false;
            stateMachine.SetPatrolAnim(false);
            stateMachine.navAgent.velocity = Vector3.zero;
            stateMachine.enemy.ParticleDamaged();

            stateMachine.SetTriggerGetHitAnim();
            stateMachine.enemy.SetNewDamageIndicator();
            stateMachine.enemy.stats.CurrentHp -= stateMachine.enemy.cuantity;
            stateMachine.navAgent.speed += 0.2f;
            stateMachine.enemy.conditions.isHitten = false;
            stateMachine.enemy.conditions.isInvincible = true;


        }

    }

}