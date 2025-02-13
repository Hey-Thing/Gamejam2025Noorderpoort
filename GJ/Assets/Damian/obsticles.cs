using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class obsticles : MonoBehaviour
{
    public bool isDayScene = true;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("car"))
        {
            if (isDayScene)
            {
                SceneManager.LoadScene("Office");
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
