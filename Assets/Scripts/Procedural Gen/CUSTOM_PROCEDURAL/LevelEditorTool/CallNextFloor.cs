using UnityEngine;

public class CallNextFloor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(GetComponentInParent<Room>().transform.GetChild(3).gameObject);
            SceneController.Instance.LoadNextFloor();
            GameManager.Instance.player.playerMovement.canAttack = true;
            GameManager.Instance.player.playerMovement.IsInteracting = false;
        }
    }
}
