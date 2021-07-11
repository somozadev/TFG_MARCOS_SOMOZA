using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UIPop : EventTrigger
{
    [SerializeField] Button button;
    [SerializeField] Toggle toggle;
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] UI_TYPE ui;


    public override void OnPointerEnter(PointerEventData data)
    {
        Hover();
    }
    private void Awake()
    {
        switch (ui)
        {
            case (UI_TYPE.BUTTON):
                {
                    button = GetComponent<Button>();
                    button.onClick.AddListener(Sound);
                    break;
                }
            case (UI_TYPE.TOGGLE):
                {
                    toggle = GetComponent<Toggle>();
                    toggle.onValueChanged.AddListener(delegate { Sound(); });
                    break;
                }
            case (UI_TYPE.DROPDOWN):
                {
                    dropdown = GetComponent<TMP_Dropdown>();
                    dropdown.onValueChanged.AddListener(delegate { Sound(); });
                    break;
                }
        }
    }

    bool playOnceOpen = true;
    bool playOnceClose = true;
    private void Update()
    {
        if (ui.Equals(UI_TYPE.DROPDOWN))
        {
            if (dropdown.IsExpanded && playOnceOpen)
            {
                Sound();
                playOnceOpen = false;
            }
            if (!dropdown.IsExpanded)
                playOnceOpen = true;

            if (!dropdown.IsExpanded && playOnceClose)
            {
                Sound();
                playOnceClose = false;
            }
            if (dropdown.IsExpanded)
                playOnceClose = true;
        }
    }

    public void Sound() => GameManager.Instance.soundManager.Play("Pop");
    public void Hover() => GameManager.Instance.soundManager.Play("Hover");


}

enum UI_TYPE
{
    BUTTON,
    TOGGLE,
    DROPDOWN
}