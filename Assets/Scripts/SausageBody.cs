using System.Linq;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class SausageBody : SausageComponent
{
    [field: SerializeField] public SausageBodyPart[] Parts { get; private set; }
    [field: SerializeField] public float CameraRotationOffset { get; private set; }
    [field: SerializeField] public float CameraRotationIntensity { get; private set; }
    [field: SerializeField] public float MaxCameraRotation { get; private set; }
    [SerializeField] private float m_minSpring = 1000;
    [SerializeField] private float m_maxSpring = 5000;

    public Collider Collider { get; private set; }

    [HideInInspector] public bool isBend;
    [HideInInspector] public float targetHeight;

    private Quaternion m_previousRotation;



    private void Awake()
    {
        Collider = GetComponent<Collider>();

        targetHeight = 1;
    }


    private void Start()
    {
        for(int x = 0; x < Parts.Length; x++)
        {
            for (int y = 0; y < Parts.Length; y++)
            {
                if (Parts[x] == Parts[y]) continue;

                Physics.IgnoreCollision(Parts[x].Collider, Parts[y].Collider, true );
            }

            var slerpDrive = Parts[x].Joint.slerpDrive;
            slerpDrive.positionSpring = Mathf.Lerp( m_maxSpring, m_minSpring, (float)x / Parts.Length);
            Parts[x].Joint.slerpDrive = slerpDrive;

            Parts[x].Rigidbody.mass = Mathf.Lerp( 1f, 0.1f, (float)x / Parts.Length );

            Physics.IgnoreCollision(Parts[x].Collider, Collider, true );
        }
    }


    private void Update()
    {
        if (isBend)
        {
            var aroundRotation = Quaternion.AngleAxis(Vector3Ext.SmoothClamp(Vector3Ext.NormalizeEulerAngle(Sausage.Camera.transform.rotation.eulerAngles.y - transform.eulerAngles.y), 90, 60, 120), Vector3.down);
            var cameraRotation = Quaternion.AngleAxis(Mathf.Clamp((Vector3Ext.NormalizeEulerAngle(Sausage.Camera.transform.rotation.eulerAngles.x + CameraRotationOffset)) * CameraRotationIntensity, -MaxCameraRotation, MaxCameraRotation), Vector3.left);
            var targetRotation = Quaternion.Lerp( Quaternion.identity, cameraRotation * aroundRotation, 1f / Parts.Length);

            foreach (var part in Parts)
            {
                part.SetTargetRotation(targetRotation);
                part.SetTargetHeightIntensive(targetHeight);
            }
        }
        else
        {
            foreach (var part in Parts)
            {
                part.SetTargetRotation(Quaternion.identity);
                part.SetTargetHeightIntensive(targetHeight);
            }
        }
    }

    public void OnJump(float velocity)
    {
        foreach (var part in Parts)
            part.Rigidbody.velocity = new Vector3(part.Rigidbody.velocity.x, velocity, part.Rigidbody.velocity.z);
    }
}
