using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StateMachine.Orc_Enemy
{
    public class PatrolState : IState
    {
        public IState DoState(OrcStateMachine stateMachine)
        {
            DoPatrol(stateMachine);
            if (stateMachine.enemy.conditions.canSpinAttack)
                return stateMachine.spinState;
            else if (stateMachine.enemy.conditions.isChasing)
                return stateMachine.pursuitState;
            else if (stateMachine.enemy.conditions.isHitten)
                return stateMachine.getHitState;
            else if (stateMachine.enemy.conditions.isDead)
                return stateMachine.deathState;
            else
                return stateMachine.patrolState;
        }

        private void DoPatrol(OrcStateMachine stateMachine)
        {
            stateMachine.navAgent.stoppingDistance = 1.5f;
            stateMachine.enemy.stats.Spd = 2f;
            stateMachine.navAgent.speed = stateMachine.enemy.stats.Spd;
            stateMachine.SetPursuitAnim(false);
            stateMachine.SetPatrolAnim(true);
            stateMachine.navAgent.SetDestination(stateMachine.patrolTarget.position);
        }




    }
}