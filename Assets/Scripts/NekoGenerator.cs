using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NekoGenerator : MonoBehaviour
{
    [SerializeField] GameObject Neko;
    [SerializeField] Transform target;
    void Start()
    {
        StartCoroutine(_nekoGenerat());
    }


    IEnumerator _nekoGenerat()
    {
        

        while (true)
        {
            float _x = Random.Range(-2f, 2f);
            float _interval = Random.Range(0.1f, 1f);
            yield return new WaitForSeconds(_interval);
            Instantiate(Neko,new Vector3(_x,6f),Quaternion.identity, transform);
        }
        
    }

    
    
}
