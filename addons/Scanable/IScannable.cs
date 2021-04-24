using Godot;
using System;

public interface IScannable{
    NodePath getSpriteNode();
    void setScanState(ScanState next_state);
    Node2D GetMyNode2D();
}

public abstract class ScanState{
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
}

public class ScanStateInactive:ScanState{

    public ScanStateInactive(IScannable parent):base(parent){
    }
   override public void enter(){

    }
    override public void exit(){

    }
    override public void PhysicsProcess(float delta){

    }


}
public class ScanStateActive:ScanState{
    public Scanner scanner;

    public float scantime = 2;
    static Shader shader = ResourceLoader.Load("res://scannables/Scannable_scanning.shader") as Shader;
    public ScanStateActive(IScannable parent,Scanner scanner) : base(parent)
    {
        this.scanner = scanner;
    }
    override public void enter(){
        Sprite sprite = (Sprite)(this.parent.GetMyNode2D().GetNode(this.parent.getSpriteNode()));
        GD.Print("being scanned..");
        ShaderMaterial material = new ShaderMaterial();
        material.Shader = shader;
        sprite.Material = material;
    }
    override public void exit(){

        GD.Print("stopped scanning.");
        Sprite sprite = (Sprite)(this.parent.GetMyNode2D().GetNode(this.parent.getSpriteNode()));
        sprite.Material = null;

    }
    override public void PhysicsProcess(float delta){
        Node2D parent_node = parent.GetMyNode2D();
        float distance = parent_node.GlobalPosition.DistanceTo(scanner.GlobalPosition);
        if(distance > scanner.MaxDistance){
            this.parent.setScanState(new ScanStateAborted(parent,scanner));
        }
        scantime -= delta;
        if(scantime<0){
            // finish scan
            this.parent.setScanState(new ScanStateSuccesful(parent,scanner));
        }

    }


}

public class ScanStateSuccesful:ScanState{
    public Scanner scanner;

    public float scantime = 0.5f;
    static Shader shader = ResourceLoader.Load("res://scannables/Scannable_success.shader") as Shader;
    public ScanStateSuccesful(IScannable parent,Scanner scanner) : base(parent)
    {
        this.scanner = scanner;
    }
    override public void enter(){
        Sprite sprite = (Sprite)(this.parent.GetMyNode2D().GetNode(this.parent.getSpriteNode()));
        GD.Print("scan successful");
        ShaderMaterial material = new ShaderMaterial();
        material.Shader = shader;
        sprite.Material = material;
    }
    override public void exit(){
        GD.Print("stopped scanning.");
        Sprite sprite = (Sprite)(this.parent.GetMyNode2D().GetNode(this.parent.getSpriteNode()));
        sprite.Material = null;
    }
    override public void PhysicsProcess(float delta){
        scantime -= delta;
        if(scantime<0){
            // finish scan
            this.parent.setScanState(new ScanStateInactive(parent));
        }

    }


}
public class ScanStateAborted:ScanState{
    public Scanner scanner;

    public float scantime = 0.5f;
    static Shader shader = ResourceLoader.Load("res://scannables/Scannable_aborted.shader") as Shader;
    public ScanStateAborted(IScannable parent,Scanner scanner) : base(parent)
    {
        this.scanner = scanner;
    }
    override public void enter(){
        Sprite sprite = (Sprite)(this.parent.GetMyNode2D().GetNode(this.parent.getSpriteNode()));
        GD.Print("scan not successful");
        ShaderMaterial material = new ShaderMaterial();
        material.Shader = shader;
        sprite.Material = material;
    }
    override public void exit(){
        GD.Print("stopped scanning.");
        Sprite sprite = (Sprite)(this.parent.GetMyNode2D().GetNode(this.parent.getSpriteNode()));
        sprite.Material = null;
    }
    override public void PhysicsProcess(float delta){
        scantime -= delta;
        if(scantime<0){
            // finish scan
            this.parent.setScanState(new ScanStateInactive(parent));
        }

    }


}