using System;
using Godot;
public class ScanStateActive:ScanState{
    public float scantime = 0 ;
    public float scantime_total = 2f;

    public ShaderMaterial material;
    public static Shader shader = ResourceLoader.Load("res://scannables/Scannable_scanning.shader") as Shader;
     public ScanStateActive(IScannable parent,Scanner scanner) : base(parent)
    {
        this.scanner = scanner;
    }
    override public void enter(){
        GD.Print("enter scanstate active");
        Sprite sprite = (Sprite)(this.parent.GetMyNode2D().GetNode(this.parent.getSpriteNode()));
        material = new ShaderMaterial();
        material.Shader = shader;
        sprite.Material = material;
        scanner.currently_scanning++;

        setupLaser();
        laser.appear();

    }
    override public void exit(){

        GD.Print("exit scanstate active");
        Sprite sprite = (Sprite)(this.parent.GetMyNode2D().GetNode(this.parent.getSpriteNode()));
        sprite.Material = null;
        this.scanner.RemoveChild(laser);
        scanner.currently_scanning--;
    }
    override public void PhysicsProcess(float delta){
        checkAbort();
        setLaserPosition();
        checkForTimeout(delta);
    }
    private void checkAbort(){
        Node2D parent_node = parent.GetMyNode2D();
        float distance = parent_node.GlobalPosition.DistanceTo(scanner.GlobalPosition);
        if(distance > scanner.MaxDistance){
            this.parent.setScanState(new ScanStateAborted(parent,scanner,laser));
        }
        if(laser.IsColliding() && laser.GetCollider() != this.parent.GetMyNode2D()){
            this.parent.setScanState(new ScanStateAborted(parent,scanner,laser));
        }

    }

    private void checkForTimeout(float delta){
        scantime += delta;
        material.SetShaderParam("dissolve_value", 1 - (scantime / scantime_total) +0.4);
        if(scantime >= scantime_total){
            // finish scan
           this.parent.setScanState(new ScanStateSuccesful(parent,scanner,laser));
        }
    }
    


}



