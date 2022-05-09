using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FolderInter : Interactable
{
    

    public override void ShowContent(Window window)
    {
        base.ShowContent(window);
        GameObject containerObject = new GameObject("Container");

        RectTransform trans = containerObject.AddComponent<RectTransform>();
        trans.transform.SetParent(window.canvas.transform); // setting parent
        trans.localScale = Vector3.one;

        trans.anchorMin = new Vector2(0, 0);
        trans.anchorMax = new Vector2(1, 1);

        
        trans.anchoredPosition = new Vector2(0f, 0f);
        trans.offsetMin = Vector2.zero;
        trans.offsetMax = Vector2.zero;

        GridLayoutGroup layout = containerObject.AddComponent<GridLayoutGroup>();
        layout.cellSize = Vector2.one;
        layout.spacing = new Vector2(0.4f, 0.4f);
        foreach (var item in content)
        {
            Icon tempIcone = GameManager.instance.dataManager.icons[item];
            var tempKey = new KeyValuePair<string, Icon>(item, tempIcone);
            GameObject objContent = GameManager.instance.CreateObject(tempKey);
            objContent.AddComponent<RectTransform>();
            objContent.transform.SetParent(trans.transform);
        }



        
    }

    public override void OnMouseUp()
    {
        base.OnMouseUp();
        
    }

}
