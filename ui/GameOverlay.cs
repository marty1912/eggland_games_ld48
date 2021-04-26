using Godot;

public class GameOverlay : NinePatchRect
{

    public Tween tween; 
    public override void _Ready()
    {
        Visible = false;
        GlobalEvents.Instance.Connect("GameOver", this, nameof(onGameOver));
        tween = (Tween)GetNode("Tween");

    }
    public void onGameOver(){
        if (!this.Visible)
        {
            this.Visible = true;
            tween.StopAll();
            tween.InterpolateProperty(this, "modulate:a", 0, 1, 3f);
            tween.Start();
        }
    }

}
