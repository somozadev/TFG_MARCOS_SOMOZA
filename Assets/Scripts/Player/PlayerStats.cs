using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

[System.Serializable]
public class PlayerStats : IDamageable
{

    //! TODO MAYBE AN INIT STATS VARIABLE, FOR THE RUN, AND RESET TO THAT WHEN NEW RUN...

    public event Action onPlayerDead;

    [SerializeField] private bool isDead;
    [Space(20)]
    [SerializeField] private int level;
    [SerializeField] private float hp;
    [SerializeField] private int xp;
    [SerializeField] private int currentXp;
    [SerializeField] private float currentHp;

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
    public bool IsDdead { get { return isDead; } set { isDead = value; } }
    public int Level { get { return level; } set { level = value; } }
    public int Xp { get { return xp; } }
    public int CurrentXp { get { return currentXp; } set { currentXp = value; } }
    public float Hp { get { return hp; } }
    public float CurrentHp { get { return currentHp; } set { currentHp = value; } }
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
    public void LevelSpend(int amount)
    {
        level -= amount;
        
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.Save();
        if (GameManager.Instance.statsCanvas != null)
            GameManager.Instance.statsCanvas.XpProgress.fillAmount = 0;
    }
    public void LevelUp()
    {
        level++; this.currentXp = 0; this.xp += (int)Mathf.Pow(level, 2f);
        GameManager.Instance.statsCanvas.XpProgress.fillAmount = 0; GameManager.Instance.player.particles.GetLvlUpParticle();
        GameManager.Instance.soundManager.Play("LvlUp");
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.Save();
    }
    public void AddItem(Item item) => inventory.Add(item);

    public void AddHp(int currentHp)
    {
        this.currentHp += currentHp;
        if (this.currentHp > this.hp)
            this.currentHp = this.hp;
        GameManager.Instance.player.particles.GetHealParticle();

    }
    public void SetCurrentHpToMax() { this.currentHp = hp; GameManager.Instance.player.statsCanvasController.UpdateCanvas(); }
    public void AddMaxHp(int hp) { this.hp += hp; GameManager.Instance.player.statsCanvasController.UpdateCanvas(); }
    public void AddDef(float def) { this.def += def; GameManager.Instance.player.statsCanvasController.UpdateCanvas(); }
    public void AddSpd(float spd) { this.spd += spd; GameManager.Instance.player.statsCanvasController.UpdateCanvas(); }
    public void AddAttr(float atr) { this.attrate += atr; GameManager.Instance.player.statsCanvasController.UpdateCanvas(); }
    public void AddDmg(float dmg) { this.dmg += dmg; GameManager.Instance.player.statsCanvasController.UpdateCanvas(); }

    public void HpRegen(float value)
    {
        GameManager.Instance.player.extraStats.Regen = true;
        GameManager.Instance.StartCoroutine(Regen(value));
    }

    public float timeElapsed = 0;
    public float timeElapsed2 = 0;
    private IEnumerator Regen(float cuantity)
    {
        while (GameManager.Instance.player.extraStats.Regen == true)
        {
            while (timeElapsed <= 10f)
            {
                timeElapsed += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            while (timeElapsed > 10f)
            {
                timeElapsed2 += Time.deltaTime;
                if (timeElapsed2 > 3f)
                {
                    currentHp += cuantity;
                    GameManager.Instance.player.statsCanvasController.UpdateCanvas();
                    timeElapsed2 = 0;
                }
                yield return new WaitForEndOfFrame();
            }
        }

    }

    public void InvOnNewRoom(float time)
    {
        GameManager.Instance.player.extraStats.Inv = true;
        GameManager.Instance.player.extraStats.InvValue = time;
    }
    public void InvOnNewRoom()
    {
        if (GameManager.Instance.player.extraStats.Inv)
            GameManager.Instance.StartCoroutine((InvXSg(GameManager.Instance.player.extraStats.InvValue)));

    }
    private IEnumerator InvXSg(float time)
    {
        GameManager.Instance.player.animator.SetBool("Invincible", true);
        yield return new WaitForSeconds(time);
        GameManager.Instance.player.animator.SetBool("Invincible", false);

    }
    public bool ShouldAddXp(int currentXp) => this.currentXp + currentXp >= this.xp ? true : false;
    public void AddXp(int currentXp) => this.currentXp += currentXp;
    public void AddMaxXp(int xp) => this.xp += xp;

    public void RecieveDamage(float cuantity)
    {
        timeElapsed = 0;
        timeElapsed2 = 0;
        if (GameManager.Instance.player.animator.GetBool("Invincible"))
            return;
        GameManager.Instance.soundManager.Play("PlayerGetHit");
        GameManager.Instance.player.animator.SetBool("Invincible", true);
        GameManager.Instance.player.particles.GetHitParticle();
        GameManager.Instance.mainCamera.GetComponent<CameraShake>().StartShake(GameManager.Instance.mainCamera.GetComponent<CameraShake>().properties);
        this.currentHp -= (int)cuantity;
        if (this.currentHp <= 0)
        {
            this.currentHp = 0;
            Dead();
        }
        GameManager.Instance.statsCanvas.AssignHp();

    }
    public void ResetOnDeadEvent() => onPlayerDead = null;
    bool deadOneShot = false;
    public void Dead()
    {
        if (!deadOneShot)
        {
            GameManager.Instance.dataController.AddAnotherDeath();
            DataController.Instance.newRun = false;
            deadOneShot = true;
        }
        onPlayerDead += OpenDeadUI;
        isDead = true;
        if (onPlayerDead != null)
            onPlayerDead();
    }
    private void OpenDeadUI()
    {
        GameManager.Instance.defaultEventSystem.gameObject.SetActive(true);
        GameManager.Instance.playerEventSystem.gameObject.SetActive(false);
        GameManager.Instance.player.playerMovement.enabled = false;
        GameManager.Instance.onDieCanvas.gameObject.SetActive(true);
        // Time.timeScale = 0;
    }
    #endregion
    public PlayerStats BaseStats()
    {
        PlayerStats playerStats = new PlayerStats(PlayerPrefs.GetInt("level", 0), 100, 0, 100, 50, 2, 5, 1, 1, 10, 10, 0, new List<Item>());
        return playerStats;
    }
}

[System.Serializable]
public class ExtraStats
{
    [SerializeField] private int numberOfShots;
    [SerializeField] private float shotsSize = 0.5f;
    [SerializeField] private bool electricShots;
    [SerializeField] private bool sales;
    [SerializeField] private float dropRate = 0.0f;
    [SerializeField] private bool hpSteal = false;
    [SerializeField] private float hpStealValue = 0f;
    [SerializeField] private bool regen = false;
    [SerializeField] private bool inv = false;
    [SerializeField] private float invValue = 0f;

    public float InvValue { get { return invValue; } set { invValue = value; } }
    public bool Inv { get { return inv; } set { inv = value; } }
    public bool Regen { get { return regen; } set { regen = value; } }
    public bool HpSteal { get { return hpSteal; } set { hpSteal = value; } }
    public float HpStealValue { get { return hpStealValue; } set { hpStealValue = value; } }

    public int NumberOfShots { get { return numberOfShots; } set { numberOfShots = value; } }
    public float ShotsSize { get { return shotsSize; } set { shotsSize = value; } }
    public bool ElectricShots { get { return electricShots; } set { electricShots = value; } }
    public bool Sales { get { return sales; } set { sales = value; } }
    public float DropRate { get { return dropRate; } set { dropRate = value; } }

    public ExtraStats BaseStats()
    {
        ExtraStats extraStats = new ExtraStats(1);
        return extraStats;
    }

    public ExtraStats(int numberOfShots)
    {
        this.numberOfShots = numberOfShots;
        electricShots = false;
        regen = false;
        sales = false;
        hpSteal = false;
        hpStealValue = 0;
        dropRate = 0;
        shotsSize = 0.5f;
    }
}