using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPoolController : MonoBehaviour
{
    [SerializeField] ItemController drink;
    [SerializeField] BomController bom;
    Vector3 setPos = new Vector3(100, 100);
    Queue<ItemController> itemQueue;
    Queue<BomController> bomQueue;
    public List<ItemController> itemList;
    public List<BomController> bomList;
    private void Awake()
    {
        itemList = new List<ItemController>();
        bomList = new List<BomController>();
        itemQueue = new Queue<ItemController>();
        bomQueue = new Queue<BomController>();
        for (int i = 0; i < 10; i++)
        {
            ItemController _item = Instantiate(drink, setPos, Quaternion.identity, transform);
            _item.gameObject.SetActive(false);
            _item.ItemInit();
            itemQueue.Enqueue(_item);

            BomController _bom = Instantiate(bom, setPos, Quaternion.identity, transform);
            _bom.BomInit();
            _bom.gameObject.SetActive(false);
            bomQueue.Enqueue(_bom);
        }
    }

    //public Tuple<Queue<ItemController>,Queue<BomController>> GetQueues()
    //{
    //    Tuple<Queue<ItemController>, Queue<BomController>> result = Tuple.Create(itemQueue,bomQueue);
    //    return result; 
    //}

    public ItemController Launch()
    {
        if (itemQueue.Count <= 0) return null;
        ItemController _item = itemQueue.Dequeue();
        _item.gameObject.SetActive(true);
        itemList.Add(_item);
        return _item;
    }

    public BomController LaunchBom()
    {
        if (bomQueue.Count <= 0) return null;
        BomController _bom = bomQueue.Dequeue();
        _bom.gameObject.SetActive(true);
        bomList.Add(_bom);
        return _bom;
    }

    public void Collect(ItemController _item)
    {
        
        _item.transform.SetParent(transform);
        _item.gameObject.SetActive(false);
        itemList.Remove(_item);
        itemQueue.Enqueue(_item);
 
    }

    public void Collect (BomController _bom)
    {
        _bom.transform.SetParent(transform);
        _bom.gameObject.SetActive(false);
        bomList.Remove(_bom);
        bomQueue.Enqueue(_bom);
    }

    public void ResetAll()
    {
        BroadcastMessage("ItemCollect", SendMessageOptions.DontRequireReceiver);
    }




}
