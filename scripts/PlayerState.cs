using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerState : State
{
    protected Player _player;

    public override void _Ready()
    {
        base._Ready();
        _player = Owner as Player;
        if (_player == null)
        {
            throw new InvalidProgramException("Player is null in PlayerState type check.");
        }
            
        GD.Print("PlayerState: Ready -> " + GetType().Name);
    }
}
