using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[SerializeField]
public class Section : MonoBehaviour
{
    public string[] tags;
    public string[] createTags;
    public Exits exits;
    public Bounds bounds;
    public int DeadEndChance;
    protected Level level;
    protected int distance; 


    public void Initialize(Level lvl, int sourceDistance) {
        
        level = lvl;
        transform.SetParent(level.transform);
        level.RegisterNewSection(this);
        distance = sourceDistance + 1;
        GenerateHoles();
        
    }

    protected void GenerateHoles()
    {
        if(createTags.Any())
        {
            foreach(var e in exits.exitSpots)
            {
                if(level.SizeAvailable > 0 && distance < level.maxAllowedDistance)
                {
                    if(RandomService.RollD100(DeadEndChance))
                        PlaceDeadEnd(e);
                    else
                        GenerateSection(e);
                }
                else
                    PlaceDeadEnd(e);
            }
        }
    }

    protected void GenerateSection(Transform exit)
    {
        var candidate = IsAdvancedExit(exit)?BuildSectionFromExit(exit.GetComponent<AdvancedExit>())
        : BuildSectionFromExit(exit);

        if(level.IsSectionValid(candidate.bounds, bounds))
            candidate.Initialize(level,distance);
        else
        {
            Destroy(candidate.gameObject);
            PlaceDeadEnd(exit);
        }
    }

    protected void PlaceDeadEnd(Transform exit) => Instantiate(level.deadEnds.PickOne(),exit).Initialize(level);
    protected bool IsAdvancedExit(Transform exit) => exit.GetComponent<AdvancedExit>() != null;
    protected Section BuildSectionFromExit(Transform exit) => Instantiate(level.PickSectionWithTag(createTags),exit).GetComponent<Section>();
    protected Section BuildSectionFromExit(AdvancedExit exit) => Instantiate(level.PickSectionWithTag(exit.createsTags),exit.transform).GetComponent<Section>();

}
