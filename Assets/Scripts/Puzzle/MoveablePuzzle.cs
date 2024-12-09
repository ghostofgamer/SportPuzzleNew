using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveablePuzzle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] RectTransform innerImage;
    Canvas canvas;
    CanvasGroup canvasGroup;
    RectTransform rectTransform;
    Vector2 puzzleID;
    Transform parentObject;

    public void Init(puzzleVariant variant, int puzzleSize, Sprite sprite, Vector2 id)
    {
        innerImage.GetComponent<Image>().sprite = sprite;
        innerImage.localScale = Vector3.one * puzzleSize;
        rectTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        puzzleID = id; 
        parentObject = transform.parent;
        
        /*float offsetX = 50 * (puzzleSize - 3) - (id.x - 1) * 100;
        float offsetY = 50 * (puzzleSize - 3) - (id.y - 1) * 100;*/
        
        float offsetX = 5 * (puzzleSize - 3) - (id.x - 1) * 5;
        float offsetY = 5 * (puzzleSize - 3) - (id.y - 1) * 5;
        
        switch (variant)
        {
            case puzzleVariant.formU:
                innerImage.anchoredPosition = new Vector2(offsetX, innerImage.anchoredPosition.y);
                break;
            case puzzleVariant.formD:
                innerImage.anchoredPosition = new Vector2(offsetX, innerImage.anchoredPosition.y);
                break;
            case puzzleVariant.formR:
                innerImage.anchoredPosition = new Vector2(innerImage.anchoredPosition.x, offsetY);
                break;
            case puzzleVariant.formL:
                innerImage.anchoredPosition = new Vector2(innerImage.anchoredPosition.x, offsetY);
                break;
            case puzzleVariant.formC:
                innerImage.anchoredPosition = new Vector2(offsetX, offsetY);
                break;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PuzzleController.instance.SetMovable(puzzleID);
        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        StartCoroutine(waitAny());
    }

    IEnumerator waitAny()
    {
        for(int i = 0; i < 3; i++)
            yield return new WaitForEndOfFrame();
        PuzzleController.instance.SetSetted(Vector2.left, Vector2.left);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        rectTransform.SetParent(PuzzleController.instance.border);
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.zero;
        Vector2 output;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(PuzzleController.instance.border, 
            eventData.position / canvas.scaleFactor, Camera.main, out output);
        rectTransform.anchoredPosition = output;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //canvasGroup.blocksRaycasts = true;
    }
    private void OnEnable()
    {
        PuzzleController.puzzleMoveResults += MovePuzzle;
    }

    private void OnDisable()
    {
        PuzzleController.puzzleMoveResults -= MovePuzzle;
    }

    private void MovePuzzle(Vector2 id, Vector2 pos, float size, bool b)
    {
        if (id == puzzleID)
        {
            if (b)
            {
                float animDelay = .5f;
                rectTransform.DOAnchorPos(pos, animDelay).SetEase(Ease.InOutBack);
                rectTransform.DOScale(size, animDelay).SetEase(Ease.OutBack);
                Destroy(gameObject, animDelay+.1f);
            }
            else
            {
                float animDelay = .5f;
                var a = rectTransform.anchoredPosition + Vector2.down * 500;

                rectTransform.DOAnchorPos(a, animDelay).SetEase(Ease.Unset);
                StartCoroutine(DecreaseAlpha(animDelay));
                
                Debug.Log("not good");
            }
        }
    }
    private void ReturnPuzzleToParent()
    {
        transform.SetParent(parentObject);
        canvasGroup.blocksRaycasts = true;
        rectTransform.GetComponent<CanvasGroup>().alpha = 1;
    }
    IEnumerator DecreaseAlpha(float del)
    {
        float cnt = (int)(del / .05f);
        var cg = rectTransform.GetComponent<CanvasGroup>();
        for (int i = 0; i < cnt+1; i++)
        {
            yield return new WaitForSeconds(.05f);
            cg.alpha = 1f - 1f / cnt * i;
            // Debug.Log(cg.alpha);
        }
        ReturnPuzzleToParent();
    }
}
