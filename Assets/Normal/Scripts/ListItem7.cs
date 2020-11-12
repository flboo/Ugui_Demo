using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SuperScrollView
{
    public class ListItem7 : MonoBehaviour
    {
        [SerializeField]
        private Text mText;
        [SerializeField]
        private Transform m_TrsScale;
        int m_CurIndex = -1;
        public int CurIndex { get { return m_CurIndex; } }

        public void Init()
        {

        }

        public void Refresh(int index, int val)
        {
            m_CurIndex = index;
            mText.text = val.ToString();
        }

        public void SetScale(float value)
        {
            //mText.transform.localScale = Vector3.one * value;
            m_TrsScale.localScale = Vector3.one * value;
        }

        public void OnListViewSnapTargetChanged(bool isSelect)
        {
            mText.color = isSelect ? Color.red : Color.black;
        }


    }
}
