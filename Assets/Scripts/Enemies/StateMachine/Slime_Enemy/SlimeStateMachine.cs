using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
namespace StateMachine.Slime_Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class SlimeStateMachine : StateMachine
    {
        public new IState currentState;
        public PursuitState pursuitState = new PursuitState();
        public AttackState attackState = new AttackState();
        public DeathState deathState = new DeathState();
        public GetHitState getHitState = new GetHitState();
        public WaitState waitState = new WaitState();

        private void Awake()
        {
            enemy = GetComponent<Enemy>();
            navAgent = enemy.agent;
            animator = enemy.animator;
        }

        public void OnEnable() { currentState = pursuitState; }
        public override void Update() { OnStateChange(); }

        private void OnStateChange()
        {
            currentState = currentState.DoState(this);
            currentStateName = currentState.ToString();
        }

        public override void WaitCor() { StartCoroutine(wait()); }
        public override IEnumerator wait()
        {
            yield return new WaitForSecondsRealtime(0.6f);
            enemy.conditions.isWait = false;
        }

        public override void SetPursuitAnim(bool condition) { animator.SetBool("Pursuit", condition); }
        public override void SetAttack1Anim(bool condition) { animator.SetBool("Attack1", condition); }
        public override void SetAttack2Anim(bool condition) { animator.SetBool("Attack2", condition); }
        public override void SetIsDieAnim(bool condition) { animator.SetBool("isDie", condition); }

        public override bool GetIsDieAnim() { return animator.GetBool("isDie"); }

        public override void SetTriggerDieAnim() { animator.SetTrigger("Die"); }
        public override void SetTriggerGetHitAnim() { animator.SetTrigger("GetHit"); }
    }

    public interface IState
    {
        IState DoState(SlimeStateMachine stateMachine);
    }
}