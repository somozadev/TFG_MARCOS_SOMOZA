using UnityEngine.AI;
using UnityEngine;
namespace StateMachine
{
    public class PatrolState : IState
    {
        StateMachine stateMachineRef;
        public IState DoState(StateMachine stateMachine)
        {
            stateMachineRef = stateMachine;

            DoPatrol();
            if (stateMachine.isRange)
                return stateMachine.attackState;
            else
                return stateMachine.idleState;

        }
        private void DoPatrol()
        {
            Debug.Log("Im patrol");
        }


    }
}