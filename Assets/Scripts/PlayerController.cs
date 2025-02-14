using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 6f;
    
    private Rigidbody _rigidbody;
    private Animator _animator;
    private Fsm _fsm;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _fsm = new Fsm();

        _fsm.AddState(new FsmStatePlayerIdle(_fsm));
        _fsm.AddState(new FsmStatePlayerRun(_fsm, _rigidbody, _animator, _speed));
        _fsm.AddState(new FsmStatePlayerJump(_fsm, _rigidbody, _animator, _speed, _jumpForce));

        _animator.SetBool("IsRunning", false);
        _animator.SetBool("IsJumping", false);

        _fsm.SetState<FsmStatePlayerIdle>();
    }

    private void Update()
    {
        _fsm.Update();
    }

    private void FixedUpdate()
    {
        _fsm.FixedUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        _fsm.OnCollisionEnter(collision);
    }
}
