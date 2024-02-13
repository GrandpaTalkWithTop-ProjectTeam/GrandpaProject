using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonpanArmAnimate : MonoBehaviour
{
    public AnimationClip animationClip;  // Inspector���� �ִϸ��̼� Ŭ���� ����

    public Animator animator;
    private float forwardStartFrame = 0f;
    private float forwardEndFrame = 100f;  // ���÷� 0���� 100�����ӱ��� ���
    private float reverseStartFrame = 100f; // ���÷� 100�����Ӻ��� �����
    private float reverseEndFrame = 0f;     // ����� ���� ������
    private bool isForward = true;  // ���������� ��������� ����

    public GameObject WonpanObj;

    WonpanArmScript was;

    void Start()
    {
        was = WonpanObj.GetComponent<WonpanArmScript>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isForward)
            {
                PlayAnimationWithFramesRange(forwardStartFrame, forwardEndFrame);
            }
            else
            {
                ReverseAnimationWithFramesRange(reverseStartFrame, reverseEndFrame);
            }

            // ���� ��ȯ
            isForward = !isForward;
        }
    }

    void PlayAnimationWithFramesRange(float start, float end)
    {
        float startNormalizedTime = start / animationClip.length;
        float endNormalizedTime = end / animationClip.length;

        animator.SetFloat("AnimationSpeed", 1f);
        animator.Play(animationClip.name, 0, startNormalizedTime);
    }

    void ReverseAnimationWithFramesRange(float start, float end)
    {
        float startNormalizedTime = start / animationClip.length;
        float endNormalizedTime = end / animationClip.length;

        animator.SetFloat("AnimationSpeed", -1f);
        animator.Play(animationClip.name, 0, startNormalizedTime);
    }
}