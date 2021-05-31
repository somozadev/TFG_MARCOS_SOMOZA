using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.Dragon_Enemy
{
    public class DragonStateMachine : StateMachine
    {
        public Transform shootingPoint;
        public GameObject bulletPrefab;
        public Collider childrenCollider;

        public new IState currentState;
        public PursuitState pursuitState = new PursuitState();
        public DeathState deathState = new DeathState();
        public GetHitState getHitState = new GetHitState();
        public ShootingState shootingState = new ShootingState();



        private void Awake()
        {
            enemy = GetComponent<Enemy>();
            navAgent = enemy.agent;
            animator = enemy.animator;
            bulletPrefab.GetComponent<EnemyBullet>().dmg = enemy.stats.Dmg * 0.89f;
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
        public void ShootingMonobehaviour()
        {
            StartCoroutine(CounterToIsShootOn(enemy.stats.Attrate));
        }
        private IEnumerator CounterToIsShootOn(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            enemy.conditions.canShoot = true;
        }


        public override void SetIsDieAnim(bool condition) { animator.SetBool("isDie", condition); }
        public void SetShootAnim(bool condition) { animator.SetBool("Shoot", condition); }
        public override bool GetIsDieAnim() { return animator.GetBool("isDie"); }
        public override void SetTriggerDieAnim() { animator.SetTrigger("Die"); }
        public override void SetTriggerGetHitAnim() { animator.SetTrigger("GetHit"); }

    }
    public interface IState
    {
        IState DoState(DragonStateMachine stateMachine);
    }
}