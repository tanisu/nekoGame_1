using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour
{
    

    [SerializeField]BonusPos _pos;

    private void Start()
    {
        _pos.EndFish = _endBonus;
    }

    public void BonusStart()
    {
        
        Debug.Log("Bonus Start");
        StartCoroutine(_bonus());
        _pos.StartBonusLaunch();
    }


    IEnumerator _bonus()
    {
        yield return new WaitForSeconds(1f);
        SoundController.I.FadeInBGM(BGMSoundData.BGM.BONUS);
        
    }

    private void _endBonus()
    {
        
        StartCoroutine(_changeStage());
    }

    IEnumerator _changeStage()
    {
        yield return new WaitForSeconds(3f);
        
        SceneMove.instance.StageClear();
    }
}
