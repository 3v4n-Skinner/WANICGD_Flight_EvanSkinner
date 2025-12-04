using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ConformationOfDestructiveAction : MonoBehaviour
{
    public Action OnComfirm;
    [SerializeField] TextMeshProUGUI MessageDisplay;
    
    public void Initilize(string message, Action onComfirm)
    {
        MessageDisplay.text = message;
        OnComfirm = onComfirm;
    }
    public void Yes()
    {
        OnComfirm.Invoke();
    }
    public void No()
    {
        Destroy(gameObject);
    }
}
