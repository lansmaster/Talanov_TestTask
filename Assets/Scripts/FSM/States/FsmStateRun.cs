using UnityEngine;

public class FsmStateRun : FsmStateMovement
{
    public FsmStateRun(Fsm fsm, Rigidbody rigidbody, Animator animator, float speed) : base(fsm, rigidbody, animator, speed) { }

    public override void Enter() 
    {
        Animator.SetBool("IsMoving", true);
    }
    public override void Exit() 
    {
        Animator.SetBool("IsMoving", false);
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
}
