using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettenPuzzle : MonoBehaviour, IDropHandler
{
    [SerializeField] RectTransform innerImage;
    Vector2 puzzleID;

    public void Init(puzzleVariant variant, int puzzleSize, Sprite sprite, Vector2 id)
    {
        innerImage.gameObject.GetComponent<Image>().sprite = sprite;
        innerImage.localScale = Vector3.one * puzzleSize;
        innerImage.GetComponent<Image>().enabled = false;
        puzzleID = id;
        float offsetX = 50 * (puzzleSize - 3) - (id.x - 1) * 100;
        float offsetY = 50 * (puzzleSize - 3) - (id.y - 1) * 100;
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

    public void OnDrop(PointerEventData eventData)
    {      
        PuzzleController.instance.SetSetted(puzzleID, GetComponent<RectTransform>().anchoredPosition);
    }
    private void OnEnable()
    {
        PuzzleController.puzzleSetResults += ChangeImage;
    }

    private void OnDisable()
    {
        PuzzleController.puzzleSetResults -= ChangeImage;
    }

    private void ChangeImage(Vector2 id, bool b)
    {
        if(id == puzzleID && b)
        {
            float animDelay = .5f;
            Invoke("ActivateImage", animDelay);
        }
    }

    private void ActivateImage()
    {
        innerImage.GetComponent<Image>().enabled = true;
    }
}
