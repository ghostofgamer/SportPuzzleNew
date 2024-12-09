using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] GameObject heart;

    public void Set(bool b)
    {
        heart.SetActive(b);
    }
}
