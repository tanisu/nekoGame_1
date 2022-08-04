using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPoolController : MonoBehaviour
{
    [SerializeField] NekoController neko;
    [SerializeField] int maxCount;
    Queue<NekoController> nekoQueue;
    Vector3 setPos = new Vector3(100, 100);


    private void Awake()
    {
        nekoQueue = new Queue<NekoController>();

        for(int i= 0; i < maxCount; i++)
        {
            NekoController _neko = Instantiate(neko, setPos, Quaternion.identity, transform);
            nekoQueue.Enqueue(_neko);
        }
    }

    public NekoController Launch(Vector3 _pos)
    {
        if (nekoQueue.Count < 0) return null;
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
