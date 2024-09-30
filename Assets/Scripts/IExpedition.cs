using System.Collections.Generic;
using UnityEngine;

public interface IExpedition
{
    string Name { get; }
    int Difficulty { get; }
    void StartExpedition(IEnumerable<Character> team);
    bool PerformCheck(Character character, string attribute);
    ExpeditionResult CompleteExpedition();
}

public abstract class Expedition : IExpedition
{
    protected List<Character> Team;
    protected IDiceRollingService DiceRollingService;
    
    public abstract string Name { get; }
    public abstract int Difficulty { get; }
    
    protected Expedition(IDiceRollingService diceRollingService)
    {
        DiceRollingService = diceRollingService;
    }
    public virtual void StartExpedition(IEnumerable<Character> team)
    {
        Team = new List<Character>(team);
        Debug.Log($"Starting expedition: {Name}");
    }

    public abstract bool PerformCheck(Character character, string attribute);
    
    public abstract ExpeditionResult CompleteExpedition();
}
    
public enum ExpeditionResult
{
    Success,
    Failure,
    PartialSuccess
}