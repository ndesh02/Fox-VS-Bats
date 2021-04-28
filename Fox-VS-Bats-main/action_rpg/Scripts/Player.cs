using Godot;
using System;

public class Player : KinematicBody2D
{

    public static float playerHealth;
    public static int playerMaxHealth=4;
    [Export] NodePath animatePlayerPath;
    [Export] NodePath animateTreePath;
    [Export] NodePath swordHitBoxPath;
    [Export] NodePath hurtSoundPath;
    public AudioStreamPlayer hurtSound;
    public AnimationPlayer animatePlayer;
    public AnimationTree animateTree;
    public AnimationNodeStateMachinePlayback animationNodeStateMachine;
    public Area2D swordHitBox;
    public PackedScene HitEffect;

    Vector2 velocity = new Vector2();
    [Export] float maxSpeed = 80;
    [Export] float acceleration = 500;
    [Export] float friction = 500;
    public float rollSpeed = 100;
    public enum states{
        MOVE,
        ROLL,
        ATTACK
    }
    public states state = states.MOVE;
    public Vector2 rollVector = Vector2.Down;

    public override void _Ready()
    {
        GD.Randomize();
        playerHealth = playerMaxHealth;
        animatePlayer = GetNode<AnimationPlayer>(animatePlayerPath) as AnimationPlayer;
        animateTree = GetNode<AnimationTree>(animateTreePath) as AnimationTree;
        animationNodeStateMachine = animateTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;
        animateTree.Active = true;

        swordHitBox = GetNode<Area2D>(swordHitBoxPath) as Area2D;
        SwordHitBox.knockbackVector = rollVector;
        
        HitEffect = GD.Load("res://Scenes/HitEffect.tscn") as PackedScene;
        hurtSound = GetNode<AudioStreamPlayer>(hurtSoundPath) as AudioStreamPlayer;
    
    
    }

    public override void _PhysicsProcess(float delta)
    {
        //checkHealth();

        switch (state){
            case states.MOVE:
                moveState(delta);
                break;
            case states.ATTACK:
                attackState(delta);
                break;
            case states.ROLL:
                rollState(delta);
                break;
        } 
        //moveState(delta);
    }
    public void moveState(float delta){
        Vector2 inputVector = new Vector2();
        inputVector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        inputVector.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
        inputVector = inputVector.Normalized();
        
        if (inputVector != new Vector2(0,0)){
            rollVector = inputVector;
            SwordHitBox.knockbackVector = inputVector;
            animateTree.Set("parameters/Idle/blend_position", inputVector);
            animateTree.Set("parameters/Run/blend_position", inputVector);
            animateTree.Set("parameters/Attack/blend_position", inputVector);
            animateTree.Set("parameters/Roll/blend_position", inputVector);
            animationNodeStateMachine.Travel("Run");
            
            velocity = velocity.MoveToward(inputVector*maxSpeed, acceleration*delta);
            limitPlayerBounds();
        }
        else{
            animationNodeStateMachine.Travel("Idle");
            velocity = velocity.MoveToward(Vector2.Zero, friction*delta);
            limitPlayerBounds();
        }

        velocity = MoveAndSlide(velocity);
        if (Input.IsActionJustPressed("attack")){
            state = states.ATTACK;
        }
        if(Input.IsActionJustPressed("roll")){
            state = states.ROLL;
        }
    }
    public void attackState(float delta){
        velocity = Vector2.Zero;
        animationNodeStateMachine.Travel("Attack");
    }
    public void rollState(float delta){
        velocity = rollVector*rollSpeed;
        animationNodeStateMachine.Travel("Roll");
        move();
        limitPlayerBounds();
    }
    public void attackFinished(){
        state = states.MOVE;
    }

    public void rollFinished(){
        velocity = Vector2.Zero;
        state = states.MOVE;
    }
    public void move(){
        velocity = MoveAndSlide(velocity);
        limitPlayerBounds();
    }
    
    public void checkHealth(){
        if (playerHealth<=0){
            GetTree().ChangeScene("res://Scenes/GameOver.tscn");
        }
    }
    public void _on_HurtBox_area_entered(Area2D area){

        Node2D effect = HitEffect.Instance() as Node2D;
        Node2D main = GetTree().CurrentScene as Node2D;
        main.AddChild(effect);
        effect.GlobalPosition = GlobalPosition;

        playerHealth--;
        hurtSound.Playing = true;
    }

    public void _on_PlayerHurtSound_finished(){
        hurtSound.Playing = false;
        checkHealth();
    }

    public void limitPlayerBounds(){
        if(GlobalPosition.x<-64){
            GlobalPosition= new Vector2(-64, GlobalPosition.y);
        }
        if(GlobalPosition.x > 448){
            GlobalPosition = new Vector2(448, GlobalPosition.y);
        }
        if(GlobalPosition.y < -64){
            GlobalPosition = new Vector2(GlobalPosition.x, -64);
        }
        if(GlobalPosition.y > 224){
            GlobalPosition = new Vector2(GlobalPosition.x, 224);
        }
    }
}
