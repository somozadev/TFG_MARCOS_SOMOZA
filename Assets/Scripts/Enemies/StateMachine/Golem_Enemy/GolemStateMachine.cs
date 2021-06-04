using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.Golem_Enemy
{

    public class GolemStateMachine : StateMachine
    {
        public new IState currentState;
        public Transform patrolTarget;
        public ParticleSystem megaAttack;
        public Collider megaAttackCollider;
        public GameObject megaAttackPreVisual;


        public PatrolState patrolState = new PatrolState();
        public DeathState deathState = new DeathState();
        public GetHitState getHitState = new GetHitState();
        public AttackState attackState = new AttackState();



        public void OnEnable() { currentState = patrolState; enemy.conditions.canMegaAttack = true; megaAttackCollider.GetComponent<MegaAttackCollider>().stateMachine = this; }
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

        [SerializeField] private bool megaAttackCorr = false;
        public void MegaAttackCD(float waitTime) => StartCoroutine(MegaAttack(waitTime));
        private IEnumerator MegaAttack(float waitTime)
        {
            if (megaAttackCorr)
                yield return null;
            else
            {
                enemy.conditions.canMegaAttack = false;
                megaAttackCorr = true;
                yield return new WaitForSecondsRealtime(waitTime);
                enemy.conditions.canMegaAttack = true;
                megaAttackCorr = false;
            }
        }
        [HideInInspector] public bool canPat = false;
        private bool canPatCorr = false;
        public void WaitToPatrol() { StartCoroutine(WaitToPatrolCorr()); }
        private IEnumerator WaitToPatrolCorr()
        {
            if (canPatCorr)
                yield return null;
            else
            {
                canPatCorr = true;
                yield return new WaitForSeconds(2f);
                canPat = true;
                canPatCorr = false;
            }

        }

        public void ParticleMegaAttack() => megaAttack.Play();
        

        public override bool GetIsDieAnim() { return animator.GetBool("isDie"); }
        public override void SetIsDieAnim(bool condition) { animator.SetBool("isDie", condition); }
        public override void SetTriggerDieAnim() { animator.SetTrigger("Die"); }
        public override void SetTriggerGetHitAnim() { animator.SetTrigger("GetHit"); }
        public void SetTriggerMegaAttackAnim() { animator.SetTrigger("MegaAttack"); }

        // public void SetIdleAnim(bool condition) { animator.SetBool("isIdle", condition); }
        // public void SetPatrolAnim(bool condition) { animator.SetBool("Patrol", condition); }



    }
    public interface IState
    {
        IState DoState(GolemStateMachine stateMachine);
    }

}

