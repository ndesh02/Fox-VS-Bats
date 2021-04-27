using Godot;
using System;

public class HitEffect : AnimatedSprite
{
    [Export] NodePath hitEffectPath;
    public AnimatedSprite hitEffect;
    public override void _Ready()
    {
        hitEffect = GetNode<AnimatedSprite>(hitEffectPath) as AnimatedSprite;
        hitEffect.Frame = 0;
        hitEffect.Play("Animate");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
    public void _on_HitEffect_animation_finished(){
        QueueFree();
    }
}
