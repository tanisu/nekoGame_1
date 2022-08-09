using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BariaController : MonoBehaviour
{

    [SerializeField] float maxSize,duration;
    [SerializeField] Image angryDoom,angryPanel;

    private void Start()
    {
        angryPanel.enabled = false;
        angryDoom.transform.DOScale(new Vector3(maxSize,maxSize), duration).OnComplete(()=> {
            angryPanel.enabled = true;
            angryDoom.gameObject.SetActive(false);
            Sequence seq = DOTween.Sequence();
            seq.Append(angryPanel.DOColor(Color.red, 0.1f))
            .Append(angryPanel.DOColor(Color.white, 0.1f)).SetLoops(11);
            
        });
    
    }
    
    
}
