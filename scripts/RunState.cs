using Godot;
using System.Collections.Generic;

public partial class RunState : PlayerState
{
    // Méthode appelée à l'entrée de l'état Run
    public override void Enter(Dictionary<string, bool> message = null)
    {
        GD.Print("Entering : " + GetType().Name);

        // Joue l'animation "Run"
        _player.animPlayer.Play("Run");
    }

    public override void PhysicsUpdate(float delta)
    {
        var inputDirectionX = Input.GetActionStrength("droite") - Input.GetActionStrength("gauche");

        _player.Motion.X += _player.ACCEL * inputDirectionX;

        _player.Motion.Y += _player.GRAVITY * delta;

        _player.Motion.X = Mathf.Clamp(_player.Motion.X, -_player.MAXSPEED, _player.MAXSPEED);

        _player.Velocity = _player.Motion;

        _player.MoveAndSlide();

        if (inputDirectionX > 0)
        {
            _player.facing_right = true;
        }
        else if (inputDirectionX < 0)
        {
            _player.facing_right = false;
        }
        
        if (Input.IsActionJustPressed("ui_jump"))
        {
            var message = new Dictionary<string, bool>() { { "doJump", true } };
            _stateMachine.TransitionTo("Jump", message);
        }
        else if (Mathf.IsEqualApprox(inputDirectionX, 0.0f))
        {
            _stateMachine.TransitionTo("Idle");
        }

        if (Input.IsActionJustPressed("ui_attack"))
        {
            _stateMachine.TransitionTo("Attack");
            return;
        }
        else if (Mathf.IsEqualApprox(inputDirectionX, 0.0f))
        {
            _stateMachine.TransitionTo("Idle");
        }

        if (!_player.IsOnFloor())
        {
            _stateMachine.TransitionTo("Fall");
            return;
        }

    }
}