﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_FallFromPlatform : Enemy
{
    private int _right = -1;

    public Transform left;
    public Transform right;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += Vector3.right * (_right * 0.04f);
        if (WallCheck())
        {
            _right *= -1;
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        }
    }

    bool WallCheck()
    {
        return Physics2D.Linecast(transform.position, left.position) || Physics2D.Linecast(transform.position, right.position);
    }

    public override void OnCollisionEnter2D(Collision2D other)
    {        
        if (other.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast"))
        {
            _right *= -1;
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        }
        if (other.gameObject.tag.Equals("Fireball"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
