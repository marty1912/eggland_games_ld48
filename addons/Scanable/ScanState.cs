using Godot;
using System;



public abstract class ScanState{
    public Scanner scanner;
    public static PackedScene laser_res = (PackedScene) ResourceLoader.Load("res://scannables/ScanLaser.tscn");
    public ScanLaser laser;

    
    public IScannable parent;
    public ScanState(IScannable parent){
        this.parent = parent;
    }
    public abstract void enter();
    public abstract void exit();
    /// <summary>
    /// this function should be called in the _PhysicsProcess function 
    /// </summary>
    /// <param name="delta"></param>
    public abstract void PhysicsProcess(float delta);

    public void setLaserPosition(){ 
        Vector2 global_pos = parent.GetMyNode2D().GlobalPosition; 
        laser.CastTo = laser.ToLocal(global_pos) ; }

        public void setupLaser(){
            laser = (ScanLaser) laser_res.Instance();
            laser.AddException(scanner.GetParent());
            this.scanner.AddChild(laser);
            setLaserPosition();
        }
}

