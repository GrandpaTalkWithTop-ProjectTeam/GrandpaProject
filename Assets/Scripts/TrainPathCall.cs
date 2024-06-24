using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainPathCall : MonoBehaviour
{
    public Transform player; // 첫 번째 오브젝트의 Transform
    public KeyCode conKey;
    public Camera cam;
    public GameObject interText;

    public TrainPath tp;
    private Animator anim;


    [Header("Battery")]
    public GameObject batParent;
    public GameObject batTarget;


    private void Start()
    {
        anim = GetComponent<Animator>();

        anim.speed = 0f;

    }

    // Start is called before the first frame update

    void Update()
    {

        float distance = Vector3.Distance(player.position, transform.position);

    

        AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);



        if ((anim.speed > 0f) && (stateInfo.normalizedTime >= 1.0f))
        {

            anim.Play(clipInfo[0].clip.name, 0, 0f);
            anim.speed = 0f;
        }


        RaycastHit raycastHit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out raycastHit, distance))
        {
            bool hasChild = HasSpecificChild(batParent.transform, batTarget);

            // 마우스가 오브젝트 위에 있을 때 수행할 작업
            if ((raycastHit.collider.gameObject == this.gameObject) && (distance < 8f) && (tp.isStopped==true) && (hasChild))
            {
                interText.SetActive(true);
                InGameTextExpose igt;
                igt = interText.GetComponent<InGameTextExpose>();

                igt.exposeDelay = 2f;

                if (Input.GetKey(conKey))
                {
                    tp.isStopped = false;

                    anim.speed = 2f;

                    AudioManager.instance.PlaySfx3D(AudioManager.Sfx.Switch, this.gameObject, Random.Range(0.8f, 1.2f));
                }

            }

        }


    }

    bool HasSpecificChild(Transform parent, GameObject targetChild)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject == targetChild)
            {
                return true;
            }
        }
        return false;
    }



}
