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
    [SerializeField] float addAnger;
    [SerializeField] int maxLoopCount,minRushCount;
    [SerializeField] SpriteRenderer stageSp;
    int targetCount,loopCount,score;
    float anger;
    bool canRush,endRush;
    public int stage;
    public int[] nekoNum = new int[] { 1,1,0,0,0,0,0,0};
    Color[] stageColors;
    private void Awake()
    {
        pool = nekoPos.nekoPool;
        pool.CheckStageState = _checkStageState;
        pool.CreateNekos(nekoNum);
        SceneMove.instance.StageChange = _changeStage;
    }


    private void Start()
    {
        stageColors = new Color[] { new Color(1f, 1f, 1f, 0), new Color(1f, 0f, 0f, 0.4f), new Color(0.1f, 0.0f, 0.1f, 0.45f), new Color(0f, 0f, 0f, 0.8f) };
        ui.OffAngry = _offAngry;
        ui.StartExplosion = _explosion;
        ui.EndExplosion = _endExplosion;
        ui.SetTotalScore = _setTotalScore;
        _initStage();
    }

    void _changeStage()
    {
        if (SceneMove.instance.isRetry)
        {
            stage = SceneMove.instance.currentStage;
            SceneMove.instance.isRetry = false;
        }
        else
        {
            stage++;
            SceneMove.instance.currentStage = stage;
        }
        
        
        
        ui.RsetPanels();
        _initStage();

    }

    void _initStage()
    {
        
        loopCount = 0;
        anger = 0;
        targetCount = area.GetTargetsLength();
        canRush = false;
        endRush = false;
        
        stageSp.color = stageColors[stage % stageColors.Length];

        pool.ResetNekoQueue();
        pool.CreateNekos(nekoNum);
        nekoPos.isStopGenerate = false;
        foreach (NekoController neko in pool.nekoList)
        {
            neko.HitAngryArea = _addAngryGage;
            neko.StealTarget = _delTarget;
            neko.LoopCount = _loop;
        }
        StartCoroutine(_startNeko());
        

        

    }

    IEnumerator _startNeko()
    {
        yield return new WaitForSeconds(1.3f);
        nekoPos.ResetNeko();
    }


    void _loop()
    {
        
        if (loopCount >= minRushCount && loopCount < maxLoopCount)
        {
            canRush = true;
            nekoPos.StopNekoGenerat();
        }
        else if(loopCount <= minRushCount)
        {
            canRush = false;
        }else if(loopCount >= maxLoopCount)
        {
            canRush = false;
            endRush = true;
        }
        loopCount++;
    }

    void _checkStageState()
    {
        if (canRush)
        {
            
            if(loopCount == (minRushCount + 1))
            {
                
                foreach (NekoController neko in pool.nekoList)
                {
                    neko.SpeedUp();
                }
            }
            
            _nekoRush();
        }
        if (endRush)
        {
            ui.OffAngry();
            ui.UpdateAnger(0);
            ui.ShowClear(stage,targetCount);
        }
    }


    void _nekoRush()
    {    
        ui.NekoRushCutIn();
        nekoPos.RushNeko();
        canRush = false;
    }

    void _delTarget()
    {
        targetCount--;
        if(targetCount <= 0)
        {
            _gameOver();
        }
    }

    void _addAngryGage(int _score)
    {
        score += _score;
        anger += addAnger;
        ui.UpdateAnger(anger);
        ui.UpdateScoreText(score);
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
        foreach (NekoController neko in nekoPos.nekoControllers)
        {
            neko.StopNeko();
        }
        
        
    }

    void _endExplosion()
    {
        
        oyaji.gameObject.SetActive(true);
        oyaji.ResetOyaji();
        foreach(NekoController neko in nekoPos.nekoControllers)
        {
            score += neko.score;
            ui.UpdateScoreText(score );
            neko.EscapeNeko();
        }
        nekoPos.ResetNeko();
    }

    void _setTotalScore(int _totalScore)
    {
        score = _totalScore;
        
    }

    void _gameOver()
    {
        nekoPos.canGenerat = false;
        foreach (NekoController neko in nekoPos.nekoControllers)
        {
            neko.StopNeko();
        }
        oyaji.OyajiLoose();
        _offAngry();
        ui.ShowGameOver();
        
    }
}
