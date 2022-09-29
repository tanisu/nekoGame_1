using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
    [SerializeField] Text angerText,scoreText;
    [SerializeField] Button ikariButton;
    [SerializeField] Image cutInImage,cutInPanel,ikariBarInner,nekoRushImage, nekoRushPanel;
    [SerializeField] BariaController baria;
    [SerializeField] GameObject gameoverPanel,koraPanel;
    [SerializeField] StageClearPanelController stageclearPanel;
    [SerializeField] Vector3 defaltPos;
    [SerializeField] IkariPanelController ikariPanelController;
    IkariButtonController ikariButtonController;
    
    public UnityAction OffAngry, StartExplosion,EndExplosion,AfterNekoRush;
    public UnityAction<int> SetTotalScore;
    int totalScore;

    private void Start()
    {
        ikariButton.onClick.AddListener(_ikariCutIn);
        ikariButtonController = ikariButton.GetComponent<IkariButtonController>();
        stageclearPanel.CalcScore = _calcScore;
    }

    public void SwitchKoraPanel(bool _bool)
    {
        koraPanel.SetActive(_bool);
    }

    public void RsetPanels()
    {
        gameoverPanel.transform.localPosition = defaltPos;
        stageclearPanel.transform.localPosition = defaltPos;
        UpdateAnger(0f);
        stageclearPanel.InitScoreText();
        ikariPanelController.ResetIkariIcons();
    }

    public void UpdateAnger(float _anger)
    {

        ikariBarInner.DOFillAmount(_anger, 0.1f).SetLink(ikariBarInner.gameObject);
        if(_anger == 0)
        {
            ikariButtonController.IkariStop();
        }
    }

    public void UpdateScoreText(int _score)
    {
        totalScore = _score;
        
        scoreText.text = $"SCORE:{totalScore}";
    }

    private void _calcScore(int _score)
    {
        totalScore += _score;
        SetTotalScore?.Invoke(totalScore);
        scoreText.text = $"SCORE:{totalScore}";
    }

    public void ShowIkariBUtton()
    {
        ikariButtonController.IkariMax();
    }


    public void ShowGameOver()
    {
        gameoverPanel.SetActive(true);
        
        gameoverPanel.transform.DOLocalMoveY(0f, 1.8f).SetEase(Ease.OutBounce).SetLink(gameoverPanel);
    }

    public void ShowClear(int _stage,int _targetCount)
    {
        stageclearPanel.UpdateClearStage(_stage);
        stageclearPanel.transform.DOLocalMoveY(0f, 1.8f).SetEase(Ease.OutBounce).SetLink(gameoverPanel).OnComplete(()=> {

            stageclearPanel.UpdateScoreText(_targetCount);
        });
    }

    public void NekoRushCutIn()
    {
        nekoRushImage.gameObject.SetActive(true);
        nekoRushPanel.gameObject.SetActive(true);
        Sequence seq = DOTween.Sequence();
        seq.Append(nekoRushImage.transform.DOLocalMoveX(0, 0.5f).SetEase(Ease.OutSine).SetLink(nekoRushImage.gameObject))
            .Join(nekoRushPanel.DOFade(0.75f,0.5f).SetLink(nekoRushPanel.gameObject))
            .AppendInterval(0.55f)
            .AppendCallback(() =>
            {
                nekoRushImage.transform.DOLocalMoveX(700, 0.5f).SetEase(Ease.InCubic).OnComplete(() =>
                {
                    nekoRushImage.transform.localPosition = new Vector3(-700, 0);
                    nekoRushImage.gameObject.SetActive(false);
                    nekoRushPanel.gameObject.SetActive(false);
                    AfterNekoRush?.Invoke();
                }).SetLink(nekoRushImage.gameObject);
                
            });
            
    }

    private void _ikariCutIn()
    {
        StartExplosion?.Invoke();
        cutInImage.gameObject.SetActive(true);
        cutInPanel.gameObject.SetActive(true);
        SoundController.I.PlaySE(SESoundData.SE.CUTIN);
        Sequence seq = DOTween.Sequence();
        seq.Append(cutInPanel.DOFade(0.75f,0.5f).SetLink(cutInPanel.gameObject))
        .Join(cutInImage.transform.DOLocalMoveX(0, 0.3f).SetEase(Ease.OutSine).SetLink(cutInImage.gameObject))
            .AppendInterval(0.55f)
            .AppendCallback(()=> {
                cutInPanel.DOFade(0f, 0.5f).SetLink(cutInPanel.gameObject);
                cutInImage.transform.DOLocalMoveX(-600, 0.3f).SetEase(Ease.InCubic).OnComplete(()=> {
                    
                    cutInImage.transform.localPosition = new Vector3(600, 0);
                    baria.StartBaria(EndExplosion);
                }).SetLink(cutInImage.gameObject);
                ikariButtonController.IkariStop();
                _offCutInUI();
                
                OffAngry?.Invoke();
                ikariPanelController.UseIkari();
        });
    }

    private void _offCutInUI()
    {
        cutInImage.gameObject.SetActive(false);
        cutInPanel.gameObject.SetActive(false);
        
    }

    
}
