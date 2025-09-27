using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Coroutine ChangeToSceneCoroutine;


    [SerializeField] Image fadeScreen;
    private Coroutine fadeScreenCoroutine;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeToScene(string sceneName, EWayPoint_Type connectType)
    {
        if (ChangeToSceneCoroutine != null)
            StopCoroutine(ChangeToSceneCoroutine);

        ChangeToSceneCoroutine = StartCoroutine(ChangeToSceneCo(sceneName, connectType));
    }

    private IEnumerator ChangeToSceneCo(string sceneName, EWayPoint_Type connectType)
    {
        FadeScreen(true);

        // Save data
        SaveManager.instance.SaveGame();
        yield return new WaitForSeconds(1f);

        // Change scene
        SceneManager.LoadScene(sceneName);

        // Save position
        yield return new WaitForSeconds(0.5f);
        Vector3 pos = GetWayPoint(connectType);
        SaveManager.instance.SavePosition(pos);

        // Load data
        yield return new WaitForSeconds(1f);
        SaveManager.instance.LoadGame();

        FadeScreen(false);
    }

    public void FadeScreen(bool enable, float duration = 1f)
    {
        if (fadeScreenCoroutine != null)
            StopCoroutine(fadeScreenCoroutine);

        fadeScreenCoroutine = StartCoroutine(FadeScreenCo(enable, duration));
    }

    private IEnumerator FadeScreenCo(bool enable, float duration)
    {
        float timer = 0;

        if (enable)
            fadeScreen.gameObject.SetActive(true);

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float startFade = enable ? 0 : 1;
            float targetFade = enable ? 1 : 0;

            Color c = fadeScreen.color;
            c.a = Mathf.Lerp(startFade, targetFade, timer / duration);
            fadeScreen.color = c;

            yield return null;
        }

        if (!enable)
            fadeScreen.gameObject.SetActive(false);
    }

    private Vector3 GetWayPoint(EWayPoint_Type connectType)
    {
        Object_WayPoint[] wayPoints = FindObjectsByType<Object_WayPoint>(FindObjectsSortMode.None);

        foreach (Object_WayPoint point in wayPoints)
        {
            if (point.GetWayPointType() == connectType)
                return point.GetTeleportPoint();
        }

        return Vector3.zero;
    }
}
