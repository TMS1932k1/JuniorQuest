using UnityEngine;

public class Object_Rope : MonoBehaviour, IBreakable
{
    [SerializeField] Object_HangingPlatform hangingPlatform;

    private bool isBreaked;

    public void Break()
    {
        if (!isBreaked)
        {
            isBreaked = true;
            gameObject.SetActive(false);
            hangingPlatform.DropPlatform();
        }
    }

}
