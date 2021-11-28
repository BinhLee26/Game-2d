using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheWin : MonoBehaviour
{
    // Bắt đầu được gọi trước khi cập nhật khung hình đầu tiên
    void Start()
    {
        
    }

    // Cập nhật được gọi một lần trên mỗi khung hình
    void Update()
    {
        
    }
    public void OnTriggerEnter2D (Collider2D coll){
        if(coll.gameObject.tag== "Player"){
            SceneManager.LoadScene("winn");
        }
    }
    
}
