using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    public GameObject root;
    void OnMouseDown()
    {
        Destroy(root);
    }
}
