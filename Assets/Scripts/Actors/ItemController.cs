using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    ItemPoolController itemPool;
    Rigidbody2D rgbd2d;
    public void ItemInit()
    {
        itemPool = transform.parent.GetComponent<ItemPoolController>();
        rgbd2d = GetComponent<Rigidbody2D>();
    }


    
    
    void Update()
    {
        if(transform.localPosition.y < -6)
        {
            ItemCollect();
        }
    }

    public void ItemMoveSwitch()
    {
        rgbd2d.simulated = !rgbd2d.simulated;
    }

    public void ItemStop()
    {
        rgbd2d.simulated = false;
    }

    public void ItemMove()
    {
        rgbd2d.simulated = true;
    }
    

    public void ItemCollect()
    {
        rgbd2d.simulated = false;
        rgbd2d.simulated = true;
        itemPool.Collect(this);
    }
}
