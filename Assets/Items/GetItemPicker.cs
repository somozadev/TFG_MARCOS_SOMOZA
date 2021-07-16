using UnityEngine;

public class GetItemPicker : MonoBehaviour
{
    void Start()
    {
        Instantiate(ItemPicker.RNGuniItemObject,transform.position,transform.rotation, transform.parent);
        Destroy(gameObject);
    }

}
