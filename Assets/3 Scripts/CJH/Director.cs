using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 내에서 어떤 상황을 파악할 때 쓰려고 만든 스크립트
// 게임 전체에서 쓰이는 변수

public class Director
{
    public static UserVariable userVariable = new UserVariable();

    // 스크롤 블럭의 이미지를 가짐
    public static Dictionary<string, Sprite> scrollBlockSprites = new Dictionary<string, Sprite>();

    public static int inventorySize = 20;    // 인벤토리 칸
    public static int maxItemCount = 25;    // 하나의 슬롯에 몇 개의 아이템이 들어갈 수 있는지

    public static int maxSpell = 50;

}
