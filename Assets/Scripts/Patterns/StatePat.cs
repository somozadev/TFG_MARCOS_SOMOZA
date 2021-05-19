using UnityEngine;
public class StatePat : MonoBehaviour
{
    IState stateA = new StateA();
    IState stateB = new StateB();
    Controller controller = new Controller();
    private void Start() 
    {
        controller.SetState(stateA);
        controller.Request();
    }
}

public class Controller 
{
    private IState state; 
    public void SetState(IState state) => this.state = state;
    public IState GetState() => state;
    public void Request() => state.Perform();
}

public interface IState { void Perform(); }
public class StateA : IState { public void Perform(){ Debug.Log("State A perform"); } }
public class StateB : IState { public void Perform(){ Debug.Log("State B perform"); } }



