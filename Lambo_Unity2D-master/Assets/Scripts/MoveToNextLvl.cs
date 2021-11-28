using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextLvl : MonoBehaviour
{
  public int nextSceneLoad;
    // Bắt đầu được gọi trước khi cập nhật khung hình đầu tiên
    void Start()
    {
      nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
      if(coll.gameObject.tag == "Player")
      {

            //chuyển đổi cấp độ
            SceneManager.LoadScene(nextSceneLoad);

          if(nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
          {
            PlayerPrefs.SetInt("levelAt", nextSceneLoad);
          }
      }
    }
}
