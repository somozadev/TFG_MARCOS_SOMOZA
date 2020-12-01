using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Level : MonoBehaviour
{
    
    public int seed;
    public int maxAllowedDistance;
    public int maxLevelSize;
    public Section[] sections;
    public string[] initialSectionTags;
    public TagRule[] tagRules;
    public DeadEnd[] deadEnds;

    protected List<Section> registeredSections = new List<Section>();
    protected IEnumerable<Collider> registeredColliders => registeredSections.SelectMany(s => s.bounds.Colliders.Union(deadEndsCollider));
    private List<Collider> deadEndsCollider = new List<Collider>();
    public int SizeAvailable {get; private set;}
    protected bool HalfLevelBuilt => registeredSections.Count > SizeAvailable;
    
    void Start()
    {
        if (seed != 0)
                RandomService.SetSeed(seed);
        else
            seed = RandomService.seed;

        SizeAvailable = maxLevelSize;
        CreateInitialSection();
        DeactivateBounds();
    }
    protected void CheckRuleIntegrity()
    {
        foreach(var ruleTag in tagRules.Select(r => r.tag))
            if(tagRules.Count( r => r.tag.Equals(ruleTag)) > 1)
                throw new InvalidRuleDeclarationException();
    }
    protected private void CreateInitialSection() => Instantiate(PickSectionWithTag(initialSectionTags),transform).Initialize(this,0);
    public void AddSectionTemplate() => Instantiate(Resources.Load("SectionTemplate"), Vector3.zero, Quaternion.identity);
    public void AddDeadEndTemplate() => Instantiate(Resources.Load("DeadEndTemplate"), Vector3.zero, Quaternion.identity);

    public bool IsSectionValid(Bounds newSection, Bounds sectionToIgnore) =>
        !registeredColliders.Except(sectionToIgnore.Colliders).Any(c => c.bounds.Intersects(newSection.Colliders.First().bounds));
    
    public void RegisterNewSection(Section newSection)
    {
        registeredSections.Add(newSection);
        if(tagRules.Any(r=> newSection.tags.Contains(r.tag)))
            tagRules.First(r => newSection.tags.Contains(r.tag)).PlaceRuleSection();
        SizeAvailable--;
    }        
    public void RegisterNewDeadEnd(IEnumerable<Collider> colliders) => deadEndsCollider.AddRange(colliders);
    public Section PickSectionWithTag(string[] tags)
    {
        if(RulesContainerTargetTags(tags) && HalfLevelBuilt)
        {
            foreach(var rule in tagRules.Where(r => r.NotSatisfied))
            {
                if(tags.Contains(rule.tag))
                {
                    return sections.Where(x => x.tags.Contains(rule.tag)).PickOne();
                }
            }
        }
        var pickedTag = PickFromExcludedTags(tags);
        return sections.Where(x => tags.Contains(pickedTag)).PickOne();
    }
    protected string PickFromExcludedTags(string[] tags)
    {
        var tagsToExclude = tagRules.Where(r => r.Completed).Select(rs => rs.tag);
        return tags.Except(tagsToExclude).PickOne();
    }
    protected bool RulesContainerTargetTags(string[] tags)=>tags.Intersect(tagRules.Where(r=>r.NotSatisfied).Select(r => r.tag)).Any();
    protected void DeactivateBounds()
    {
        foreach(Collider col in registeredColliders)
            col.enabled = false;
    }
}
