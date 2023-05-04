using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    public Canvas optionsMenu;
    public Canvas howToPlay;

    private void Start() {
        optionsMenu.enabled = false;
        howToPlay.enabled = false;
    }
    public void OpenMenu(){
        optionsMenu.enabled = true;
        howToPlay.enabled = false;
    }

    public void OpenHelp(){
        howToPlay.enabled = true;
        optionsMenu.enabled = false;
    }

    public void CloseMenu(){
        optionsMenu.enabled = false;
    }

    public void CloseHelp(){
        howToPlay.enabled = false;
    }
}
