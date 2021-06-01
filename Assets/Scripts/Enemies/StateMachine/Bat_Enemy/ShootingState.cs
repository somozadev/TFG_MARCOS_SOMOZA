using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Bat_Enemy
{
    public class ShootingState : IState
    {
        public IState DoState(BatStateMachine stateMachine)
        {
            DoShooting(stateMachine);

            if (stateMachine.enemy.conditions.isDead)
                return stateMachine.deathState;
            else if (stateMachine.enemy.conditions.isHitten)
                return stateMachine.getHitState;
            else if (stateMachine.enemy.conditions.isChasing)
                return stateMachine.pursuitState;
            else if (stateMachine.enemy.conditions.isAttackRange)
                return stateMachine.attackState;
            else return this;


        }
        private void DoShooting(BatStateMachine stateMachine)
        {
            stateMachine.SetShootAnim(true);

            if (Vector3.Distance(stateMachine.navAgent.transform.position, stateMachine.navAgent.destination) <= stateMachine.navAgent.stoppingDistance)
            {
                stateMachine.enemy.conditions.isShootingRange = false;
                stateMachine.enemy.conditions.isAttackRange = true;
                stateMachine.enemy.conditions.isChasing = false;
                stateMachine.SetShootAnim(false);
            }
            else if (Vector3.Distance(stateMachine.navAgent.transform.position, stateMachine.navAgent.destination) > stateMachine.enemy.stats.ShootingRange)
            {
                stateMachine.enemy.conditions.isShootingRange = false;
                stateMachine.enemy.conditions.canShoot = false;
                stateMachine.StopCoroutine("CounterToIsShootOn");
                stateMachine.enemy.conditions.isWait = false;
                stateMachine.SetShootAnim(false);

            }
        }




    }
}

