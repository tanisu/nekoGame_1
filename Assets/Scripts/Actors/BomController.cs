using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomController : MonoBehaviour
{
    [SerializeField] Sprite bomExplode;
    Sprite defaultSp;
    SpriteRenderer sp;
    Rigidbody2D rgbd2d;
    ItemPoolController itemPool;

    public void BomInit()
    {
        sp = GetComponent<SpriteRenderer>();
        rgbd2d = GetComponent<Rigidbody2D>();
        itemPool = transform.parent.GetComponent<ItemPoolController>();
        defaultSp = sp.sprite;
    }

    private void Update()
    {
        if (transform.localPosition.y < -6)
        {
            ItemCollect();
        }
    }

    public void Explode(Vector3 _pos)
    {
        rgbd2d.simulated = false;
        
        sp.sprite = bomExplode;
        transform.localPosition = new Vector3(_pos.x,-3f);
        StartCoroutine(_endExplode());
    }

    IEnumerator _endExplode()
    {
        float _a = sp.color.a;
        while(_a > 0)
        {
            _a -= 0.05f;
            sp.color = new Color(1f, 1f, 1f, _a);
            yield return new WaitForSeconds(0.03f);

        }
        ItemCollect();
    }

    public void ItemCollect()
    {
        sp.sprite = defaultSp;
        sp.color = new Color(1f, 1f, 1f, 1f);
        rgbd2d.simulated = false;
        rgbd2d.simulated = true;
        itemPool.Collect(this);
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
}
