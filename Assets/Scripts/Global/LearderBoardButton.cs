using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LearderBoardButton : MonoBehaviour
{
    public void LoadLeaderBoard()
    {
        SceneManager.LoadScene("LeaderBoard");
    }
}
