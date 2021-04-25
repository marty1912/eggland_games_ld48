using Godot;
using System;

public interface IScannable{
    NodePath getSpriteNode();
    void setScanState(ScanState next_state);
    ScanState getScanState();
    Node2D GetMyNode2D();

    float getMinerals();
}
