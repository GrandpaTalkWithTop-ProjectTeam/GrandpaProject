using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public Slider loadingBar;

    public string LoadScene;

    void Start()
    {
        // 로딩 함수 호출
        LoadSceneAsync(LoadScene);
    }

    void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
    }

    IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncOperation.isDone)
        {
            // 로딩 바에 진행 상황 반영
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            loadingBar.value = progress;

            yield return null; // 다음 프레임까지 대기
        }
    }
}