using UnityEngine.AI;
using UnityEngine;
using System.Collections;

namespace StateMachine.Bat_Enemy
{
    public class AttackState : IState
    {

        public IState DoState(BatStateMachine stateMachine)
        {
            DoAttack(stateMachine);
            if (stateMachine.enemy.conditions.isRange)
                return stateMachine.attackState;
            else if (stateMachine.enemy.conditions.isHitten)
                return stateMachine.getHitState;
            else if (stateMachine.enemy.conditions.isDead)
                return stateMachine.deathState;
            else
                return stateMachine.pursuitState;
        }
        private void DoAttack(BatStateMachine stateMachine)
        {
            stateMachine.SetPursuitAnim(false);

            if (!stateMachine.enemy.conditions.isAttacking)// && stateMachine.enemy.conditions.isChasing)
            {
                stateMachine.transform.LookAt(GameManager.Instance.player.transform.position);

                stateMachine.SetAttackAnim(true);

                stateMachine.enemy.conditions.isAttacking = true;
            }
            if (Vector3.Distance(stateMachine.transform.position, GameManager.Instance.player.transform.position) > stateMachine.navAgent.stoppingDistance)
            {
                stateMachine.enemy.conditions.isRange = false;
                stateMachine.enemy.conditions.isChasing = true;
                stateMachine.enemy.conditions.isAttacking = false;
                stateMachine.SetAttackAnim(false);
            }
        }

    }
}