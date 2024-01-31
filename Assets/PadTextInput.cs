using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PadTextInput : MonoBehaviour
{
    public InputField inputField;   // ���� ���� �ԷµǴ� InputField
    public Text stackText;          // ������ ǥ���ϴ� Text

    private Stack<int> valueStack = new Stack<int>();  // �Էµ� ���� ������ ������ ����

    void Start()
    {
        // InputField�� ���� �Էµ� ������ �̺�Ʈ�� ���
        inputField.onValueChanged.AddListener(OnInputValueChanged);
    }

    void OnInputValueChanged(string newValue)
    {
        // InputField���� ���� ����� �� ȣ��Ǵ� �Լ�

        // ���� ���ڷ� ��ȯ �������� Ȯ���ϰ� ���ÿ� �߰�
        if (int.TryParse(newValue, out int numericValue))
        {
            valueStack.Push(numericValue);

            // ������ ������ �ؽ�Ʈ�� ������Ʈ
            UpdateStackText();
        }

        // �Է� �ʵ� �ʱ�ȭ
        inputField.text = "";
    }

    void UpdateStackText()
    {
        // ������ ������ �ؽ�Ʈ�� ������Ʈ
        stackText.text = "Stack:\n";

        foreach (int value in valueStack)
        {
            stackText.text += value + "\n";
        }
    }
}
