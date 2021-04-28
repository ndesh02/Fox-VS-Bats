using Godot;
using System;

public class NextLevel : Node2D
{
    
    public override void _Ready()
    {
        Global.currentLevel++;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void _on_Timer_timeout(){
        string levelLoad = "res://Scenes/Level"+Global.currentLevel+".tscn";
        GetTree().ChangeScene(levelLoad);
    }
}
