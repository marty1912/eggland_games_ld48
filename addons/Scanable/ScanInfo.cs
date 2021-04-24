using System;
using Godot;


public class ScanInfo{
    static int scan_id_count = 0;
    public ScanInfo(IScannable scanned,int score){
        scan_id = scan_id_count++;
        obj = scanned;
        scanScore = score;
    }
    public int scan_id;
    public IScannable obj;
    public int scanScore;


}