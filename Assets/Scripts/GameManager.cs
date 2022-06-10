using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using static DataManager;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public DataManager dataManager;
    public string overedObject;

    public int maxOrder;

    public GameObject windowPrefab;
    public GameObject textPrefab;
    public List<GameObject> windowList;

    public Sprite vulnerableSprite;
    public Sprite protectedSprite;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void AddWindow(GameObject window, Interactable obj)
    {
        windowList.Add(window);
        window.GetComponent<SortingGroup>().sortingOrder = maxOrder+1;
        Window windowComp = window.GetComponent<Window>();
        windowComp.windowName.text = obj.gameObject.name;
        obj.ShowContent(windowComp);
    }

    public GameObject CreateObject(KeyValuePair<string, Icon> o)
    {
        WWW file = new WWW(Application.streamingAssetsPath + "/" + o.Value.imagePath + ".png");
        var sprite = Sprite.Create(file.texture as Texture2D, new Rect(0, 0, file.texture.width, file.texture.height), Vector2.zero);
        //Resources.LoadAll<Sprite>(o.Value.imagePath);

        GameObject tempObj = new GameObject(o.Value.name);
        SpriteRenderer spriteRenderer = tempObj.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        if (o.Value.content != null && o.Value.content.Count > 0)
        {
            //spriteRenderer.sprite = sprite[1];
        }
        else
        {
            //spriteRenderer.sprite = sprite[0];
        }

        tempObj.transform.localScale = new Vector3(o.Value.imageScale, o.Value.imageScale, o.Value.imageScale);
        tempObj.transform.position = new Vector3(o.Value.x, o.Value.y, tempObj.transform.position.z);

        tempObj.AddComponent<BoxCollider2D>();

        Interactable inter;
        switch (o.Value.objectType)
        {
            case ObjectType.Folder:
                inter = tempObj.AddComponent<FolderInter>();
                inter.content = o.Value.content;
                break;
            case ObjectType.Program:
                inter = tempObj.AddComponent<Interactable>();
                break;
            case ObjectType.Image:
                inter = tempObj.AddComponent<ImageInter>();
                break;
            case ObjectType.Tool:
                inter = tempObj.AddComponent<Interactable>();
                break;
            default:
                inter = null;
                break;
        }

        //Texte part
        GameObject text = Instantiate(GameManager.instance.textPrefab);
        text.transform.SetParent(tempObj.transform);
        text.GetComponent<TextMeshPro>().text = o.Value.name;
        text.transform.position = Vector2.zero;
        RectTransform rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.one * 5;
        rectTransform.anchoredPosition = Vector2.zero;

        inter.ChangeState(o.Value.protection);
        inter.objType = o.Value.objectType;
        inter.objectName = o.Key;

        return tempObj;
    }

    public void GetInteractions(Interactable obj,Interactable other)
    {
        if (dataManager.interactions.ContainsKey(obj.objectName))
        {
            Dictionary<string, Interaction> tempDic = dataManager.interactions[obj.objectName];
            if (tempDic.ContainsKey(other.objType + "/" + other.state))
            {
                string fullString = tempDic[other.objType + "/" + other.state].result;
                State realState = (State)System.Enum.Parse(typeof(State), fullString.Split("/")[1]);
                other.ChangeState(realState);
            }
        }
    }

}
