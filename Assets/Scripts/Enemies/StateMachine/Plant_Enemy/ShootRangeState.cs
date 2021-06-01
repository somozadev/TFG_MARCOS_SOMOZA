using UnityEngine;
namespace StateMachine.Plant_Enemy
{

    public class ShootRangeState : IState
    {
        public IState DoState(PlantStateMachine stateMachine)
        {
            DoShootRange(stateMachine);
            throw new System.NotImplementedException();
        }
        private void DoShootRange(PlantStateMachine stateMachine)
        {
            stateMachine.SetIdleAnim(false);
            stateMachine.SetInRangeToShootAnim(true); 
            if (Vector3.Distance(stateMachine.transform.position, GameManager.Instance.player.transform.position) > stateMachine.enemy.stats.ShootingRange)
            {
                stateMachine.enemy.conditions.isAttackRange = false;
                stateMachine.enemy.conditions.isIdle = true;
            }

        }
    }

}