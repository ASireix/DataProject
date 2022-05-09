using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageInter : Interactable
{
    public override void ShowContent(Window window)
    {
        base.ShowContent(window);
        GameObject imgObject = new GameObject("Image");

        RectTransform trans = imgObject.AddComponent<RectTransform>();
        trans.transform.SetParent(window.canvas.transform); // setting parent
        trans.localScale = Vector3.one;
        trans.anchorMin = new Vector2(0, 0);
        trans.anchorMax = new Vector2(1, 1);
        trans.anchoredPosition = new Vector2(0f, 0f); // setting position, will be on center

        Image image = imgObject.AddComponent<Image>();
        image.sprite = spriteRenderer.sprite;
        imgObject.transform.SetParent(window.canvas.transform);
        image.preserveAspect = true;

        trans.offsetMin = Vector2.zero;
        trans.offsetMax = Vector2.zero;
    }
}
