using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AnimationWoldSpaceCanvas))]
public class ShopSceneItem : MonoBehaviour
{
    private AnimationWoldSpaceCanvas anim;
    [SerializeField] TMP_Text price;
    public TMP_Text Price  { get {return price;}  set { price = value; } }
    private void Start()
    {
        anim = GetComponent<AnimationWoldSpaceCanvas>();
        anim.Activate();
    }
}
