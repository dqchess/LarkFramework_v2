using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIPanel
{
    void Open(object arg = null);
    void Close(object arg = null);
    void Destroy();
    void BringTop();
    void SetDepth(int value);
}
