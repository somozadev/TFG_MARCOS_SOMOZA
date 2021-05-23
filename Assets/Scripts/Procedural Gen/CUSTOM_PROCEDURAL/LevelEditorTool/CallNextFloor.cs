using UnityEngine;

public class CallNextFloor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SceneController.Instance.LoadNextFloor();
        }
    }
}
