using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;


public class WalkingMovement : Movement
{
    [field: Header("Horizontal Movement")]
    [field: SerializeField] public float MaxSpeed { get; private set; }
    [field: SerializeField] public float MaxSpeedInSprint { get; private set; }
    [field: SerializeField] public float Acceleration { get; private set; }
    [field: SerializeField] public float Braking { get; private set; }
    [field: SerializeField] public float InAirIntensity { get; private set; } = 0.3f;
    [field: Header("Vertical Movement")]
    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField] public float GravityScale { get; private set; } = 1;
    [field: SerializeField] public float FeetRadius { get; private set; } = 0.5f;
    [field: SerializeField] public float FeetHeight { get; private set; } = 0.25f;
    [field: SerializeField] public LayerMask Mask { get; private set; }

    private RaycastHit m_surface;




    private void Update()
    {
        if (isTryJump) TryJump();
    }


    private void FixedUpdate()
    {
        IsGrounded = Physics.Raycast(transform.position + Vector3.up * FeetHeight, Vector3.down, out m_surface, FeetRadius, Mask);

        var velocity = Rigidbody.velocity; // Эта хрень неправиьно поворачивает относительно нормали, разберись!
        var targetVelocity = Quaternion.FromToRotation(Vector3.up, m_surface.normal) * transform.forward * direction.normalized.magnitude * (isTrySprint ? MaxSpeedInSprint : MaxSpeed);

        velocity += Physics.gravity * (GravityScale - 1) * Time.fixedDeltaTime;

        targetVelocity.y += velocity.y;

        if (IsGrounded == false && direction.magnitude > 0)
        {
            var targetRotation = Quaternion.Euler(Vector3.up * (Sausage.Camera.transform.eulerAngles.y + Vector3.SignedAngle(Vector3.forward, direction, Vector3.up)));

            Rigidbody.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 25f * Time.fixedDeltaTime); ;

        }
        else if (direction.magnitude > 0 || (Sausage.Body.isBend && Mathf.Abs(Vector3.SignedAngle( transform.forward, Sausage.Camera.transform.forward, Vector3.up )) > 90))
        {
            var targetRotation = Quaternion.Euler( Vector3.up * (Sausage.Camera.transform.eulerAngles.y + Vector3.SignedAngle( Vector3.forward, direction, Vector3.up ) ) );

            Rigidbody.rotation = Quaternion.Lerp( transform.rotation, targetRotation, 5f * Time.fixedDeltaTime );
        }

        if (direction.magnitude > 0)
            Rigidbody.velocity = Vector3.Lerp( velocity, targetVelocity, (IsGrounded ? 1 : InAirIntensity) * Acceleration * Time.fixedDeltaTime); 
        else
            Rigidbody.velocity = Vector3.Lerp( velocity, targetVelocity, Braking * Time.fixedDeltaTime);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere( transform.position + Vector3.up * FeetHeight, FeetRadius );

        Gizmos.color = Color.red;
        Gizmos.DrawLine( transform.position, transform.position + Quaternion.FromToRotation(Vector3.up, m_surface.normal) * transform.forward);
    }


    private void TryJump()
    {
        if (IsGrounded)
        {
            var velocity = Rigidbody.velocity;
            
            velocity.y = JumpForce;

            Rigidbody.velocity = velocity;

            Sausage.Body.OnJump(velocity.y);
        }
    }
}
