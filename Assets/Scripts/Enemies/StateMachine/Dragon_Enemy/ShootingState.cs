using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Dragon_Enemy
{
    public class ShootingState : IState
    {
        public IState DoState(DragonStateMachine stateMachine)
        {
            DoShooting(stateMachine);

            if (stateMachine.enemy.conditions.isDead)
                return stateMachine.deathState;
            else if (stateMachine.enemy.conditions.isHitten)
                return stateMachine.getHitState;
            else if (stateMachine.enemy.conditions.isChasing)
                return stateMachine.pursuitState;
            else return this;


        }
        private void DoShooting(DragonStateMachine stateMachine)
        {
            stateMachine.SetShootAnim(true);

            if (Vector3.Distance(stateMachine.navAgent.transform.position, stateMachine.navAgent.destination) > stateMachine.enemy.stats.ShootingRange)
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

