using UnityEngine;

public abstract class FsmStateMovement : FsmState
{
    protected readonly Rigidbody Rigidbody;
    protected readonly Animator Animator;
    protected readonly float Speed;

    protected Vector2 inputDirection;

    private Transform cameraTransform;

    public FsmStateMovement(Fsm fsm, Rigidbody rigidbody, Animator animator, float speed) : base(fsm)
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

    protected virtual void Move(Vector2 inputDirection)
    {
        Vector3 direction = cameraTransform.forward * inputDirection.y + cameraTransform.right * inputDirection.x;
        direction.y = 0;

        Rigidbody.MovePosition(Rigidbody.position + direction * (Speed * Time.fixedDeltaTime));

        if (direction != Vector3.zero)
            Rigidbody.rotation = Quaternion.Slerp(Rigidbody.rotation, Quaternion.LookRotation(direction), Time.fixedDeltaTime * Speed);
    }

    protected Vector2 ReadInput()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
    }
}