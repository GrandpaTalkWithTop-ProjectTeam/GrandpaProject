using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonpanRotate : MonoBehaviour
{
    public Transform player; // ù ��° ������Ʈ�� Transform
    public KeyCode conKey;
    public float conCool = 0;
    public Camera cam;
    public GameObject interText;

    public GameObject WonpanObj;
    public GameObject WonpanPlate1;
    public GameObject WonpanPlate2;
    public GameObject WonpanPlate3;

    public bool RotateRight;


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
            WonpanArmAnimate waa1, waa2, waa3;
            waa1 = WonpanPlate1.GetComponent<WonpanArmAnimate>();
            waa2 = WonpanPlate2.GetComponent<WonpanArmAnimate>();
            waa3 = WonpanPlate3.GetComponent<WonpanArmAnimate>();


            // ���콺�� ������Ʈ ���� ���� �� ������ �۾�
            if ((raycastHit.collider.gameObject == this.gameObject) && (conCool <= 0) && (distance < 8f) && (waa1.doormode==false) && (waa1.doorclear == false))
            {
                interText.SetActive(true);
                InGameTextExpose igt;
                igt = interText.GetComponent<InGameTextExpose>();
                igt.exposeDelay = 2f;

                if (Input.GetKey(conKey))
                {
                    conCool = 2f;
                    anim.speed = 2f;

                    WonpanArmScript was;
                    was = WonpanObj.GetComponent<WonpanArmScript>();

                    


                    waa1.doormode = true;
                    waa2.doormode = true;
                    waa3.doormode = true;


                    if (RotateRight)
                    was.targetRotation += 90;
                    else
                    was.targetRotation -= 90;

                    AudioManager.instance.PlaySfx3D(AudioManager.Sfx.Switch, this.gameObject);
                }

            }

        }


    }



}
