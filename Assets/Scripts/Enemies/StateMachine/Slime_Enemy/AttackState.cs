using UnityEngine;

namespace StateMachine.Slime_Enemy
{
    public class AttackState : IState
    {

        public IState DoState(SlimeStateMachine stateMachine)
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
        private void DoAttack(StateMachine stateMachine)
        {
            stateMachine.SetPursuitAnim(false);

            if (!stateMachine.enemy.conditions.isAttacking)
            {
                stateMachine.transform.LookAt(GameManager.Instance.player.transform.position);
                if (Random.Range(0, 2) == 0)
                {
                    stateMachine.SetAttack1Anim(true);
                    stateMachine.SetAttack2Anim(false);
                }
                else
                {
                    stateMachine.SetAttack2Anim(true);
                    stateMachine.SetAttack1Anim(false);
                }
                stateMachine.enemy.conditions.isAttacking = true;
            }

            if (Vector3.Distance(stateMachine.navAgent.transform.position, GameManager.Instance.player.transform.position) > stateMachine.navAgent.stoppingDistance)
            {
                stateMachine.enemy.conditions.isRange = false;
                stateMachine.enemy.conditions.isChasing = true;
                stateMachine.enemy.conditions.isAttacking = false;
                stateMachine.SetAttack2Anim(false);
                stateMachine.SetAttack1Anim(false);
            }
        }

    }
}