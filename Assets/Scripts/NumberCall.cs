
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using static UnityEditorInternal.ReorderableList;

public class NumberCall : MonoBehaviour
{
    public Transform player; // ù ��° ������Ʈ�� Transform
    public KeyCode conKey;
    public float conCool = 0;
    public Camera cam;
    public GameObject interText;

    public int[] cNum= new int[4];
    public int[] answer= new int[4];

    [Header("TEST")]
    public GameObject testObj;



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
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out raycastHit, distance))
        {
            // ���콺�� ������Ʈ ���� ���� �� ������ �۾�
            if ((raycastHit.collider.gameObject == this.gameObject) && (conCool <= 0) && (distance < 8f))
            {
                interText.SetActive(true);
                InGameTextExpose igt;
                igt = interText.GetComponent<InGameTextExpose>();

                igt.exposeDelay = 2f;

                if (Input.GetKey(conKey))
                {
                    conCool = 2f;
                    anim.speed = 2f;
                    bool areEqual = cNum.SequenceEqual(answer);

                    if (areEqual)
                    {
                        PushingDoorScript pds;
                        pds=testObj.GetComponent<PushingDoorScript>();

                        pds.switchOn = true;
                    }

                    

                    AudioManager.instance.PlaySfx3D(AudioManager.Sfx.Switch, this.gameObject, Random.Range(0.8f, 1.2f));
                }

            }

        }


    }



}
