using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class BonusTutorialController : MonoBehaviour
{
    [SerializeField] Button closeButton;

    public UnityAction EndTutorial;

    void Start()
    {
        closeButton.onClick.AddListener(() =>_hideTutorial());
    }

    void _hideTutorial()
    {
        transform.DOScale(0f, 1f).SetEase(Ease.InOutBack).SetLink(gameObject).OnComplete(() => {
            gameObject.SetActive(false);
            EndTutorial?.Invoke();
        });
    }
}
