using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NekoPos : MonoBehaviour
{
    [SerializeField] public CatPoolController nekoPool;
    [SerializeField] float shortInterval, longInterval;

    public List<NekoController> nekoControllers;
    public bool canGenerat;
    Coroutine cor;

    

    void Start()
    {
        
        nekoControllers = new List<NekoController>();
    }

    public void StartNekoGenerat()
    {
        cor = StartCoroutine(_nekoGenerat());
    }

    public void ResetNeko()
    {
        nekoControllers.Clear();
        canGenerat = true;
        cor = StartCoroutine(_nekoGenerat());
    }

    public void RushNeko()
    {
        StopCoroutine(cor);
        nekoControllers.Clear();
        canGenerat = true;
        StartCoroutine(_nekoRush());
    }

    IEnumerator _nekoRush()
    {
        Debug.Log($"DO nekoRush {nekoPool.nekoList.Count}");
        for(int i = 0; i < nekoPool.nekoList.Count; i++)
        {
            Debug.Log($"nekoRush {i}");
            float _x = Random.Range(-2f, 2f);
            NekoController _neko = nekoPool.Launch(new Vector3(_x, 6f));
            if(_neko != null)
            {
                _neko.state = NekoController.STATE.WALK;
                _neko.transform.SetParent(transform);
            }
            
            yield return null;
        }
        
    }

    IEnumerator _nekoGenerat()
    {
        yield return null;

        while (canGenerat)
        {
            float _x = Random.Range(-2f, 2f);
            float _interval = Random.Range(shortInterval, longInterval);
            NekoController _neko = nekoPool.Launch(new Vector3(_x, 6f));

            nekoControllers.Add(_neko);
            _neko.state = NekoController.STATE.WALK;
            _neko.transform.SetParent(transform);
            yield return new WaitForSeconds(_interval);
 
        }
    }


}
