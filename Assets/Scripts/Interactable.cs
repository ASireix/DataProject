using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Interactable : MonoBehaviour
{
    public string objectName;
    protected SpriteRenderer spriteRenderer;
    protected SpriteRenderer stateRenderer;
    public List<string> content;

    public State state;
    public ObjectType objType;

    private Vector3 _dragOffset;
    private Camera _cam;
    protected bool clicked;
    protected bool dragged;
    [SerializeField] private float delay;

    [SerializeField] private float _speed = 50;
    [SerializeField] private float clickDelay = 0.2f;

    protected BoxCollider2D col;
    protected Vector2 startPos;

    public void Init(State s)
    {
        state = s;
    }
    // Start is called before the first frame update
    void Awake()
    {
        startPos = transform.position;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _cam = Camera.main;
        col = GetComponent<BoxCollider2D>();
        GameObject tempObj = new GameObject("Protection");
        stateRenderer = tempObj.AddComponent<SpriteRenderer>();
        tempObj.transform.SetParent(gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime;
        if (delay <= 0)
        {
            clicked = false;
        }
        stateRenderer.sortingOrder = spriteRenderer.sortingOrder + 1;
    }

    public virtual void OnMouseDown()
    {
        _dragOffset = transform.position - GetMousePos();
        if (clicked)
        {
            OnDoubleClick();
        }
        clicked = true;
        delay = clickDelay;
        dragged = false;
        startPos = transform.position;
    }

    public virtual void OnDoubleClick()
    {
        clicked = false;
        if (!gameObject.GetComponent<Window>() && state == State.Neutral)
        {
            GameObject newWindow = Instantiate(GameManager.instance.windowPrefab);
            newWindow.transform.position = Vector3.zero;
            GameManager.instance.AddWindow(newWindow, this);
        }
        
    }
    public virtual void OnMouseUp()
    {
        if (dragged)
        {
            dragged = false;
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, col.size*gameObject.transform.localScale, 0f);

            if (colliders.Length > 1)
            {
                GameManager.instance.GetInteractions(this,colliders[1].gameObject.GetComponent<Interactable>());
                gameObject.transform.position = startPos;
            }
            else
            {
                startPos = transform.position;
            }

        }
    }
    public virtual void OnMouseDrag()
    {
        dragged = true;
        transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + _dragOffset, _speed * Time.deltaTime);
        if (!gameObject.GetComponent<SortingGroup>())
        {
            if (spriteRenderer.sortingOrder < GameManager.instance.maxOrder)
            {
                
                GameManager.instance.maxOrder += 1;
                spriteRenderer.sortingOrder = GameManager.instance.maxOrder;
            }
            else
            {
                GameManager.instance.maxOrder = spriteRenderer.sortingOrder;
            }
        }
        else
        {
            SortingGroup sorting = gameObject.GetComponent<SortingGroup>();
            if (sorting.sortingOrder < GameManager.instance.maxOrder)
            {
                GameManager.instance.maxOrder += 1;
                sorting.sortingOrder = GameManager.instance.maxOrder;
            }
            else
            {
                GameManager.instance.maxOrder = sorting.sortingOrder;
            }
        }
    }
    Vector3 GetMousePos()
    {
        var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

    public virtual void ShowContent(Window window)
    {
        window.canvas.sortingOrder = GameManager.instance.maxOrder+1;
    }

    public void ChangeState(State newState)
    {
        
        stateRenderer.gameObject.transform.localPosition = Vector2.zero;
        stateRenderer.gameObject.transform.localScale = Vector2.one;
        switch (newState)
        {
            case State.Protected:
                stateRenderer.sprite = GameManager.instance.protectedSprite;
                
                break;
            case State.Vulnerable:
                stateRenderer.sprite = GameManager.instance.vulnerableSprite;
                break;
            case State.Neutral:
                stateRenderer.sprite = null;
                break;
            default:
                break;
        }

        state = newState;
    }
}
