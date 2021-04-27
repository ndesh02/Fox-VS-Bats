using Godot;
using System;

public class HowToPlay : Node2D
{

    public override void _Ready()
    {
        
    }

    public void _on_Play_pressed(){
        GetTree().ChangeScene("res://Scenes/Level1.tscn");
    }

    public void _on_Back_pressed(){
        GetTree().ChangeScene("res://Scenes/Menu.tscn");
    }
}
