using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Slime_Enemy
{
    public class WaitState : IState
    {
        public IState DoState(SlimeStateMachine stateMachine)
        {
            DoWait(stateMachine);

            if (stateMachine.enemy.conditions.isWait)
                return this;
            else if (stateMachine.enemy.conditions.isAttacking)
                return stateMachine.attackState;
            else if (stateMachine.enemy.conditions.isHitten)
                return stateMachine.getHitState;
            else
                return stateMachine.pursuitState;
        }

        private void DoWait(StateMachine stateMachine)
        {
            stateMachine.enemy.conditions.isHitten = false;
            stateMachine.WaitCor();
        }



    }
}
