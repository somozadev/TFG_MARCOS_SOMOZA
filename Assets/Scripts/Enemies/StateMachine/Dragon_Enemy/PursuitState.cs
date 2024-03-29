﻿using UnityEngine.AI;
using UnityEngine;
namespace StateMachine.Dragon_Enemy
{
    public class PursuitState : IState
    {
        public IState DoState(DragonStateMachine stateMachine)
        {
            DoPursuit(stateMachine);
            if (stateMachine.enemy.conditions.canShoot)
                return stateMachine.shootingState;
            else if (stateMachine.enemy.conditions.isHitten)
                return stateMachine.getHitState;
            else if (stateMachine.enemy.conditions.isDead)
                return stateMachine.deathState;
            else
                return stateMachine.pursuitState;

        }
        private void DoPursuit(DragonStateMachine stateMachine)
        {
            stateMachine.navAgent.isStopped = false;
            if (stateMachine.navAgent.destination != GameManager.Instance.player.transform.position)
            {
                stateMachine.navAgent.SetDestination(GameManager.Instance.player.transform.position);
            }
            if (Vector3.Distance(stateMachine.navAgent.transform.position, stateMachine.navAgent.destination) <= stateMachine.enemy.stats.ShootingRange)
            {
                stateMachine.enemy.conditions.isShootingRange = true;
                stateMachine.enemy.conditions.isChasing = false;
                stateMachine.ShootingMonobehaviour();
            }
            else if (Vector3.Distance(stateMachine.navAgent.transform.position, stateMachine.navAgent.destination) > stateMachine.enemy.stats.ShootingRange)
            {
                stateMachine.enemy.conditions.isShootingRange = false;
                stateMachine.enemy.conditions.canShoot = false;
                stateMachine.StopCoroutine("CounterToIsShootOn");
                stateMachine.enemy.conditions.isWait = false;

            }


        }



    }
}