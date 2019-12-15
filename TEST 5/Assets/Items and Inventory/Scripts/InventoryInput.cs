using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    [SerializeField] GameObject characterPanelGameObject;
    [SerializeField] GameObject equipmentPanelGameObject;
    [SerializeField] KeyCode[] toggleCharacterPanelyKeys;
    [SerializeField] KeyCode[] toggleInventoryKeys;
    // Update is called once per frame

    private void Start()
    {
        characterPanelGameObject.SetActive(false);
    }
    void Update()
    {
        for (int i = 0; i < toggleCharacterPanelyKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleCharacterPanelyKeys[i]))
            {
                characterPanelGameObject.SetActive(!characterPanelGameObject.activeSelf);
                if (characterPanelGameObject.activeSelf)
                {
                    equipmentPanelGameObject.SetActive(true);
                }
                /*else if(equipmentPanelGameObject.activeSelf && !characterPanelGameObject.activeSelf)
                {
                    characterPanelGameObject.SetActive(true);
                }*/
                break;
            }
        }

        for (int i = 0; i < toggleInventoryKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleInventoryKeys[i]))
            {
                if (!characterPanelGameObject.activeSelf)
                {
                    characterPanelGameObject.SetActive(true);
                    equipmentPanelGameObject.SetActive(false);

                }

                else if (equipmentPanelGameObject.activeSelf)
                {
                    equipmentPanelGameObject.SetActive(false);
                }
                else
                {
                    characterPanelGameObject.SetActive(false);
                }
                break;
            }
        }
    }

    public void ToggleEquipmentPanel()
    {
        equipmentPanelGameObject.SetActive(!equipmentPanelGameObject.activeSelf);
    }
}
