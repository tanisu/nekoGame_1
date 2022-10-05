using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class TutorialButton : MonoBehaviour
{

    [SerializeField] TutorialController tutorial;

    void Start()
    {
        if (PlayerPrefs.GetInt("notFirst") == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
        GetComponent<Button>().onClick.AddListener(() => ShowTutorial());
        
    }

    void ShowTutorial()
    {
        tutorial.gameObject.SetActive(true);
        tutorial.gameObject.transform.DOScale(1f, 1f).SetEase(Ease.OutBounce).SetLink(tutorial.gameObject);
    }

    
    
}
