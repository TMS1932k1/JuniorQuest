using UnityEngine;

public class Gollux_CommandControler : MonoBehaviour
{
    private Gollux gollux;

    private void Awake()
    {
        gollux = GetComponent<Gollux>();
    }

    void Start()
    {
        InvokeRepeating(nameof(DecideNextAction), 1f, 2f);
    }

    private void DecideNextAction()
    {
        if (gollux.playerTrans != null)
        {
            Vector2 movePos = new Vector2(gollux.playerTrans.position.x, gollux.transform.position.y);
            gollux.AddCommand(new Gollux_MoveCommand(movePos));
        }
    }
}
