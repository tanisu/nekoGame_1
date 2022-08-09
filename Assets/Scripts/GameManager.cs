using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] CatPoolController[] pools;
    [SerializeField] UIController ui;
    [SerializeField] OyajiController oyaji;
    int anger;
    

    private void Start()
    {
        ui.OffAngry = _offAngry;
        ui.StartExplosion = _explosion;
        ui.EndExplosion = _endExplosion;
        foreach(CatPoolController pool in pools)
        {
            foreach (NekoController neko in pool.nekoList)
            {
                neko.HitAngryArea = _addAngryGage;
            }
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

    void _explosion()
    {
        
        oyaji.gameObject.SetActive(false);
        
        foreach(CatPoolController pool in pools)
        {
            pool.gameObject.SetActive(false);
            foreach (NekoController neko in pool.nekoList)
            {
                neko.StopNeko();
            }
        }
    }

    void _endExplosion()
    {
        oyaji.gameObject.SetActive(true);

        foreach (CatPoolController pool in pools)
        {
            pool.gameObject.SetActive(true);
            foreach (NekoController neko in pool.nekoList)
            {
                neko.EscapeNeko();
            }
        }
    }
}
