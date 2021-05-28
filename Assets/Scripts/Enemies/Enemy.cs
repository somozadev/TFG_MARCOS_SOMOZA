using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable, IDamager
{
    public EnemyStats stats;
    public ConditionsState conditions;
    public NavMeshAgent agent;
    public Animator animator;
    public ParticleSystem damagedParticle;
    [SerializeField] GameObject damageIndicatorPrefab;
    [HideInInspector] public float cuantity;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = stats.Spd;
        agent.stoppingDistance = stats.Range;

    }

    public void ParticleDamaged() => damagedParticle.Play();
    public void SetNewDamageIndicator() => Instantiate(damageIndicatorPrefab, transform.position, Quaternion.identity, transform).GetComponent<DamageIndicator>().SetDamageText(cuantity);


    public void MakeDamage() { GameManager.Instance.player.playerStats.RecieveDamage(stats.Dmg); }
    public void RecieveDamage(float cuantity)
    {
        this.cuantity = (cuantity - (stats.Def/100));
        if (this.stats.CurrentHp - (int)cuantity <= 0)
            conditions.isDead = true;
        else
        {
            conditions.isHitten = true;
            conditions.isChasing = false;
            conditions.isRange = false;
        }

    }

}

[System.Serializable]
public class ConditionsState
{
    public bool isChasing;
    public bool isRange;
    public bool isAttacking;
    public bool isHitten;
    public bool isDead;
    public bool isWait;
}
