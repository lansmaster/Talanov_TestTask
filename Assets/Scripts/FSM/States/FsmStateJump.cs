using UnityEngine;

public class FsmStateJump : FsmStateMovement
{
    private readonly float _jumpForce;

    public FsmStateJump(Fsm fsm, Rigidbody rigidbody, Animator animator, float speed, float jumpForce) : base(fsm, rigidbody, animator, speed)
    {
        _jumpForce = jumpForce;
    }

    public override void Enter() 
    {
        Animator.SetBool("IsJumping", true);

        Jump();
    }
    public override void Exit() 
    {
        Animator.SetBool("IsJumping", false);
    }
    public override void Update()
    {
        var inputDirection = ReadInput();
        Move(inputDirection);

        if (Rigidbody.velocity.y < 0)
        {
            if (IsGrounded())
            {
                if (inputDirection.sqrMagnitude == 0f)
                {
                    Fsm.SetState<FsmStateIdle>();
                }
                else
                {
                    Fsm.SetState<FsmStateRun>();
                }
            }
        }
    }

    private void Jump()
    {
        Rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
    private bool IsGrounded()
    {
        if(Physics.Raycast(Rigidbody.position, Vector3.down, 0.1f))
        {
            return true;
        }

        return false;
    }
}