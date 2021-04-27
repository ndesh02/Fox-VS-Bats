using Godot;
using System;

public class WanderController : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export] int wanderRange = 32;
    public Vector2 startPos;
    public Vector2 targetPos;
    Random r = new Random();
    [Export] NodePath timerPath;
    public Timer timer;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        startPos = GlobalPosition;
        timer = GetNode<Timer>(timerPath) as Timer;
        updateTargetPos();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    
    public void _on_Timer_timeout(){
        updateTargetPos();
    }
    public void updateTargetPos(){
        Vector2 targetVec = new Vector2(r.Next(-wanderRange, wanderRange), r.Next(-wanderRange, wanderRange));
        targetPos = startPos + targetVec;
    }

    public float timeLeft(){
        return timer.TimeLeft;
    }
    public void startTimer(int duration){
        timer.Start(duration);
    }
    
}
