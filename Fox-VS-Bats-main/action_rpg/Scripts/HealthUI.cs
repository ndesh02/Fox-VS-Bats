using Godot;
using System;

public class HealthUI : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public int maxHearts = Player.playerMaxHealth;
    public float hearts;
    [Export] NodePath heartUIEmptyPath;
    [Export] NodePath heartUIFullPath;
    public TextureRect heartUIEmpty;
    public TextureRect heartUIFull;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        heartUIEmpty = GetNode<TextureRect>(heartUIEmptyPath) as TextureRect;
        heartUIFull = GetNode<TextureRect>(heartUIFullPath) as TextureRect;
        hearts = maxHearts;
        heartUIEmpty.RectSize = new Vector2(hearts*15, heartUIEmpty.RectSize.y);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta){
        hearts = Player.playerHealth;
        heartUIFull.RectSize = new Vector2(hearts*15, heartUIFull.RectSize.y);
        if(hearts<=0){
            heartUIFull.Hide();
        }
    }

}
