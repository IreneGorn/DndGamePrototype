using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject] private IExpeditionFactory _expeditionFactory;
    [Inject] private IDiceRollingService _diceRollingService;
    [Inject] private IDicePoolService _dicePoolService;

    private List<Character> _selectedTeam = new List<Character>();
    private IExpedition _currentExpedition;

    public void SelectCharacter(Character character)
    {
        if (_selectedTeam.Count < 3)
        {
            _selectedTeam.Add(character);
            Debug.Log($"Added {character._name} to the team.");
        }
        else
        {
            Debug.LogWarning("Team is already full (3 characters maximum).");
        }
    }

    public void StartExpedition(string expeditionType)
    {
        if (_selectedTeam.Count < 1 || _selectedTeam.Count > 3)
        {
            Debug.LogError("Invalid team size. Please select 1 to 3 characters.");
            return;
        }
        
        _currentExpedition = _expeditionFactory.CreateExpedition(expeditionType);
        _currentExpedition.StartExpedition(_selectedTeam);
        _dicePoolService.InitializeDicePool(_selectedTeam);
    }

    public bool PerformCheck(Character character, string attribute)
    {
        return _currentExpedition.PerformCheck(character, attribute);
    }

    public ExpeditionResult CompleteExpedition()
    {
        return _currentExpedition.CompleteExpedition();
    }

    public void ShowDicePanel()
    {
        _dicePoolService.ShowDicePanel();
    }
    
    public void HideDicePanel()
    {
        _dicePoolService.HideDicePanel();
    }
}