using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Hookeable : MonoBehaviour, IHookeable
{
    public Rigidbody Rigidbody { get; private set; }


    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }



    public void OnHooked() { }

    public void OnReleased() { }
}
