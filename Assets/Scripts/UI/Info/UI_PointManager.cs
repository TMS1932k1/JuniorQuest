using TMPro;
using UnityEngine;

public class UI_PointManager : MonoBehaviour
{
    [SerializeField] Player player;
    private Entity_Stat stat;
    private Player_XP xp;


    [SerializeField] TextMeshProUGUI pointText;
    private int usedPoint;
    private bool needUpdate = true;

    void Awake()
    {
        stat = player.GetComponent<Entity_Stat>();
        xp = player.GetComponent<Player_XP>();
    }

    void OnEnable()
    {
        needUpdate = true;
    }

    void Update()
    {
        // When update point or level up
        if (needUpdate || xp.newLevelUp)
        {
            SetPointDisplay();
            needUpdate = false;
        }
    }

    public bool CanIncrementStat()
    {
        return xp.GetLevel() - usedPoint > 0;
    }

    public void IncrementStatWithType(EStat_Type type)
    {
        stat.AddModifierWithType(type, "Point", 1);
        usedPoint++;

        needUpdate = true;
    }

    private void SetPointDisplay()
    {
        pointText.text = $"Point:\t{xp.GetLevel() - usedPoint}";
    }

    public void ResetPoint()
    {
        // Reset used point
        usedPoint = 0;

        // Remove Modifer from Point
        stat.RemoveModifierWithType(EStat_Type.Strength, "Point");
        stat.RemoveModifierWithType(EStat_Type.Agility, "Point");
        stat.RemoveModifierWithType(EStat_Type.Vitality, "Point");

        needUpdate = true;
    }
}
