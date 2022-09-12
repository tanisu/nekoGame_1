using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IkariPanelController : MonoBehaviour
{
    Image[] ikariIcons;
    void Start()
    {
        ikariIcons = GetComponentsInChildren<Image>();
    }

    public void UseIkari()
    {
        for (int i = 0; i < ikariIcons.Length; i++)
        {
            if(ikariIcons[i].color.a == 1)
            {
                ikariIcons[i].color = new Color(0.5f, 0.5f, 0.5f, 0.3f);
                break;
            }
        }
    }

    public void ResetIkariIcons()
    {
        foreach(Image ikariIcon in ikariIcons)
        {
            ikariIcon.color = Color.white;
        }
    }

}
