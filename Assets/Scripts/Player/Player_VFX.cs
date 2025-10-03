using System.Collections;
using UnityEngine;

public class Player_VFX : Entity_VFX
{
    private Player player;

    [Header("Level Up")]
    [SerializeField] UI_LevelUp lvUpUI;
    [SerializeField] float durationShow;
    [SerializeField] float showSpeed = 1f;


    [Header("Comeback skill")]
    [SerializeField] GameObject comebackVFX;

    [Header("Invisibility skill")]
    [SerializeField] GameObject invisibilityVFX;

    [Header("Shield Barrier skill")]
    [SerializeField] GameObject shieldBarrierVFX;

    [Header("Battle Cry skill")]
    [SerializeField] GameObject battleCryVFX;

    [Header("Ice Prison skill")]
    [SerializeField] GameObject icePrisonVFX;

    [Header("Fire Blade skill")]
    [SerializeField] GameObject fireBladeVFX;

    [Header("Line Arm skill")]
    [SerializeField] RectTransform lineArmUI;


    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<Player>();
    }

    public void ShowLevelUpVFX(int level)
    {
        lvUpUI.ShowText(level, durationShow, showSpeed);
    }

    public void ShowComebackVFX()
    {
        comebackVFX.SetActive(true);
        Invoke(nameof(HideComebackVFX), 1f);
    }

    private void HideComebackVFX()
    {
        comebackVFX.SetActive(false);
    }

    public void ShowIcePrisonVFX()
    {
        icePrisonVFX.SetActive(true);
        Invoke(nameof(HideIcePrisonVFX), 1f);
    }

    private void HideIcePrisonVFX()
    {
        icePrisonVFX.SetActive(false);
    }

    public void ShowInvisibilityVFX()
    {
        invisibilityVFX.SetActive(true);
        Invoke(nameof(HideInvisibilityVFX), 1f);
    }

    private void HideInvisibilityVFX()
    {
        invisibilityVFX.SetActive(false);
    }

    public void ShowShieldBarrierVFX(float durationShow)
    {
        shieldBarrierVFX.SetActive(true);
        Invoke(nameof(HideShieldBarrierVFX), durationShow);
    }

    private void HideShieldBarrierVFX()
    {
        shieldBarrierVFX.SetActive(false);
    }

    public void ShowBattleCryVFX(float durationShow)
    {
        battleCryVFX.SetActive(true);
        Invoke(nameof(HideBattleCryVFX), durationShow);
    }

    private void HideBattleCryVFX()
    {
        battleCryVFX.SetActive(false);
    }

    public void ShowFireBladeVFX()
    {
        fireBladeVFX.SetActive(true);
        lineArmUI.gameObject.SetActive(true);
    }

    public void HideFireBladeVFX()
    {
        fireBladeVFX.SetActive(false);
        lineArmUI.gameObject.SetActive(false);
    }

    public void SetLineArmRotate(out float angleZ)
    {
        if (Application.isMobilePlatform) // Get position of stick
        {
            angleZ = Mathf.Atan2(player.moveInput.y, player.moveInput.x) * Mathf.Rad2Deg;
        }
        else // Get position of mouse
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;

            Vector3 dir = mouseWorldPos - lineArmUI.position;

            angleZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        }

        lineArmUI.rotation = Quaternion.Euler(0, 0, angleZ);
    }

    public void SetFadePlayer(float fadePercent)
    {
        Color c = sr.color;
        c.a = fadePercent;
        sr.color = c;
    }

    public override void ResetVFX()
    {
        base.ResetVFX();

        HideComebackVFX();
        HideInvisibilityVFX();
        HideShieldBarrierVFX();
        HideBattleCryVFX();
        HideIcePrisonVFX();
        HideFireBladeVFX();
    }
}
