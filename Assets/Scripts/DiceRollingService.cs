using UnityEngine;

public interface IDiceRollingService
{
    int RollDice(int sides);
    int[] RollMultipleDice(int numberOfDice, int sides);
}

public class DiceRollingService : IDiceRollingService
{
    public int RollDice(int sides)
    {
        return Random.Range(1, sides + 1);
    }

    public int[] RollMultipleDice(int numberOfDice, int sides)
    {
        int[] results = new int[numberOfDice];
        for (int i = 0; i < numberOfDice; i++)
        {
            results[i] = RollDice(sides);
        }
        return results;
    }
}