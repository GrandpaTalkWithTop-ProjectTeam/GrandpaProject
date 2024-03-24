using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkyScript : MonoBehaviour
{
    public Transform player; // ù ��° ������Ʈ�� Transform
    public KeyCode conKey;
    public float conCool = 0;
    public Camera cam;
    public GameObject interText;
    public GameObject TalkyObject;


    private void Start()
    {

    }

    // Start is called before the first frame update

    void Update()
    {

        float distance = Vector3.Distance(player.position, transform.position);

        if (conCool > 0)
        {
            conCool -= Time.deltaTime;
        }



        RaycastHit raycastHit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out raycastHit, distance))
        {
            // ���콺�� ������Ʈ ���� ���� �� ������ �۾�
            if ((raycastHit.collider.gameObject == this.gameObject) && (conCool <= 0) && (distance < 30f))
            {
                interText.SetActive(true);
                InGameTextExpose igt;
                igt = interText.GetComponent<InGameTextExpose>();

                igt.exposeDelay = 2f;

                if (Input.GetKey(conKey))
                {
                    conCool = 2f;

                    TalkyObject.SetActive(true);
                    this.gameObject.SetActive(false);

                }

            }

        }


    }



}
