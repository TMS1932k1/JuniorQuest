using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private float value;

    public float GetValue() => value;
}
