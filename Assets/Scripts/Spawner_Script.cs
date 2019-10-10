using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Script : MonoBehaviour
{
    // Tetronimo Prefabs
    public GameObject[] Tetronimos;
    private float timeTracker = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnNext();
    }

    public void spawnNext()
    {
        int rand = Random.Range(0, Tetronimos.Length);

        Instantiate(Tetronimos[rand], transform.position, Quaternion.identity);
    }
}
