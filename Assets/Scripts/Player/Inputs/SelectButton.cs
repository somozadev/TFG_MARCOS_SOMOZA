using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    private void Start()
    {
        SelectThisButton();
    }
    private void SelectThisButton() => GetComponent<Button>().Select();
}
