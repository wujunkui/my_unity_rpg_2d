using System;
using SaveSystem;
using UnityEngine;


public class PlayerManager : MonoBehaviour, ISaveManager
{
    public Player.Player player;
    public static PlayerManager instance;

    public int currency;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance.gameObject);
    }

    public void LoadData(GameData _data)
    {
        currency = _data.currency;
    }

    public void SaveData(ref GameData _data)
    {
        _data.currency = currency;
    }
}