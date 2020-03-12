﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class dragon : MonoBehaviour
{
   
    Rigidbody2D rb;
    [SerializeField]
    float maxSpeed = 3f;
    float move = 1;
    // Start is called before the first frame update
    void Start()
    { rb = GetComponent<Rigidbody2D>();}

    // Update is called once per frame
    void FixedUpdate()
    //apply velocity to rb
    { rb.velocity = new Vector2(move * maxSpeed, 0); }

    //move the animated dragon between two edges.
    private void OnTriggerEnter2D(Collider2D other)
    { if(other.gameObject.tag == "edge")
        { FlipSprite();} }
    private void FlipSprite()
    { Vector3 spriteScale = transform.localScale;
        spriteScale.x *= -1;
        transform.localScale = spriteScale;
        move *= -1;}
    

    
}
