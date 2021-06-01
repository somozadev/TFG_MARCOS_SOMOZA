using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinColliderDamager : MonoBehaviour
{
    public float DmgCuantity;

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag=="Player")
        {
            GameManager.Instance.player.playerStats.RecieveDamage(DmgCuantity);
        }
    }
}
