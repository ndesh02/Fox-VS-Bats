using Godot;
using System;

public class GrassEffect : Node2D
{
    [Export] NodePath grassAnimatedEffectPath;
    public AnimatedSprite grassAnimatedEffect;
    public override void _Ready()
    {
        grassAnimatedEffect = GetNode<AnimatedSprite>(grassAnimatedEffectPath) as AnimatedSprite;
        grassAnimatedEffect.Frame = 0;
        grassAnimatedEffect.Play("Animate");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
    public void _on_AnimatedSprite_animation_finished(){
        QueueFree();
    }
}
