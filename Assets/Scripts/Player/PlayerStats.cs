using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerStats : IDamageable
{

    [SerializeField] private int level;
    [SerializeField] private int hp;
    [SerializeField] private int xp;
    [SerializeField] private int currentXp;
    [SerializeField] private int currentHp;

    [SerializeField] private float def;
    [SerializeField] private float dmg;
    [Range(1, 10)]
    [SerializeField] private float spd;

    [SerializeField] private float attspd;
    [SerializeField] private float attrate;
    [SerializeField] private float range;

    [SerializeField] private int soulCoins;

    [SerializeField] private List<Item> inventory;


    #region GETTERS
    public int Level { get { return level; } }
    public int Xp { get { return xp; } }
    public int CurrentXp { get { return currentXp; } set { currentXp = value; } }
    public int Hp { get { return hp; } }
    public int CurrentHp { get { return currentHp; } set { currentHp = value; } }
    public float Dmg { get { return dmg; } set { dmg = value; GameManager.Instance.player.statsCanvasController.UpdateCanvas(); } }
    public float Spd { get { return spd; } set { spd = value; GameManager.Instance.player.statsCanvasController.UpdateCanvas(); } }
    public float Def { get { return def; } set { def = value; GameManager.Instance.player.statsCanvasController.UpdateCanvas(); } }
    public float Attspd { get { return attspd; } set { attspd = value; GameManager.Instance.player.statsCanvasController.UpdateCanvas(); } }
    public float Attrate { get { return attrate; } set { attrate = value; GameManager.Instance.player.statsCanvasController.UpdateCanvas(); } }
    public float Range { get { return range; } set { range = value; GameManager.Instance.player.statsCanvasController.UpdateCanvas(); } }
    public int SoulCoins { get { return soulCoins; } set { soulCoins = value; GameManager.Instance.player.particles.GetCoinParticle(); } }
    public List<Item> Inventory { get { return inventory; } }
    #endregion

    #region CONSTRUCTOR
    public PlayerStats(int level, int currentHp, int currentXp, int hp, int xp, float dmg, float spd, float def, float attspd, float attrate, float range, int soulCoins, List<Item> inventory)
    {
        this.level = level;
        this.xp = xp;
        this.currentHp = currentHp;
        this.currentXp = currentXp;
        this.hp = hp;
        this.dmg = dmg;
        this.spd = spd;
        this.def = def;
        this.attspd = attspd;
        this.attrate = attrate;
        this.range = range;
        this.soulCoins = soulCoins;
        this.inventory = inventory;
    }
    #endregion

    #region METODOS
    public void LevelUp() { level++; this.currentXp = 0; this.xp += (int)Mathf.Pow(level, 2f); GameManager.Instance.statsCanvas.XpProgress.fillAmount = 0; GameManager.Instance.player.particles.GetLvlUpParticle(); }
    public void AddItem(Item item) => inventory.Add(item);

    public void AddHp(int currentHp)
    {
        this.currentHp += currentHp;
        if (this.currentHp > this.hp)
            this.currentHp = this.hp;
        GameManager.Instance.player.particles.GetHealParticle();

    }
    public void AddMaxHp(int hp) => this.hp += hp;

    public bool ShouldAddXp(int currentXp) => this.currentXp + currentXp >= this.xp ? true : false;
    public void AddXp(int currentXp) => this.currentXp += currentXp;
    public void AddMaxXp(int xp) => this.xp += xp;
    public void AddDmg(float dmg) { this.dmg += dmg; GameManager.Instance.player.statsCanvasController.UpdateCanvas(); }

    public void RecieveDamage(float cuantity)
    {
        if(GameManager.Instance.player.animator.GetBool("Invincible"))
            return;
        GameManager.Instance.player.animator.SetBool("Invincible",true);
        GameManager.Instance.player.particles.GetHitParticle();
        GameManager.Instance.mainCamera.GetComponent<CameraShake>().StartShake(GameManager.Instance.mainCamera.GetComponent<CameraShake>().properties);
        this.currentHp -= (int)cuantity;
        GameManager.Instance.statsCanvas.AssignHp();
        if (this.currentHp <= 0)
        {
            Die();
        }
    }
    public void Die() => Debug.Log("Dead");
    #endregion

}

[System.Serializable]
public class ExtraStats
{
    [SerializeField] private int numberOfShots;



    public int NumberOfShots { get { return numberOfShots; } set { numberOfShots = value; } }


    public ExtraStats(int numberOfShots)
    {
        this.numberOfShots = numberOfShots;
    }
}