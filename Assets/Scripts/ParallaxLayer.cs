using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    [SerializeField] Transform transform;
    [SerializeField] float speedMutiplier;

    private float widthLayer;
    private float halfWidthLayer;

    public void CalculateWidthLayer()
    {
        widthLayer = transform.GetComponent<SpriteRenderer>().bounds.size.x;
        halfWidthLayer = widthLayer / 2;
    }

    public void MovePosition(float distance)
    {
        transform.position += Vector3.right * (distance * speedMutiplier);
    }

    /// <summary>
    /// If layer out arena from (leftPositionX) to (rightPositionX)
    /// then move layer right or left with width of layer to create loop
    /// </summary>
    /// <param name="leftPositionX">Left of current position of mainCamera</param>
    /// <param name="rightPositionX">Right of current position of mainCamera</param>
    public void LoopLayer(float leftPositionX, float rightPositionX)
    {
        if (transform.position.x + halfWidthLayer < leftPositionX) // To right
        {
            transform.position += Vector3.right * widthLayer;
        }
        else if (transform.position.x - halfWidthLayer > rightPositionX) // To left
        {
            transform.position += Vector3.left * widthLayer;
        }
    }
}
