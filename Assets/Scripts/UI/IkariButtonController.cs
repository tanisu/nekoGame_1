using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IkariButtonController : MonoBehaviour
{
    Button _ikariButton;
    Image _ikariImage;
    Coroutine _cor;
    bool _isFlash;
    private void Start()
    {
        _ikariButton = GetComponent<Button>();
        _ikariImage = GetComponent<Image>();
    }

    

    public void IkariMax()
    {
        _ikariButton.interactable = true;
        _isFlash = true;
        _cor = StartCoroutine(_rainbow());
    }

    public void IkariStop()
    {
        _isFlash = false;   
        if(_cor != null)
        {
            StopCoroutine(_cor);
        }
        
        _ikariImage.color = Color.white;
        _ikariButton.interactable = false;
    }

    IEnumerator _rainbow()
    {
        
        while (_isFlash)
        {
            _ikariImage.color = Color.HSVToRGB(Time.time % 1,1,1);
            yield return new WaitForFixedUpdate();
        }
    }
}
