using System.Collections;
using TMPro;
using UnityEngine;

public class OnDieCanvas : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text seedText;
    [SerializeField] TMP_Text runsText;
    [SerializeField] TMP_Text deathsText;
    [SerializeField] TMP_Text enemiesKilledText;
    [SerializeField] StatsVisualController statsText;
    [SerializeField] CurrentItemsVisual items;


    public void SetTimerInfo()
    {
        float timer = GameManager.Instance.dataController.stupidButCoolStats.runTime;
        string min = ((int)timer / 60).ToString();
        string sg = ((int)(timer % 60)).ToString();
        timerText.text = (min + "' : " + sg + "''");
    }
    public void SetExtraStats()
    {
        runsText.text = "runs: " + GameManager.Instance.dataController.stupidButCoolStats.runs.ToString();
        deathsText.text = "enemies killed: " + GameManager.Instance.dataController.stupidButCoolStats.enemiesKilled.ToString();
        enemiesKilledText.text ="deaths: " + GameManager.Instance.dataController.stupidButCoolStats.deaths.ToString();
    }
    public void SetItems()
    {
        items.InitializeInventory();
    }
    public void SetSeedInfo()
    {
        seedText.text = GameManager.Instance.dataController.seed;
    }
    public void SetStatsInfo()
    {
        statsText.UpdateStats();
    }
    public void OnEnable()
    {
        SetTimerInfo();
        SetStatsInfo();
        SetSeedInfo();
        SetItems();
        SetExtraStats();
        animator.SetTrigger("Open");
        GameManager.Instance.statsCanvas.gameObject.SetActive(false);

    }

    public void Exit()
    {
        SceneController.Instance.LoadAdresseableScene(SceneName.MenuScene, true);//GameManager.Instance.ExitGame();
        GameManager.Instance.player.playerMovement.enabled = true;
        GameManager.Instance.player.playerStats.CurrentHp = 100;
        GameManager.Instance.statsCanvas.AssignHp();

        GameManager.Instance.player.gameObject.SetActive(false);
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
