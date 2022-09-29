using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BonusData")]
public class BonusData : ScriptableObject
{
    public BonusFishController bonusFish;
    public int score;
}
