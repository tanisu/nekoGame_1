using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    
    public enum AREA_STATE
    {
        NORMAL,
        ANGRY

    }

    public AREA_STATE state;
    SpriteRenderer _sp;

    private void Start()
    {
        _sp = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _sp.color = Color.red;
            state = AREA_STATE.ANGRY;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _sp.color = Color.white;
            state = AREA_STATE.NORMAL;
        }
    }
}
