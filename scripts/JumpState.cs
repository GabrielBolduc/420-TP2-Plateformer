using Godot;
using System.Collections.Generic;

public partial class JumpState : PlayerState
{
    private bool _hasJumped = false;

    public override void Enter(Dictionary<string, bool> message = null)
    {
        GD.Print("Entering : " + GetType().Name);

        if (message != null && message.ContainsKey("doJump") && message["doJump"])
        {
            _player.Motion.Y = -_player.JUMPFORCE;
            _hasJumped = true;
        }

        if (_player.animPlayer != null)
        {
            _player.animPlayer.Play("Jump");
        }
    }

    public override void PhysicsUpdate(float delta)
    {
        _player.Motion.Y += _player.GRAVITY * delta;
        _player.Motion.Y = Mathf.Clamp(_player.Motion.Y, -_player.MAXFALLSPEED, _player.MAXFALLSPEED);

        var inputDirectionX = Input.GetActionStrength("droite") - Input.GetActionStrength("gauche");
        _player.Motion.X = Mathf.Lerp(_player.Motion.X, inputDirectionX * _player.MAXSPEED, 0.1f);

        _player.Velocity = _player.Motion;
        _player.MoveAndSlide();

        if (_player.IsOnFloor())
        {
            _hasJumped = false;

            if (Mathf.Abs(inputDirectionX) > 0.1f)
            {
                _stateMachine.TransitionTo("Run");
            }

            else
            {
                _stateMachine.TransitionTo("Idle");
            }
                
        }

        if (inputDirectionX > 0)
        {
            _player.facing_right = true;
        }
        else if (inputDirectionX < 0)
        {
            _player.facing_right = false;
        }
    }

    public override void Exit()
    {
        _hasJumped = false;
    }
}
