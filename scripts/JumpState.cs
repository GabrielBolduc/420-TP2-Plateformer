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
            // Applique la force de saut
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
        // Application de la gravité
        _player.Motion.Y += _player.GRAVITY * delta;
        _player.Motion.Y = Mathf.Clamp(_player.Motion.Y, -_player.MAXFALLSPEED, _player.MAXFALLSPEED);

        // Mouvement horizontal (en vol)
        var inputDirectionX = Input.GetActionStrength("droite") - Input.GetActionStrength("gauche");
        _player.Motion.X = Mathf.Lerp(_player.Motion.X, inputDirectionX * _player.MAXSPEED, 0.1f);

        // Mise à jour de la vélocité et mouvement
        _player.Velocity = _player.Motion;
        _player.MoveAndSlide();

        // Détection du sol
        if (_player.IsOnFloor())
        {
            _hasJumped = false;
            // Transition selon si on bouge ou non
            if (Mathf.Abs(inputDirectionX) > 0.1f)
                _stateMachine.TransitionTo("Run");
            else
                _stateMachine.TransitionTo("Idle");
        }

        // Flip direction selon mouvement
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
