using UnityEngine.AI;
using UnityEngine;
namespace StateMachine.Pursuit_Basic_Enemy
{
    public class PursuitState : IState
    {
        public IState DoState(StateMachine stateMachine)
        {
            DoPursuit(stateMachine);
            if (stateMachine.enemy.conditions.isRange)
                return stateMachine.attackState;
            else if(stateMachine.enemy.conditions.isHitten)
                return stateMachine.getHitState;
            else if(stateMachine.enemy.conditions.isDead)
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
                stateMachine.animator.SetBool("Pursuit", true);
            }
            if (Vector3.Distance(stateMachine.navAgent.transform.position, stateMachine.navAgent.destination) <= stateMachine.navAgent.stoppingDistance)
            {
                stateMachine.enemy.conditions.isRange = true;
                stateMachine.enemy.conditions.isChasing = false;
            }


        }



    }
}