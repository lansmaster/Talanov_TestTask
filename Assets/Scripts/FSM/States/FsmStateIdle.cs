using UnityEngine;

public class FsmStateIdle : FsmState
{
    public FsmStateIdle(Fsm fsm) : base(fsm) { }

    public override void Update() 
    {
        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {
            Fsm.SetState<FsmStateRun>();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fsm.SetState<FsmStateJump>();
            return;
        }
    }
}
