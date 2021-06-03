using UnityEngine;
using StateMachine.Turtle_Enemy;

public class TurtleCallInvincibleHitEvent : MonoBehaviour
{
    public void EndHit()
    {
        GetComponent<TurtleStateMachine>().SetGetHitInvincibleAnim(false);
    }
}
