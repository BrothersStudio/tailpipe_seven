﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScroll : MonoBehaviour
{
    public GameObject title_text;
    public GameObject any_button_text;
    Vector3 top = new Vector3(-4.29f, 6.66f, -10);
    float move_speed = 0.1f;

    bool moving = false;
    bool press_button = false;

    public AudioClip cut_theme;

    public void StartMoving()
    {
        moving = true;
        Invoke("Speedup", 2f);
    }

    void Speedup()
    {
        move_speed += 0.04f;
    }

    void FixedUpdate()
    {
        if (moving && transform.position.y < top.y)
        {
            Vector3 current_position = transform.position;
            current_position.y += move_speed;
            transform.position = current_position;
        }
        else if (moving && transform.position.y >= top.y)
        {
            moving = false;
            Invoke("AppearTextSoon", 1.4f);
        }
    }

    void AppearTextSoon()
    {
        title_text.SetActive(true);
        any_button_text.SetActive(true);
        press_button = true;
    }

    void Update()
    {
        if (press_button && Input.anyKeyDown)
        {
            SceneManager.LoadScene("Game");
        }
        else if (Input.anyKeyDown)
        {
            moving = false;

            CancelInvoke();
            transform.position = top;
            AppearTextSoon();

            GetComponentInChildren<AudioSource>().clip = cut_theme;
            GetComponentInChildren<AudioSource>().Play();
        }
    }
}
