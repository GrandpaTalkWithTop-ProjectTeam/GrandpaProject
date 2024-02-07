using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PadTextInput : MonoBehaviour
{
    public Text stackText;
    public int[] sNum = new int[4];
    public int stackIndex = 0;
    public string stackTextString = "";


    void Update()
    {
        
        
    }


    public void Push(int value)
    {
        if (stackIndex < sNum.Length)
        {
            sNum[stackIndex] = value;
            stackTextString += sNum[stackIndex];

            stackIndex++;

            
                
            
            stackText.text = stackTextString;



        }
        if (stackIndex == sNum.Length)
        {
            ResetStack();
        }
    }

    void ResetStack()
    {
        stackIndex = 0; // �ε��� �ʱ�ȭ
        for (int i = 0; i < sNum.Length; i++)
        {
            sNum[i] = 0; // �� ��Ҹ� 0���� �ʱ�ȭ
        }

        stackTextString = "";
        stackText.text = stackTextString;
        Debug.Log("Stack reset!");
    }
}