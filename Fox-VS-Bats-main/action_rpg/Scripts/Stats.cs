using Godot;
using System;

public class Stats : Node
{
    [Export] public int maxHealth = 1;
    public static float realMaxHealth;
    public static float health;
    public float realHealth; 
    public override void _Ready()
    {
        realMaxHealth = maxHealth;
        health = maxHealth;
        realHealth = health;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
      
      if(health <=0){
          EmitSignal("noHealth");
      }
  }
    [Signal]
    public delegate void noHealth();
}
