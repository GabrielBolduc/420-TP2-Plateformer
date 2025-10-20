using Godot;
using System.Collections.Generic;

public partial class RunState : PlayerState
{
    // Méthode appelée à l'entrée de l'état Run
    public override void Enter(Dictionary<string, bool> message = null)
    {
        // Affiche le nom de l'état courant dans la console
        GD.Print("Entering : " + GetType().Name);

        // Joue l'animation "Run"
        _player.animPlayer.Play("Run");
    }

    // Mise à jour physique appelée à chaque frame physique
    public override void PhysicsUpdate(float delta)
    {
        // Calcule la direction horizontale selon les touches pressées
        var inputDirectionX = Input.GetActionStrength("droite") - Input.GetActionStrength("gauche");

        // Applique l'accélération horizontale
        _player.Motion.X += _player.ACCEL * inputDirectionX;

        // Applique la gravité verticale
        _player.Motion.Y += _player.GRAVITY * delta;

        // Limite la vitesse horizontale
        _player.Motion.X = Mathf.Clamp(_player.Motion.X, -_player.MAXSPEED, _player.MAXSPEED);

        // Met à jour la vélocité du joueur
        _player.Velocity = _player.Motion;

        // Applique le mouvement avec glissement
        _player.MoveAndSlide();

        // Met à jour l'orientation du joueur
        if (inputDirectionX > 0)
            _player.facing_right = true;
        else if (inputDirectionX < 0)
            _player.facing_right = false;

        // Transition vers l’état Jump si la touche de saut est pressée
        if (Input.IsActionJustPressed("ui_jump"))
        {
            var message = new Dictionary<string, bool>() { { "doJump", true } };
            _stateMachine.TransitionTo("Jump", message);
        }
        // Transition vers Idle si aucune direction n’est pressée
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

    }
}