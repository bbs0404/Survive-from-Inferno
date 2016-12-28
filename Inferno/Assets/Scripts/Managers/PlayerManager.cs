using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonBehaviour<PlayerManager> {

    [SerializeField]
    private GameObject player;

    public GameObject getPlayer()
    {
        return player;
    }
}
