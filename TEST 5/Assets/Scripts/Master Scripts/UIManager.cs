using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager: MonoBehaviour
{


    public void SaveGame()
    {
        Debug.Log("game saved");
        SaveLoadDataManager.instance.SaveGame();
    }

    public void LoadGame()
    {
        Debug.Log("game loaded");
        SaveLoadDataManager.instance.LoadGame();
    }
}
