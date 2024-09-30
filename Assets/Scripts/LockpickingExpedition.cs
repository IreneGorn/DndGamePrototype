using System;
using System.Collections.Generic;
using UnityEngine;

public class LockpickingExpedition : Expedition
{
    public override string Name => "Lockpicking";
    public override int Difficulty => 15;

    private bool _isLockPicked = false;
    
    public LockpickingExpedition(IDiceRollingService diceRollingService) : base(diceRollingService) { }

    public override void StartExpedition(IEnumerable<Character> team)
    {
        base.StartExpedition(team);
        if (Team.Count < 1 || Team.Count > 3)
        {
            throw new System.ArgumentException("Lockpicking expedition requires 1 to 3 characters.");
        }
        Debug.Log($"Team of {Team.Count} characters is attempting to pick a lock on a chest.");
    }

    public override bool PerformCheck(Character character, string attribute)
    {
        if (attribute.ToLower() != "dexterity")
        {
            throw new System.ArgumentException("Chest Lockpicking expedition only checks Dexterity.");
        }

        int diceRoll = DiceRollingService.RollDice(20);
        int totalResult = diceRoll + character._dexterity;

        bool success = totalResult >= Difficulty;
        Debug.Log($"{character._name} rolled {diceRoll} + {character._dexterity} (Dexterity) = {totalResult}. {(success ? "Success!" : "Failure.")}");

        if (success)
        {
            _isLockPicked = true;
        }

        return success;
    }

    public override ExpeditionResult CompleteExpedition()
    {
        if (_isLockPicked)
        {
            Debug.Log("The chest has been successfully unlocked!");
            return ExpeditionResult.Success;
        }
        else
        {
            Debug.Log("The team failed to unlock the chest.");
            return ExpeditionResult.Failure;
        }
    }
}