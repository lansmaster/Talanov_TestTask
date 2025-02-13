using UnityEngine;

public class FsmStateRun : FsmState
{
    private readonly Rigidbody _rigidbody;
    private readonly Animator _animator;
    private readonly float _speed;

    public FsmStateRun(Fsm fsm, Rigidbody rigidbody, Animator animator, float speed) : base(fsm)
    {
        _rigidbody = rigidbody;
        _animator = animator;
        _speed = speed;
    }

    public override void Enter() 
    {
        _animator.SetBool("IsMoving", true);
    }
    public override void Exit() 
    {
        _animator.SetBool("IsMoving", false);
    }
    public override void Update()
    {
        var inputDirection = ReadInput();

        if (inputDirection.sqrMagnitude == 0f)
        {
            Fsm.SetState<FsmStateIdle>();
        }
        else
        {
            Move(inputDirection);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fsm.SetState<FsmStateJump>();
        }
    }

    private void Move(Vector2 inputDirection)
    {
        var direction = new Vector3(inputDirection.x, 0, inputDirection.y) * (_speed * Time.deltaTime);
        _rigidbody.MovePosition(_rigidbody.position + direction);

        _rigidbody.rotation = Quaternion.LookRotation(direction);
    }

    private Vector2 ReadInput()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
    }
}
