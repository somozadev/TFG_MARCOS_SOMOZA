using System.Collections;
using TMPro;
using UnityEngine;

public class ShopSlot : MonoBehaviour
{
    public GameObject itemToSell;
    [SerializeField] float speed;
    [SerializeField] bool selected;
    [SerializeField] private TMP_Text text;


    private void Start()
    {   
        text = GetComponentInChildren<TMP_Text>();
        speed = Random.Range(7, 16);
        selected = false;
    }


    public void RotateStuff()
    {
        RotateObject();
    }

    public void SelectedItem()
    {
        selected = true;
        text.color = Color.yellow;
    }
    public void CantAfford() => text.color = Color.red;
    public void DeselectedItem()
    {
        selected = false;
        text.color = Color.white;
    }

    private void RotateObject()
    {
        StartCoroutine(RotateIt());
    }
    

    IEnumerator RotateIt()
    {
        while (true)
        {
            itemToSell.transform.Rotate(Vector3.up * speed * Time.deltaTime, Space.World);
            yield return new WaitForEndOfFrame();
        }
    }

}
