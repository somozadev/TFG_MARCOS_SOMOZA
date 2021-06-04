using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.Turtle_Enemy
{

    public class TurtleStateMachine : StateMachine
    {
        public new IState currentState;

        [SerializeField] GameObject PointsObj;
        public PatrolPoint[] points;
        public PatrolPoint currentPoint;


        public IdleState idleState = new IdleState();
        public PatrolState patrolState = new PatrolState();
        public DeathState deathState = new DeathState();
        public InvincibleState invincibleState = new InvincibleState();
        public GetHitState getHitState = new GetHitState();
        // public WaitState waitState = new WaitState();



        public void OnEnable() { currentState = idleState; points = PointsObj.GetComponentsInChildren<PatrolPoint>(); }
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
        public void RestartCdInvincible() => isCdInvCorr = false;
        [SerializeField] bool isCdInvCorr = false;
        public void StartCdInvincible(float waitTime) { StartCoroutine(CdInvCorr(waitTime)); }
        private IEnumerator CdInvCorr(float waitTime)
        {
            if (isCdInvCorr)
                yield return null;
            else
            {
                isCdInvCorr = true;
                yield return new WaitForSecondsRealtime(waitTime);
                enemy.conditions.isInvincible = false;
                enemy.conditions.isIdle = true;

            }
        }
        bool canDamage = true;
        private void OnTriggerEnter(Collider other)
        {
            if (canDamage)
            {
                if (other.gameObject.tag == "Player" && currentState == patrolState)
                {
                    GameManager.Instance.player.playerStats.RecieveDamage(enemy.stats.Dmg);
                    canDamage = false;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player" && currentState == patrolState)
            {
                canDamage = true;
            }
        }

        [SerializeField] bool isPatCorr = false;
        public void WaitToPatrol(float waitTime) { StartCoroutine(waitPatCorr(waitTime)); }
        private IEnumerator waitPatCorr(float waitTime)
        {
            if (isPatCorr)
                yield return null;

            else
            {
                isPatCorr = true;
                // enemy.conditions.isPatrol = false;
                yield return new WaitForSecondsRealtime(waitTime);
                enemy.conditions.isPatrol = true;
                enemy.conditions.isIdle = false;
                isPatCorr = false;
            }
        }



        public override bool GetIsDieAnim() { return animator.GetBool("isDie"); }
        public override void SetIsDieAnim(bool condition) { animator.SetBool("isDie", condition); }
        public override void SetTriggerDieAnim() { animator.SetTrigger("Die"); }
        public override void SetTriggerGetHitAnim() { animator.SetTrigger("GetHit"); }

        public void SetGetHitInvincibleAnim(bool condition) { animator.SetBool("GetHitInvulnerable", condition); }
        public void SetIdleAnim(bool condition) { animator.SetBool("isIdle", condition); }
        public void SetPatrolAnim(bool condition) { animator.SetBool("Patrol", condition); }



    }
    public interface IState
    {
        IState DoState(TurtleStateMachine stateMachine);
    }


}

