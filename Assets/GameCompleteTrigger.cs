using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class GameCompleteTrigger : MonoBehaviour
{
    public string nextLevel = "L9_GameComplete";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hero"))
        {
            var player = other.gameObject.GetComponent<FirstPersonController>();
            player.LoadQuest(13);
            StartCoroutine(LoadEndLevel());
        }
    }

    IEnumerator LoadEndLevel()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(nextLevel);
    }

}

