using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _gm;
    public static GameManager GM { get => _gm; }

    [SerializeField] public LevelManager _lm;
    [SerializeField] public DeathManager _dm;

    void Start()
    {
        if (GM != null) return;
        Init();
    }

    void Init()
    {
        _gm = this;
    }

}
