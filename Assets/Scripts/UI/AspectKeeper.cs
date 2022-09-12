using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectKeeper : MonoBehaviour
{
    [SerializeField] Camera targetCam;
    [SerializeField] Vector2 aspectVec;

    void Update()
    {
        float screenAspect = Screen.width / (float)Screen.height;
        float targetAspect = aspectVec.x / aspectVec.y;
        float magRate = targetAspect / screenAspect;
        Rect viewportRect = new Rect(0, 0, 1, 1);
        if(magRate < 1)
        {
            viewportRect.width = magRate;
            viewportRect.x = 0.5f - viewportRect.width * 0.5f;
        }
        else
        {
            viewportRect.height = 1 / magRate;
            viewportRect.y = 0.5f - viewportRect.height * 0.5f;
        }

        targetCam.rect = viewportRect;
    }
}
