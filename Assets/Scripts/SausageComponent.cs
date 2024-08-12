using System;
using UnityEngine;


public abstract class SausageComponent : MonoBehaviour
{
    public Sausage Sausage { get; private set; }


    public void SetSausage(Sausage sausage)
    {
        if (sausage == null) throw new ArgumentNullException(nameof(sausage));

        Sausage = sausage;
    }
}
