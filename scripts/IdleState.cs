using Godot;
using System;
using System.Collections.Generic;

public partial class IdleState : PlayerState
{
    // Méthode appelée à l'entrée de l'état Idle
    public override void Enter(Dictionary<string, bool> message = null)
    {
        // Ralentit le mouvement du joueur vers zéro
        _player.Motion = _player.Motion.Lerp(Vector2.Zero, 0.2f);

        // Joue l'animation "Idle" si l'AnimationPlayer est disponible
        if (_player.animPlayer != null)
        {
            _player.animPlayer.Play("Idle");
        }

        // Affiche dans la console le nom de l'état actuel
        GD.Print("Entering : " + GetType().Name);
    }

    // Mise à jour physique appelée à chaque frame physique
    public override void PhysicsUpdate(float delta)
    {
        // Continue de ralentir le mouvement vers zéro
        _player.Motion = _player.Motion.Lerp(Vector2.Zero, 0.2f);

        // Applique le mouvement au vecteur de vitesse
        _player.Velocity = _player.Motion;

        // Si une touche directionnelle est pressée, transition vers l'état "Run"
        if (Input.IsActionPressed("droite") || Input.IsActionPressed("gauche"))
        {
            _stateMachine.TransitionTo("Run");
        }

        if (Input.IsActionJustPressed("ui_attack"))
        {
            _stateMachine.TransitionTo("AttackState");
            return;
        }

    }
}