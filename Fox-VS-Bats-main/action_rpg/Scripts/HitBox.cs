using Godot;
using System;

public class HitBox : Area2D
{
    [Export] public float damage = 1;
    public static float damageSword = 1;
    public override void _Ready()
    {
        damageSword = damage;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
