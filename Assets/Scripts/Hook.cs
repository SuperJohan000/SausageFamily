using Unity.VisualScripting;
using UnityEngine;


public class Hook : SausageComponent
{
    [field: SerializeField] public Rigidbody Mount { get; private set; }
    [field: SerializeField] public Vector3 Offset { get; private set; }
    [field: SerializeField] public float MountRadius { get; private set; } = 0.125f;


    public IHookeable Hookeable { get; private set; }
    public bool IsHooked => Hookeable != null;

    [HideInInspector] public bool isTryHook;

    private FixedJoint m_joint;




    private void Update()
    {
        if (isTryHook && IsHooked == false)
            TryHook();
        else if (isTryHook == false && IsHooked == true)
            TryRelease();
    }



    private bool TryHook()
    {
        var raycastResults = Physics.OverlapSphere( Mount.transform.position + Mount.transform.rotation * Offset, MountRadius );

        foreach( var result in raycastResults )
        {
            if (result.TryGetComponent<IHookeable>(out var hookeable) || (hookeable = result.GetComponentInParent<IHookeable>()) != null)
            {
                m_joint = hookeable.Rigidbody.AddComponent<FixedJoint>();

                m_joint.connectedBody = Mount;
                
                Hookeable = hookeable;

                Hookeable.OnHooked();

                return true;
            }
        }

        return false;
    }

    private bool TryRelease()
    {
        Destroy( m_joint );

        Hookeable.OnReleased();

        Hookeable = null;

        return true;
    }


    private void OnDrawGizmos()
    {
        if (Mount != null)
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere( Mount.transform.position + Mount.transform.rotation * Offset, MountRadius );
        }
    }
}
