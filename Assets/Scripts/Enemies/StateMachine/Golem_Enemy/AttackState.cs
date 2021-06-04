using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.Golem_Enemy
{
    public class AttackState : IState
    {
        public IState DoState(GolemStateMachine stateMachine)
        {
            DoAttack(stateMachine);
            if (!stateMachine.enemy.conditions.canMegaAttack)
            {
                stateMachine.WaitToPatrol();
                if(stateMachine.canPat)
                    return stateMachine.patrolState;
            }
            return this;
        }
        private void DoAttack(GolemStateMachine stateMachine)
        {
            stateMachine.navAgent.isStopped = true;
            stateMachine.navAgent.velocity = Vector3.zero;
            //set anim and dmg stuff
            if (stateMachine.enemy.conditions.canMegaAttack)
            {   
                stateMachine.SetTriggerMegaAttackAnim();
                stateMachine.LookPlayer();
                stateMachine.MegaAttackCD(Random.Range(5f, 10f));
            }
        }
    }
}