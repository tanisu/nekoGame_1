using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CloseButton : MonoBehaviour
{
    [SerializeField] Button closeButton;
    void Start()
    {
        closeButton.onClick.AddListener(() => _hidePanel());
    }

    void _hidePanel()
    {
        transform.DOScale(0f, 1f).SetEase(Ease.InOutBack).SetLink(gameObject).OnComplete(() => {
            gameObject.SetActive(false);
        });
    }
    
}
