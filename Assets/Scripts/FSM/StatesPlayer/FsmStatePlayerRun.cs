using UnityEngine;

public class FsmStatePlayerRun : FsmStatePlayerMovement
{
    public FsmStatePlayerRun(Fsm fsm, Rigidbody rigidbody, Animator animator, float speed) : base(fsm, rigidbody, animator, speed) { }

    public override void Enter() 
    {
        Animator.SetBool("IsRunning", true);
    }
    public override void Exit() 
    {
        Animator.SetBool("IsRunning", false);
    }
    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fsm.SetState<FsmStatePlayerJump>();
            return;
        }

        if (inputDirection.sqrMagnitude == 0f)
        {
            Fsm.SetState<FsmStatePlayerIdle>();
            return;
        }
    }
}
