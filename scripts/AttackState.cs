using Godot;
using System;
using System.Collections.Generic;

public partial class AttackState : PlayerState
{
    public override void Enter(Dictionary<string, bool> message = null)
    {

        if (_player.animPlayer != null)
        {
            _player.animPlayer.Play("Attack");
            _player.animPlayer.Connect("animation_finished", new Callable(this, nameof(OnAnimationFinished)));
        }
    }

    public override void Exit()
    {
        if (_player.animPlayer != null)
        {
            _player.animPlayer.Disconnect("animation_finished", new Callable(this, nameof(OnAnimationFinished)));
        }
    }

    public override void PhysicsUpdate(float delta)
    {
        _player.Motion = _player.Motion.Lerp(Vector2.Zero, 0.2f);
        _player.Velocity = _player.Motion;
    }

    private void OnAnimationFinished(string animName)
    {
		if (animName != "Attack")
		{
			return;
		}
            
        float horiz = Input.GetActionStrength("droite") - Input.GetActionStrength("gauche");

		if (Mathf.Abs(horiz) > 0.1f)
		{
			_stateMachine.TransitionTo("Run");
		}

		else
		{
			_stateMachine.TransitionTo("Idle");
		}
    }
}