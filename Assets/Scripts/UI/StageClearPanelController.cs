using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StageClearPanelController : MonoBehaviour
{
    [SerializeField] Text clearText,scoreText;
    int targetCount;
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

    IEnumerator _calcScore()
    {
        int score = 0;
        while(score <= targetCount * 100)
        {
            
            yield return null;
            scoreText.text = $"×{targetCount}={score}";
            score += 5;
        }
        CalcScore?.Invoke(targetCount * 100);
        yield return new WaitForSeconds(1f);
        
        SceneMove.instance.StageClear();
    }
}
