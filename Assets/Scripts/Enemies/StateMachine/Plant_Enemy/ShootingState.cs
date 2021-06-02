using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.Plant_Enemy
{
    public class ShootingState : IState
    {
        public IState DoState(PlantStateMachine stateMachine)
        {
            DoShooting(stateMachine);
            // if (stateMachine.enemy.conditions.isDead)
            //     return stateMachine.deathState;
            // else if (stateMachine.enemy.conditions.isHitten)
            //     return stateMachine.getHitState;
            if (stateMachine.enemy.conditions.isIdle)
                return stateMachine.idleState;
            else //if (stateMachine.enemy.conditions.isShootingRange)
                return stateMachine.shootRangeState;

        }

        private void DoShooting(PlantStateMachine stateMachine)
        {

            stateMachine.SetTriggerShootAnim();
            if (Vector3.Distance(stateMachine.transform.position, GameManager.Instance.player.transform.position) > stateMachine.enemy.stats.ShootingRange)
            {
                stateMachine.enemy.conditions.isShootingRange = false;
                stateMachine.enemy.conditions.isIdle = true; 
                stateMachine.enemy.conditions.canShoot = false;
                stateMachine.StopCoroutine("CounterToIsShootOn");

            }

        }



    }
}