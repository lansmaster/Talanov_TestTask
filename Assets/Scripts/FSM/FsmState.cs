using UnityEngine;

public abstract class FsmState
{
    protected readonly Fsm Fsm;

    public FsmState(Fsm fsm)
    {
        Fsm = fsm;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void OnCollisionEnter(Collision collision) { }
}
