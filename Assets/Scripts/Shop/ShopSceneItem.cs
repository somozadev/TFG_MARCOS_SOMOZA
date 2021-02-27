using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopSceneItem : MonoBehaviour
{
    private AnimationWoldSpaceCanvas anim;
    [SerializeField] TMP_Text price;
    public TMP_Text Price { get { return price; } set { price = value; } }
    private void Start()
    {
        anim = GetComponentInChildren<AnimationWoldSpaceCanvas>();
        anim.Activate();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("Player"))
        {
            //smth visible
        }
    }



}
