﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
 public LayerMask enemyMask;
 public float speed = 1;
 Rigidbody2D myBody;
 Transform myTrans;
 float myWidth, myHeight;

	
	void Start () {
		myTrans = this.transform;
  		myBody = this.GetComponent<Rigidbody2D>();
		SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
		myWidth = mySprite.bounds.extents.x;
		myHeight = mySprite.bounds.extents.y;
	}

	// Cập nhật được gọi một lần trên mỗi khung hình
	void FixedUpdate () {

		Vector2 lineCastPos = myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth + Vector2.up * myHeight;
		Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
  		bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);

		Debug.DrawLine(lineCastPos, lineCastPos - myTrans.right.toVector2() * .05f);
  		bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2() * .05f, enemyMask);

		//khi nó chạm vào các lớp isBlocked và isGrounded, nó sẽ đảo ngược hướng
		if (isBlocked||isGrounded){
			Vector3 currRot = myTrans.eulerAngles;
			currRot.y += 180;
			myTrans.eulerAngles =currRot;
		}
		//Luôn tiến về phía trước
		Vector2 myVel = myBody.velocity;
			myVel.x = -myTrans.right.x * speed;
			myBody.velocity = myVel;
	}
	void OnTriggerEnter2D(Collider2D coll){
		if(coll.gameObject.name=="fireball"){
			Destroy(gameObject);
		}
	}
}
