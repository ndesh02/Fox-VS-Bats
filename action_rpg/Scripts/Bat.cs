using Godot;
using System;
using System.Collections.Generic;

public class Bat : KinematicBody2D
{
    Vector2 knockback = Vector2.Zero;
   // [Export] NodePath statsPath;
    public Node stats;
    
    public float batHealth;
    public static int numberOfBats = 1;
    [Export] public float batMaxHealth = 2;
    public PackedScene HitEffect;
    [Export] public int acceleration = 300;
    [Export] public int maxSpeed = 50;
    [Export] public int friction = 200;
    [Export] NodePath spritePath;
    [Export] NodePath softCollisionPath;
    SoftCollision softCollision;
    AnimatedSprite sprite;
    KinematicBody2D player;
    Random r = new Random();
    [Export] NodePath wanderControllerPath;
    WanderController wanderController;
    [Export] int targetRange=4;
    public enum states{
        IDLE,
        WANDER,
        CHASE
    }

    public states state = states.CHASE;
    public Vector2 velocity = Vector2.Zero;

    public override void _Ready()
    {
        batHealth = batMaxHealth;
        HitEffect = GD.Load("res://Scenes/HitEffect.tscn") as PackedScene;
        sprite = GetNode<AnimatedSprite>(spritePath) as AnimatedSprite;
        softCollision = GetNode<SoftCollision>(softCollisionPath) as SoftCollision;
        wanderController = GetNode<WanderController>(wanderControllerPath) as WanderController;
        states[] list = {states.IDLE, states.WANDER};
        state = pickRandState(list, 2);
        
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    public override void _PhysicsProcess(float delta)
    {
        knockback = knockback.MoveToward(Vector2.Zero, friction*delta);
        knockback = MoveAndSlide(knockback);

        switch (state){
            case states.IDLE:
                
                velocity = velocity.MoveToward(Vector2.Zero, friction*delta);
                seekPlayer();

                if(wanderController.timeLeft() == 0){
                    states[] list = {states.IDLE, states.WANDER};
                    state = pickRandState(list, 2);
                    wanderController.startTimer(r.Next(1,4));
                }
                break;
            case states.WANDER:
                seekPlayer();
                if(wanderController.timeLeft() == 0){
                    states[] list = {states.IDLE, states.WANDER};
                    state = pickRandState(list, 2);
                    wanderController.startTimer(r.Next(1,4));
                }
                Vector2 direction = GlobalPosition.DirectionTo(wanderController.targetPos);
                velocity = velocity.MoveToward(maxSpeed*direction, acceleration*delta);
                
                if(GlobalPosition.DistanceTo(wanderController.targetPos) <= targetRange){
                    states[] list = {states.IDLE, states.WANDER};
                    state = pickRandState(list, 2);
                    wanderController.startTimer(r.Next(1,4));
                }
                break;

            case states.CHASE:
                if(player!=null){
                    Vector2 new_direction = GlobalPosition.DirectionTo(player.GlobalPosition);
                    velocity = velocity.MoveToward(maxSpeed*new_direction, acceleration*delta);
                }
                else{
                    state = states.IDLE;
                }
                break;
            
        }
        sprite.FlipH = velocity.x < 0;


        if(softCollision.isColliding()){
            velocity +=softCollision.getPushVector()*delta*400;
        }
        velocity = MoveAndSlide(velocity); 
    }
    public void seekPlayer(){
        if (canSeePlayer()){
            state = states.CHASE;
        }
    }
    public void _on_HurtBox_area_entered(Area2D area){
        knockback = SwordHitBox.knockbackVector *120;
        batHealth-=SwordHitBox.damage;
        Node2D effect = HitEffect.Instance() as Node2D;
        Node2D main = GetTree().CurrentScene as Node2D;
        main.AddChild(effect);
        effect.GlobalPosition = GlobalPosition;
       // QueueFree();
        if(batHealth <=0){
            PackedScene EnemyEffect = GD.Load("res://Scenes/EnemyDeathEffect.tscn") as PackedScene;
            Node2D enemyEffect = EnemyEffect.Instance() as Node2D;
            Node2D world = GetTree().CurrentScene as Node2D;
            GetParent().AddChild(enemyEffect);
            enemyEffect.GlobalPosition = GlobalPosition;
            numberOfBats--;
            QueueFree();
        }
    }

    public void _on_PlayerDetectionZone_body_entered(Node body){
        player = body as KinematicBody2D;
    }
    
    public void _on_PlayerDetectionZone_body_exited(Node body){
        player = null;
    }
    
    public bool canSeePlayer(){
        return player!=null;
    }

    public states pickRandState(states[] state_list, int size){
        int index = r.Next(0,size);
        return state_list[index];
    }

    
}
