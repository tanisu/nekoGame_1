using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CatPoolController : MonoBehaviour
{
    
    
    [SerializeField] NekoData[] nekoDatas;
    Queue<NekoController> nekoQueue;
    List<Queue<NekoController>> nekoQueueBase;
    
    
    public List<NekoController> nekoList;
    Vector3 setPos = new Vector3(100, 100);
    
    public UnityAction CheckStageState;

    private void Awake()
    {
        
        nekoQueueBase = new List<Queue<NekoController>>();    
        for (int i = 0; i < nekoDatas.Length; i++)
        {
            nekoQueueBase.Add(new Queue<NekoController>());
            for (int j = 0; j < 100; j++)
            {
                NekoController _neko = Instantiate(nekoDatas[i].neko,setPos, Quaternion.identity, transform);
                _neko.nekoDataIndex = i;
                _neko.NekoInit(nekoDatas[i]);
                nekoQueueBase[i].Enqueue(_neko);
            }
        }
    }


    public void CreateNekos(int[] _nekoArr)
    {
        nekoQueue = new Queue<NekoController>();
        nekoList = new List<NekoController>();

        for(int i = 0;i < _nekoArr.Length; i++)
        {
            for(int j = 0;j < _nekoArr[i]; j++)
            {
                NekoController _neko = nekoQueueBase[i].Dequeue();
                nekoQueue.Enqueue(_neko);
                nekoList.Add(_neko);
            }
        }
        nekoList[nekoList.Count - 1].isLastCat = true;
    }


    public void ResetNekoQueue()
    {
        nekoQueue.Clear();
        
        foreach(NekoController _neko in nekoList)
        {
            if (_neko.GetComponentInChildren<TargetController>(true))
            {
                Destroy(_neko.GetComponentInChildren<TargetController>(true).gameObject);
            }
            _neko.isLastCat = false;
            nekoQueueBase[_neko.nekoDataIndex].Enqueue(_neko);
        }
        nekoList.Clear();

    }


    public NekoController Launch(Vector3 _pos)
    {
        if (nekoQueue.Count <= 0) return null;

        
        NekoController _neko = nekoQueue.Dequeue();        
        _neko.gameObject.SetActive(true);
        _neko.ShowInStage(_pos);
        return _neko;
        
    }
    

    public void Collect(NekoController _neko)
    {
        _neko.transform.SetParent(transform);
        _neko.gameObject.SetActive(false);
        nekoQueue.Enqueue(_neko);
        if(nekoQueue.Count == nekoList.Count)
        {
            CheckStageState?.Invoke();
        }
    }

}
