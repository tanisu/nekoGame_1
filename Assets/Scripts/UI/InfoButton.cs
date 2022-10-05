using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InfoButton : MonoBehaviour
{
    [SerializeField] GameObject info;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(()=>_showInfo());
    }

    void _showInfo()
    {
        info.SetActive(true);
        info.transform.DOScale(1f, 1f).SetEase(Ease.OutBounce).SetLink(info);
    }
}
