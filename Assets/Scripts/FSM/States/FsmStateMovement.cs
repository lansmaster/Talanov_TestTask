using UnityEngine;

public class FsmStateMovement : FsmState
{
    protected readonly Rigidbody Rigidbody;
    protected readonly Animator Animator;
    protected readonly float Speed;

    public FsmStateMovement(Fsm fsm, Rigidbody rigidbody, Animator animator, float speed) : base(fsm)
    {
        Rigidbody = rigidbody;
        Animator = animator;
        Speed = speed;
    }

    public override void Enter() { }
    public override void Exit() { }
    public override void Update() { }

    protected virtual void Move(Vector2 inputDirection)
    {
        var direction = new Vector3(inputDirection.x, 0, inputDirection.y) * (Speed * Time.deltaTime);
        Rigidbody.MovePosition(Rigidbody.position + direction);
        
        if (direction != Vector3.zero)
            Rigidbody.rotation = Quaternion.LookRotation(direction);
    }

    protected Vector2 ReadInput()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
    }
}