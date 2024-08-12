using UnityEngine;


[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class SausageBodyPart : MonoBehaviour
{
    public ConfigurableJoint Joint { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public Collider Collider { get; private set; }

    private float m_defaultHeight;
    private float m_targetHeight;


    private void Awake()
    {
        Joint = GetComponent<ConfigurableJoint>();
        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();

        m_defaultHeight = Joint.anchor.y;
    }


    public void SetTargetRotation(Quaternion rotation)
    {
        Joint.targetRotation = rotation;
    }

    public void SetTargetHeightIntensive(float heightIntensive)
    {
        m_targetHeight = m_defaultHeight * heightIntensive;
    }


    private void Update()
    {
        Joint.anchor = Vector3.Lerp(Joint.anchor, Vector3.up * m_targetHeight, 10 * Time.deltaTime);
    }
}
