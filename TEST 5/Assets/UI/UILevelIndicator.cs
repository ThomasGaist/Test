using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UILevelIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onLevelChanged += ChangeLevelText;
    }
    private void ChangeLevelText()
    {
        GetComponent<Text>().text = PlayerLevel.Level.ToString();
    }
}
