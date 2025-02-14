using UnityEngine;

public abstract class FsmStatePlayerMovement : FsmState
{
    protected readonly Rigidbody Rigidbody;
    protected readonly Animator Animator;
    protected readonly float Speed;

    protected Vector2 inputDirection;
    protected bool isGrounded = true;

    private Transform cameraTransform;

    public FsmStatePlayerMovement(Fsm fsm, Rigidbody rigidbody, Animator animator, float speed) : base(fsm)
    {
        Rigidbody = rigidbody;
        Animator = animator;
        Speed = speed;

        cameraTransform = Camera.main.transform;
    }

    public override void Update()
    {
        inputDirection = ReadInput();
    }

    public override void FixedUpdate()
    {
        if (inputDirection.sqrMagnitude != 0)
            Move(inputDirection);
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            isGrounded = true;
    }

    protected virtual void Move(Vector2 inputDirection)
    {
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 cameraRight = cameraTransform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        Vector3 direction = cameraForward * inputDirection.y + cameraRight * inputDirection.x;

        Rigidbody.MovePosition(Rigidbody.position + direction * (Speed * Time.fixedDeltaTime));

        if (direction != Vector3.zero)
            Rigidbody.rotation = Quaternion.Slerp(Rigidbody.rotation, Quaternion.LookRotation(direction), Time.fixedDeltaTime * Speed);
    }

    protected Vector2 ReadInput()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
    }
}