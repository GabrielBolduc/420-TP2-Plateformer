using Godot;
using System;
using System.Collections.Generic;

public partial class FallState : PlayerState
{
    public override void Enter(Dictionary<string, bool> message = null)
    {
        GD.Print("Entering : " + GetType().Name);

        _player.Motion.Y = 0;

        if (_player.animPlayer != null)
        {
            _player.animPlayer.Play("Fall");
        }
    }

    public override void PhysicsUpdate(float delta)
    {
        _player.Motion.Y += _player.GRAVITY * delta;

        _player.Velocity = _player.Motion;
        _player.MoveAndSlide();

        if (_player.IsOnFloor())
        {
            float horiz = Input.GetActionStrength("droite") - Input.GetActionStrength("gauche");
            if (Mathf.Abs(horiz) > 0.1f)
                _stateMachine.TransitionTo("Run");
            else
                _stateMachine.TransitionTo("Idle");
        }
    }
}
