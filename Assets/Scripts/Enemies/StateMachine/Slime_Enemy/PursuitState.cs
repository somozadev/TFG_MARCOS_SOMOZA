using UnityEngine;
namespace StateMachine.Slime_Enemy
{
    public class PursuitState : IState
    {
        public IState DoState(SlimeStateMachine stateMachine)
        {
            DoPursuit(stateMachine);
            if (stateMachine.enemy.conditions.isAttackRange)
                return stateMachine.attackState;
            else if (stateMachine.enemy.conditions.isHitten)
                return stateMachine.getHitState;
            else if (stateMachine.enemy.conditions.isDead)
                return stateMachine.deathState;
            else
                return stateMachine.pursuitState;

        }
        private void DoPursuit(StateMachine stateMachine)
        {
            stateMachine.navAgent.isStopped = false;
            if (stateMachine.navAgent.destination != GameManager.Instance.player.transform.position)
            {
                stateMachine.navAgent.SetDestination(GameManager.Instance.player.transform.position);
                stateMachine.SetPursuitAnim(true);
            }
            if (Vector3.Distance(stateMachine.navAgent.transform.position, stateMachine.navAgent.destination) <= stateMachine.navAgent.stoppingDistance)
            {
                stateMachine.enemy.conditions.isAttackRange = true;
                stateMachine.enemy.conditions.isChasing = false;
            }


        }



    }
}