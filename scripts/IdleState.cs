using Godot;
using System;
using System.Collections.Generic;

public partial class IdleState : PlayerState
{
    public override void Enter(Dictionary<string, bool> message = null)
    {
        _player.Motion = _player.Motion.Lerp(Vector2.Zero, 0.2f);

        if (_player.animPlayer != null)
        {
            _player.animPlayer.Play("Idle");
        }
        GD.Print("Entering : " + GetType().Name);
    }

    public override void PhysicsUpdate(float delta)
    {
        _player.Motion = _player.Motion.Lerp(Vector2.Zero, 0.2f);

        _player.Velocity = _player.Motion;

        if (Input.IsActionPressed("droite") || Input.IsActionPressed("gauche"))
        {
            _stateMachine.TransitionTo("Run");
            return;
        }

        if (Input.IsActionJustPressed("ui_jump"))
        {
            var message = new Dictionary<string, bool>()
            {
                { "doJump", true }
            };
            _stateMachine.TransitionTo("Jump", message);
            return;
        }

        if (Input.IsActionJustPressed("ui_attack"))
        {
            _stateMachine.TransitionTo("Attack");
            return;
        }
    }
}
