using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsCanvasController : MonoBehaviour
{

    [SerializeField] private TMP_Text soulCoins;
    [SerializeField] private TMP_Text keys;

   
    public void UpdateCanvas()
    {
        AssignCoins();
        AssignKeys();
    }
    
    public void AssignCoins() => soulCoins.text = GameManager.Instance.player.playerStats.SoulCoins.ToString("000");
    public void AssignKeys() => keys.text = GameManager.Instance.player.playerStats.Inventory.Find(x => x.Id == 3).Cuantity.ToString("000");
    
}
