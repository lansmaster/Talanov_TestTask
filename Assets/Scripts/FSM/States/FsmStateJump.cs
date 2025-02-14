using UnityEngine;

public class FsmStateJump : FsmStateMovement
{
    private readonly float _jumpForce;

    private bool _isGrounded;

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

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    public override void Update()
    {
        base.Update();

        if (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) // поменять на что то другое
        {
            if (_isGrounded)
            {
                if (inputDirection.sqrMagnitude == 0f)
                {
                    Fsm.SetState<FsmStateIdle>();
                    return;
                }
                else
                {
                    Fsm.SetState<FsmStateRun>();
                    return;
                }
            }
        }
    }

    private void Jump()
    {
        Rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _isGrounded = false;
    }
}