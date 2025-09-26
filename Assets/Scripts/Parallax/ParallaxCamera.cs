using UnityEngine;

public class ParallaxCamera : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
