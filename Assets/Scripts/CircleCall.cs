using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCall : MonoBehaviour
{
    public Transform player;
    public KeyCode interactionKey;
    public float interactionCooldown = 0;
    public Camera camera;
    public GameObject interactionText;
    public PuzzleCircleBody puzzleCircleBody;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0f;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (interactionCooldown > 0)
        {
            interactionCooldown -= Time.deltaTime;
        }

        CheckAnimationState();

        if (IsRaycastHit(distance, out RaycastHit raycastHit))
        {
            HandleInteraction(distance, raycastHit);
        }
    }

    private void CheckAnimationState()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (animator.speed > 0f && stateInfo.normalizedTime >= 1.0f)
        {
            AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
            animator.Play(clipInfo[0].clip.name, 0, 0f);
            animator.speed = 0f;
        }
    }

    private bool IsRaycastHit(float distance, out RaycastHit raycastHit)
    {
        return Physics.Raycast(camera.transform.position, camera.transform.forward, out raycastHit, distance);
    }

    private void HandleInteraction(float distance, RaycastHit raycastHit)
    {
        if (raycastHit.collider.gameObject == this.gameObject && interactionCooldown <= 0 && distance < 8f)
        {
            interactionText.SetActive(true);

            InGameTextExpose interactionTextExpose = interactionText.GetComponent<InGameTextExpose>();
            interactionTextExpose.exposeDelay = 2f;

            if (Input.GetKey(interactionKey))
            {
                puzzleCircleBody.SwitchOn = true;
                interactionCooldown = 2f;
                animator.speed = 2f;

                AudioManager.instance.PlaySfx3D(AudioManager.Sfx.Switch, this.gameObject, Random.Range(0.8f, 1.2f));
            }
        }
    }
}