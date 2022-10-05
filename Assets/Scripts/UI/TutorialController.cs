using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class TutorialController : MonoBehaviour
{

    [SerializeField] Button closeButton, nextButton, prevButton;
    [SerializeField] Text tutorialHeader;
    [SerializeField] Transform tutoriarlImagesTf;
    [SerializeField] Image[] images;
    int currentPage, pageMax;
    float moveRange = 500f;
    public UnityAction EndTutorial;
    void Start()
    {
        _updateTutorialHeader();
        pageMax = images.Length;
        closeButton.onClick.AddListener(() => _hideTutorial());
        prevButton.gameObject.SetActive(false);
        nextButton.onClick.AddListener(() => _showNextImage());
        prevButton.onClick.AddListener(() => _showPrevImage());
        _selectImage();
    }

    void _hideTutorial()
    {
        transform.DOScale(0f,1f).SetEase(Ease.InOutBack).SetLink(gameObject).OnComplete(() => {
            gameObject.SetActive(false);
            EndTutorial?.Invoke();
        });
    }

    void _showNextImage()
    {
        currentPage += 1;
        _selectImage();
        tutoriarlImagesTf.DOLocalMoveX(tutoriarlImagesTf.localPosition.x - moveRange, 0.1f).SetLink(tutoriarlImagesTf.gameObject);
        nextButton.gameObject.SetActive(currentPage + 1 == pageMax ? false : true);
        prevButton.gameObject.SetActive(true);
        _updateTutorialHeader();
    }

    void _showPrevImage()
    {
        currentPage -= 1;
        _selectImage();
        tutoriarlImagesTf.DOLocalMoveX(tutoriarlImagesTf.localPosition.x + moveRange, 0.1f).SetLink(tutoriarlImagesTf.gameObject);
        prevButton.gameObject.SetActive(currentPage == 0 ? false : true);
        nextButton.gameObject.SetActive(true);
        _updateTutorialHeader();
    }

    void _updateTutorialHeader()
    {
        tutorialHeader.text = $"‚ ‚»‚Ñ‚©‚½ {currentPage + 1}/{images.Length}";
    }

    void _selectImage()
    {
        for (int i = 0; i < images.Length; i++)
        {
            if(i == currentPage)
            {
                images[i].gameObject.SetActive(true);
            }
            else
            {
                images[i].gameObject.SetActive(false);
            }
        }
    }
}
