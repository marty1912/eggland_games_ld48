using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;

/// <summary>
///  this class represents a scanner that can scan in a specific area
/// </summary>
public class Scanner: Node2D
{

    public int currently_scanning = 0;
    public int maxObjects = 2;
    public float MaxDistance = 1000;
    public float mineralMultiplier = 1;

    public void onScanFinished(ScanInfo scan){
    }

    [Signal]
    public delegate void addedMinerals(float amount);
    public void addMinerals(float amount){
        // TODO emit signal
        amount *= mineralMultiplier;
        GD.Print("added minerals", amount);
        EmitSignal(nameof(addedMinerals),amount);
        PlayerStats.Instance.MineralCount +=(int) Mathf.Floor(amount);

    }
    
    /// <summary>
    /// this function scans a radius.  
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="space_state"></param>
    public List<IScannable> ScanRadius(float radius, Physics2DDirectSpaceState space_state, Godot.Collections.Array exceptions,int n_rays=36){

        List<Dictionary> results = new List<Dictionary>();
        for (int i = 0; i < n_rays;i++){
            Vector2 endpoint = new Vector2(this.GlobalPosition.x,this.GlobalPosition.y);
            float angle = ((float)i /(float) n_rays) * Mathf.Pi * 2f;
            endpoint.x += radius * Mathf.Cos(angle);
            endpoint.y -= radius * Mathf.Sin(angle);
            Dictionary result = space_state.IntersectRay(this.GlobalPosition, endpoint,exceptions);
            if(result.Count == 0){
                continue;
            }
            results.Add(result);
        }


        List<IScannable> unique_objects = new List<IScannable>();
        foreach (Dictionary result in results)
        {
            Node2D obj = (Node2D) result["collider"];
            //GD.Print("object found:", obj, "is IScannable:", obj is IScannable);
            if (obj is IScannable)
            {
            }
            if(obj is IScannable && !unique_objects.Contains((IScannable)obj)){
                unique_objects.Add((IScannable)obj);
            }
        }
        return unique_objects;

        
    }
    public void scanObjects(List<IScannable> to_scan){

        foreach(IScannable obj in to_scan){
            scanObject(obj);
        }
    }

    public void scanObject(IScannable to_scan){
        to_scan.setScanState(new ScanStateActive(to_scan,this));
    }

}