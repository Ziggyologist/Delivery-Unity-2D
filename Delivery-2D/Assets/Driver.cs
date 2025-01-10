using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float steerSpeed = 200f;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float slowSpeed = 2f;
    [SerializeField] float boostSpeed = 20f;
    [SerializeField] float defaultSpeed = 10f;
    [SerializeField] float buffDuration = 5f;

    bool isBoosted = false;
    bool isSlowed = false;
    float boostTimer = 0f;
    Coroutine boostCoroutine;

    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, moveAmount, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boost"))
        {
            HandleBoost();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBoosted)
        {
            StopCoroutine(boostCoroutine);
            isBoosted = false;  // Reset boost state on collision
        }
        HandleSlow();
    }

    void HandleBoost()
    {
        if (!isBoosted)
        {
            boostCoroutine = StartCoroutine(BoostRoutine());
        }
        else
        {
            boostTimer += buffDuration;  // Extend boost duration
        }
    }

    void HandleSlow()
    {
        isSlowed = true;
        moveSpeed = slowSpeed;
        Invoke(nameof(ResetSlow), buffDuration);
    }

    IEnumerator BoostRoutine()
    {
        isBoosted = true;
        boostTimer = buffDuration;
        moveSpeed = boostSpeed;

        while (boostTimer > 0)
        {
            boostTimer -= Time.deltaTime;
            yield return null;
        }

        isBoosted = false;
        ReturnToSpeed();
    }

    void ResetSlow()
    {
        isSlowed = false;
        ReturnToSpeed();
    }

    void ReturnToSpeed()
    {
        if (!isBoosted && !isSlowed)
        {
            moveSpeed = defaultSpeed;
        }
        else if (isBoosted)
        {
            moveSpeed = boostSpeed;
        }
        else if (isSlowed)
        {
            moveSpeed = slowSpeed;
        }
    }
}

