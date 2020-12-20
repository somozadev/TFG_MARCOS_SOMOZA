using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsCanvasController : MonoBehaviour
{

    [SerializeField] private TMP_Text soulCoins;
    [SerializeField] private TMP_Text keys;
    [SerializeField] private TMP_Text health;
    [SerializeField] private TMP_Text lvl;
    [SerializeField] private Image xpProgress;

    public Image XpProgress{get {return xpProgress;}}

   private void Start() 
   {
       UpdateCanvas();
   }

    public void UpdateCanvas()
    {
        AssignHp();
        AssignCoins();
        AssignXp();
        // AssignKeys(); 
    }
    public void AssignXp() 
    {
        float value = ((float)GameManager.Instance.player.playerStats.CurrentXp / (float)GameManager.Instance.player.playerStats.Xp);
        StopAllCoroutines();
        StartCoroutine(LerpXp(value));
        lvl.text = GameManager.Instance.player.playerStats.Level.ToString();
    
    }

    IEnumerator LerpXp(float value)
    {   
        float yVel = 0.0f;
        float smoothTime = 0.3f; 

        while(xpProgress.fillAmount < value)
        {
            float aux =  Mathf.SmoothDamp( xpProgress.fillAmount, value, ref yVel, smoothTime);
            xpProgress.fillAmount = aux;
            yield return new WaitForSeconds(0.01f);
        }
    }
    public void AssignCoins() => soulCoins.text = GameManager.Instance.player.playerStats.SoulCoins.ToString("000");
    public void AssignKeys() => keys.text = GameManager.Instance.player.playerStats.Inventory.Find(x => x.Id == 3).Cuantity.ToString("000");
    public void AssignHp() => health.text = GameManager.Instance.player.playerStats.CurrentHp.ToString("000");
    
}
