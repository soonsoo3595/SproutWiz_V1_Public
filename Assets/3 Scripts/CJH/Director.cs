using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ������ � ��Ȳ�� �ľ��� �� ������ ���� ��ũ��Ʈ
// ���� ��ü���� ���̴� ����

public class Director
{
    public static UserVariable userVariable = new UserVariable();

    // ��ũ�� ���� �̹����� ����
    public static Dictionary<string, Sprite> scrollBlockSprites = new Dictionary<string, Sprite>();

    public static int inventorySize = 20;    // �κ��丮 ĭ
    public static int maxItemCount = 25;    // �ϳ��� ���Կ� �� ���� �������� �� �� �ִ���

    public static int maxSpell = 50;

}
