using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpIndicator : MonoBehaviour
{
    [SerializeField] Image hp;
    [SerializeField] Enemy enemy;


    public void UpdateHp()
    {
        hp.fillAmount = enemy.stats.CurrentHp / enemy.stats.Hp;
    }


}
