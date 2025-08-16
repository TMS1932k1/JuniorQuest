using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] ParallaxLayer[] layers;

    private Camera mainCamera;
    private float lastPositionX;
    private float distanceMove;

    private float halfWitdhCamera;
    private float leftPositonXCamera;
    private float rightPositonXCamera;

    void Awake()
    {
        mainCamera = Camera.main;
        halfWitdhCamera = mainCamera.orthographicSize * mainCamera.aspect;
    }

    void Start()
    {
        CalculateWidthAllLayers();
    }

    void LateUpdate()
    {
        CalculateDistanceMove();
        CalculateLeftAndRightPositionX();

        foreach (ParallaxLayer layer in layers)
        {
            layer.MovePosition(distanceMove);
            layer.LoopLayer(leftPositonXCamera, rightPositonXCamera);
        }
    }

    /// <summary>
    /// Calculate width of all layers
    /// </summary>
    private void CalculateWidthAllLayers()
    {
        foreach (ParallaxLayer layer in layers)
        {
            layer.CalculateWidthLayer();
        }
    }

    /// <summary>
    /// Calculate distance to layer chilren move to (mainCamera)
    /// </summary>
    private void CalculateDistanceMove()
    {
        float currentVelocityX = mainCamera.transform.position.x;
        distanceMove = currentVelocityX - lastPositionX;
        lastPositionX = currentVelocityX;
    }

    /// <summary>
    /// Calculate Left and Right position of (mainCamera)
    /// </summary>
    private void CalculateLeftAndRightPositionX()
    {
        leftPositonXCamera = mainCamera.transform.position.x - halfWitdhCamera;
        rightPositonXCamera = mainCamera.transform.position.x + halfWitdhCamera;
    }
}
