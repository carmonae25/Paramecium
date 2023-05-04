using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    public static LevelMusic level1Music;

    // Start is called before the first frame update
    void Start()
    {
        if(level1Music == null){
            level1Music = this;
            DontDestroyOnLoad(this.gameObject);       
        }
        else{
            Destroy(this.gameObject);
        }

        
    }

    private void OnLevelWasLoaded(int level){
        
        if(level == 0){
            Destroy(this.gameObject);
        }
    }
}
