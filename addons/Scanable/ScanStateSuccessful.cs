using System;
using Godot;
public class ScanStateSuccesful:ScanState{


    public float scantime = 0.5f;
    public static Shader shader = ResourceLoader.Load("res://scannables/Scannable_success.shader") as Shader;
    public ScanStateSuccesful(IScannable parent,Scanner scanner,ScanLaser laser) : base(parent)
    {
        this.laser = laser;
        this.scanner = scanner;
    }
    override public void enter(){

        Sprite sprite = (Sprite)(this.parent.GetMyNode2D().GetNode(this.parent.getSpriteNode()));
        ShaderMaterial material = new ShaderMaterial();
        material.Shader = shader;
        sprite.Material = material;
        this.scanner.AddChild(laser);
        laser.dissapear();
        this.scanner.addMinerals(this.parent.getMinerals());
    }

    override public void exit(){
        Sprite sprite = (Sprite)(this.parent.GetMyNode2D().GetNode(this.parent.getSpriteNode()));
        sprite.Material = null;
        this.scanner.RemoveChild(laser);
        // destoy our parent..
        Node2D par = this.parent.GetMyNode2D();
        par.QueueFree();
        //par.GetParent().RemoveChild(par);
    }
    override public void PhysicsProcess(float delta){
        scantime -= delta;
        if(scantime<0){
            // finish scan
            this.parent.setScanState(new ScanStateInactive(parent));
        }
        setLaserPosition();

    }


}