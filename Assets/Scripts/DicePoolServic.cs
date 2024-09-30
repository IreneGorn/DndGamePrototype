using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface IDicePoolService
{
    void InitializeDicePool(List<Character> characters);
    void ShowDicePanel();
    void HideDicePanel();
}

public class DicePoolService : IDicePoolService
{
    private readonly IDiceFactory _diceFactory;
    private readonly List<DiceRoller> _dicePool = new List<DiceRoller>();
    private readonly Transform _dicePanel;


    public DicePoolService(IDiceFactory diceFactory, [Inject(Id = "DicePanel")] Transform dicePanel)
    {
        _diceFactory = diceFactory;
        _dicePanel = dicePanel;
    }

    public void InitializeDicePool(List<Character> characters)
    {
        ClearDicePool();

        foreach (var character in characters)
        {
            for (int i = 0; i < character._dicePool; i++)
            {
                var dice = _diceFactory.CreateDice(character._diceColor);
                dice.transform.SetParent(_dicePanel);
                _dicePool.Add(dice);
            }
        }
        
        HideDicePanel();
    }

    public void ShowDicePanel()
    {
        _dicePanel.gameObject.SetActive(true);
    }

    public void HideDicePanel()
    {
        _dicePanel.gameObject.SetActive(false);
    }
    
    private void ClearDicePool()
         {
             foreach (var dice in _dicePool)
             {
                 Object.Destroy(dice.gameObject);
             }
             _dicePool.Clear();
         }
}