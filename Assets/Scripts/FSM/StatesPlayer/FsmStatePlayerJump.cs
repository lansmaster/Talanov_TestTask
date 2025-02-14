using UnityEngine;

public class FsmStatePlayerJump : FsmStatePlayerMovement
{
    private readonly float _jumpForce;

    public FsmStatePlayerJump(Fsm fsm, Rigidbody rigidbody, Animator animator, float speed, float jumpForce) : base(fsm, rigidbody, animator, speed)
    {
        _jumpForce = jumpForce;
    }

    public override void Enter() 
    {
        if (isGrounded && Rigidbody.velocity.y >= -0.1f)
        {
            Animator.SetBool("IsJumping", true);

            Jump();
        }
    }
    public override void Exit() 
    {
        Animator.SetBool("IsJumping", false);
    }

    public override void Update()
    {
        base.Update();

        if (isGrounded)
        {
            if (inputDirection.sqrMagnitude == 0f)
            {
                Fsm.SetState<FsmStatePlayerIdle>();
                return;
            }
            else
            {
                Fsm.SetState<FsmStatePlayerRun>();
                return;
            }
        }
    }

    private void Jump()
    {
        Rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }
}