using System.Collections;
using TMPro;
using UnityEngine;

public class UI_LevelUp : MonoBehaviour
{
    //Component
    private CanvasGroup canvasGroup;
    private TextMeshProUGUI levelText;


    private Coroutine displayCoroutine;
    private float currentScale;
    private Vector3 showScale = new Vector3(0.05f, 0.05f, 0.05f);


    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        levelText = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        currentScale = 0f;
    }

    void Update()
    {
        transform.rotation = Quaternion.identity;
    }

    public void ShowText(float level, float durationShow, float showSpeed)
    {
        gameObject.SetActive(true);
        SetText(level);

        if (displayCoroutine != null)
            StopCoroutine(displayCoroutine);

        displayCoroutine = StartCoroutine(ShowTextCo(durationShow, showSpeed));
    }

    private IEnumerator ShowTextCo(float durationShow, float showSpeed)
    {
        while (currentScale < 1)
        {
            currentScale += Time.deltaTime * showSpeed;

            gameObject.transform.localScale = Vector3.Lerp(Vector3.zero, showScale, currentScale); // Scale
            canvasGroup.alpha = Mathf.Lerp(0, 1, currentScale); // Fade

            yield return null;
        }

        yield return new WaitForSeconds(durationShow);
        HideText();
    }

    private void SetText(float level)
    {
        levelText.text = $"UP Level {level}";
    }

    private void HideText()
    {
        gameObject.SetActive(false);
        currentScale = 0f;
    }
}
