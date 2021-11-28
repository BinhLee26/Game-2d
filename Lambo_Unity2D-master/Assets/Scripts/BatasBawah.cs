using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatasBawah : MonoBehaviour
{
    // Bắt đầu được gọi trước khi cập nhật khung hình đầu tiên
    void Start()
    {
        
    }

    // Cập nhật được gọi một lần trên mỗi khung hình
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D coll){
        if(coll.tag=="Player"){
            PlayerHealth theplayerHealth = coll.gameObject.GetComponent<PlayerHealth>();
            theplayerHealth.makeDead();
        }
    }
}
