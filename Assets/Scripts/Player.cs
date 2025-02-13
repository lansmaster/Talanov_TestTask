using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _jumpForce = 2f;
    
    private Rigidbody _rigidbody;
    private Animator _animator;
    private Fsm _fsm;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _fsm = new Fsm();

        _fsm.AddState(new FsmStateIdle(_fsm));
        _fsm.AddState(new FsmStateRun(_fsm, _rigidbody, _animator, _speed));
        _fsm.AddState(new FsmStateJump(_fsm, _rigidbody, _animator, _speed, _jumpForce));

        _animator.SetBool("IsMoving", false);
        _animator.SetBool("IsJumping", false);

        _fsm.SetState<FsmStateIdle>();
    }

    private void Update()
    {
        _fsm.Update();
    }
}
