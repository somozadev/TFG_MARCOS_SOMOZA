using System.Collections;
using UnityEngine;

namespace StateMachine.Orc_Enemy
{
    public class OrcStateMachine : StateMachine
    {

        public new IState currentState;
        public Transform patrolTarget;
        public GameObject spinCollider;

        public IdleState idleState = new IdleState();
        public AttackState attackState = new AttackState();
        public PursuitState pursuitState = new PursuitState();
        public DeathState deathState = new DeathState();
        public GetHitState getHitState = new GetHitState();
        public PatrolState patrolState = new PatrolState();
        public WaitState waitState = new WaitState();
        public SpinState spinState = new SpinState();


        private void Awake()
        {
            enemy = GetComponent<Enemy>();
            navAgent = enemy.agent;
            animator = enemy.animator;
            spinCollider.GetComponent<SpinColliderDamager>().DmgCuantity = enemy.stats.Dmg * 0.45f;
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


        private bool isCorrRunning = false;
        public IEnumerator IdleToPatrolWait(float time)
        {
            if (isCorrRunning)
                yield return null;
            else
            {
                isCorrRunning = true;
                yield return new WaitForSeconds(time);
                enemy.conditions.isPatrol = true;
            }
        }

        public bool isSpinCorrRunning = false;
        public void WaitToSpin() { StartCoroutine(SpinCd()); }
        private IEnumerator SpinCd()
        {
            if (isSpinCorrRunning)
                yield return null;
            else
            {
                enemy.conditions.isPatrol = true;
                float waitTime = Random.Range(15, 25);
                Debug.Log("waitTime:" + waitTime);
                isSpinCorrRunning = true;
                yield return new WaitForSeconds(waitTime);
                isSpinCorrRunning = false;
            }

        }

        public void SetPatrolAnim(bool condition) { animator.SetBool("Patrol", condition); }
        public override void SetIsDieAnim(bool condition) { animator.SetBool("isDie", condition); }
        public void SetShootAnim(bool condition) { animator.SetBool("Shoot", condition); }
        public override bool GetIsDieAnim() { return animator.GetBool("isDie"); }
        public override void SetTriggerDieAnim() { animator.SetTrigger("Die"); }
        public override void SetTriggerGetHitAnim() { animator.SetTrigger("GetHit"); }
        public void SetTriggerAttackAnim() { animator.SetTrigger("Attack"); }
        public void SetTriggerSpinAnim() { animator.SetTrigger("Spin"); }

        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.cyan;
        //     Gizmos.DrawWireSphere(transform.position, enemy.stats.PursuitRange);
        // }


    }
    public interface IState
    {
        IState DoState(OrcStateMachine stateMachine);
    }

}