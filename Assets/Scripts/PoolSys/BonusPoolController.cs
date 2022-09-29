using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class BonusPoolController : MonoBehaviour
{
    [SerializeField] BonusData[] bonusDatas;
    
    Queue<BonusFishController> bonusQueue;
    List<BonusFishController> bonusList;
    Vector3 setPos = new Vector3(100, 100);
    bool isFinish;
    public UnityAction EndBonus;
    void Awake()
    {
        bonusQueue = new Queue<BonusFishController>();
        bonusList = new List<BonusFishController>();
        for (int i = 0; i < bonusDatas.Length; i++)
        {
            
            for(int j = 0; j < 10; j++)
            {
                BonusFishController _obj = Instantiate(bonusDatas[i].bonusFish, setPos, Quaternion.identity, transform);
                bonusList.Add(_obj);
            }
        }
        

    }

    public void ShuffleBonus()
    {
        bonusList = bonusList.OrderBy(a => Guid.NewGuid()).ToList();
        foreach(BonusFishController _obj in bonusList)
        {
            bonusQueue.Enqueue(_obj);
        }
    }
    public BonusFishController Launch(Vector3 _pos)
    {
        if (bonusQueue.Count <= 0) {
            isFinish = true;
            return null; 
        }


        BonusFishController _bonus = bonusQueue.Dequeue();
        _bonus.gameObject.SetActive(true);
        _bonus.gameObject.transform.localPosition = _pos;
        return _bonus;
    }

    public void Collect(BonusFishController _obj)
    {
        _obj.transform.SetParent(transform);
        _obj.gameObject.SetActive(false);
        
        if (bonusQueue.Count == 0 && isFinish)
        {
            EndBonus?.Invoke();
            isFinish = false;
        }
    }
}
