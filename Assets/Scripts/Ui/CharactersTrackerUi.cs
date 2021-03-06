﻿using System.Collections.Generic;
using Model;
using UnityEngine;

namespace Ui
{
    public class CharactersTrackerUi : MonoBehaviour
    {
        [SerializeField] private CharacterStatusUi prefab;

        private List<CharacterStatusUi> items = new List<CharacterStatusUi>();

        private void Awake()
        {
            GameManager.StartEvent += OnGameStart;
            if (GameManager.GameStarted) OnGameStart();
        }

        private void OnDestroy()
        {
            GameManager.StartEvent -= OnGameStart;
        }


        private void OnGameStart()
        {
            GameManager.GameCharacter.Foreach(CreateTracker);
        }

        private void CreateTracker(GameCharacter character)
        {
            var tracker = Instantiate(prefab, transform);
            tracker.character = character;
            tracker.color = character == GameManager.PlayerController.Target ? Color.green : Color.red;
            items.Add(tracker);
        }
    }
}