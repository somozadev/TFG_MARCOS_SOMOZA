using UnityEngine;
using StateMachine.Orc_Enemy;
public class SpinCallEvent : MonoBehaviour
{
    public void EndSpin()
    {
        GetComponent<OrcStateMachine>().WaitToSpin();
        GetComponent<OrcStateMachine>().spinCollider.SetActive(false);
        GetComponent<OrcStateMachine>().enemy.conditions.canSpinAttack = false;
    }
}
