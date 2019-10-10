using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pauseControl : MonoBehaviour
{
    public bool pause = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            pause = !pause;
            Time.timeScale = pause ? 0 : 1;
            if (gameObject.GetComponent<Text>() != null) {
                gameObject.GetComponent<Text>().enabled = pause;
            }
            if (gameObject.GetComponent<Renderer>() != null) {
                gameObject.GetComponent<Renderer>().enabled = pause;
            }
        }   
    }
}
