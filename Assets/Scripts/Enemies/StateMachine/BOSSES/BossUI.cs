using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossUI : MonoBehaviour
{
    [SerializeField] TMP_Text boosName;
    [SerializeField] Image leftSide;
    [SerializeField] Image rightSide;

    float value = ((float)GameManager.Instance.player.playerStats.CurrentXp / (float)GameManager.Instance.player.playerStats.Xp);

    public void Init(string bossName)
    {
        this.boosName.text = bossName; 
        leftSide.fillAmount = 1;
        rightSide.fillAmount = 1;
    }
    public void UpdateHp(float value)
    {
        leftSide.fillAmount = value;
        rightSide.fillAmount = value;
    }

}
