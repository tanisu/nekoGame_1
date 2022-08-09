using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
    [SerializeField] Text angerText;
    [SerializeField] Button ikariButton;
    [SerializeField] Image cutInImage,cutInPanel;

    public UnityAction OffAngry;
    private void Start()
    {
        ikariButton.onClick.AddListener(_ikariCutIn);
    }

    public void UpdateAngerText(int _anger)
    {
        angerText.text = $"ƒCƒ‰ƒCƒ‰“x {_anger.ToString()}";
    }

    public void ShowIkariBUtton()
    {
        ikariButton.gameObject.SetActive(true);
    }

    private void _ikariCutIn()
    {
        
        cutInImage.gameObject.SetActive(true);
        cutInPanel.gameObject.SetActive(true);
        Sequence seq = DOTween.Sequence();
        seq.Append(cutInPanel.DOFade(1f,0.5f))
            .Join(cutInImage.transform.DOLocalMoveX(0, 0.5f).SetEase(Ease.OutSine))
            .AppendInterval(0.75f)
            .AppendCallback(()=> {
                cutInPanel.DOFade(0f, 0.5f);
                cutInImage.transform.DOLocalMoveX(-600, 0.5f).SetEase(Ease.InCubic).OnComplete(()=> {
                    cutInImage.transform.localPosition = new Vector3(600, 0);
                });
                ikariButton.gameObject.SetActive(false);
                OffAngry?.Invoke();
            });
    }
}
