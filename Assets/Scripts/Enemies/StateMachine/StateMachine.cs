using UnityEngine;
using UnityEngine.AI;

namespace StateMachine
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private string currentStateName;
        private IState currentState;
        public NavMeshAgent navAgent;
        public bool isChasing, isRange;


        public float detectionRange;



        public IdleState idleState = new IdleState();
        public PursuitState pursuitState = new PursuitState();
        public AttackState attackState = new AttackState();


        private void OnEnable()
        {
            currentState = idleState;
        }

        private void Update()
        {
            OnStateChange();
        }

        private void OnStateChange()
        {
            currentState = currentState.DoState(this);
            currentStateName = currentState.ToString();
        }
    }
}