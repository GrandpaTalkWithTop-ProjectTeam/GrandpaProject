using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrainCall : MonoBehaviour
{

    public Material nm1; // ������ ���ο� ��Ƽ������ �Ҵ��� ����
    public Material nm2;
    public Transform object1; // ù ��° ������Ʈ�� Transform
    public Transform object2; // �� ��° ������Ʈ�� Transform
    public KeyCode conKey;
    public float conCool = 0;
    public CreateTrain cret;
    public Camera cam;
    public GameObject interText;
    // Start is called before the first frame update

    

    void Update()
    {
        
        float distance = Vector3.Distance(object1.position, object2.position);

        if (conCool > 0)
        {
            conCool-=Time.deltaTime;
        }
        else
        {
            Renderer rend = GetComponent<Renderer>(); // �ش� ������Ʈ�� Renderer ������Ʈ ��������
            if (rend.material = nm2)
            {
                rend.material = nm1;
            }

        }
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // ���콺�� ������Ʈ ���� ���� �� ������ �۾�
            if ((hit.collider.gameObject == this.gameObject) && (conCool <= 0) && (distance < 5f))
            {
                interText.SetActive(true);

                if (Input.GetKey(conKey))
                    {
                        Renderer rend = GetComponent<Renderer>(); // �ش� ������Ʈ�� Renderer ������Ʈ ��������
                        rend.material = nm2; // ��Ƽ���� ����
                        cret.BTrain_Call = true;
                        conCool = 3f;

                        AudioManager.instance.PlaySfx3D(AudioManager.Sfx.Switch, this.gameObject);
                }

            }
            else
                interText.SetActive(false);
        }


    }

   

}
