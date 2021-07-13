using UnityEngine;

public class CallNextFloor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(GetComponentInParent<Room>().transform.GetChild(3).gameObject);
            SceneController.Instance.LoadNextFloor();
        }
    }
}
