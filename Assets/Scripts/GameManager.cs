using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] CatPoolController pool;
    [SerializeField] UIController ui;

    int anger;
    

    private void Start()
    {
        ui.OffAngry = _offAngry;
        foreach(NekoController neko in pool.nekoList)
        {
            neko.HitAngryArea = _addAngryGage;
        }
    }

    void _addAngryGage()
    {
        anger += 1;
        ui.UpdateAngerText(anger);
        if(anger > 3)
        {
            ui.ShowIkariBUtton();
        }
    }

    void _offAngry()
    {
        anger = 0;
        ui.UpdateAngerText(anger);
    }
}
