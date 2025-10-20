using Godot;
using System;
using System.Collections.Generic;

public partial class StateMachine : Node
{
    [Export] public NodePath InitialState;
    public State State;

    public override void _Ready()
    {
        if (InitialState == null || !HasNode(InitialState))
        {
            GD.PrintErr("StateMachine: InitialState not assigned or not found. Set InitialState NodePath to your Idle state node.");
            return;
        }

        State = GetNode<State>(InitialState);

        // assigner la state machine à tous les enfants de type State
        foreach (State child in GetChildren())
        {
            if (child != null)
                child._stateMachine = this;
        }

        State.Enter();
        GD.Print("StateMachine: Ready with initial state " + State.Name);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        State?.HandleInputs(@event);
    }

    public override void _Process(double delta)
    {
        State?.Update(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        State?.PhysicsUpdate((float)delta);
    }

    public void TransitionTo(string targetStateName, Dictionary<string, bool> message = null)
    {
        GD.Print("StateMachine: Transition to " + targetStateName);
        if (!HasNode(targetStateName))
        {
            GD.PrintErr("StateMachine: target state node not found: " + targetStateName);
            return;
        }

        State.Exit();
        State = GetNode<State>(targetStateName);
        State.Enter(message);
        EmitSignal("Transitioned", State.Name);
    }
}
