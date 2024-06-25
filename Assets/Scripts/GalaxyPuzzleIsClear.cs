using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyPuzzleIsClear : MonoBehaviour
{
    public GameObject[] spheres;  // 각 구체 오브젝트들을 배열로 저장


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
        // 모든 구체 오브젝트의 변수가 true인지 체크
        bool allSpheresTrue = true;

        foreach (GameObject sphere in spheres)
        {
            // 각 구체 오브젝트의 변수를 체크하고, 하나라도 false이면 allSpheresTrue를 false로 설정
            if (!sphere.GetComponent<ClearSphere>().clearOn)
            {
                allSpheresTrue = false;
                break;
            }
        }

        // 만약 모든 구체 오브젝트의 변수가 true이면 특정 오브젝트의 변수를 true로 변경
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