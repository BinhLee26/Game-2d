using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour

{
    public float rotationSpeed;
    // Bắt đầu được gọi trước khi cập nhật khung hình đầu tiên
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.Rotate(new Vector3(0,0, rotationSpeed));
    }
    // void OnTriggerEnter2D(Collider2D collider){
    //     Destroy(collider.gameObject);
    // }
}
