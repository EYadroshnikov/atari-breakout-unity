using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Deserialization;
using Newtonsoft.Json;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class HighScoreTable : MonoBehaviour
{
    public static HighScoreTable Instance;
    private Transform entryContainer;
    private Transform entryTemplate;
    private LeaderboardResponse response;
    public Button nextPage;
    public Button previousPage;
    public List<Transform> entrytransforms;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        StartCoroutine(GetLeaderBoard());
    }

    public void GetNextPage()
    {
        StartCoroutine(GetLeaderBoard(response.meta.page + 1));
    }

    public void GetPreviousPage()
    {
        StartCoroutine(GetLeaderBoard(response.meta.page - 1));
    }


    IEnumerator GetLeaderBoard(int page = 1)
    {
        // Отправка GET-запроса
        using (UnityWebRequest webRequest = UnityWebRequest.Get($"localhost:3000/scores?page={page}&take=8"))
        {
            // Ожидание ответа от сервера
            yield return webRequest.SendWebRequest();

            // Обработка результата запроса
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                foreach (var item in entrytransforms)
                {
                    item.gameObject.SetActive(false);
                }

                string jsonResponse = webRequest.downloadHandler.text;
                this.response = JsonConvert.DeserializeObject<LeaderboardResponse>(jsonResponse);
                entryContainer = transform.Find("HighScoreEntryContainer");
                entryTemplate = entryContainer.Find("HighScoreEntryTemplate");
                entryTemplate.gameObject.SetActive(false);

                float templateHeight = 72f;
                for (int i = 0; i < this.response.data.Count; i++)
                {
                    Transform entryTransform = Instantiate(entryTemplate, entryContainer);
                    RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
                    entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
                    entryTransform.gameObject.SetActive(true);
                    entrytransforms.Add(entryTransform);

                    entryTransform.Find("PositionText").GetComponent<TextMeshProUGUI>().text = ((response.meta.page - 1) * 8 + i + 1).ToString();
                    entryTransform.Find("NicknameText").GetComponent<TextMeshProUGUI>().text = response.data[i].nickname;
                    entryTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = response.data[i].score.ToString();
                }

                nextPage.interactable = response.meta.hasNextPage;
                previousPage.interactable = response.meta.hasPreviousPage;
            }
            else
            {
                Debug.Log("http error " + webRequest.error);
            }
        }
    }
}
