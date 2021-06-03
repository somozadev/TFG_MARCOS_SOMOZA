using UnityEngine;
namespace StateMachine.Plant_Enemy
{

    public class ShootRangeState : IState
    {
        public IState DoState(PlantStateMachine stateMachine)
        {
            DoShootRange(stateMachine);
            if (stateMachine.enemy.conditions.isIdle)
                return stateMachine.idleState;
            else if (stateMachine.enemy.conditions.isHitten)
                return stateMachine.getHitState;
            else if (stateMachine.enemy.conditions.isDead)
                return stateMachine.deathState;
            else if (stateMachine.enemy.conditions.canShoot)
                return stateMachine.shootingState;
            else
                return this;
        }
        private void DoShootRange(PlantStateMachine stateMachine)
        {
            stateMachine.SetIdleAnim(false);
            stateMachine.SetInRangeToShootAnim(true);
            if (Vector3.Distance(stateMachine.transform.position, GameManager.Instance.player.transform.position) > stateMachine.enemy.stats.ShootingRange)
            {
                stateMachine.enemy.conditions.isShootingRange = false;
                stateMachine.enemy.conditions.isIdle = true;
                stateMachine.enemy.conditions.canShoot = false;
                stateMachine.StopCoroutine("CounterToIsShootOn");
            }
            else
            {
                stateMachine.enemy.conditions.isShootingRange = true;
                stateMachine.ShootingMonobehaviour();
            }

        }
    }

}