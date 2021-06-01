using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Orc_Enemy
{
    public class IdleState : IState
    {
        public IState DoState(OrcStateMachine stateMachine)
        {
            DoIdle(stateMachine);
            if (stateMachine.enemy.conditions.isAttackRange)
                return stateMachine.attackState;
            else if (stateMachine.enemy.conditions.isPursuitRange)
                return stateMachine.pursuitState;
            else if (stateMachine.enemy.conditions.isPatrol)
                return stateMachine.patrolState;
            else if (stateMachine.enemy.conditions.isHitten)
                return stateMachine.getHitState;
            else if (stateMachine.enemy.conditions.isDead)
                return stateMachine.deathState;
            else
                return this;
        }

        private void DoIdle(OrcStateMachine stateMachine)
        {
            stateMachine.StartCoroutine(stateMachine.IdleToPatrolWait(1.2f));
        }
    }
}