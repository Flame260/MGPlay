using System.Collections;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Wave
{
    public GameObject[] enemyPrefabs;
    public int count = 6;
    public float spawnDelay = 0.6f;
}

public class WaveManager : MonoBehaviour
{
    public Wave[] waves;
    public TMP_Text waveText;

    void Start()
    {
        StartCoroutine(PlayWaves());
    }

    IEnumerator PlayWaves()
    {
        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < waves.Length; i++)
        {
            waveText.text = "WAVE " + (i + 1);
            yield return new WaitForSeconds(2f);
            waveText.text = "";

            yield return StartCoroutine(SpawnWave(waves[i]));
            yield return new WaitForSeconds(3f);
        }

        GameManager2.Instance.Win();
    }

    IEnumerator SpawnWave(Wave wave)
    {
        Camera cam = Camera.main;
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        for (int i = 0; i < wave.count; i++)
        {
            int pick = Random.Range(0, wave.enemyPrefabs.Length);
            float x = Random.Range(-halfWidth + 1, halfWidth - 1);
            Instantiate(wave.enemyPrefabs[pick], new Vector3(x, 0, halfHeight + 2), Quaternion.identity);
            yield return new WaitForSeconds(wave.spawnDelay);
        }

        while (FindObjectsByType<EnemyShip>(FindObjectsSortMode.None).Length > 0)
        {
            yield return null;
        }
    }
}