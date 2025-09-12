using UnityEngine;

public class Gollux_MoveCommand : ICommand
{
    private Gollux gollux;

    public Gollux_MoveCommand(Gollux gollux)
    {
        this.gollux = gollux;
    }

    public void Execute()
    {
        gollux.Move();
    }

    public void Undo()
    {
        gollux.Idle();
    }
}
