using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGamePanel : MonoBehaviour
{
    [SerializeField] Button btnNewGame;
    [SerializeField] InputField playerNameInput;

    private void Start()
    {
        btnNewGame.onClick.AddListener(() => GameManager.Instance.StartNewGame(playerNameInput.text != null ? playerNameInput.text : "Player", GetComponentInChildren<ColorPicker>().GetColor()));
    }
}
