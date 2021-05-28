using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

namespace StateMachine.Pursuit_Basic_Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private string currentStateName;
        private IState currentState;

        [HideInInspector] public NavMeshAgent navAgent;
        [HideInInspector] public Animator animator;
        [HideInInspector] public Enemy enemy;


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

        private void OnEnable() { currentState = pursuitState; }
        private void Update() { OnStateChange(); }

        private void OnStateChange()
        {
            currentState = currentState.DoState(this);
            currentStateName = currentState.ToString();
        }

        public void WaitCor()
        {
            StartCoroutine(wait());
        }
        private IEnumerator wait()
        {
            yield return new WaitForSecondsRealtime(0.6f);
            enemy.conditions.isWait = false;
        }
    }

    public interface IState
    {
        IState DoState(StateMachine stateMachine);
    }
}