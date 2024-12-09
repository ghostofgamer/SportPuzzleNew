using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelScriptableObject : ScriptableObject
{
    [SerializeField]
    int id;
    public int Id { get => id; private set => id = value; }

    [SerializeField]
    int puzzleCount;
    public int PuzzleCount { get => puzzleCount; private set => puzzleCount = value; }

    [SerializeField]
    int time;
    public int Time { get => time; private set => time = value; }

    [SerializeField]
    Sprite sprite;
    public Sprite Sprite { get => sprite; private set => sprite = value; }
}
