using UnityEngine;

public class FsmStateIdle : FsmState
{
    private Animator _animator;

    public FsmStateIdle(Fsm fsm, Animator animator) : base(fsm)
    {
        _animator = animator;
    }

    public override void Enter() { }
    public override void Exit() { }
    public override void Update() 
    {
        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {
            Fsm.SetState<FsmStateRun>();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fsm.SetState<FsmStateJump>();
        }
    }
}
