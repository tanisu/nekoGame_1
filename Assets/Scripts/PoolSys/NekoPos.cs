using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NekoPos : MonoBehaviour
{
    [SerializeField] CatPoolController[] nekoPools;
    [SerializeField] float shortInterval, longInterval;
    void Start()
    {
        StartCoroutine(_nekoGenerat());
    }


    IEnumerator _nekoGenerat()
    {


        while (true)
        {
            float _x = Random.Range(-2f, 2f);
            float _interval = Random.Range(shortInterval, longInterval);
            int _idx = Random.Range(0, nekoPools.Length);
            NekoController _neko = nekoPools[_idx].Launch(new Vector3(_x, 6f));
            _neko.transform.SetParent(transform);
            yield return new WaitForSeconds(_interval);
            
        }

    }
}
