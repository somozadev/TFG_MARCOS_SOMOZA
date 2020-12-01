using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerStats
{

    [SerializeField] private int hp;
    [SerializeField] private float dmg;
    [SerializeField] private float spd;
    [SerializeField] private float def;
    [SerializeField] private float attspd;

    [SerializeField] private int soulCoins;

    [SerializeField] private List<Item> inventory; 


    #region Getters
    public int Hp { get { return hp; } }
    public float Dmg { get { return dmg; } }
    public float Spd { get { return spd; } }
    public float Def { get { return def; } }
    public float Attspd { get { return attspd; } }
    public List<Item> Inventory { get { return inventory; } }
    #endregion

    //constructor
    public PlayerStats(int hp, float dmg, float spd, float def, float attspd, int soulCoins, List<Item> inventory)
    {
        this.hp = hp;
        this.dmg = dmg;
        this.spd = spd;
        this.def = def;
        this.attspd = attspd;
        this.soulCoins = soulCoins;
        this.inventory = inventory;
    }


    public void AddItem(Item item) => inventory.Add(item);
    


}
