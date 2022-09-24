using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomPos : MonoBehaviour
{
    [SerializeField] float interval;
    [SerializeField] public ItemPoolController itemPool;
    Coroutine cor;
    public bool canGenerate;


    public void SetInterval(float _time)
    {
        if(interval > 5)
        {
            interval = interval - _time * 2;
        }
        
    }

    public void StartItemGenarate()
    {
        if(cor != null)
        {
            StopCoroutine(cor);
        }
        canGenerate = true;
        cor = StartCoroutine(_itemLaunch());
    }


    public void StopItemGenerate(bool _reset = false)
    {
        if (_reset)
        {
            itemPool.ResetAll();
        }
        
        canGenerate = false;
        StopCoroutine(cor);
    }

    IEnumerator _itemLaunch()
    {
        while (canGenerate)
        {

            yield return new WaitForSeconds(interval);

            float _x = Random.Range(-1f, 1f);
            BomController _bom = itemPool.LaunchBom();
            if (_bom != null)
            {

                _bom.gameObject.SetActive(true);
                _bom.transform.localPosition = new Vector3(_x, 6f);
            }

        }


    }
}
