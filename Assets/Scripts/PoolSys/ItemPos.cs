using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPos : MonoBehaviour
{
    [SerializeField] float interval;
    [SerializeField] public ItemPoolController itemPool;
    Coroutine cor;
    public bool canGenerate;
    public void StartItemGenarate()
    {
        if (cor != null)
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
            ItemController _item = itemPool.Launch();
            if(_item != null)
            {
                
                _item.gameObject.SetActive(true);
                _item.transform.localPosition = new Vector3(_x, 6f);
            }
            
        }
        

    }
}
