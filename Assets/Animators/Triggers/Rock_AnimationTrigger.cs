using UnityEngine;

public class Rock_AnimationTrigger : MonoBehaviour
{
    private Gollux_Rock rock;

    private void Awake()
    {
        rock = GetComponentInParent<Gollux_Rock>();
    }

    private void RockDrop()
    {
        rock.Drop();
    }

    private void Hide()
    {
        rock.Hide();
    }
}
