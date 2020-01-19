using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildDemoText : MonoBehaviour
{
   
    private Text text;
    [SerializeField]
    private MobSpawnerTest mobSpawner;
    private Player player;
    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponentInChildren<Text>();
        player = FindObjectOfType<Player>();
        text.text = $"{mobSpawner.mobSpawnKey}: Spawn enemy\n{player.attackKeys[0]} or {player.attackKeys[1]}: Attack enemy (no attack animation yet)\nC: Inventory\nE: Pick up items\n{FindObjectOfType<PauseMenu>().pauseKey}: Pause game\n\nIn this demo you don't take damage from the enemies";
    }
    private void Update()
    {
        if (PauseMenu.gameIsPaused)
        {
            GetComponentInChildren<Text>().enabled = false;
        }
        else
        {
            GetComponentInChildren<Text>().enabled = true;
        }
    }

}
