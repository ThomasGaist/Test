using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    Camera camera;

    private GameEvents eventsystem;

    private void Start()
    {
        eventsystem = GameEvents.current;
        eventsystem.onEnemyDamage += EnemyDamageShake;
        eventsystem.onPlayerDamage += PlayerDamageShake;
    }

    private void Awake()
    {
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = camera.transform.localPosition;

        float elapsed = 0f; 
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;


            camera.transform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        camera.transform.localPosition = originalPos; 
    }

    void EnemyDamageShake()
    {
        StartCoroutine(Shake(0.1f, 0.15f));
    }

    void PlayerDamageShake()
    {
        StartCoroutine(Shake(0.2f, 0.2f));
    }
}

