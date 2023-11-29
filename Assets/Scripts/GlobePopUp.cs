using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class GlobePopUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshPro textMeshPro;
    private string displayText = "";

    // SetDataメソッドをpublicで追加
    public void SetData(string data)
    {
        displayText = data;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textMeshPro.text = displayText;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        textMeshPro.text = " ";
   
    }
}
