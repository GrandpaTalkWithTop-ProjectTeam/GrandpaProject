using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_voicActor : MonoBehaviour
{
    public KeyCode conKey;
    public float conCool = 0;
    public Camera cam;
    public GameObject interText;

    private void FixedUpdate()
    {
        if (conCool > 0)
        {
            conCool -= Time.deltaTime;
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(cam.transform.position, transform.position);

        RaycastHit raycastHit;

        int layerMask = ~LayerMask.GetMask("Ignore Raycast");
        if (
            Physics.Raycast(
                cam.transform.position,
                cam.transform.forward,
                out raycastHit,
                distance,
                layerMask
            )
        )
        {
            // 마우스가 오브젝트 위에 있을 때 수행할 작업
            if (
                (raycastHit.collider.gameObject == this.gameObject)
                && (conCool <= 0)
                && (distance < 10f)
            )
            {
                interText.SetActive(true);
                InGameTextExpose igt;
                igt = interText.GetComponent<InGameTextExpose>();

                igt.exposeDelay = 2f;

                if (Input.GetKey(conKey))
                {
                    conCool = 2f;
                    AudioManager.instance.StopAllSfx();
                    AudioManager.instance.PlaySfx3D(
                        AudioManager.Sfx.FVoice001,
                        this.gameObject,
                        1f
                    );
                }
            }
        }
    }
}
