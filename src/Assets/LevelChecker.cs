using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelChecker : MonoBehaviour
{
    public int TotalEnemyCount = 999;
    public int CurrentScene = 1;
    // Start is called before the first frame update
    void Start()
    {
        CurrentScene=SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void LateUpdate()
    {        
        TotalEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(TotalEnemyCount <= 0)
        {
            CurrentScene++;
            if (CurrentScene <= SceneManager.sceneCount || !Player.Alive)
            {
                CurrentScene = 1;
            }
            Invoke("SceneSwitch", 5f);
        }
    }
    IEnumerator SceneSwitch()
    {
         SceneManager.LoadScene(CurrentScene);
        return null;
    }
}
