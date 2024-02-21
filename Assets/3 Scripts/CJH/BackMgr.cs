using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 팝업 창이 열리면 이 스크립트에 있는 스택에 넣음
// 팝업 종료하려면
// 팝업 제외한 검은 화면 클릭
// 휴대폰의 뒤로 가기 버튼

public class BackMgr : MonoBehaviour
{
    public static BackMgr instance;
    public Stack<PopupBtn> st;

    void Start()
    {
        if (instance == null)
        {
            instance = this;

            st = new Stack<PopupBtn>();
        }
    }

    // 팝업 열었을 때 스택에 푸시
    public void Push(PopupBtn popup)
    {
        st.Push(popup);
    }

    public void Pop()
    {
        if(st.Count > 0) 
        {
            PopupBtn popup = st.Peek();
            st.Pop();

            popup.BackClick();
        }
    }
}
