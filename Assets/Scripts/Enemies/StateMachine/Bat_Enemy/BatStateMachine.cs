using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

namespace StateMachine.Bat_Enemy
{
    public class BatStateMachine : StateMachine
    {
        public Transform shootingPoint;
        public GameObject bulletPrefab;


        public new IState currentState;
        public PursuitState pursuitState = new PursuitState();
        public AttackState attackState = new AttackState();
        public DeathState deathState = new DeathState();
        public GetHitState getHitState = new GetHitState();
        public WaitState waitState = new WaitState();
        public ShootingState shootingState = new ShootingState();

        private void Awake()
        {
            enemy = GetComponent<Enemy>();
            navAgent = enemy.agent;
            animator = enemy.animator;
            bulletPrefab.GetComponent<BatBullet>().dmg = enemy.stats.Dmg * 0.89f;
        }

        public void OnEnable() { currentState = pursuitState; }
        public override void Update()
        {
            OnStateChange();
            transform.LookAt(GameManager.Instance.player.transform);
        }

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

        public void ShootingMonobehaviour()
        {
            StartCoroutine(CounterToIsShootOn(enemy.stats.Attrate));
        }
        private IEnumerator CounterToIsShootOn(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            enemy.conditions.canShoot = true;
        }
        

        public override void SetPursuitAnim(bool condition) { animator.SetBool("Pursuit", condition); }
        public override void SetIsDieAnim(bool condition) { animator.SetBool("isDie", condition); }
        public void SetAttackAnim(bool condition) { animator.SetBool("Attack", condition); }
        public void SetShootAnim(bool condition) { animator.SetBool("Shoot", condition); }

        public override bool GetIsDieAnim() { return animator.GetBool("isDie"); }

        public override void SetTriggerDieAnim() { animator.SetTrigger("Die"); }
        public override void SetTriggerGetHitAnim() { animator.SetTrigger("GetHit"); }
    }
    public interface IState
    {
        IState DoState(BatStateMachine stateMachine);
    }
}