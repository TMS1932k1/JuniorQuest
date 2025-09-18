using System.Collections;
using UnityEngine;

public class Skill_Invisibility : Skill_Base
{
    [Header("Details")]
    [Range(0, 1)]
    [SerializeField] float fadePercent;

    private Player player;
    private Coroutine ChangeLayerMaskCoroutine;


    protected override void Awake()
    {
        base.Awake();

        player = GetComponentInParent<Player>();
    }

    public override void PerformSkill()
    {
        base.PerformSkill();

        playerVFX.ShowInvisibilityVFX(); // VFX
        Invisibility();
    }

    private void Invisibility()
    {
        if (ChangeLayerMaskCoroutine != null)
            StopCoroutine(ChangeLayerMaskCoroutine);

        ChangeLayerMaskCoroutine = StartCoroutine(ChangeLayerMaskCo());
    }

    /// <summary>
    /// While duration:
    ///     - Change Layer = "Invisibility"
    ///     - Set fade = (fadePercent)
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeLayerMaskCo()
    {
        player.gameObject.layer = LayerMask.NameToLayer(LayerStrings.INVISIBILITY_LAYER);
        playerVFX.SetFadePlayer(fadePercent);

        yield return new WaitForSeconds(skillData.duration);

        player.gameObject.layer = LayerMask.NameToLayer(LayerStrings.PLAYER_LAYER); ;
        playerVFX.SetFadePlayer(1);
    }
}
