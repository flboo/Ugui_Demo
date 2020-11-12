using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SuperScrollView
{

    public class SpinDatePickerDemoScript : MonoBehaviour
    {
        public LoopListView2 mLoopListViewHour;

        int m_CurSelectedIndex = 2;

        void Start()
        {
            mLoopListViewHour.InitListView(-1, OnGetItemByIndexForHour);
            mLoopListViewHour.mOnSnapNearestChanged = OnHourSnapTargetChanged;
        }

        LoopListViewItem2 OnGetItemByIndexForHour(LoopListView2 listView, int index)
        {
            LoopListViewItem2 item = listView.NewListViewItem("ItemPrefab1");
            ListItem7 itemScript = item.GetComponent<ListItem7>();
            if (item.IsInitHandlerCalled == false)
            {
                item.IsInitHandlerCalled = true;
                itemScript.Init();
            }
            int firstItemVal = 1;
            int itemCount = 24;
            int val = 0;
            if (index >= 0)
            {
                val = index % itemCount;
            }
            else
            {
                val = itemCount + ((index + 1) % itemCount) - 1;
            }
            val = val + firstItemVal;
            itemScript.Refresh(index, val);
            return item;
        }


        void LateUpdate()
        {
            mLoopListViewHour.UpdateAllShownItemSnapData();
            for (int i = 0; i < mLoopListViewHour.ShownItemCount; ++i)
            {
                LoopListViewItem2 item = mLoopListViewHour.GetShownItemByIndex(i);
                ListItem7 itemScript = item.GetComponent<ListItem7>();
                float scale = 1 - Mathf.Abs(item.DistanceWithViewPortSnapCenter) / 700f;
                scale = Mathf.Clamp(scale, 0.4f, 1);
                itemScript.SetScale(scale);
            }
        }



        void OnHourSnapTargetChanged(LoopListView2 listView, LoopListViewItem2 item)
        {
            int index = listView.GetIndexInShownItemList(item);
            if (index < 0)
            {
                return;
            }
            ListItem7 itemScript = item.GetComponent<ListItem7>();
            m_CurSelectedIndex = itemScript.CurIndex;
            OnListViewSnapTargetChanged(listView, index);
        }


        void OnListViewSnapTargetChanged(LoopListView2 listView, int targetIndex)
        {
            for (int i = 0; i < listView.ShownItemCount; ++i)
            {
                LoopListViewItem2 item2 = listView.GetShownItemByIndex(i);
                ListItem7 itemScript = item2.GetComponent<ListItem7>();
                itemScript.OnListViewSnapTargetChanged(i == targetIndex);
            }
        }

    }
}