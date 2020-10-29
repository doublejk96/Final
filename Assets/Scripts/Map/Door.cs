using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject dungeonPos;

    public bool isInDungeon = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInDungeon = true;
            Player.Ins.transform.position = dungeonPos.transform.position;
        }
    }
}
