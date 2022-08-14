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
    [SerializeField] Image cutInImage,cutInPanel,ikariBarInner,nekoRushImage;
    [SerializeField] BariaController baria;
    [SerializeField] GameObject gameoverPanel,nekoRushPanel;
    IkariButtonController ikariButtonController;

    public UnityAction OffAngry, StartExplosion,EndExplosion;
    private void Start()
    {
        ikariButton.onClick.AddListener(_ikariCutIn);
        ikariButtonController = ikariButton.GetComponent<IkariButtonController>();
    }

    public void UpdateAnger(float _anger)
    {
        //angerText.text = $"ƒCƒ‰ƒCƒ‰“x {_anger.ToString()}";
        
        ikariBarInner.DOFillAmount(_anger, 0.1f);
    }

    public void ShowIkariBUtton()
    {
        ikariButtonController.IkariMax();
    }


    public void ShowGameOver()
    {
        gameoverPanel.SetActive(true);
        gameoverPanel.transform.DOLocalMoveY(250f, 1.8f).SetEase(Ease.OutBounce);
    }


    public void NekoRushCutIn()
    {
        nekoRushImage.gameObject.SetActive(true);
        nekoRushPanel.SetActive(true);
        Sequence seq = DOTween.Sequence();
        seq.Append(nekoRushImage.transform.DOLocalMoveX(0, 0.5f).SetEase(Ease.OutSine))
            .AppendInterval(0.75f)
            .AppendCallback(() =>
            {
                nekoRushImage.transform.DOLocalMoveX(-600, 0.5f).SetEase(Ease.InCubic).OnComplete(() =>
                {
                    nekoRushImage.transform.localPosition = new Vector3(600, 0);
                    nekoRushImage.gameObject.SetActive(false);
                    nekoRushPanel.SetActive(false);
                });
                
            });
            
    }

    private void _ikariCutIn()
    {
        StartExplosion?.Invoke();
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
                    baria.StartBaria(EndExplosion);
                });
                ikariButtonController.IkariStop();
                _offCutInUI();
                
                OffAngry?.Invoke();
        });
    }

    private void _offCutInUI()
    {
        cutInImage.gameObject.SetActive(false);
        cutInPanel.gameObject.SetActive(false);
        
    }

    
}
