using UnityEngine;
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
    public EnemyHpIndicator hpIndicator;

    private EnemyStats initialStats;

    private void Awake()
    {
        hpIndicator = GetComponentInChildren<EnemyHpIndicator>();
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
            if (GameManager.Instance.player.extraStats.DropRate != 0)
            {
                if (Random.Range(0, 101) <= stats.DropItemRatio + GameManager.Instance.player.extraStats.DropRate)
                    Instantiate(stats.DropItem, transform.position + Vector3.up, Quaternion.identity, GetComponentInParent<Room>().transform.GetChild(3));
            }
            else
            {

                if (Random.Range(0, 101) <= stats.DropItemRatio)
                    Instantiate(stats.DropItem, transform.position + Vector3.up, Quaternion.identity, GetComponentInParent<Room>().transform.GetChild(3));
            }
        }
        DropXp();
        Destroy(gameObject);
    }

    public void DropXp()
    {
        GameManager.Instance.xpController.PerformXps(stats.DropXp, transform);
    }



    public void ParticleDead() => deadParticle.Play();
    public void ParticleDamaged() => damagedParticle.Play();
    public void SetNewDamageIndicator() => Instantiate(damageIndicatorPrefab, transform.position + Vector3.up * 3f, Quaternion.identity, transform).GetComponent<DamageIndicator>().SetDamageText(cuantity);
    public void SetNewInvencibleDamageIndicator() => Instantiate(damageIndicatorPrefab, transform.position + Vector3.up * 3f, Quaternion.identity, transform).GetComponent<DamageIndicator>().SetDamageText(0);


    public void MakeDamage() { GameManager.Instance.player.playerStats.RecieveDamage(stats.Dmg); }
    public void RecieveDamage(float cuantity)
    {
        this.cuantity = (cuantity - (stats.Def / 100));

        if (this.stats.CurrentHp - (int)cuantity <= 0)
        {
            this.stats.CurrentHp = 0;
            conditions.isDead = true;
        }
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
    public bool isInvincible;
    public bool canMegaAttack;

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
        isInvincible = false;
        canMegaAttack = false;
    }
}
