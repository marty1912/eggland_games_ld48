using Godot;
using System;

public interface IScannable{
    NodePath getSpriteNode();
    void setScanState(ScanState next_state);
    ScanState getScanState();
    Node2D GetMyNode2D();

    float getMinerals();
}

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
        Vector2 global_pos = parent.GetMyNode2D().GlobalPosition; laser.CastTo = laser.ToLocal(global_pos) ; }

        public void setupLaser(){
            laser = (ScanLaser) laser_res.Instance();
            laser.AddException(scanner.GetParent());
            setLaserPosition();
            this.scanner.AddChild(laser);
        }
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
    public float scantime = 0 ;
    public float scantime_total = 2f;

    public ShaderMaterial material;
    public static Shader shader = ResourceLoader.Load("res://scannables/Scannable_scanning.shader") as Shader;
     public ScanStateActive(IScannable parent,Scanner scanner) : base(parent)
    {
        this.scanner = scanner;
    }
    override public void enter(){
        Sprite sprite = (Sprite)(this.parent.GetMyNode2D().GetNode(this.parent.getSpriteNode()));
        material = new ShaderMaterial();
        material.Shader = shader;
        sprite.Material = material;
        scanner.currently_scanning++;

        setupLaser();
        laser.appear();

    }
    override public void exit(){
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
        par.GetParent().RemoveChild(par);
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