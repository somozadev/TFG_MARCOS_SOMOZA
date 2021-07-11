using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Golem_Enemy;

public class MegaAttackCallEvent : MonoBehaviour
{
    public void PerfEndMegaAttack()
    {
        GolemStateMachine stateMachine = GetComponent<GolemStateMachine>();
        stateMachine.megaAttackCollider.enabled = false;
        stateMachine.megaAttackCollider.GetComponent<MegaAttackCollider>().hasHitten = false;
    }
    public void PerfMegaAttack()
    {
        GameManager.Instance.soundManager.Play("GolemAttack2");
        GolemStateMachine stateMachine = GetComponent<GolemStateMachine>();
        stateMachine.megaAttackPreVisual.SetActive(false);
        stateMachine.megaAttackCollider.enabled = true;
        stateMachine.ParticleMegaAttack();
    }
    public void PerfStartMegaAttack()
    {
        GameManager.Instance.soundManager.Play("GolemAttack1");
        GolemStateMachine stateMachine = GetComponent<GolemStateMachine>();
        stateMachine.megaAttackPreVisual.SetActive(true);

    }
}
