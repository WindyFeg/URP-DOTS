using System;
using System.Collections;
using System.Collections.Generic;
using CortexDeveloper.ECSMessages.Service;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private Button _button;

    // get panel
    public GameObject panel;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(StartGame);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(StartGame);
    }

    private void StartGame()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        MessageBroadcaster
            .PrepareMessage()
            .AliveForUnlimitedTime()
            .PostImmediate(entityManager,
                new StartCommand
                {
                    IsStart = true
                });

        // turn UI off
        TurnUIOff();

    }

    private void TurnUIOff()
    {
        this.gameObject.SetActive(false);
        panel.SetActive(false);
    }
}
