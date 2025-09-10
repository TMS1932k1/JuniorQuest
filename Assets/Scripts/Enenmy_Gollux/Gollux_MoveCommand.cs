using UnityEngine;

public class Gollux_MoveCommand : ICommand
{
    public Vector3 targetPos;

    public Gollux_MoveCommand(Vector3 targetPos)
    {
        this.targetPos = targetPos;
    }

    public void Execute(Gollux gollux)
    {
        // Move to target position
        gollux.MoveTo(targetPos);
    }
}
