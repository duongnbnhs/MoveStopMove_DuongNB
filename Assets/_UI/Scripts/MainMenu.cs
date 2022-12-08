using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    public void PlayButton()
    {
        UIManager.Ins.OpenUI<GamePlay>();
        SpawnBot.Ins.spawnStart = true;
        SpawnBot.Ins.Spawn(Random.Range(3,5));
        Close();
    }
}
