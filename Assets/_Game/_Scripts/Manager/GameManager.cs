using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public Transform PlayerTF;
    private void Awake()
    {
        PlayerTF = player.transform;
        player.inventory = new Inventory(10);
    }
}
