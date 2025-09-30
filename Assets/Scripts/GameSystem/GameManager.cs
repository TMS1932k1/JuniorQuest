using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [SerializeField] List<SceneSO> scenes;
    private string currentScene;


    [SerializeField] Image fadeScreen;
    private Coroutine sceneToMainMenuCoroutine;
    private Coroutine ChangeToSceneCoroutine;
    private Coroutine mainMenuToSceneCoroutine;
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

        currentScene = SceneNameStrings.SCENE_MAIN_MENU;
    }

    private void Start()
    {
        PlayBgmAudio();
    }

    public void SceneToMainMenu()
    {
        if (sceneToMainMenuCoroutine != null)
            StopCoroutine(sceneToMainMenuCoroutine);

        sceneToMainMenuCoroutine = StartCoroutine(SceneToMainMenuCo());
    }

    private IEnumerator SceneToMainMenuCo()
    {
        FadeScreen(true);
        yield return new WaitForSeconds(1f);

        currentScene = SceneNameStrings.SCENE_MAIN_MENU;
        SceneManager.LoadScene(currentScene);

        FadeScreen(false);
        PlayBgmAudio();
    }

    public void MainMenuToScene()
    {
        if (mainMenuToSceneCoroutine != null)
            StopCoroutine(mainMenuToSceneCoroutine);

        mainMenuToSceneCoroutine = StartCoroutine(MainMenuToSceneCo());
    }

    /// <summary>
    /// Change from (MainMenu) to (sceneName)
    /// If (sceneName) is null or empty => Load new game (Change to Lv0)
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    private IEnumerator MainMenuToSceneCo()
    {
        FadeScreen(true);
        SaveManager.instance.LoadGame();

        // Check saved data to load scene
        yield return new WaitForSeconds(1f);
        string sceneName = SaveManager.instance.GetGameData().sceneName;
        currentScene = !string.IsNullOrEmpty(sceneName) ? sceneName : SceneNameStrings.SCENE_LV0;
        SceneManager.LoadScene(currentScene);

        yield return new WaitForSeconds(0.5f);
        if (string.IsNullOrEmpty(sceneName))
            SaveManager.instance.SaveGame();
        else
            SaveManager.instance.LoadGame();

        FadeScreen(false);
        PlayBgmAudio();
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
        currentScene = sceneName;

        // Change scene
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);

        // Save position
        yield return new WaitForSeconds(0.5f);
        Vector3 pos = GetWayPoint(connectType);
        SaveManager.instance.SaveWayPoint(sceneName, pos);

        // Load data
        yield return new WaitForSeconds(0.5f);
        SaveManager.instance.LoadGame();

        FadeScreen(false);
        PlayBgmAudio();
    }

    private void FadeScreen(bool enable, float duration = 1f)
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

    private SceneSO GetSceneSO(string sceneName)
    {
        return scenes.FirstOrDefault(sceneData => sceneData.sceneName == sceneName);
    }

    public void PlayBgmAudio(bool isBattle = false)
    {
        SceneSO sceneData = GetSceneSO(currentScene);

        if (sceneData != null)
            AudioManager.instance.PlayBgmAudioClip(isBattle ? sceneData.battleAudioName : sceneData.normalAudioName);
    }
}
