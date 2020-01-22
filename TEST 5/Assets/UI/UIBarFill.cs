using UnityEngine;
using UnityEngine.UI;

public class UIBarFill : MonoBehaviour
{
    
    public float fillBar(int current, int max)
    {
        float currentValue = current *1f;
        float maxValue = max * 1f;
        

        return (currentValue / maxValue);
    }
   
}
