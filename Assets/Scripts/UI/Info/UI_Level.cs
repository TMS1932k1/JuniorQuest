using TMPro;
using UnityEngine;

public class UI_Level : MonoBehaviour
{
    [SerializeField] Player_XP xp;

    private TextMeshProUGUI levelText;

    void Awake()
    {
        levelText = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        SetDisplayLevel();
    }

    void Update()
    {
        if (xp.newLevelUp)
        {
            SetDisplayLevel();
            xp.newLevelUp = false;
        }
    }

    private void SetDisplayLevel()
    {
        levelText.text = $"Level {xp.GetLevel()}";
    }
}
