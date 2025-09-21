using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Object_CheckPoint : MonoBehaviour, ISaveable
{
    [SerializeField] TextMeshProUGUI saveText;
    [SerializeField] Light2D lightOn;


    private Object_CheckPoint[] checkPoints;
    private bool isShowingText;
    public bool isActivity;


    private Coroutine lightVFXCoroutine;
    private float lightPercent;
    private float lightInner;
    private float lightOuter;
    private bool isPlayingVFX;


    private void Awake()
    {
        checkPoints = FindObjectsByType<Object_CheckPoint>(FindObjectsSortMode.None);
    }

    private void Start()
    {
        isShowingText = false;
        isPlayingVFX = false;
        lightInner = lightOn.pointLightInnerRadius;
        lightOuter = lightOn.pointLightOuterRadius;

        EnableCheckPoint(false);
        saveText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isShowingText && !isPlayingVFX)
        {
            OffAllCheckPoint();
            EnableCheckPoint(true);
            PlayLightVFX();

            SaveManager.instance.SaveGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(LayerStrings.PLAYER_LAYER) && !isShowingText)
        {
            isShowingText = true;
            saveText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(LayerStrings.PLAYER_LAYER))
        {
            isShowingText = false;
            saveText.gameObject.SetActive(false);
        }
    }

    public void EnableCheckPoint(bool enable)
    {
        if (!enable)
            lightPercent = 0f;

        isActivity = enable;
        lightOn.gameObject.SetActive(enable);
    }

    private void OffAllCheckPoint()
    {
        foreach (Object_CheckPoint checkPoint in checkPoints)
        {
            if (checkPoint != this)
                checkPoint.EnableCheckPoint(false);
        }
    }

    private void PlayLightVFX()
    {
        if (lightVFXCoroutine != null)
            StopCoroutine(lightVFXCoroutine);

        lightVFXCoroutine = StartCoroutine(LightVFXCo());
    }

    private IEnumerator LightVFXCo()
    {
        isPlayingVFX = true;
        saveText.gameObject.SetActive(false);

        while (lightPercent > 0.5f)
        {
            lightPercent -= Time.deltaTime;

            lightOn.pointLightInnerRadius = lightInner * lightPercent;
            lightOn.pointLightOuterRadius = lightOuter * lightPercent;

            yield return null;
        }

        while (lightPercent < 1f)
        {
            lightPercent += Time.deltaTime;

            lightOn.pointLightInnerRadius = lightInner * lightPercent;
            lightOn.pointLightOuterRadius = lightOuter * lightPercent;

            yield return null;
        }

        saveText.gameObject.SetActive(true);
        isPlayingVFX = false;
    }

    public void SaveData(ref GameData gameData)
    {
        Debug.Log($"SAVE_MANAGER: Save {gameObject.name} ({transform.position})");

        if (isActivity)
            gameData.position = transform.position;
    }

    public void LoadData(GameData gameData)
    {
        Debug.Log($"SAVE_MANAGER: Load {gameObject.name} ({transform.position})");

        if (gameData.position == transform.position)
        {
            EnableCheckPoint(true);
            PlayLightVFX();
        }
    }
}
