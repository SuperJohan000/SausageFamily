using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public abstract class Movement : SausageComponent
{
    public Rigidbody Rigidbody { get; private set; }

    [HideInInspector] public Vector3 direction;
    [HideInInspector] public bool isTryJump;
    [HideInInspector] public bool isTrySprint;
    [HideInInspector] public bool isTryCrouch;

    public bool IsGrounded { get; protected set; }


    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
}
