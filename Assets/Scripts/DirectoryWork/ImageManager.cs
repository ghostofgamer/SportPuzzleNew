using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
public class ImageManager : MonoBehaviour
{
    public GameObject filesListPan, filesContent, filePrefab;
    public RawImage img;
    private DirectoryInfo dirInfo = new DirectoryInfo("C:\\Users\\vanya\\Downloads");
    private FileInfo[] files;
    private GameObject[] instancedObjs;
    public static ImageManager instance;

    private Texture2D currentTexture;
    private int amountpuzzles = 3;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        filesListPan.SetActive(false);
        img.gameObject.SetActive(false);
    }
    public void LoadImagesList()
    {
        filesListPan.SetActive(true); img.gameObject.SetActive(false);
        files = new string[] { "*.jpeg", "*.jpg", "*.png" }.SelectMany(ext => dirInfo.GetFiles(ext, SearchOption.AllDirectories)).ToArray();
        instancedObjs = new GameObject[files.Length];
        for (int i = 0; i < files.Length; i++)
        {
            FileScript file = Instantiate(filePrefab, filesContent.transform).GetComponent<FileScript>();
            file.fileNameText.text = files[i].Name;
            file.index = i;
            instancedObjs[i] = file.gameObject;
        }
    }
    public void SelectImage(int index)
    {
        WWW www = new WWW("file://" + files[index].FullName);
        currentTexture = www.texture; 
        img.texture = www.texture;
        filesListPan.SetActive(false); img.gameObject.SetActive(true);
        foreach (GameObject obj in instancedObjs)
            Destroy(obj);
    }

    public void OnAmountInput(string amount)
    {
        int i;
        bool success = int.TryParse(amount, out i);
        if (success && i <= 60 && i >= 2)
        {
            amountpuzzles = i;
        }
        else Debug.Log("amount error");
    }
    public void CreatePuzzle()
    {
        Sprite mySprite = Sprite.Create(currentTexture, new Rect(0.0f, 0.0f, currentTexture.width, currentTexture.height), new Vector2(0.5f, 0.5f), 100.0f);   
        PuzzleController.instance.CreateUserPuzzle(mySprite, amountpuzzles);
        PuzzleController.instance.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
