using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace StateMachine
{
    // [RequireComponent(typeof(NavMeshAgent))]
    public class StateMachine : MonoBehaviour
    {
        public string currentStateName;
        public IState currentState;

        [HideInInspector] public NavMeshAgent navAgent;
        [HideInInspector] public Animator animator;
        [HideInInspector] public Enemy enemy;

        private void Awake()
        {
            enemy = GetComponent<Enemy>();
            if (enemy.agent != null)
                navAgent = enemy.agent;
            if (enemy.animator != null)
                animator = enemy.animator;
        }

        // public virtual void OnEnable() { currentState = pursuitState; }
        public virtual void Update() { OnStateChange(); }

        private void OnStateChange()
        {
            if (currentState != null)
            {
                currentState = currentState.DoState(this);
                currentStateName = currentState.ToString();
            }
        }

        public virtual void WaitCor() { StartCoroutine(wait()); }
        public virtual IEnumerator wait()
        {
            yield return new WaitForSecondsRealtime(0.6f);
            enemy.conditions.isWait = false;
        }

        public virtual void SetPursuitAnim(bool condition) { animator.SetBool("Pursuit", condition); }
        public virtual void SetAttack1Anim(bool condition) { animator.SetBool("Attack1", condition); }
        public virtual void SetAttack2Anim(bool condition) { animator.SetBool("Attack2", condition); }
        public virtual void SetIsDieAnim(bool condition) { animator.SetBool("isDie", condition); }

        public virtual bool GetIsDieAnim() { return animator.GetBool("isDie"); }

        public virtual void SetTriggerDieAnim() { animator.SetTrigger("Die"); }
        public virtual void SetTriggerGetHitAnim() { animator.SetTrigger("GetHit"); }
    }

    public interface IState
    {
        IState DoState(StateMachine stateMachine);
    }

}