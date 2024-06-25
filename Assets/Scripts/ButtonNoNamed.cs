using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public Material nm1; // ������ ���ο� ��Ƽ������ �Ҵ��� ����
    public Material nm2;
    public Transform object1; // ù ��° ������Ʈ�� Transform
    public Transform object2; // �� ��° ������Ʈ�� Transform
    public KeyCode conKey;
    public float conCool = 0;

    public Camera cam;
    public GameObject interText;

    public List<Animator> objectAnimators; // ���� Animator ������Ʈ�� �����ϱ� ���� ����Ʈ
    public string animationTrigger; // Ȱ��ȭ�� �ִϸ��̼��� Ʈ���� �̸�

    void Update()
    {
        float distance = Vector3.Distance(object1.position, object2.position);

        if (conCool > 0)
        {
            conCool -= Time.deltaTime;
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
            if ((hit.collider.gameObject == this.gameObject) && (conCool <= 0) && (distance < 5f))
            {
                interText.SetActive(true);
                InGameTextExpose igt;
                igt = interText.GetComponent<InGameTextExpose>();

                igt.exposeDelay = 2f;

                if (Input.GetKey(conKey))
                {
                    Renderer rend = GetComponent<Renderer>();
                    rend.material = nm2;

                    // ��� �ִϸ����Ϳ� ���� �ִϸ��̼� Ʈ���� Ȱ��ȭ
                    foreach (Animator animator in objectAnimators)
                    {
                        animator.SetTrigger(animationTrigger);
                    }

                    conCool = 3f;
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
