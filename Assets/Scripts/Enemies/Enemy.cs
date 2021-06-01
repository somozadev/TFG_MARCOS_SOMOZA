﻿using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;
public class Enemy : MonoBehaviour, IDamageable, IDamager
{
    public EnemyStats stats;
    public ConditionsState conditions;
    public NavMeshAgent agent;
    public Animator animator;
    public ParticleSystem damagedParticle;
    public VisualEffect deadParticle;
    [SerializeField] GameObject damageIndicatorPrefab;
    [HideInInspector] public float cuantity;

    private EnemyStats initialStats;

    private void Awake()
    {
        initialStats = stats;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = stats.Spd;
        agent.stoppingDistance = stats.AttackRange;

    }


    public void Reset()
    {
        stats = initialStats;
        transform.GetChild(1).gameObject.SetActive(true);
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("isDie", false);
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("GetHit");
        animator.ResetTrigger("Die");
        conditions.Reset();
    }
    public void Drop()
    {
        if (stats.DropItem != null)
        {
            Instantiate(stats.DropItem, transform.position + Vector3.up, Quaternion.identity);
        }
    }
    public void ParticleDead() => deadParticle.Play();
    public void ParticleDamaged() => damagedParticle.Play();
    public void SetNewDamageIndicator() => Instantiate(damageIndicatorPrefab, transform.position + Vector3.up*3f, Quaternion.identity, transform).GetComponent<DamageIndicator>().SetDamageText(cuantity);


    public void MakeDamage() { GameManager.Instance.player.playerStats.RecieveDamage(stats.Dmg); }
    public void RecieveDamage(float cuantity)
    {
        this.cuantity = (cuantity - (stats.Def / 100));
        if (this.stats.CurrentHp - (int)cuantity <= 0)
            conditions.isDead = true;
        else
        {
            conditions.isHitten = true;
            conditions.isChasing = false;
            conditions.isAttackRange = false;
        }

    }

}

[System.Serializable]
public class ConditionsState
{
    public bool isChasing;
    public bool isAttacking;
    public bool isHitten;
    public bool isDead;
    public bool isWait;
    public bool isIdle;
    public bool canShoot;
    public bool isShootingRange;
    public bool isAttackRange;
    public bool isPursuitRange;
    public bool isPatrol;
    public bool canSpinAttack;

    public void Reset()
    {
        isChasing = false;
        isAttackRange = false;
        isPursuitRange = false;
        isShootingRange = false; 
        isAttacking = false;
        isHitten = false;
        isDead = false;
        isWait = false;
        isPatrol = false; 
        canShoot = false;
        canSpinAttack = false;
        isIdle = false;
    }
}
