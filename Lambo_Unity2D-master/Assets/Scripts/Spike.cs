using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour {

    public float damage;
	public float damageRate;
	public float pushBackForce;

	float nextDamage;

	// Sử dụng cái này để khởi tạo
	void Start () {
		nextDamage = 0f;
	}

	// Cập nhật được gọi một lần trên mỗi khung hình
	void Update () {
		
	}
    // private void OnTriggerEnter2D(Collider2D collision)
    // {
        // if(collision.CompareTag("Player"))
        // {
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //     Debug.Log("Die");
        // }
    // }
	void OnTriggerStay2D(Collider2D other){
		if(other.tag=="Player" && nextDamage<Time.time){
			PlayerHealth thePlayerHeath = other.gameObject.GetComponent<PlayerHealth>();
			thePlayerHeath.addDamage(damage);
			nextDamage = Time.time + damageRate;

			pushBack(other.transform);
		}
	}

	void pushBack(Transform pushObject){
		Vector2 pushDirection = new Vector2(0, (pushObject.position.y - transform.position.y)).normalized;
		pushDirection*=pushBackForce;
		Rigidbody2D pushRB = pushObject.gameObject.GetComponent<Rigidbody2D>();
		pushRB.velocity = Vector2.zero;
		pushRB.AddForce(pushDirection, ForceMode2D.Impulse);
	}
}
