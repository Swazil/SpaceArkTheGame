using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleButton : MonoBehaviour
{
    public event EventHandler OnModuleClick;

    void OnMouseDown()
    {
        if (OnModuleClick != null)
            OnModuleClick(this, EventArgs.Empty);
    }
}
