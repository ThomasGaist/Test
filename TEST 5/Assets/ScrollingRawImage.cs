using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingRawImage : MonoBehaviour
{
    [SerializeField]
    private float verticalSpeed;
    [SerializeField]
    private float horizontalSpeed;
    private RawImage rawImage;
    // Start is called before the first frame update
    void Start()
    {
        rawImage = GetComponent<RawImage>();

    }

    // Update is called once per frame
    void Update()
    {
        verticalFlow();
        horizontalFlow();
    }

    void verticalFlow()
    {
        float speed = rawImage.uvRect.y;
        rawImage.uvRect = new Rect(rawImage.uvRect.x, speed + (verticalSpeed*0.01f), rawImage.uvRect.width, rawImage.uvRect.height);
    }
    void horizontalFlow()
    {
        float speed = rawImage.uvRect.x;
        rawImage.uvRect = new Rect(speed + (horizontalSpeed * 0.01f), rawImage.uvRect.y, rawImage.uvRect.width, rawImage.uvRect.height);
    }
}
