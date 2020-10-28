using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public bool isEntrance = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isEntrance == false)
            {
                SceneManager.LoadScene(2);
                isEntrance = true;
            }

            if (isEntrance == true)
            {
                // 방이동
            }
        }
    }
}
