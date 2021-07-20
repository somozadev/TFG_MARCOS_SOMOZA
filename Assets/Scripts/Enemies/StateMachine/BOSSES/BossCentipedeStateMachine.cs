using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.BossCentipede
{
    public class BossCentipedeStateMachine : StateMachine
    {
        public CentipedeController movement;
        public new IState currentState;
        public FirstStageState firstStageState = new FirstStageState();

        public void OnEnable() { currentState = firstStageState; }
        public override void Update() { OnStateChange(); }

        private void OnStateChange()
        {
            currentState = currentState.DoState(this);
            currentStateName = currentState.ToString();
        }

    }
    public interface IState
    {
        IState DoState(BossCentipedeStateMachine stateMachine);
    }
}
