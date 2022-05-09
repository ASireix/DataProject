using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using TMPro;

public class DataManager : MonoBehaviour
{
    public Dictionary<string, Icon> icons;
    public Dictionary<string, Dictionary<string, Interaction>> interactions;

    public class Interaction
    {
        public string result;
    }

    void Start()
    {
        var jsonTextFile = Resources.Load<TextAsset>("Data/Icones");
        icons = JsonConvert.DeserializeObject<Dictionary<string, Icon>>(jsonTextFile.text);

        foreach (KeyValuePair<string, Icon> o in icons)
        {
            if (!o.Value.hidden)
            {
                GameManager.instance.CreateObject(o);
            }
            
        }

        var jsonTextFile2 = Resources.Load<TextAsset>("Data/Interactions");
        interactions = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Interaction>>>(jsonTextFile2.text);

        foreach (KeyValuePair<string, Dictionary<string, Interaction>> o in interactions)
        {
            foreach (KeyValuePair<string, Interaction> o2 in o.Value)
            {
                Debug.Log(o2.Value.result);
            }
        }
    }

    
}