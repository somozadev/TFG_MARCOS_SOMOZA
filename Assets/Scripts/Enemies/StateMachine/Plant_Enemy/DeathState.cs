using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Plant_Enemy
{
    public class DeathState : IState
    {
        public IState DoState(PlantStateMachine stateMachine)
        {
            if (!stateMachine.GetIsDieAnim())
                DoDeathState(stateMachine);
            return this;
        }

        private void DoDeathState(PlantStateMachine stateMachine)
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
