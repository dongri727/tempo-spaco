using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PopUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshPro textMeshPro;
    private string displayText = "";

    // SetDataメソッドをpublicで追加
    public void SetData(string data)
    {
        displayText = data;
    }

        void Awake()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshPro>();
        }
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