using UnityEngine;
using Zenject;

public interface IDiceFactory
{
    DiceRoller CreateDice(Material color);
}

public class DiceFactory : IDiceFactory
{
    private readonly DiContainer _container;
    private readonly GameObject _dicePrefab;

    public DiceFactory(DiContainer container, [Inject(Id = "DicePrefab")] GameObject dicePrefab)
    {
        _container = container;
        _dicePrefab = dicePrefab;
    }

    public DiceRoller CreateDice(Material color)
    {
        var diceInstance = _container.InstantiatePrefab(_dicePrefab);
        var diceRoller = diceInstance.GetComponent<DiceRoller>();

        var renderer = diceInstance.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.materials[0] = color;
        }

        return diceRoller;
    }
}