using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    public TextMeshProUGUI onSaveText;
    public TextMeshProUGUI player;
    public Button save;
    public Button mainMenu;
    public void OnSaveButtonClicked()
    {
        StartCoroutine(SendGetRequest());
        this.mainMenu.gameObject.SetActive(true);
    }

    IEnumerator SendGetRequest()
    {

        WWWForm form = new WWWForm();
        form.AddField("nickname", this.player.text);
        form.AddField("score", GameManager.Instance.score);

        // Отправка GET-запроса
        using (UnityWebRequest webRequest = UnityWebRequest.Post(
            "http://localhost:3000/scores",
            form))
        {
            // Ожидание ответа от сервера
            yield return webRequest.SendWebRequest();

            // Обработка результата запроса
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                this.save.interactable = false;
                this.save.GetComponentInChildren<TextMeshProUGUI>().text = "Saved!";
            }
            else
            {
                onSaveText.text = "Error...";
                Debug.Log("http: " + webRequest.error);
            }
        }
    }
}
