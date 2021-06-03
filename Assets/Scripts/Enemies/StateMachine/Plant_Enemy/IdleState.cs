using UnityEngine;
namespace StateMachine.Plant_Enemy
{

    public class IdleState : IState
    {
        public IState DoState(PlantStateMachine stateMachine)
        {
            DoIdle(stateMachine);
            if (stateMachine.enemy.conditions.isShootingRange)
                return stateMachine.shootRangeState;
            else if (stateMachine.enemy.conditions.isHitten)
                return stateMachine.getHitState;
            else if (stateMachine.enemy.conditions.isDead)
                return stateMachine.deathState;
            else
                return this;
        }

        private void DoIdle(PlantStateMachine stateMachine)
        {
            stateMachine.SetIdleAnim(true);
            stateMachine.SetInRangeToShootAnim(false);
            if (Vector3.Distance(stateMachine.transform.position, GameManager.Instance.player.transform.position) <= stateMachine.enemy.stats.ShootingRange)
            {
                stateMachine.enemy.conditions.isShootingRange = true;
                stateMachine.enemy.conditions.isIdle = false;
            }
        }
    }

}