using UnityEngine;

public class FsmStateJump : FsmState
{
    private readonly Rigidbody _rigidbody;
    private readonly Animator _animator;
    private readonly float _jumpForce;

    public FsmStateJump(Fsm fsm, Rigidbody rigidbody, Animator animator, float jumpForce) : base(fsm)
    {
        _rigidbody = rigidbody;
        _animator = animator;
        _jumpForce = jumpForce;
    }

    public override void Enter() 
    {
        _animator.SetBool("IsJumping", true);

        Jump();
    }
    public override void Exit() 
    {
        _animator.SetBool("IsJumping", false);
    }
    public override void Update()
    {
        Debug.Log(_rigidbody.velocity);

        //if (IsGrounded())
        //{
        //    Fsm.SetState<FsmStateIdle>();
        //}
    }

    private Vector2 ReadInput()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
    }

    private void Jump()
    {
        _rigidbody.AddForce(Vector3.up * _jumpForce);
    }

    private bool IsGrounded()
    {
        if(Physics.Raycast(_rigidbody.position, Vector3.down, 1.1f))
            return true;

        return false;
    }
}