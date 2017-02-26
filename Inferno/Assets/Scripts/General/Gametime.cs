using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gametime : MonoBehaviour {

    public static float deltaTime {
        get
        {
            if (InGameSystemManager.Inst().isPaused || InGameSystemManager.Inst().isGameOver)
                return 0;
            else
                return Time.deltaTime;
        }
    }
}
