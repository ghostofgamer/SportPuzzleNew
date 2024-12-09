using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FileScript : MonoBehaviour
{
    public TextMeshProUGUI fileNameText;
    internal int index;
    
    public void Onclick()
    {
        ImageManager.instance.SelectImage(index);
    }
}
