using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvents : MonoBehaviour
{
    [SerializeField] Animator animator;
    

    public void ResetInvincible()
    {
        animator.SetBool("Invincible", false);
    }
}
