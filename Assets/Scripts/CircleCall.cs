using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCall : MonoBehaviour
{
    public Transform player;
    public KeyCode conKey;
    public float conCool = 0;
    public Camera cam;
    public GameObject interText;

    public PuzzleCircleBody pcb;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

        anim.speed = 0f;
    }

    // Start is called before the first frame update

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (conCool > 0)
        {
            conCool -= Time.deltaTime;
        }

        AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if ((anim.speed > 0f) && (stateInfo.normalizedTime >= 1.0f))
        {
            anim.Play(clipInfo[0].clip.name, 0, 0f);
            anim.speed = 0f;
        }

        RaycastHit raycastHit;
        if (
            Physics.Raycast(cam.transform.position, cam.transform.forward, out raycastHit, distance)
        )
        {
            if (
                (raycastHit.collider.gameObject == this.gameObject)
                && (conCool <= 0)
                && (distance < 8f)
            )
            {
                interText.SetActive(true);
                InGameTextExpose igt;
                igt = interText.GetComponent<InGameTextExpose>();

                igt.exposeDelay = 2f;

                if (Input.GetKey(conKey))
                {
                    pcb.SwitchOn = true;
                    conCool = 2f;

                    anim.speed = 2f;

                    AudioManager.instance.PlaySfx3D(
                        AudioManager.Sfx.Switch,
                        this.gameObject,
                        Random.Range(0.8f, 1.2f)
                    );
                }
            }
        }
    }
}
