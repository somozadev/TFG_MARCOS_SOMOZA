﻿using UnityEngine;

[System.Serializable]
public class EnemyStats 
{
    #region VARIABLES
    [SerializeField] private int hp;
    [SerializeField] private int currentHp;

    [SerializeField] private float def;
    [SerializeField] private float dmg;
    [Range(1, 10)] [SerializeField] private float spd;
    [SerializeField] private float range;

    [SerializeField] private int dropXp;
    [SerializeField] private Item dropItem;
    #endregion
    #region GETTERS

    public int Hp { get { return hp; } }
    public int CurrentHp { get { return currentHp; } set { currentHp = value; } }
    public float Def { get { return def; } }
    public float Dmg { get { return dmg; } }
    public float Spd { get { return spd; } }
    public float Range { get { return range; } }
    public int DropXp { get { return dropXp; } set { dropXp = value; } }
    public Item DropItem { get { return dropItem; } set { dropItem = value; } }

    #endregion
    #region  CONSTRUCTORS
    public EnemyStats(int hp, int currentHp, int def, int dmg, float spd, float range, int dropXp, Item dropItem)
    {
        this.hp = hp;
        this.currentHp = currentHp;
        this.def = def;
        this.dmg = dmg;
        this.spd = spd;
        this.range = range;
        this.dropXp = dropXp;
        this.dropItem = dropItem;
    }
    public EnemyStats()
    {
        this.hp = 100;
        this.currentHp = 100;
        this.def = 5;
        this.dmg = 5;
        this.spd = 1;
        this.range = 1.5f;
        this.dropXp = 2;
        this.dropItem = null;

    }
    #endregion

    
}
