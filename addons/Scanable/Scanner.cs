using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;

/// <summary>
///  this class represents a scanner that can scan in a specific area
/// </summary>
public class Scanner: Node2D
{
    public float MaxDistance = 100;
    public void onScanFinished(ScanInfo scan){

    }
    /// <summary>
    /// this function scans a radius.  
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="space_state"></param>
    public void ScanRadius(float radius,Physics2DDirectSpaceState space_state,int n_rays=100){

        List<Dictionary> results = new List<Dictionary>();
        for (int i = 0; i < n_rays;i++){
            Vector2 endpoint = new Vector2(this.GlobalPosition.x,this.GlobalPosition.y);
            endpoint.x += Mathf.Cos((i / n_rays) * (Mathf.Pi * 2))*radius;
            endpoint.y -= Mathf.Sin((i / n_rays) * (Mathf.Pi * 2))*radius;
            Dictionary result = space_state.IntersectRay(this.GlobalPosition, endpoint);
            results.Add(result);
        }

        List<IScannable> unique_objects = new List<IScannable>();
        foreach (Dictionary result in results)
        {
            Node2D obj = (Node2D) result["collider"];
            GD.Print("object found:", obj, "is IScannable:", obj is IScannable);
            if (obj is IScannable)
            {
                GD.Print("object found:", obj, "is IScannable:", obj is IScannable);
            }
            if(obj is IScannable && !unique_objects.Contains((IScannable)obj)){
                unique_objects.Add((IScannable)obj);
            }
        }

        foreach(IScannable obj in unique_objects){
            GD.Print("scannable object found:", obj);
            scanObject(obj);
        }
    }
    public void scanObject(IScannable to_scan){
        to_scan.setScanState(new ScanStateActive(to_scan,this));
    }

}