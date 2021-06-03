using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Turtle_Enemy
{

    public class InvincibleState : IState
    {
        public IState DoState(TurtleStateMachine stateMachine)
        {
            DoInvincible(stateMachine);
            if (!stateMachine.enemy.conditions.isInvincible)
                return stateMachine.patrolState;
            else
                return this;
        }

        private void DoInvincible(TurtleStateMachine stateMachine)
        {
            stateMachine.enemy.conditions.isIdle = false;
            stateMachine.enemy.conditions.isPatrol = false;
            stateMachine.navAgent.velocity = Vector3.zero;
            stateMachine.navAgent.isStopped = true;
            stateMachine.SetPatrolAnim(false);
            if (stateMachine.enemy.conditions.isHitten)
            {
                stateMachine.enemy.SetNewInvencibleDamageIndicator();
                stateMachine.enemy.ParticleDamaged();
                stateMachine.StopCoroutine("CdInvCorr");
                stateMachine.SetGetHitInvincibleAnim(true);
                stateMachine.RestartCdInvincible();
                stateMachine.enemy.conditions.isHitten = false;

            }
            stateMachine.StartCdInvincible(Random.Range(4f, 10f));

        }
    }

}