using UnityEngine;

namespace StateMachine.Slime_Enemy
{
    public class DeathState : IState
    {
        public IState DoState(SlimeStateMachine stateMachine)
        {
            if (!stateMachine.GetIsDieAnim())
                DoDeathState(stateMachine);
            return this;
        }

        private void DoDeathState(StateMachine stateMachine)
        {
            stateMachine.navAgent.isStopped = true;
            stateMachine.navAgent.velocity = Vector3.zero;
            stateMachine.enemy.SetNewDamageIndicator();
            stateMachine.enemy.ParticleDamaged();
            stateMachine.SetTriggerDieAnim();
            stateMachine.SetIsDieAnim(true);
            stateMachine.enemy.conditions.isAttacking = false;
            stateMachine.enemy.conditions.isChasing = false;
            stateMachine.enemy.conditions.isAttackRange = false;
            stateMachine.enemy.conditions.isHitten = false;
            stateMachine.enemy.conditions.isDead = true;
        }

    }
}