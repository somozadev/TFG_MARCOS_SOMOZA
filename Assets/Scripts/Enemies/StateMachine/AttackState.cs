using UnityEngine.AI;
using UnityEngine;
namespace StateMachine
{
    public class AttackState : IState
    {
        public IState DoState(StateMachine stateMachine)
        {
            DoAttack();
            if (stateMachine.isRange)
                return stateMachine.attackState;
            else
                return stateMachine.pursuitState;
        }
        private void DoAttack()
        {
            Debug.Log("Im attack");
        }
    }
}