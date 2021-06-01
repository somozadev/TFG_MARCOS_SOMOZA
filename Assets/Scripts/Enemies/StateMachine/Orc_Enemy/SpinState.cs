using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Orc_Enemy
{

    public class SpinState : IState
    {
        public IState DoState(OrcStateMachine stateMachine)
        {
            DoSpin(stateMachine);
            if(stateMachine.enemy.conditions.isChasing || stateMachine.enemy.conditions.isPursuitRange)
                return stateMachine.pursuitState;
            else if(stateMachine.enemy.conditions.isPatrol)
                return stateMachine.patrolState;
            return stateMachine.waitState;
        }

        private void DoSpin(OrcStateMachine stateMachine)
        {
            stateMachine.spinCollider.SetActive(true);
            stateMachine.enemy.conditions.canSpinAttack = false; 
            stateMachine.navAgent.speed = 20f;
            stateMachine.navAgent.acceleration = 15f;
            stateMachine.enemy.conditions.isPatrol = false;
            stateMachine.enemy.conditions.isChasing = false;
            stateMachine.navAgent.SetDestination(GameManager.Instance.player.transform.position);
            stateMachine.SetTriggerSpinAnim();

        }
    }

}