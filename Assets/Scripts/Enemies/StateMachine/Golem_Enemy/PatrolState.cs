using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Golem_Enemy
{

    public class PatrolState : IState
    {
        public IState DoState(GolemStateMachine stateMachine)
        {
            DoPatrol(stateMachine);
            if (stateMachine.enemy.conditions.isHitten)
                return stateMachine.getHitState;
            else if (stateMachine.enemy.conditions.isDead)
                return stateMachine.deathState;
            else if (stateMachine.enemy.conditions.isShootingRange && stateMachine.enemy.conditions.canMegaAttack)
                return stateMachine.attackState;
            else
                return stateMachine.patrolState;
        }

        private void DoPatrol(GolemStateMachine stateMachine)
        {
            stateMachine.navAgent.isStopped = false;
            stateMachine.navAgent.SetDestination(stateMachine.patrolTarget.position);
            if (Vector3.Distance(GameManager.Instance.player.transform.position, stateMachine.transform.position) <= stateMachine.enemy.stats.ShootingRange)
            {
                stateMachine.enemy.conditions.isShootingRange = true;
                stateMachine.enemy.conditions.isPatrol = false;
            }
            else
            {
                stateMachine.enemy.conditions.isShootingRange = false;
            }
            stateMachine.enemy.conditions.isPatrol = true;

        }
    }

}