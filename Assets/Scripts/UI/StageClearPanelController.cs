using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class StageClearPanelController : MonoBehaviour
{
    [SerializeField] Text clearText,scoreText,bonusText;
    int targetCount;
    float bonusColor;

    public UnityAction<int> CalcScore;
    public void UpdateClearStage(int _stage)
    {
        clearText.text = $"ステージ {_stage + 1} クリア";
    }

    public void UpdateScoreText(int _targetCount)
    {
        targetCount = _targetCount;
        StartCoroutine(_calcScore());
        
    }

    public void InitScoreText()
    {
        scoreText.text = $"×0=0";
        bonusText.text = "";
        bonusText.color = new Color(0, 0, 0, 0);
    }

    IEnumerator _calcScore()
    {
        int score = 0;
        while(score <= targetCount * 100)
        {
            
            yield return null;
            SoundController.I.PlaySE(SESoundData.SE.CLEARSCORE);
            scoreText.text = $"×{targetCount}={score}";
            score += 5;
        }
        
        CalcScore?.Invoke(targetCount * 100);
        yield return new WaitForSeconds(1f);
        
        if(targetCount == 5)
        {
            bonusText.text = $"Bonus = 3000";
            CalcScore?.Invoke(3000);
            bonusColor = 1f;
        }
        else
        {
            bonusText.text = "No Bonus";
            bonusColor = 0f;
        }


        StartCoroutine(_fadeInBonus(bonusColor));
        
    }

    IEnumerator _fadeInBonus(float _r)
    {

        float a = 0;

        while(a <= 1)
        {
            a += 0.1f;
            
            bonusText.color = new Color(_r, 0, 0, a);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);

        SceneMove.instance.StageClear();

    }

}
