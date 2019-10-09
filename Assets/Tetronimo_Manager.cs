using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetronimo_Manager: MonoBehaviour
{

    float sinceLastFall = 0;

    bool isValidGridPos() {
        foreach (Transform child in transform) {
            Vector2 v = Playfield.roundVec2(child.position);

            if (!Playfield.InsideBorder(v)) {
                return false;
            }

            if (Playfield.grid[(int)v.x, (int)v.y] != null && Playfield.grid[(int)v.x, (int)v.y].parent != transform) {
                return false;
            }
        }
        return true;
    }

    void updateGrid() {
        for (int y = 0; y < Playfield.height; y++) {
            for (int x = 0; x < Playfield.width; x++) {
                if (Playfield.grid[x, y] != null) {
                    if (Playfield.grid[x, y].parent == transform) {
                        Playfield.grid[x, y] = null;
                    }
                }
            }
        }
        
        foreach (Transform child in transform) {
            Vector2 v = Playfield.roundVec2(child.position);
            Playfield.grid[(int)v.x, (int)v.y] = child;
        }
    }
    
    void Update() {
        Debug.Log(sinceLastFall);
        sinceLastFall += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            transform.position += new Vector3(-1, 0, 0);

            if (isValidGridPos()) {
                updateGrid();
            } else {
                transform.position += new Vector3(1, 0, 0);
            }
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            transform.position += new Vector3(1, 0, 0);

            if (isValidGridPos()) {
                updateGrid();
            } else {
                transform.position += new Vector3(-1, 0, 0);
            }
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            transform.Rotate(0, 0, -90);

            if (isValidGridPos()) {
                updateGrid();
            } else {
                transform.Rotate(0, 0, 90);
            }
        } else if (Input.GetKeyDown(KeyCode.DownArrow) || sinceLastFall >= 1) {
            transform.position += new Vector3(0, -1, 0);
            
            if (isValidGridPos()) {
                updateGrid();
            } else {
                transform.position += new Vector3(0, 1, 0);
                
                Playfield.deleteFullRows();
                
                FindObjectOfType<Spawner_Script>().spawnNext();
                
                enabled = false;
            }
            sinceLastFall = 0;
        }
    }

    private void Start() {
        if (!isValidGridPos()) {
            Debug.Log("Game Loss");
            Destroy(gameObject);
        }
    }
}
