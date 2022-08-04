using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NekoPos : MonoBehaviour
{
    [SerializeField] CatPoolController nekoPool;
    void Start()
    {
        StartCoroutine(_nekoGenerat());
    }


    IEnumerator _nekoGenerat()
    {


        while (true)
        {
            float _x = Random.Range(-2f, 2f);
            float _interval = Random.Range(0.1f, 1f);
            NekoController _neko = nekoPool.Launch(new Vector3(_x, 6f));
            _neko.transform.SetParent(transform);
            yield return new WaitForSeconds(_interval);
            
        }

    }
}
