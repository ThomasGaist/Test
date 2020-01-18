using UnityEngine;


public class MobSpawnerTest : MonoBehaviour
{

    public KeyCode mobSpawnKey = KeyCode.Q;
    private Vector3 mobSpawnPos;
 
    [SerializeField]
    private GameObject mob;
    [SerializeField]
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        // mobSpawnPos = this.transform;
        GetPlayerPosition();

    }
    private void GetPlayerPosition()
    {
        mobSpawnPos = new Vector3(player.transform.position.x+100, player.transform.position.y, player.transform.position.z);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(mobSpawnKey))
        {
            GetPlayerPosition();
            Instantiate(mob,mobSpawnPos, Quaternion.identity);
            GameEvents.current.EnemyInstantiated();
        }
    }
}
