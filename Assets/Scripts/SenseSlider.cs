using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SenseSlider : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    public void OnSliderEvent(float value)
    {
        text.text = $"���콺 X �ΰ��� { value * 10:F1}%";
    }
}
