using Godot;
using System;

public class ScanLaser : RayCast2D
{


    public Line2D line;
    public CPUParticles2D start_particles;
    public CPUParticles2D end_particles;
    public float line_width = 3;
    Tween tween;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        tween= (Tween) GetNode("Tween");
        line = (Line2D) GetNode("Line2D");
        start_particles = (CPUParticles2D) GetNode("Particles2DStart");
        end_particles = (CPUParticles2D) GetNode("Particles2DEnd");
        line.SetPointPosition(0, this.Transform.origin);
    }
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        Vector2 line_goes_to = CastTo;
        ForceRaycastUpdate();

        if(IsColliding()){
            line_goes_to = ToLocal(GetCollisionPoint());
        }
        line.SetPointPosition(0, this.Transform.origin - this.Transform.origin) ;
        line.SetPointPosition(1, line_goes_to- this.Transform.origin);
        end_particles.Position = line_goes_to;
        start_particles.Position = this.Transform.origin;
        end_particles.Direction = -line_goes_to;
        start_particles.Direction = line_goes_to;


    }

    public void dissapear(){
        tween.StopAll();
        tween.InterpolateProperty(line, "width", line_width, 0, 0.5f);
        tween.Start();
    }
    /// <summary>
    /// makes the line appear. using the tween
    /// </summary>
    public void appear(){
        tween.StopAll();
        tween.InterpolateProperty(line, "width", 0, line_width, 0.5f);
        tween.Start();
    }

}
