﻿using UnityEngine;

[System.Serializable]
public class EnemyStats
{
    #region VARIABLES
    [SerializeField] private int hp;
    [SerializeField] private float currentHp;

    [SerializeField] private float def;
    [SerializeField] private float dmg;
    [Range(1, 10)] [SerializeField] private float spd;
    [SerializeField] private float attackRange; //being the attackRange
    [SerializeField] private float shootingRange;
    [SerializeField] private float pursuitRange;
    [SerializeField] private float attrate;

    [SerializeField] private int dropXp;
    [SerializeField] private SceneItem dropItem;
    [Tooltip("Ratio value such as 4, which is converted to a 4% chance")]
    [Range(0,100)]
    [SerializeField] private float dropItemRatio;
    #endregion
    #region GETTERS

    public int Hp { get { return hp; } set { hp = value; } }
    public float CurrentHp { get { return currentHp; } set { currentHp = value; } }
    public float Def { get { return def; } set { def = value;}}
    public float Dmg { get { return dmg; } set { dmg = value;}}
    public float Spd { get { return spd; } set { spd = value; } }
    public float AttackRange { get { return attackRange; } set { attackRange = value; } }
    public float PursuitRange { get { return pursuitRange; } set { pursuitRange = value; } }
    public float ShootingRange { get { return shootingRange; } set { shootingRange = value; } }
    public float Attrate { get { return getRandomRate(); } set { attrate = value; } }
    public int DropXp { get { return dropXp; } set { dropXp = value; } }
    public SceneItem DropItem { get { return dropItem; } set { dropItem = value; } }
    public float DropItemRatio { get { return dropItemRatio; } set { dropItemRatio = value; } }

    #endregion
    #region  CONSTRUCTORS
    public EnemyStats(int hp, int currentHp, int def, int dmg, float spd, float range, int dropXp, SceneItem dropItem)
    {
        this.hp = hp;
        this.currentHp = currentHp;
        this.def = def;
        this.dmg = dmg;
        this.spd = spd;
        this.attackRange = range;
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
        this.attackRange = 1.5f;
        this.dropXp = 2;
        this.dropItem = null;

    }
    #endregion

    private float getRandomRate() { return Random.Range(2, 9); }

}
