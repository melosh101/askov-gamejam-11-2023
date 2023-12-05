using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable all
public class TEventBase : MonoBehaviour
{ 
    // ReSharper restore all
    public string type;

    protected virtual void PerformAction()
    {
        Debug.Log($"event triggered: {type}");
    }
}
