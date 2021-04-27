using Godot;
using System;

public class EnemyDeathEffect : Node2D
{
    [Export] NodePath enemyDeathPath;
    public AnimatedSprite enemyDeath;
    public override void _Ready()
    {
        enemyDeath = GetNode<AnimatedSprite>(enemyDeathPath) as AnimatedSprite;
        enemyDeath.Frame = 0;
        enemyDeath.Play("Animate");
    }
    public void _on_AnimatedSprite_animation_finished(){
        QueueFree();
    }
}
