using UnityEngine.AI;
using UnityEngine;
namespace StateMachine
{
    public class IdleState : IState
    {
        public IState DoState(StateMachine stateMachine)
        {
            DoIdle();
            if (stateMachine.isRange)
                return stateMachine.attackState;
            else if(stateMachine.isChasing)
                return stateMachine.pursuitState;
            else
                return stateMachine.idleState;
        }
        private void DoIdle()
        {
            Debug.Log("Im idle");
        }
    }
}