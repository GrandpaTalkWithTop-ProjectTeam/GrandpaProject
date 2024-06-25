using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage; // 페이드 인/아웃에 사용될 이미지
    public float fadeSpeed = 0.5f; // 페이드 속도

    private void Start()
    {
        // 초기 알파값 설정 (투명도)
        fadeImage.color = new Color(0f, 0f, 0f, 1f); // 검은색 배경에서 시작 (투명도 1)
        StartCoroutine(FadeIn());
    }

    // 페이드 인 코루틴
    IEnumerator FadeIn()
    {
        while (fadeImage.color.a > 0)
        {
            // 알파값 감소 (투명도 증가)
            fadeImage.color = new Color(0f, 0f, 0f, fadeImage.color.a - fadeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    // 페이드 아웃 코루틴
    IEnumerator FadeOut()
    {
        while (fadeImage.color.a < 1)
        {
            // 알파값 증가 (투명도 감소)
            fadeImage.color = new Color(0f, 0f, 0f, fadeImage.color.a + fadeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    // 다른 스크립트에서 호출하여 페이드 인 시작
    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    // 다른 스크립트에서 호출하여 페이드 아웃 시작
    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }
}