using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyPuzzleIsClear : MonoBehaviour
{
    public GameObject[] spheres;  // �� ��ü ������Ʈ���� �迭�� ����


    [HideInInspector]
    public Material CheckMaterial1;
    public Material CheckMaterial2;

    public GameObject ClearCheckObject;

    private Renderer rend;


    private void Start()
    {
        rend = ClearCheckObject.GetComponent<Renderer>();
        CheckMaterial1 = rend.material;
    }

    void Update()
    {
        // ��� ��ü ������Ʈ�� ������ true���� üũ
        bool allSpheresTrue = true;

        foreach (GameObject sphere in spheres)
        {
            // �� ��ü ������Ʈ�� ������ üũ�ϰ�, �ϳ��� false�̸� allSpheresTrue�� false�� ����
            if (!sphere.GetComponent<ClearSphere>().clearOn)
            {
                allSpheresTrue = false;
                break;
            }
        }

        // ���� ��� ��ü ������Ʈ�� ������ true�̸� Ư�� ������Ʈ�� ������ true�� ����
        if (allSpheresTrue)
        {
            rend.material = CheckMaterial2;
        }
        else
        {
            rend.material = CheckMaterial1;
        }
    }
}