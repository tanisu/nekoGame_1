using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class BariaController : MonoBehaviour
{

    [SerializeField] float maxSize,duration;
    [SerializeField] Image angryDoom,angryPanel;


    
    public void StartBaria(UnityAction _endAction)
    {
        angryDoom.transform.localScale = Vector3.zero;
        angryDoom.gameObject.SetActive(true);
        gameObject.SetActive(true);
        angryPanel.enabled = false;
        SoundController.I.PlaySE(SESoundData.SE.EXPLODEDOOM);

        angryDoom.transform.DOScale(new Vector3(maxSize, maxSize), duration).OnComplete(() => {
            angryPanel.enabled = true;
            angryDoom.gameObject.SetActive(false);
            SoundController.I.PlaySE(SESoundData.SE.EXPLODE);
            Sequence seq = DOTween.Sequence();
            seq.Append(angryPanel.DOColor(new Color(0.9f,0,0,0.5f), 0.05f).SetLink(angryPanel.gameObject))
            .Append(angryPanel.DOColor(Color.white, 0.05f).SetLink(angryPanel.gameObject))
            .SetLoops(7).OnComplete(()=> {
                _endAction?.Invoke();
                gameObject.SetActive(false);
            });
        }).SetLink(angryDoom.gameObject);
    }


    
}
