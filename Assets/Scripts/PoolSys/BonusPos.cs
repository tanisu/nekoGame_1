using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BonusPos : MonoBehaviour
{
    [SerializeField] BonusPoolController pool;
    
    Coroutine cor;
    bool _bonus;
    public UnityAction EndFish;

    private void Start()
    {
        pool.EndBonus = StopBonusLaunch;
    }

    public void StartBonusLaunch()
    {
        pool.ShuffleBonus();
        cor = StartCoroutine(_bonusLaunch());
        
    }

    public void StopBonusLaunch()
    {
        
        _bonus = false;
        StopCoroutine(cor);
        EndFish?.Invoke();
    }

    IEnumerator _bonusLaunch()
    {
        _bonus = true;
        while (_bonus)
        {
            yield return new WaitForSeconds(0.73f);
            BonusFishController _fish = pool.Launch(transform.localPosition);
            
            if(_fish != null)
            {
                //èoåªÇµÇΩÇÁè„Ç…çsÇ´ÇΩÇ¢
                //_fish.OnStage();
            }
            
        }
    }
}
