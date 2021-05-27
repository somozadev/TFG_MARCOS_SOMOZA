using UnityEngine.AI;
using UnityEngine;
namespace StateMachine
{
    public class PursuitState : IState
    {

        public IState DoState(StateMachine stateMachine)
        {
            DoPursuit(stateMachine);
            if (stateMachine.isRange)
                return stateMachine.attackState;
            else
                return stateMachine.pursuitState;

        }
        private void DoPursuit(StateMachine stateMachine)
        {
            if (stateMachine.navAgent.destination != GameManager.Instance.player.transform.position)
                stateMachine.navAgent.SetDestination(GameManager.Instance.player.transform.position);

        }



    }
}