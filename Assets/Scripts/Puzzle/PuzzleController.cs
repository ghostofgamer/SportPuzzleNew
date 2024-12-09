using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum puzzleVariant
{
    formL, formR, formU, formD, formC, formLU, formLD, formRU, formRD
}

public class PuzzleController : MonoBehaviour
{
    [Header("moveable puzzles")]
    [SerializeField] private GameObject formL;
    [SerializeField] private GameObject formR;
    [SerializeField] private GameObject formU;
    [SerializeField] private GameObject formD;
    [SerializeField] private GameObject formC;
    [SerializeField] private GameObject formLU;
    [SerializeField] private GameObject formLD;
    [SerializeField] private GameObject formRU;
    [SerializeField] private GameObject formRD;

    [Header("setted puzzles")]
    [SerializeField] private GameObject formLset;
    [SerializeField] private GameObject formRset;
    [SerializeField] private GameObject formUset;
    [SerializeField] private GameObject formDset;
    [SerializeField] private GameObject formCset;
    [SerializeField] private GameObject formLUset;
    [SerializeField] private GameObject formLDset;
    [SerializeField] private GameObject formRUset;
    [SerializeField] private GameObject formRDset;

    [SerializeField] internal RectTransform border;
    [SerializeField] private RectTransform scrollBorder;
    

    [SerializeField] private int puzzleSize = 2;
    [SerializeField] private float moveableSizeMultiplier = 2;
    [SerializeField] private bool shuffle = true;
    [SerializeField] internal bool setUserPuzzle = false;
    [SerializeField] TextMeshProUGUI levelName;
    [SerializeField] private Sprite[] sprites;
    private Sprite currentSprite;

    HorizontalLayoutGroup hlg;

    private int correctPuzzles = 0;
    Vector2 movableID;
    Vector2 settedID;
    float detailMult;
    float rectMoveScale;

    public static PuzzleController instance;
    public static event Action<Vector2, bool> puzzleSetResults;
    public static event Action<Vector2, Vector2, float, bool> puzzleMoveResults;


    public void SetMovable(Vector2 id)
    {
        movableID = id;
    }
    public void SetSetted(Vector2 id, Vector2 pos)
    {
        settedID = id;
        if(settedID == movableID)
        {
            puzzleSetResults?.Invoke(settedID, true);
            puzzleMoveResults?.Invoke(movableID, pos, detailMult, true);
            scrollBorder.offsetMax -= new Vector2(rectMoveScale + hlg.spacing, 0);

            correctPuzzles++;
            if(correctPuzzles == puzzleSize * puzzleSize)
            {
                WinScript.instance.Win(puzzleSize * puzzleSize);
            }
        }
        else
        {
            puzzleSetResults?.Invoke(settedID, false);
            puzzleMoveResults?.Invoke(movableID, pos, detailMult, false);
        }
        settedID = Vector2.left;
        movableID = Vector2.down;
    }

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {       
        if (setUserPuzzle) gameObject.SetActive(false);
    }

    void GenerateForm()
    {
        GameObject instSetVariant = null;
        GameObject instMoveVariant = null;
        GameObject instSet = null;
        GameObject instMove = null;
        RectTransform rectSet = null;
        RectTransform rectMove = null;
        puzzleVariant variant;
        List<GameObject> puzzlesForShuffle = new List<GameObject>();

        hlg = scrollBorder.GetComponent<HorizontalLayoutGroup>();
        hlg.spacing = 66 * moveableSizeMultiplier;//rectMoveScale;
        hlg.padding.left = (int)(hlg.spacing / 2);//(int)(rectMoveScale/2);
        scrollBorder.offsetMax = new Vector2((rectMoveScale + hlg.spacing) * puzzleSize * puzzleSize, scrollBorder.offsetMax.y);
        scrollBorder.offsetMin = Vector2.zero;


        for (int i = 0; i<puzzleSize; i++)
        {
            for (int j = 0; j < puzzleSize; j++)
            {
                if(i == 0)
                {
                    if (j == 0)
                    {                       
                        variant = puzzleVariant.formLD;
                    }
                    else if (j == puzzleSize - 1)
                    {                        
                        variant = puzzleVariant.formLU;
                    }
                    else
                    {                       
                        variant = puzzleVariant.formL;
                    }
                }
                else if(i == puzzleSize - 1)
                {
                    if (j == 0)
                    {                        
                        variant = puzzleVariant.formRD;
                    }
                    else if (j == puzzleSize - 1)
                    {                        
                        variant = puzzleVariant.formRU;
                    }
                    else
                    {                       
                        variant = puzzleVariant.formR;
                    }
                }
                else if (j == 0)
                {
                    variant = puzzleVariant.formD;                   
                }
                else if (j == puzzleSize - 1)
                {
                    variant = puzzleVariant.formU;                    
                }
                else
                {
                    variant = puzzleVariant.formC;                   
                }
                switch (variant)
                {
                    case puzzleVariant.formLU:
                        instSetVariant = formLUset;
                        instMoveVariant = formLU;
                        break;
                    case puzzleVariant.formLD:
                        instSetVariant = formLDset;
                        instMoveVariant = formLD;
                        break;
                    case puzzleVariant.formRU:
                        instSetVariant = formRUset;
                        instMoveVariant = formRU;
                        break;
                    case puzzleVariant.formRD:
                        instSetVariant = formRDset;
                        instMoveVariant = formRD;
                        break;
                    case puzzleVariant.formR:
                        instSetVariant = formRset;
                        instMoveVariant = formR;
                        break;
                    case puzzleVariant.formL:
                        instSetVariant = formLset;
                        instMoveVariant = formL;
                        break;
                    case puzzleVariant.formU:
                        instSetVariant = formUset;
                        instMoveVariant = formU;
                        break;
                    case puzzleVariant.formD:
                        instSetVariant = formDset;
                        instMoveVariant = formD;
                        break;
                    case puzzleVariant.formC:
                        instSetVariant = formCset;
                        instMoveVariant = formC;
                        break;
                }
                
                instSet = Instantiate(instSetVariant, border);
                rectSet = instSet.GetComponent<RectTransform>();
                rectSet.anchoredPosition = new Vector2((i+0.5f)*100*detailMult, (j + 0.5f) * 100*detailMult);
                rectSet.localScale *= detailMult;
                instSet.GetComponent<SettenPuzzle>().Init(variant, puzzleSize, currentSprite, new Vector2(i,j));

                instMove = Instantiate(instMoveVariant, scrollBorder);
                instMove.GetComponent<MoveablePuzzle>().Init(variant, puzzleSize, currentSprite, new Vector2(i, j));
                rectMove = instMove.GetComponent<RectTransform>();
                rectMove.localScale *= moveableSizeMultiplier;// (puzzleSize > 5 ? detailMult : (puzzleSize < 3 ? detailMult / 3 : detailMult / 2));
                if(shuffle) puzzlesForShuffle.Add(instMove);

            }
        }
        if (shuffle)
        {
            Shuffle(puzzlesForShuffle);
            for (int i = 0; i < puzzlesForShuffle.Count; i++)
                puzzlesForShuffle[i].transform.SetParent(border);
            for (int i = 0; i < puzzlesForShuffle.Count; i++)
                puzzlesForShuffle[i].transform.SetParent(scrollBorder);
        }
        
    }
    void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n+1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    public void SetRandomSprite()
    {
        int rand = UnityEngine.Random.Range(0, sprites.Length);
        currentSprite = sprites[rand];
    }
    public void CreateUserPuzzle(Sprite s, int i)
    {
        puzzleSize = i;
        currentSprite = s;
        detailMult = (border.offsetMax.x - border.offsetMin.x) / puzzleSize / 100;
        rectMoveScale = 100 * moveableSizeMultiplier;//100 * (puzzleSize > 5 ? detailMult : (puzzleSize < 3 ? detailMult / 3 : detailMult / 2));

        GenerateForm();
    }
    public void CreateDefaultPuzzle(int id, int cnt, int time, Sprite s)
    {
        if (setUserPuzzle)
            return;
        puzzleSize = cnt;
        levelName.text = "Level " + id;
        Timer.instance.StartTimer(time);
        currentSprite = s;
        detailMult = (border.offsetMax.x - border.offsetMin.x) / puzzleSize / 100;
        rectMoveScale = 100 * moveableSizeMultiplier;

        GenerateForm();
    }
}
