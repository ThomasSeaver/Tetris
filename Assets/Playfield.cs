using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playfield : MonoBehaviour
{
    public static int height = 20;
    public static int width = 10;
    public static Transform[,] grid = new Transform[width, height];

    // Necessary as some rotations can make coordinates imperfect
    public static Vector2 roundVec2(Vector2 v) {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    // Y above not checked as rotations may need to be made into that above space, and is not usually considered illegal
    public static bool InsideBorder(Vector2 pos) {
        return ((int)pos.x >= 0 && (int)pos.x < width && (int)pos.y >= 0);
    }

    public static void deleteRow(int y) {
        for (int x = 0; x < width; x++) {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public static void decreaseRow(int y) {
        for (int x = 0; x < width; x++) {
            if (grid[x, y] != null) {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public static void decreaseRowsAbove(int y) {
        for (int i = y; i < height; i++) {
            decreaseRow(i);
        }
    }

    public static bool isRowFull(int y) {
        for (int x = 0; x < width; x++) {
            if (grid[x, y] == null) {
                return false;
            }
        }
        return true;
    }

    public static void deleteFullRows() {
        for (int y = 0; y < height; y++) {
            if (isRowFull(y)) {
                deleteRow(y);
                decreaseRowsAbove(y + 1);
                y--;
            }
        }
    }
}
