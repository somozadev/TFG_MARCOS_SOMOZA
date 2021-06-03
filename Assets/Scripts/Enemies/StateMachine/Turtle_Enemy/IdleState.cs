using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.Turtle_Enemy
{

    public class IdleState : IState
    {
        public IState DoState(TurtleStateMachine stateMachine)
        {
            DoIdle(stateMachine);
            if(stateMachine.enemy.conditions.isPatrol)
                return stateMachine.patrolState;
            else
                return stateMachine.idleState; 
        }

        private void DoIdle(TurtleStateMachine stateMachine)
        {
            stateMachine.enemy.conditions.isInvincible = false;
            stateMachine.enemy.conditions.isIdle = true; 
            stateMachine.SetIdleAnim(true);
            stateMachine.WaitToPatrol(2f);
        }
    }

}