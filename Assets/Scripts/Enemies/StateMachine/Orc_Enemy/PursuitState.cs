using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.Orc_Enemy
{
    public class PursuitState : IState
    {
        public IState DoState(OrcStateMachine stateMachine)
        {
            DoPursuit(stateMachine);
            if (stateMachine.enemy.conditions.isAttackRange)
                return stateMachine.attackState;
            else if (stateMachine.enemy.conditions.isHitten)
                return stateMachine.getHitState;
            else if (!stateMachine.enemy.conditions.isPursuitRange)
                return stateMachine.patrolState;
            else if (stateMachine.enemy.conditions.isDead)
                return stateMachine.deathState;
            else
                return this;
        }

        private void DoPursuit(OrcStateMachine stateMachine)
        {   
            stateMachine.LookPlayer();
            stateMachine.enemy.stats.Spd = 4f;
            stateMachine.navAgent.speed = stateMachine.enemy.stats.Spd;
            stateMachine.navAgent.stoppingDistance = 3f;
            stateMachine.navAgent.isStopped = false;
            if (stateMachine.navAgent.destination != GameManager.Instance.player.transform.position)
            {
                stateMachine.navAgent.SetDestination(GameManager.Instance.player.transform.position);
                stateMachine.SetPatrolAnim(false);
                stateMachine.SetPursuitAnim(true);
            }
            if (Vector3.Distance(stateMachine.navAgent.transform.position, stateMachine.navAgent.destination) <= stateMachine.navAgent.stoppingDistance)
            {
                stateMachine.enemy.conditions.isAttackRange = true;
                stateMachine.enemy.conditions.isChasing = false;
            }
            if (Vector3.Distance(stateMachine.navAgent.transform.position, stateMachine.navAgent.destination) >= stateMachine.enemy.stats.PursuitRange)
            {
                stateMachine.enemy.conditions.isPatrol = true;
                stateMachine.enemy.conditions.isChasing = false;
                stateMachine.enemy.conditions.isPursuitRange = false;

            }
            if (!stateMachine.isSpinCorrRunning)
                stateMachine.enemy.conditions.canSpinAttack = true;

        }


    }
}