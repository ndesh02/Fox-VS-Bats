using Godot;
using System;

public class Level5 : Node2D
{
    public Timer timer;
    [Export] NodePath timerPath;
    public Label label;
    [Export] NodePath labelPath;
    public override void _Ready()
    {
        Bat.numberOfBats = 15;
        timer = GetNode<Timer>(timerPath) as Timer;
        label = GetNode<Label>(labelPath) as Label;
        label.Text = "Level "+Global.currentLevel;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta){
        if(Bat.numberOfBats <=0){
            if(timer.TimeLeft == 0){
                timer.Start((float)0.88);
            }
            
        }
    }
    public void _on_Timer_timeout(){
        
        GetTree().ChangeScene("res://Scenes/YouWon.tscn");
    }
}