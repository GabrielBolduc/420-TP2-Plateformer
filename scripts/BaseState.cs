using Godot;
using System.Collections.Generic;

public partial class State : Node
{
    public StateMachine _stateMachine = null;

    public virtual void HandleInputs(InputEvent inputEvent)
    {
        return;
    }
    public virtual void Update(double delta)
    {
        return;
    }

    public virtual void PhysicsUpdate(float delta)
    {
        return;
    }

    public virtual void Enter(Dictionary<string, bool> message = null)
    {
        return;
    }

    public virtual void Exit()
    {
        return;
    }
}
