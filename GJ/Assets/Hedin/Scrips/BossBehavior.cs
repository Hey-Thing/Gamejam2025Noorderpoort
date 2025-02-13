using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BossBehavior : MonoBehaviour
{
    public bool beerholding = false;
    public bool bossclose = false;

    private void Update()
    {
        if (bossclose && beerholding)
        {
            Debug.Log("fail");
            StartCoroutine(WaitAndLoadScene());
        }
    }

    IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("damian1");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            Debug.Log("bossclose");
            bossclose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            bossclose = false;
        }
    }
}
