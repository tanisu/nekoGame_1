using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    [SerializeField] NekoPos nekoPos;    
     CatPoolController pool;
    [SerializeField] UIController ui;
    [SerializeField] OyajiController oyaji;
    [SerializeField] AreaController area;

    int targetCount,loopCount;
    float anger;
    bool canRush;




    private void Start()
    {
        ui.OffAngry = _offAngry;
        ui.StartExplosion = _explosion;
        ui.EndExplosion = _endExplosion;
        pool = nekoPos.nekoPool;
        foreach (NekoController neko in pool.nekoList)
        {
            neko.HitAngryArea = _addAngryGage;
            neko.StealTarget = _delTarget;
            neko.LoopCount = _loop;
            neko.RushStart = _nekoRush;
        }
        
        targetCount = area.targets.Length;
        
        StartCoroutine(_startNeko());
        
    }

    IEnumerator _startNeko()
    {
        Debug.Log("wait");
        yield return new WaitForSeconds(1.3f);
        nekoPos.canGenerat = true;
        nekoPos.StartNekoGenerat();
    }


    void _loop()
    {
        if (loopCount >= 1)
        {
            _nekoRushBefor();
            canRush = true;
        }
        loopCount++;
        
    }

    void _nekoRushBefor()
    {
        nekoPos.canGenerat = false;

    }

    void _nekoRush()
    {
        if(canRush)
        {
            foreach (NekoController neko in nekoPos.nekoControllers)
            {
                neko.EscapeNeko();
            }
            //oyaji.gameObject.SetActive(false);
            ui.NekoRushCutIn();
            nekoPos.RushNeko();
        }
        
    }

    void _delTarget()
    {
        targetCount--;
        
        if(targetCount <= 0)
        {
            
            _gameOver();
        }
    }

    void _addAngryGage()
    {
        anger += 0.5f;
        ui.UpdateAnger(anger);
        if(anger >= 1)
        {
            ui.ShowIkariBUtton();
        }
    }

    void _offAngry()
    {
        anger = 0;
        ui.UpdateAnger(anger);
    }

    void _explosion()
    {
        nekoPos.canGenerat = false;
        oyaji.gameObject.SetActive(false);
        foreach(NekoController neko in nekoPos.nekoControllers)
        {
            neko.StopNeko();
        }
    }

    void _endExplosion()
    {
        
        oyaji.gameObject.SetActive(true);
        foreach(NekoController neko in nekoPos.nekoControllers)
        {
            neko.EscapeNeko();
        }
        nekoPos.ResetNeko();
    }

    void _gameOver()
    {
        nekoPos.canGenerat = false;
        foreach (NekoController neko in nekoPos.nekoControllers)
        {
            
            neko.EscapeNeko();
        }
        oyaji.OyajiLoose();
        ui.ShowGameOver();
        
    }
}
