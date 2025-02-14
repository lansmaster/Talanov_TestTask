using UnityEngine;

public class FsmStatePlayerIdle : FsmState
{
    public FsmStatePlayerIdle(Fsm fsm) : base(fsm) { }

    public override void Update() 
    {
        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {
            Fsm.SetState<FsmStatePlayerRun>();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fsm.SetState<FsmStatePlayerJump>();
            return;
        }
    }
}
