using System.Collections;
using UnityEngine;

public class UI_Window : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private Coroutine displayCoroutine;
    private float currentScale;


    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        currentScale = 0f;
        canvasGroup.alpha = currentScale;
        gameObject.transform.localScale = Vector3.zero;
    }

    public void ShowWindow()
    {
        if (displayCoroutine != null)
            StopCoroutine(displayCoroutine);

        displayCoroutine = StartCoroutine(ShowWindowCo());
    }

    public void HideWindow()
    {
        if (displayCoroutine != null)
            StopCoroutine(displayCoroutine);

        displayCoroutine = StartCoroutine(HideWindowCo());
    }

    private IEnumerator ShowWindowCo()
    {
        while (currentScale < 1)
        {
            currentScale += Time.deltaTime * 2f;

            gameObject.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, currentScale); // Scale
            canvasGroup.alpha = Mathf.Lerp(0, 1, currentScale); // Fade

            yield return null;
        }

        Time.timeScale = 0f;
    }

    private IEnumerator HideWindowCo()
    {
        Time.timeScale = 1f;

        while (currentScale > 0)
        {
            currentScale -= Time.deltaTime * 2f;

            gameObject.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, currentScale); // Scale
            canvasGroup.alpha = Mathf.Lerp(0, 1, currentScale); // Fade

            yield return null;
        }
    }
}
