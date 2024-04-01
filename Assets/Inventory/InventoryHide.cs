using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHide : MonoBehaviour
{
    // Panel�� CanvasGroup ������Ʈ
    private CanvasGroup canvasGroup;
    public GameObject TalkyObject;

    private void Start()
    {
        // Panel�� CanvasGroup ������Ʈ�� ������
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup not found");
        }
    }

    private void Update()
    {
        // Tab Ű�� ������ ��
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // ���� Panel�� ���¿� ���� Hide �Ǵ� Show �Լ��� ȣ��
            if (canvasGroup.alpha == 1f)
            {
                Hide();
            }
            else
            {

                if ((GameManager.GameIsPaused == false) && (!TalkyObject.activeSelf))
                    Show();
            }
        }
    }

    // Panel�� ����� �Լ�
    public void Hide()
    {
        // Panel�� ������ 0���� �����Ͽ� ȭ�鿡�� ������ �ʵ��� ��
        canvasGroup.alpha = 0f;
        // Panel�� ��ȣ�ۿ��� ��Ȱ��ȭ�Ͽ� ����� �Է��� ����
        canvasGroup.interactable = false;
        // Panel�� ����� ��Ȱ��ȭ�Ͽ� �ٸ� UI ��ҿ� ���� �������� �ʵ��� ��
        canvasGroup.blocksRaycasts = false;

        GameManager.GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Panel�� ���̰� �ϴ� �Լ� (�ʿ��� ���)
    public void Show()
    {
        // Panel�� ������ 1�� �����Ͽ� ȭ�鿡 ���̵��� ��
        canvasGroup.alpha = 1f;
        // Panel�� ��ȣ�ۿ��� Ȱ��ȭ�Ͽ� ����� �Է��� �޵��� ��
        canvasGroup.interactable = true;
        // Panel�� ����� Ȱ��ȭ�Ͽ� �ٸ� UI ��ҿ� ���� �������� �ʵ��� ��
        canvasGroup.blocksRaycasts = true;

        GameManager.GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}