using Godot;
using System;

public class Menu : Node2D
{

    public override void _Ready()
    {

    }

    public void _on_Play_pressed(){
        GetTree().ChangeScene("res://Scenes/Level1.tscn");
    }
    public void _on_HowToPlay_pressed(){
        GetTree().ChangeScene("res://Scenes/HowToPlay.tscn");
    }
    public void _on_Quit_pressed(){
        GetTree().Quit();
    }
}
