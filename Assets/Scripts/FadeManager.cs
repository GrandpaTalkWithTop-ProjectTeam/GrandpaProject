using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage; // ���̵� ��/�ƿ��� ���� �̹���
    public float fadeSpeed = 0.5f; // ���̵� �ӵ�

    private void Start()
    {
        // �ʱ� ���İ� ���� (����)
        fadeImage.color = new Color(0f, 0f, 0f, 1f); // ������ ��濡�� ���� (���� 1)
        StartCoroutine(FadeIn());
    }

    // ���̵� �� �ڷ�ƾ
    IEnumerator FadeIn()
    {
        while (fadeImage.color.a > 0)
        {
            // ���İ� ���� (���� ����)
            fadeImage.color = new Color(0f, 0f, 0f, fadeImage.color.a - fadeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    // ���̵� �ƿ� �ڷ�ƾ
    IEnumerator FadeOut()
    {
        while (fadeImage.color.a < 1)
        {
            // ���İ� ���� (���� ����)
            fadeImage.color = new Color(0f, 0f, 0f, fadeImage.color.a + fadeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    // �ٸ� ��ũ��Ʈ���� ȣ���Ͽ� ���̵� �� ����
    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    // �ٸ� ��ũ��Ʈ���� ȣ���Ͽ� ���̵� �ƿ� ����
    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }
}