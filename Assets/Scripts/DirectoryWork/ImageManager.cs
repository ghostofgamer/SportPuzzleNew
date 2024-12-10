using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System;
using Unity.VisualScripting;
using NativeGalleryNamespace;

public class ImageManager : MonoBehaviour
{
    public Image image;
    public GameObject filesListPan, filesContent, filePrefab;
    public RawImage img;
    public Image imageNew;
    // private DirectoryInfo dirInfo = new DirectoryInfo("C:\\Users\\vanya\\Downloads");
    private DirectoryInfo dirInfo = new DirectoryInfo("C:\\Users\\VadimPC\\Downloads");
    private FileInfo[] files;
    private GameObject[] instancedObjs;
    public static ImageManager instance;

    private Texture2D currentTexture;
    private int amountpuzzles = 3;

    private void Awake()
    {
        imageNew.gameObject.SetActive(false);
        instance = this;
    }
    
     /*private void Start()
    {
        filesListPan.SetActive(false);
        img.gameObject.SetActive(false);

        // Инициализация пути для сохранения данных
        string path = Application.persistentDataPath;
        dirInfo = new DirectoryInfo(path);
    }

    public void LoadImagesList()
    {
        filesListPan.SetActive(true);
        img.gameObject.SetActive(false);

        // Использование NativeGallery для получения изображений из галереи
        NativeGallery.GetImageFromGallery((path) =>
        {
            if (path != null)
            {
                // Загрузка текстуры из пути
                Texture2D texture = NativeGallery.LoadImageAtPath(path, 512);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                // Создание читаемой текстуры
                RenderTexture renderTexture = RenderTexture.GetTemporary(texture.width, texture.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
                Graphics.Blit(texture, renderTexture);
                RenderTexture.active = renderTexture;
                Texture2D readableTexture = new Texture2D(texture.width, texture.height);
                readableTexture.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);
                readableTexture.Apply();
                RenderTexture.ReleaseTemporary(renderTexture);

                // Сохранение текстуры в папке persistentDataPath
                byte[] bytes = readableTexture.EncodeToPNG();
                string filePath = Path.Combine(Application.persistentDataPath, Path.GetFileName(path));
                File.WriteAllBytes(filePath, bytes);

                // Обновление списка файлов
                files = new DirectoryInfo(Application.persistentDataPath).GetFiles("*.png", SearchOption.AllDirectories);
                instancedObjs = new GameObject[files.Length];
                for (int i = 0; i < files.Length; i++)
                {
                    FileScript file = Instantiate(filePrefab, filesContent.transform).GetComponent<FileScript>();
                    file.fileNameText.text = files[i].Name;
                    file.index = i;
                    instancedObjs[i] = file.gameObject;
                }
            }
        });
    }

    public void SelectImage(int index)
    {
        string path = files[index].FullName;
        byte[] fileData = File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);
        currentTexture = texture;
        img.texture = texture;
        filesListPan.SetActive(false);
        img.gameObject.SetActive(true);
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

        int createPuzzleCount = PlayerPrefs.GetInt("CreatePuzzle", 0);

        if (createPuzzleCount < 2)
        {
            createPuzzleCount++;
            PlayerPrefs.SetInt("CreatePuzzle", createPuzzleCount);
        }
    }
    */
    
    
    
    
    
    
    
    
    
    
    
    
    private void Start()
    {
        filesListPan.SetActive(false);
        img.gameObject.SetActive(false);
    }
    public void LoadImagesList()
    {
        NativeGallery.GetImageFromGallery((path) =>
        {
            // Проверка, был ли выбран файл
            if (path != null)
            {
                // Загрузка изображения из выбранного файла
                Texture2D texture = LoadTexturessss(path);

                // Установка выбранного изображения в объект Image
                if (texture != null)
                {
                    imageNew.gameObject.SetActive(true);
                    imageNew.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                    /*img.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));*/
                    // OnImageLoad.Invoke();
                }
                else
                {
                    Debug.LogError("Не удалось загрузить изображение из файла: " + path);
                }
            }
            else
            {
                Debug.Log("Пользователь отменил выбор изображения.");
            }
        });
        
        /*filesListPan.SetActive(true); img.gameObject.SetActive(false);
        files = new string[] { "*.jpeg", "*.jpg", "*.png" }.SelectMany(ext => dirInfo.GetFiles(ext, SearchOption.AllDirectories)).ToArray();
        instancedObjs = new GameObject[files.Length];
        for (int i = 0; i < files.Length; i++)
        {
            FileScript file = Instantiate(filePrefab, filesContent.transform).GetComponent<FileScript>();
            file.fileNameText.text = files[i].Name;
            file.index = i;
            instancedObjs[i] = file.gameObject;
        }*/
    }
    
    private Texture2D LoadTexturessss(string path)
    {
        // Загрузка файла в виде байтов
        byte[] bytes = System.IO.File.ReadAllBytes(path);

        // Создание текстуры из байтов
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(bytes);

        return texture;
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
        if (success && i <= 5 && i >= 2)
        {
            amountpuzzles = i;
        }
        else Debug.Log("amount error");
    }
    public void CreatePuzzle()
    {
        // Sprite mySprite = Sprite.Create(currentTexture, new Rect(0.0f, 0.0f, currentTexture.width, currentTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
        Sprite mySprite = imageNew.sprite;
        PuzzleController.instance.CreateUserPuzzle(mySprite, amountpuzzles);
        PuzzleController.instance.gameObject.SetActive(true);
        gameObject.SetActive(false);

        int createPuzzleCount = PlayerPrefs.GetInt("CreatePuzzle", 0);
        
        if (createPuzzleCount < 2)
        {
            createPuzzleCount++;
            PlayerPrefs.SetInt("CreatePuzzle", createPuzzleCount);
        }
    }
}
