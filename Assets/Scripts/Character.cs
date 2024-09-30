using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Character", menuName = "Game/Character")]
public class Character : ScriptableObject
{
    public Sprite _icon;
    public string _name;
    public int _strength;
    public int _dexterity;
    public int _constitution;
    public int _intelligence;
    public int _wisdom;
    public int _charisma;
    public int _dicePool;
    public Material _diceColor;
}
