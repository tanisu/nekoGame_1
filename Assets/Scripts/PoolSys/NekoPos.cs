using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NekoPos : MonoBehaviour
{
    [SerializeField] public CatPoolController nekoPool;
    [SerializeField] float shortInterval, longInterval;

    public List<NekoController> nekoControllers;
    public bool canGenerat,isStopGenerate;
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
        if (isStopGenerate) return;
        nekoControllers.Clear();
        canGenerat = true;
        cor = StartCoroutine(_nekoGenerat());
    }

    public void StopNekoGenerat()
    {
        isStopGenerate = true;
        canGenerat = false;
        StopCoroutine(cor);
    }

    public void RushNeko()
    {
        StopCoroutine(cor);
        StartCoroutine(_nekoRush());
    }

    IEnumerator _nekoRush()
    {

        
        for(int i = 0; i < nekoPool.nekoList.Count; i++)
        {
            
            float _x = Random.Range(-2f, 2f);
            NekoController _neko = nekoPool.Launch(new Vector3(_x, 6f));
            if(_neko != null)
            {
                nekoControllers.Add(_neko);
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
            if(_neko != null)
            {

                nekoControllers.Add(_neko);
                _neko.state = NekoController.STATE.WALK;
                _neko.transform.SetParent(transform);
                SoundController.I.PlaySE(SESoundData.SE.NEKO);
            }
            
            yield return new WaitForSeconds(_interval);
 
        }
    }


}
