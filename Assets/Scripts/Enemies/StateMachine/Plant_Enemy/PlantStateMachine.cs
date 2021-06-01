using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.Plant_Enemy
{
    public class PlantStateMachine : StateMachine
    {
        public new IState currentState;

        public IdleState idleState = new IdleState();
        // public AttackState attackState = new AttackState();
        public ShootRangeState shootRangeState = new ShootRangeState();
        // public DeathState deathState = new DeathState();
        // public GetHitState getHitState = new GetHitState();
        // public WaitState waitState = new WaitState();


        private void Awake()
        {
            enemy = GetComponent<Enemy>();
            navAgent = enemy.agent;
            animator = enemy.animator;
        }

        public void OnEnable() { currentState = idleState; }
        public override void Update()
        {
            OnStateChange();
        }

        public void LookPlayer()
        {
            transform.LookAt(GameManager.Instance.player.transform);
        }
        private void OnStateChange()
        {
            currentState = currentState.DoState(this);
            currentStateName = currentState.ToString();
        }




        public override bool GetIsDieAnim() { return animator.GetBool("isDie"); }

        public override void SetIsDieAnim(bool condition) { animator.SetBool("isDie", condition); }
        public override void SetTriggerDieAnim() { animator.SetTrigger("Die"); }
        public void SetTriggerShootAnim() { animator.SetTrigger("Shoot"); }
        public override void SetTriggerGetHitAnim() { animator.SetTrigger("GetHit"); }
        public void SetInRangeToShootAnim(bool condition) { animator.SetBool("isRangeToShoot", condition); }
        public void SetIdleAnim(bool condition) { animator.SetBool("isIdle", condition); }




    }
    public interface IState
    {
        IState DoState(PlantStateMachine stateMachine);
    }

}