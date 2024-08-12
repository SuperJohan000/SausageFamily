using UnityEngine;



public class Sausage : MonoBehaviour
{
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Movement Movement { get; private set; }
    [field: SerializeField] public SausageCamera Camera { get; private set; }
    [field: SerializeField] public SausageBody Body { get; private set; }
    [field: SerializeField] public Hook Hook { get; private set; }


    private void Awake()
    {
        Movement.SetSausage(this);
        Camera.SetSausage(this);
        Body.SetSausage(this);
    }



    private void Update()
    {
        Movement.direction.x = Input.GetAxisRaw("Horizontal");
        Movement.direction.z = Input.GetAxisRaw("Vertical");

        Movement.isTryCrouch = Input.GetKeyDown(KeyCode.LeftControl);
        Movement.isTrySprint = Input.GetKey(KeyCode.LeftShift);
        Movement.isTryJump = Input.GetKey(KeyCode.Space);

        Camera.direction.x = Input.GetAxis("Mouse X");
        Camera.direction.y = Input.GetAxis("Mouse Y");

        Body.isBend = Input.GetMouseButton(0);
        Body.targetHeight = Mathf.Clamp(Body.targetHeight + Input.mouseScrollDelta.y * 0.25f, 0.5f, 1.5f);

        Hook.isTryHook = Input.GetMouseButton(0);

        Animator.SetFloat("Speed", Movement.Rigidbody.velocity.ToHorizontal().magnitude);
    }
}
