using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPoolController : MonoBehaviour
{
    //[SerializeField] NekoController neko;
    [SerializeField] int maxCount;
    [SerializeField] NekoData[] nekoDatas;
    Queue<NekoController> nekoQueue;
    public List<NekoController> nekoList;
    Vector3 setPos = new Vector3(100, 100);
    int RateMax,rota;

    private void Awake()
    {
        nekoQueue = new Queue<NekoController>();
        nekoList = new List<NekoController>();
        foreach(NekoData nekodata in nekoDatas)
        {
            nekodata.min = RateMax;
            RateMax += nekodata.Rate -1;
            nekodata.max = RateMax;
            //Debug.Log($"min {nekodata.min} max {nekodata.max}");
        }

        

        for(int i= 0; i < maxCount; i++)
        {
            rota = Random.Range(0, RateMax);
            
            foreach(NekoData nekodata in nekoDatas)
            {
                if(rota >= nekodata.min && rota < nekodata.max)
                {
                    NekoController _neko = Instantiate(nekodata.neko, setPos, Quaternion.identity, transform);
                    if(i == 0)
                    {
                        _neko.isRush = true;
                    }
                    nekoQueue.Enqueue(_neko);
                    nekoList.Add(_neko);
                }
            }
            
        }
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
    }
}
