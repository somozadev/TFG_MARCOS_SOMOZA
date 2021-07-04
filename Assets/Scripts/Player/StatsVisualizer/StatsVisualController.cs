using UnityEngine;

public class StatsVisualController : MonoBehaviour
{
    public StatVisual[] stats;
    private void Start()
    {
        UpdateStats();
    }
    public void UpdateStats()
    {
        foreach (StatVisual stat in stats)
            stat.FillStat();
    }
}
