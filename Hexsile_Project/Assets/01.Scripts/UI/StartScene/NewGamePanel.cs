using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGamePanel : MonoBehaviour
{
    [SerializeField] Button btnNewGame;
    [SerializeField] InputField playerNameInput;

    private void Start()
    {
        btnNewGame.onClick.AddListener(() =>
        {
            foreach (var item in GameManager.Instance.GetAllSaveFiles())
            {
                if(item.Name.Contains(playerNameInput.text))
                {
                    playerNameInput.text = "";
                    playerNameInput.placeholder.GetComponent<Text>().text = "! 이미 존재하는 이름입니다 !";
                    playerNameInput.placeholder.GetComponent<Text>().color = Color.red;
                    return;
                }
            }

            GameManager.Instance.StartNewGame(playerNameInput.text != "" ? playerNameInput.text : "Player", GetComponentInChildren<ColorPicker>().GetColor());
        });
    }
}
