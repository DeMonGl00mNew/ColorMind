using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����� ColorManager, ���������� �� ���������� ������� � ����
public class ColorManager : MonoBehaviour
{
    static public ColorManager Instance { get; private set; } // ����������� �������� ��� ������� � ������������� ���������� ������
    public Color[] teamColours; // ������ ������ ��� ������

    // �����, ���������� ��� �������� �������
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // ���������� ������� ������ ��� ������������ ���������

        }
        else if (Instance != this)
        {
            Destroy(gameObject); // ���������� ������, ���� ��� ���������� ������ ��������� ������
        }
    }
}