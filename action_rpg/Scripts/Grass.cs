using Godot;
using System;

public class Grass : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }
    public void _on_HurtBox_area_entered(Area2D area){
        createGrassEffect();
        QueueFree();
    }
    public void createGrassEffect(){
        PackedScene GrassEffect = GD.Load("res://Scenes/GrassEffect.tscn") as PackedScene;
        Node2D grassEffect = GrassEffect.Instance() as Node2D;
        Node2D world = GetTree().CurrentScene as Node2D;
        GetParent().AddChild(grassEffect);
        grassEffect.GlobalPosition = GlobalPosition;
        QueueFree();
    }
}
