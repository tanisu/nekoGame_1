using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{

    [SerializeField] NekoPos nekoPos;
    [SerializeField] ItemPos itemPos;
    [SerializeField] BomPos bomPos;
    [SerializeField] ItemPoolController itemPool;
    CatPoolController pool;
    //ItemPoolController itemPool;
    [SerializeField] UIController ui;
    [SerializeField] OyajiController oyaji;
    [SerializeField] AreaController area;
    [SerializeField] BonusController bonus;
    [SerializeField] float addAnger;
    [SerializeField] int maxRushCount,minLoopCount,nekoRushCount,explodeCount,maxExplodeCount;
    [SerializeField] SpriteRenderer stageSp;
    int targetCount,loopCount,score;
    float anger;
    bool canRush,isExplode,isRetry,isGameOver,isPlaing,isBonus;
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
        stageColors = new Color[] { new Color(1f, 1f, 1f, 0), new Color(1f, 0.3f, 0.3f, 0.4f), new Color(0.1f, 0.0f, 0.1f, 0.45f), new Color(0f, 0f, 0f, 0.8f) };
        SceneMove.instance.StageChange = _changeStage;
        ui.OffAngry = _offAngry;
        ui.StartExplosion = _explosion;
        ui.EndExplosion = _endExplosion;
        ui.SetTotalScore = _setTotalScore;
        ui.AfterNekoRush = _reStartItems;
        isRetry = SceneMove.instance.isRetry;
        SceneMove.instance.isRetry = false;
        
        _initStage();

        //var queues = itemPool.GetQueues();
        
    }


    private void Update()
    {
        if (isPlaing && !isBonus)
        {
            oyaji.OyajiWalk();
        }
        
    }

    void _changeStage()
    {
        
        if( stage > 1 && (stage +1) % 4 == 0 && !isBonus)
        {
            Camera.main.transform.position = new Vector3(7f, 0,-10);
            SoundController.I.FadeOutBGM();
            bonus.gameObject.SetActive(true);
            bonus.BonusStart();
            isBonus = true;
            //ボーナス
        }
        else
        {
            if (!isRetry)
            {
                stage++;
                if (isBonus)
                {
                    bonus.gameObject.SetActive(false);
                    isBonus = false;
                    Camera.main.transform.position = new Vector3(0f, 0, -10);
                    SoundController.I.FadeInBGM();

                }


                //難易度調整　ネコ
                if (nekoNum.Sum() < 100)
                {
                    //strong cat add + 1
                    int nekoIdx = stage % 6 + 2;
                    
                    nekoNum[nekoIdx] += 1;

                    //base cat add
                    if(stage % 4 == 0)
                    {
                        if (nekoNum[1] < 20)
                        {
                            nekoNum[1] += 1;
                            nekoNum[2] += 1;
                        }

                    }
                }

                SceneMove.instance.currentStage = stage;
            }
            else
            {
                stage = SceneMove.instance.currentStage;
                if(SceneMove.instance.currentNekoNum != null)
                {
                    nekoNum = SceneMove.instance.currentNekoNum;
                    SceneMove.instance.currentNekoNum = null;
                    
                }
            }

            //難易度調整　ネコラッシュ＆通常ループ
            if(stage >= 8 && stage < 16)
            {
                minLoopCount = 1;
            }else if(stage >= 16)
            {
                minLoopCount = 2;
            }
            if (stage % 4 == 0)
            {
                
                nekoNum[0] = 0;
                
                if (maxRushCount < 5)
                {
                    maxRushCount++;
                }

            }
            else if(stage > 4)
            {
                int currentMaxVal = stage / 4;
                
                if(maxRushCount < 5 && maxRushCount < 1 + currentMaxVal)
                {
                    maxRushCount = 1 + currentMaxVal;
                }
                
            }
            isRetry = false;
            _initStage();
        }
        ui.RsetPanels();
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

        oyaji.OyajiInit();
 
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
        isPlaing = false;
        yield return new WaitForSeconds(1.3f);
        nekoPos.ResetNeko();
        itemPos.StartItemGenarate();
        bomPos.StartItemGenarate();
        
        
        SceneMove.instance.isGameStart = true;
        isPlaing = true;
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
        else if (nekoRushCount >= maxRushCount && !isGameOver)
        {
            //クリア
            itemPos.StopItemGenerate(true);
            bomPos.StopItemGenerate(true);
            oyaji.ResetOyaji();
            ui.OffAngry();
            ui.UpdateAnger(0);
            ui.ShowClear(stage,targetCount);
        }
    }



    void _nekoRush()
    {    
        ui.NekoRushCutIn();
        itemPos.canGenerate = false;
        bomPos.canGenerate = false;
        

        if(itemPool.itemList != null)
        {
            foreach(ItemController item in itemPool.itemList)
            {
                item.ItemStop();
            }
        }

        if(itemPool.bomList != null)
        {
            foreach(BomController _bom in itemPool.bomList)
            {
                _bom.ItemStop();
            }
        }

        nekoPos.RushNeko();
        nekoRushCount++;
        SoundController.I.PlaySE(SESoundData.SE.NEKORUSH);
    }

    void _reStartItems()
    {
        itemPos.StopItemGenerate();
        bomPos.StopItemGenerate();

        if (itemPool.itemList != null)
        {
            foreach (ItemController item in itemPool.itemList)
            {
                item.ItemMove();
            }
        }

        if (itemPool.bomList != null)
        {
            foreach (BomController _bom in itemPool.bomList)
            {
                _bom.ItemMove();
            }
        }
        itemPos.StartItemGenarate();
        bomPos.StartItemGenarate();
    }

    void _delTarget()
    {
        targetCount = area.GetTargetsLength();

        SoundController.I.PlaySE(SESoundData.SE.SAKANATORARETA);

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
            SoundController.I.PlaySE(SESoundData.SE.IKARIUP);
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
        itemPos.StopItemGenerate(true);
        bomPos.StopItemGenerate(true);


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
        itemPos.StartItemGenarate();
        bomPos.StartItemGenarate();


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
        itemPos.StopItemGenerate(true);
        bomPos.StopItemGenerate(true);
        oyaji.OyajiLoose();
        _offAngry();
        SceneMove.instance.currentNekoNum = nekoNum;
        ui.ShowGameOver();
        
    }
}
