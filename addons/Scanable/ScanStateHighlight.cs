using Godot;
using System;

public class ScanStateHighlight:ScanState{
    public float scantime = 0 ;
    public float scantime_total = 0.5f;

    public ShaderMaterial material;
    public static Shader shader = ResourceLoader.Load("res://scannables/Scannable_highlight.shader") as Shader;
     public ScanStateHighlight(IScannable parent,Scanner scanner) : base(parent)
    {
        this.scanner = scanner;
    }
    override public void enter(){
        Sprite sprite = (Sprite)(this.parent.GetMyNode2D().GetNode(this.parent.getSpriteNode()));
        material = new ShaderMaterial();
        material.Shader = shader;
        sprite.Material = material;


    }
    override public void exit(){
        Sprite sprite = (Sprite)(this.parent.GetMyNode2D().GetNode(this.parent.getSpriteNode()));
        sprite.Material = null;
    }
    override public void PhysicsProcess(float delta){
        checkAbort();
    }
    private void checkAbort(){
        Node2D parent_node = parent.GetMyNode2D();
        float distance = parent_node.GlobalPosition.DistanceTo(scanner.GlobalPosition);
        if(distance > scanner.MaxDistance){
            this.parent.setScanState(new ScanStateInactive(parent));
        }

    }

    private void checkForTimeout(float delta){
        scantime += delta;
        if(scantime >= scantime_total){
            // finish scan
            this.parent.setScanState(new ScanStateInactive(parent));
        }
    }
    


}