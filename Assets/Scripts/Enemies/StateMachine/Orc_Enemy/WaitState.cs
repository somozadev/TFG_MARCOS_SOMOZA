
namespace StateMachine.Orc_Enemy
{
    public class WaitState : IState
    {
        public IState DoState(OrcStateMachine stateMachine)
        {
            DoWait(stateMachine);

            if (stateMachine.enemy.conditions.isWait)
                return this;
            else if (stateMachine.enemy.conditions.isAttacking)
                return stateMachine.attackState;
            else if (stateMachine.enemy.conditions.isHitten)
                return stateMachine.getHitState;
            else if (!stateMachine.enemy.conditions.isPursuitRange)
                return stateMachine.pursuitState;
            else 
                return stateMachine.patrolState;

        }

        private void DoWait(OrcStateMachine stateMachine)
        {
            stateMachine.enemy.conditions.isHitten = false;
            stateMachine.WaitCor();
        }
    }
}