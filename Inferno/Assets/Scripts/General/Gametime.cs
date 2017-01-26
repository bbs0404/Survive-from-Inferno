using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gametime : MonoBehaviour {

    public static float deltaTime {
        get
        {
            if (UserInterfaceManager.Inst().isPaused)
                return 0;
            else
                return Time.deltaTime;
        }
    }
}
