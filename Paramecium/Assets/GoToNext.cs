using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNext : MonoBehaviour
{
    public int sceneIndex;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "PlayerCharacter"){
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }
    }
}
