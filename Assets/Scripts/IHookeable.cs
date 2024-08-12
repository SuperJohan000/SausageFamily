using UnityEngine;


public interface IHookeable
{
    public Rigidbody Rigidbody { get; }


    public void OnHooked();

    public void OnReleased();
}
