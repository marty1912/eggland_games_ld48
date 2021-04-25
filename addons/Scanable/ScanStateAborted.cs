using System;
using Godot;

public class ScanStateAborted:ScanState{

    public float scantime = 0.5f;
    public static Shader shader = ResourceLoader.Load("res://scannables/Scannable_aborted.shader") as Shader;
    public ScanStateAborted(IScannable parent,Scanner scanner,ScanLaser laser) : base(parent)
    {
        this.scanner = scanner;
        this.laser = laser;
    }
    override public void enter(){
        Sprite sprite = (Sprite)(this.parent.GetMyNode2D().GetNode(this.parent.getSpriteNode()));
        ShaderMaterial material = new ShaderMaterial();

        material.Shader = shader;
        sprite.Material = material;
        this.scanner.AddChild(laser);
        laser.dissapear();
    }
    override public void exit(){
        Sprite sprite = (Sprite)(this.parent.GetMyNode2D().GetNode(this.parent.getSpriteNode()));
        sprite.Material = null;
        scanner.RemoveChild(laser);
    }
    override public void PhysicsProcess(float delta){
        scantime -= delta;
        if(scantime<0){
            // finish scan
            this.parent.setScanState(new ScanStateInactive(parent));
        }

    }


}