using UnityEngine;
namespace StateMachine.Orc_Enemy
{
    public class AttackState : IState
    {
        public IState DoState(OrcStateMachine stateMachine)
        {
            DoAttack(stateMachine);
            if (stateMachine.enemy.conditions.isAttackRange)
                return stateMachine.attackState;
            else if (stateMachine.enemy.conditions.isHitten)
                return stateMachine.getHitState;
            else if (stateMachine.enemy.conditions.isDead)
                return stateMachine.deathState;
            else if (!stateMachine.enemy.conditions.isPursuitRange)
                return stateMachine.patrolState;
            else
                return stateMachine.pursuitState;
        }

        private void DoAttack(OrcStateMachine stateMachine)
        {
            stateMachine.SetPursuitAnim(false);
            if (!stateMachine.enemy.conditions.isAttacking)
            {

                stateMachine.transform.LookAt(GameManager.Instance.player.transform.position);
                GameManager.Instance.soundManager.Play("OrcAttack");
                stateMachine.SetTriggerAttackAnim();
                stateMachine.enemy.conditions.isAttacking = true;
            }
            if (Vector3.Distance(stateMachine.navAgent.transform.position, GameManager.Instance.player.transform.position) > stateMachine.enemy.stats.AttackRange)
            {
                stateMachine.enemy.conditions.isAttackRange = false;
                stateMachine.enemy.conditions.isChasing = true;
                stateMachine.enemy.conditions.isAttacking = false;
            }

        }

    }
}