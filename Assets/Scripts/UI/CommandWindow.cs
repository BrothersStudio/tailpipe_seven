﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandWindow : MonoBehaviour
{
    Player selected_player;

    public void SetButtons(Player player)
    {
        selected_player = player;

        bool has_ball = selected_player.HasBall();

        Button attack_button = transform.Find("Attack Button").GetComponent<Button>();
        if (has_ball && !selected_player.took_attack)
        {
            attack_button.interactable = true;
            attack_button.onClick.RemoveAllListeners();
            attack_button.onClick.AddListener(selected_player.CheckPass);

            attack_button.gameObject.GetComponentInChildren<Text>().text = "Pass";
        }
        else if (!selected_player.took_attack && Utils.ReturnAdjacentOpponents(selected_player).Count > 0)
        {
            attack_button.interactable = true;
            attack_button.onClick.RemoveAllListeners();
            attack_button.onClick.AddListener(selected_player.CheckPush);

            attack_button.gameObject.GetComponentInChildren<Text>().text = "Push";
        }
        else
        {
            attack_button.interactable = false;

            if (has_ball)
            {
                attack_button.gameObject.GetComponentInChildren<Text>().text = "Pass";
            }
            else
            {
                attack_button.gameObject.GetComponentInChildren<Text>().text = "Push";
            }
        }

        Button move_button = transform.Find("Move Button").GetComponent<Button>();
        if (!selected_player.took_move)
        {
            move_button.interactable = true;
            move_button.onClick.RemoveAllListeners();
            move_button.onClick.AddListener(selected_player.CheckMove);
        }
        else
        {
            move_button.interactable = false;
        }

        gameObject.SetActive(true);
    }

    public void Cancel()
    {
        selected_player.SetInactive();
        selected_player = null;
        
        gameObject.SetActive(false);
    }
}
