using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerStats
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

    [SerializeField] private int soulCoins;

    [SerializeField] private List<Item> inventory;


    #region GETTERS
    public int Level { get { return level; } }
    public int Xp { get { return xp; } }
    public int CurrentXp { get { return currentXp; } set { currentXp = value; } }
    public int Hp { get { return hp; } }
    public int CurrentHp { get { return currentHp; } set { currentHp = value; } }
    public float Dmg { get { return dmg; } set { dmg = value; } }
    public float Spd { get { return spd; } set { spd = value; } }
    public float Def { get { return def; } set { def = value; } }
    public float Attspd { get { return attspd; } set { attspd = value; } }
    public float Attrate { get { return attrate; } set { attrate = value; } }
    public int SoulCoins { get { return soulCoins; } set { soulCoins = value; } }
    public List<Item> Inventory { get { return inventory; } }
    #endregion

    #region CONSTRUCTOR
    public PlayerStats(int level, int currentHp, int currentXp, int hp, float dmg, float spd, float def, float attspd,float attrate, int soulCoins, List<Item> inventory)
    {
        this.level = level;
        this.currentHp = currentHp;
        this.currentXp = currentXp;
        this.hp = hp;
        this.dmg = dmg;
        this.spd = spd;
        this.def = def;
        this.attspd = attspd;
        this.attrate = attrate;
        this.soulCoins = soulCoins;
        this.inventory = inventory;
    }
    #endregion

    #region METODOS
    public void LevelUp() { level++; this.currentXp = 0; }
    public void AddItem(Item item) => inventory.Add(item);

    public void AddHp(int currentHp) => this.currentHp += currentHp;
    public void AddMaxHp(int hp) => this.hp += hp;

    public void AddXp(int currentXp) => this.currentXp += currentXp;
    public void AddMaxXp(int xp) => this.xp += xp;
    #endregion



}
