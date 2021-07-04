using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class StatVisual : MonoBehaviour
{
    [Header("Modifiers")]
    public Color32 color;
    public Sprite sprite;
    public StatType statType;
    [Header("Refs")]
    public TMP_Text text;
    public TMP_Text backgroundText;
    public Image image;

    public void FillStat()
    {
        text.color = color;
        image.sprite = sprite;

        switch (statType)
        {
            case StatType.DMG:
                text.text = GameManager.Instance.player.playerStats.Dmg.ToString();
                break;
            case StatType.DEF:
                text.text = GameManager.Instance.player.playerStats.Def.ToString();
                break;
            case StatType.SPD:
                text.text = GameManager.Instance.player.playerStats.Spd.ToString();
                break;
            case StatType.ATTSPD:
                text.text = GameManager.Instance.player.playerStats.Attspd.ToString();
                break;
            case StatType.ATTRATE:
                text.text = GameManager.Instance.player.playerStats.Attrate.ToString();
                break;
            case StatType.RANGE:
                text.text = GameManager.Instance.player.playerStats.Range.ToString();
                break;
        }

        backgroundText.text = text.text;
    }
}
[System.Serializable]
public enum StatType
{
    DMG,
    DEF,
    SPD,
    ATTSPD,
    ATTRATE,
    RANGE
}