using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageButtons : MonoBehaviour
{
    public void NextPageClicked()
    {
        HighScoreTable.Instance.GetNextPage();
    }

    public void PreviousPageClicked()
    {
        HighScoreTable.Instance.GetPreviousPage();
    }
}
