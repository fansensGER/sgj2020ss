﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCursor : MonoBehaviour
{
    public MusicManagement musicManager;
    public IconChanger iconChanger;
    private Vector2 movement;
    private Vector3 viewport;
    public float speed = 1;
    private Camera _camera;
    public bool down;
    public bool cmp = false;
    public static Air air;

    public bool waterU, fireU, airU, earthU;


    //Element Switch
    public enum Element
    {
        WATER,
        FIRE,
        EARTH,
        AIR
    }

    public Element element;


    private void Start()
    {
        air = GetComponent<Air>();
        element = Element.EARTH;
        Cursor.lockState = CursorLockMode.Locked;
        _camera = Camera.main;
    }

    private void Update()
    {
        //element switch
        if (Input.GetKeyDown("q") && !down)
        {
            element = (Element) (((int) element + 5) % 4);
            switch (element)
            {
                case Element.WATER:
                    musicManager.WaterActivated();
                    break;
                case Element.FIRE:
                    musicManager.FireActivated();
                    break;
                case Element.EARTH:
                    musicManager.EarthActivated();
                    break;
                case Element.AIR:
                    musicManager.AirActivated();
                    break;
            }

            iconChanger.SwitchTo(element);
        }
        else if (Input.GetKeyDown("e") && !down)
        {
            element = (Element) (((int) element + 3) % 4);
            switch (element)
            {
                case Element.WATER:
                    musicManager.WaterActivated();
                    break;
                case Element.FIRE:
                    musicManager.FireActivated();
                    break;
                case Element.EARTH:
                    musicManager.EarthActivated();
                    break;
                case Element.AIR:
                    musicManager.AirActivated();
                    break;
            }

            iconChanger.SwitchTo(element);
        }

        //air
        if (element == Element.AIR && airU)
        {
            if (down && !cmp)
            {
                cmp = !cmp;
                air.MouseDown();
            }

            if (!down && cmp)
            {
                cmp = !cmp;
                air.MouseUp();
            }
        }


        movement.x = Input.GetAxisRaw("Mouse X");
        movement.y = Input.GetAxisRaw("Mouse Y");

        if (Input.GetMouseButtonDown(0))
        {
            down = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            down = false;
        }

        transform.position += new Vector3(movement.x, movement.y, 0) * speed / 10;

        //constraints
        viewport = Camera.main.WorldToViewportPoint(transform.position);
        //Top
        if (viewport.y > 1)
        {
            transform.position = _camera.ViewportToWorldPoint(new Vector3(viewport.x, 1, viewport.z));
        }
        //Bottom
        else if (viewport.y < 0)
        {
            transform.position = _camera.ViewportToWorldPoint(new Vector3(viewport.x, 0, viewport.z));
        }

        //Right
        if (viewport.x > 1)
        {
            transform.position = _camera.ViewportToWorldPoint(new Vector3(1, viewport.y, viewport.z));
        }

        //Left
        if (viewport.x < 0)
        {
            transform.position = _camera.ViewportToWorldPoint(new Vector3(0, viewport.y, viewport.z));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enter");
        if (other.CompareTag("Interactable"))
        {
            other.GetComponent<Interactable>().CursorEnter(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("exit");
        if (other.CompareTag("Interactable"))
        {
            other.GetComponent<Interactable>().CursorExit();
        }
    }

    public void UnlockElement(Element element)
    {
        switch (element)
        {
            case Element.AIR:
                airU = true;
                break;
            case Element.FIRE:
                fireU = true;
                break;
            case Element.EARTH:
                earthU = true;
                musicManager.EarthActivated();
                iconChanger.SwitchTo(element);
                break;
            case Element.WATER:
                waterU = true;
                break;
        }
    }
}