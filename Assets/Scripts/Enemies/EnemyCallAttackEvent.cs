using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCallAttackEvent : MonoBehaviour
{
    public void DoDamage()
    {
        GetComponent<Enemy>().MakeDamage();
    }
}
