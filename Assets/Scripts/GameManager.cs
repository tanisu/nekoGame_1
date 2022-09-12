using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{

    [SerializeField] NekoPos nekoPos;    
    CatPoolController pool;
    [SerializeField] UIController ui;
    [SerializeField] OyajiController oyaji;
    [SerializeField] AreaController area;
    [SerializeField] float addAnger;
    [SerializeField] int maxRushCount,minLoopCount,nekoRushCount,explodeCount,maxExplodeCount;
    [SerializeField] SpriteRenderer stageSp;
    int targetCount,loopCount,score;
    float anger;
    bool canRush,isExplode,isRetry,isGameOver;
    public int stage;
    public int[] nekoNum;
    Color[] stageColors;
    private void Awake()
    {
        pool = nekoPos.nekoPool;
        pool.CheckStageState = _checkStageState;
        pool.CreateNekos(nekoNum);
        
    }


    private void Start()
    {
        stageColors = new Color[] { new Color(1f, 1f, 1f, 0), new Color(1f, 0f, 0f, 0.4f), new Color(0.1f, 0.0f, 0.1f, 0.45f), new Color(0f, 0f, 0f, 0.8f) };
        SceneMove.instance.StageChange = _changeStage;
        ui.OffAngry = _offAngry;
        ui.StartExplosion = _explosion;
        ui.EndExplosion = _endExplosion;
        ui.SetTotalScore = _setTotalScore;
        isRetry = SceneMove.instance.isRetry;
        SceneMove.instance.isRetry = false;
        
        _initStage();
    }

    void _changeStage()
    {
        
        if (!isRetry)
        {
            stage++;
            
            if(nekoNum.Sum() <= 100)
            {
                int nekoIdx = stage % 5 + 2;
                nekoNum[nekoIdx] += 1;
            }
            
            SceneMove.instance.currentStage = stage;
        }
        else
        {
            stage = SceneMove.instance.currentStage;
        }
        isRetry = false;
        ui.RsetPanels();
        _initStage();

    }

    void _initStage()
    {
        loopCount = 0;
        nekoRushCount = 0;
        anger = 0;
        explodeCount = 0;
        canRush = false; 
        isExplode = false;
        stageSp.color = stageColors[stage % stageColors.Length];
        
 
        targetCount = area.CreateFishs();
        
        
 
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
        SceneMove.instance.isGameStart = true;
    }


    void _loop()
    {
        loopCount++;
        if (loopCount > minLoopCount )
        {
            canRush = true;
            nekoPos.StopNekoGenerat();
        }
        
    }

    void _checkStageState()
    {
        
        if (!isGameOver &&  canRush && nekoRushCount < maxRushCount)
        {
            
            if(nekoRushCount == 0)
            {
                
                foreach (NekoController neko in pool.nekoList)
                {
                    neko.SpeedUp();
                }
            }
            
            _nekoRush();
        }
        else if (nekoRushCount >= maxRushCount)
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
        nekoRushCount++;
        
    }

    void _delTarget()
    {
        targetCount = area.GetTargetsLength();
        


        if (targetCount <= 0)
        {
            _gameOver();
        }
    }

    void _addAngryGage(int _score)
    {
        score += _score;
        
        if (!isExplode)
        {
            anger += addAnger;
            ui.UpdateAnger(anger);
            ui.UpdateScoreText(score);
            if (anger >= 1)
            {
                ui.ShowIkariBUtton();
            }
        }
        
    }

    void _offAngry()
    {
        anger = 0;
        ui.UpdateAnger(anger);
    }

    void _explosion()
    {
        explodeCount++;
        nekoPos.canGenerat = false;
        oyaji.gameObject.SetActive(false);
        foreach (NekoController neko in nekoPos.GetNekoControllers())
        {
            neko.StopNeko();
        }

        if(explodeCount >= maxExplodeCount)
        {
            isExplode = true;
        }
        
        
        
    }

    void _endExplosion()
    {
        
        oyaji.gameObject.SetActive(true);
        oyaji.ResetOyaji();
        foreach(NekoController neko in nekoPos.GetNekoControllers())
        {
            score += neko.score;
            ui.UpdateScoreText(score);
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
        isGameOver = true;
        nekoPos.canGenerat = false;
        foreach (NekoController neko in nekoPos.GetNekoControllers())
        {
            neko.StopNeko();
        }
        oyaji.OyajiLoose();
        _offAngry();
        //naichilab.RankingLoader.Instance.SendScoreAndShowRanking(score);
        ui.ShowGameOver();
        
    }
}
