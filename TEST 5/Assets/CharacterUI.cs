using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField]
    private Transform position;
    [SerializeField]
    private float xOffset;
    [SerializeField]
    private float yOffset;

    private void Start()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponentInChildren<GameObject>().SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponentInChildren<GameObject>().SetActive(false);
    }

    // Start is called before the first frame update
    void LateUpdate()
    {
        transform.position = new Vector2(position.position.x + xOffset, position.position.y + yOffset);
        transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
