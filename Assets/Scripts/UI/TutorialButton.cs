using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using DG.Tweening;

public class TutorialButton : MonoBehaviour
{

    [SerializeField] Button tutorialButton,closeButton;
    [SerializeField] GameObject videoObj;
    [SerializeField] VideoPlayer video;

    void Start()
    {
        tutorialButton.onClick.AddListener(() => ShowTutorial());
        closeButton.onClick.AddListener(() => HideTutorial()) ;
    }

    void ShowTutorial()
    {
        videoObj.SetActive(true);
        videoObj.transform.DOScale(1f, 1f).SetEase(Ease.OutBounce).SetLink(videoObj).OnComplete(()=>video.Play());
    }

    void HideTutorial()
    {
        videoObj.transform.DOScale(0f, 1f).SetEase(Ease.InOutBack).SetLink(videoObj).OnComplete(() => {
            video.Stop();
            videoObj.SetActive(false);
        });
    }
    
}
