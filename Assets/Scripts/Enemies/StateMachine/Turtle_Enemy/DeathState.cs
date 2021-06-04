using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Turtle_Enemy
{
    public class DeathState : IState
    {
        public IState DoState(TurtleStateMachine stateMachine)
        {
            if (!stateMachine.GetIsDieAnim())
                DoDeathState(stateMachine);
            return this;
        }
        private void DoDeathState(TurtleStateMachine stateMachine)
        {

            stateMachine.navAgent.velocity = Vector3.zero;
            stateMachine.enemy.SetNewDamageIndicator();
            stateMachine.enemy.ParticleDamaged();
            stateMachine.SetTriggerDieAnim();
            stateMachine.SetIsDieAnim(true);

            stateMachine.enemy.conditions.Reset();
        }

    }

}