using System;
using UnityEngine;


public class SausageCamera : SausageComponent
{
    [field: SerializeField] public Rigidbody Target { get; private set; }
    [field: Header("Parameters")]
    [field: SerializeField] public float Distance { get; private set; } = 5;
    [field: SerializeField] public float Smoothness { get; private set; } = 15;
    [field: SerializeField] public float Sensitivity { get; private set; } = 100;
    [field: SerializeField] public float HorizontalRange { get; private set; } = 90;
    [field: SerializeField] public LayerMask Mask { get; private set; } = 90;
    [field: SerializeField] public Vector3 Offset { get; private set; }

    [HideInInspector] public Vector3 direction;

    private Vector3 m_targetRotation;
    private Vector3 m_targetPosition;

    private float m_distance;





    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        m_distance = Distance;
    }


    private void LateUpdate()
    {
        var eulerAngles = m_targetRotation;
        eulerAngles.x += -direction.y * Sensitivity;
        eulerAngles.y += direction.x * Sensitivity;

        eulerAngles.x = Mathf.Clamp( eulerAngles.x > 180 ? (eulerAngles.x - 360) : eulerAngles.x, -HorizontalRange, HorizontalRange);

        transform.rotation = Quaternion.Lerp( transform.rotation, Quaternion.Euler( m_targetRotation ), Smoothness * Time.deltaTime );

        m_targetRotation = eulerAngles;
        m_targetPosition = Vector3.Lerp( m_targetPosition, Target.transform.position, Smoothness * Time.deltaTime);

        if (Physics.SphereCast(m_targetPosition, 0.1f, -transform.forward, out var hit, Distance, Mask))
            m_distance = hit.distance;
        else
            m_distance = Mathf.MoveTowards(m_distance, Distance, 10 * Time.deltaTime);

        transform.position = m_targetPosition + transform.rotation * Offset - transform.forward * m_distance;
    }
}
