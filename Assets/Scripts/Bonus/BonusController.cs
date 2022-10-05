using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BonusController : MonoBehaviour
{
    

    [SerializeField]BonusPos _pos;
    [SerializeField] BonusNeko bonusNeko;
    [SerializeField] BonusOyaji bonusOyaji;
    [SerializeField] Text bonusStartText, bonusEndText;

    private void Start()
    {
        _pos.EndFish = _endBonus;

    }

    public void BonusStart()
    {
        
        
        StartCoroutine(_bonus());
        _pos.StartBonusLaunch();
    }


    IEnumerator _bonus()
    {
        
        yield return new WaitForSeconds(1f);
        SoundController.I.FadeInBGM(BGMSoundData.BGM.BONUS);
        bonusStartText.DOFade(1f, 0.8f).SetLink(bonusStartText.gameObject).OnComplete(()=> {
            bonusStartText.DOFade(0f, 0.8f).SetLink(bonusStartText.gameObject);
        });
        
        
        bonusNeko.WalkStart();
        bonusOyaji.WalkStart();
    }

    private void _endBonus()
    {
        StartCoroutine(_changeStage());
    }

    IEnumerator _changeStage()
    {
        bonusEndText.DOFade(1f, 0.3f).SetLink(bonusEndText.gameObject);
        yield return new WaitForSeconds(3f);
        bonusEndText.DOFade(0f, 0.3f).SetLink(bonusEndText.gameObject);
        bonusNeko.EndWalk();
        bonusOyaji.EndWalk();
        SceneMove.instance.StageClear();

    }
}
