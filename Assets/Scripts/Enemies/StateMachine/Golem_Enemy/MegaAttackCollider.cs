using UnityEngine;
using StateMachine.Golem_Enemy;

public class MegaAttackCollider : MonoBehaviour
{
    [HideInInspector] public GolemStateMachine stateMachine;
    public bool hasHitten = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!hasHitten)
        {
            if (other.gameObject.tag == "Player")
            {
                hasHitten = true;
                GameManager.Instance.player.playerMovement.Rb.AddForce(Vector3.up * 5f,ForceMode.Impulse);
                // GameManager.Instance.player.playerMovement.Rb.AddForce(Vector3.down*-25f,ForceMode.Impulse);
                GameManager.Instance.player.playerStats.RecieveDamage(stateMachine.enemy.stats.Dmg * 1.45f);
            }

        }
    }
}
