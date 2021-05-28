using UnityEngine.AI;
using UnityEngine;
using System.Collections;

namespace StateMachine.Pursuit_Basic_Enemy
{
    public class AttackState : IState
    {

        public IState DoState(StateMachine stateMachine)
        {
            DoAttack(stateMachine);
            if (stateMachine.enemy.conditions.isRange)
                return stateMachine.attackState;
            else if(stateMachine.enemy.conditions.isHitten)
                return stateMachine.getHitState;
            else if(stateMachine.enemy.conditions.isDead)
                return stateMachine.deathState;
            else
                return stateMachine.pursuitState;
        }
        private void DoAttack(StateMachine stateMachine)
        {
            stateMachine.animator.SetBool("Pursuit", false);

            if (!stateMachine.enemy.conditions.isAttacking)
            {
                stateMachine.transform.LookAt(GameManager.Instance.player.transform.position);
                if (Random.Range(0, 2) == 0)
                {
                    stateMachine.animator.SetBool("Attack2", false);
                    stateMachine.animator.SetBool("Attack1", true);
                }
                else
                {
                    stateMachine.animator.SetBool("Attack1", false);
                    stateMachine.animator.SetBool("Attack2", true);
                }
                stateMachine.enemy.conditions.isAttacking = true;
            }

            if (Vector3.Distance(stateMachine.navAgent.transform.position, GameManager.Instance.player.transform.position) > stateMachine.navAgent.stoppingDistance)
            {
                stateMachine.enemy.conditions.isRange = false;
                stateMachine.enemy.conditions.isChasing = true;
                stateMachine.enemy.conditions.isAttacking = false;
                stateMachine.animator.SetBool("Attack1", false);
                stateMachine.animator.SetBool("Attack2", false);
            }
        }

    }
}