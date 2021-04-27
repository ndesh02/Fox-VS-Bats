using Godot;
using System;

public class SwordHitBox : Area2D
{
    public static Vector2 knockbackVector  = Vector2.Zero;
    public static float damage = HitBox.damageSword;
}
