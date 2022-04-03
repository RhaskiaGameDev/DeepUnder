using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmScreen : MonoBehaviour
{
    public enum State { radar, inventory, flare }
    public State currentState;
    public float switchTime;

    public bool open;
    bool switching;

    [Header("Keys")]
    public KeyCode openMenu;
    public KeyCode radar, inventory, flare;

    public GameObject[] menus;


    void Update()
    {
        if (Input.GetKeyDown(openMenu))
        {
            open = !open;
            if (open) SwitchStateInvoke(((int)currentState));
            else OpenMenu(4);
        }

        if (Input.GetKeyDown(radar)) SwitchStateInvoke(0);
        if (Input.GetKeyDown(inventory)) SwitchStateInvoke(1);
        if (Input.GetKeyDown(flare)) SwitchStateInvoke(2);
    }

    void SwitchStateInvoke(int state)
    {
        if (open && !switching)
        {
            switching = true;
            currentState = (State)state;

            Invoke("SwitchState", switchTime);
            ManageScreens();
        }
    }

    void SwitchState() { switching = false; ManageScreens(); }

    void ManageScreens()
    {
        if (switching) OpenMenu(3);
        else OpenMenu(((int)currentState));
    }

    void OpenMenu(int menu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (i == menu && open) menus[i].SetActive(true);
            else menus[i].SetActive(false);
        }
    }
}

