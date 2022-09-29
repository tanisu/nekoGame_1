using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    [SerializeField] GameObject fishs;
    int maxFish = 5;
    float xDiff = 0.125f;
    
    public int  CreateFishs()
    {
        foreach(TargetController target in GetComponentsInChildren<TargetController>())
        {
            Destroy(target.gameObject);
        }
        float beforePos = -0.25f;

        for (int i = 0; i < maxFish; i++)
        {
            Transform fish = Instantiate(fishs, transform).transform;
            
            fish.localPosition = new Vector3(beforePos,-0.1f);
            beforePos += xDiff;
        }
        return maxFish;
    }

    public int GetTargetsLength()
    {
        return GetComponentsInChildren<TargetController>().Length;
    }
}
