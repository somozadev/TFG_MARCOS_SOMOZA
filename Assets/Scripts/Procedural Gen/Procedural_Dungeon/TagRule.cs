using System;
[Serializable]
public class TagRule 
{
    public string tag;
    public int minAmount;
    public int maxAmount;
    int sectionsPlaced;
    
    RuleStatus Status => sectionsPlaced < minAmount 
    ? RuleStatus.NotSatisfied : sectionsPlaced < maxAmount 
    ? RuleStatus.Satisfied : RuleStatus.Completed;

    public bool Satisfied => Status == RuleStatus.Satisfied;
    public bool Completed => Status == RuleStatus.Completed;
    public bool NotSatisfied => Status == RuleStatus.NotSatisfied;
    public void PlaceRuleSection() => sectionsPlaced++;
}

public enum RuleStatus
{
    NotSatisfied,
    Satisfied,
    Completed
}
