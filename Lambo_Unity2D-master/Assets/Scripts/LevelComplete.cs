using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    // Bắt đầu được gọi trước khi cập nhật khung hình đầu tiên
    public int nextSceneLoad;
    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex +1;
    }

    // Cập nhật được gọi một lần trên mỗi khung hình
    public void LoadNextLevel(){
        SceneManager.LoadScene(nextSceneLoad);
        if(nextSceneLoad >PlayerPrefs.GetInt("levelAt")){
            PlayerPrefs.SetInt("levelAt",nextSceneLoad);
        }
    }
}
