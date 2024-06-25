using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkyScript : MonoBehaviour
{
    [Header("아이템")]
    public Item item;

    [Header("인벤토리")]
    public Inventory inventory;

    public Transform player; // 첫 번째 오브젝트의 Transform
    public KeyCode conKey;
    public float conCool = 0;
    public Camera cam;
    public GameObject interText;
    public GameObject TalkyObject;
    public AudioClip vocClip;

    public Animator animator;

    void Start()
    {
        if (animator != null)
        {
            // 애니메이션의 속도를 0으로 설정
            animator.speed = 0f;
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(cam.transform.position, transform.position);

        if (conCool > 0)
        {
            conCool -= Time.deltaTime;
        }

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
                    inventory.AddItem(item);

                    TalkyObject.SetActive(true);
                    this.gameObject.SetActive(false);

                    AudioManager.instance.vocClip = vocClip;
                    AudioManager.instance.VOCAwake();
                }
            }
        }
    }
}
