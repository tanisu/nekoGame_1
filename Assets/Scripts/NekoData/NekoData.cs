using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "NekoData")]
public class NekoData : ScriptableObject
{
    public NekoController neko;
    public float  maxSpeed;
    public int Rate,min,max,score;
    public NEKO_TYPE NekoType;
    
}

public enum NEKO_TYPE
{
    STRAIGHT,
    WAVE,
    STOPANDGO
}
