using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(GridLayoutGroup))]
public class ResponsiveGridFitter : UIBehaviour {

    [System.Serializable]
    public class Item
    {
        public uint breakPoint;
        public uint columns;
    }

    public Item[] itemList;

    public float aspectRatio;


    protected override void OnRectTransformDimensionsChange()
    {

        float currentWidth = rectTransform.rect.width;
       
        uint numbColumns = 1;

        int len = itemList.Length;
        for(int i = 0; i< len;i++)
        {
            if( itemList[i].breakPoint < currentWidth )
            {
                numbColumns = itemList[i].columns;
                break;
            }
        }

        float w = Mathf.Floor(currentWidth / numbColumns);
        float h = Mathf.Floor(w * aspectRatio);
        gridLayout.cellSize = new Vector2(w, h);

    }

    private GridLayoutGroup _gridLayout;

    private GridLayoutGroup gridLayout
    {
        get
        {
            if (_gridLayout == null)
                _gridLayout = GetComponent<GridLayoutGroup>();
            return _gridLayout;
        }
    }

    private RectTransform _rectTransform;

    private RectTransform rectTransform
    {
        get
        {
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();
            return _rectTransform;
        }
    }



}
