using System.Collections;
using UnityEngine.Experimental.Rendering.LWRP;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    UnityEngine.Experimental.Rendering.Universal.Light2D TorchLight;
    [SerializeField] private float minWaitTime = 0.2f;
    [SerializeField] private float maxWaitTime = 0.5f;
    [SerializeField] private float minIntensity = 0.2f;
    [SerializeField] private float maxIntensity = 1f;
    //[SerializeField] private float minFallOff = 0.2f;
    //[SerializeField] private float maxFallOff = 1f;
    [SerializeField] private float minOuterRadius = 30f;
    [SerializeField] private float maxOuterRadius = 80f;


  



    private void Start()
    {
        TorchLight = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
     

        StartCoroutine(Flickering() );
        


    }
    

    IEnumerator Flickering()
    {
       

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            TorchLight.intensity = Random.Range(minIntensity, maxIntensity);
            // FallOffIntensity er Get, men ikke Set
            // TorchLight.falloffIntensity = Random.Range(minFallOff, maxFallOff);
            TorchLight.pointLightOuterRadius = Random.Range(minOuterRadius, maxOuterRadius);
        }
    }


}
