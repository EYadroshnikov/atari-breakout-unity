using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deserialization : MonoBehaviour
{
    public class Score
{
    public int id { get; set; }
    public string createdAt { get; set; }
    public string nickname { get; set; }
    public int score { get; set; }
}

// Класс для десериализации объекта Meta
public class Meta
{
    public int page { get; set; }
    public int take { get; set; }
    public int itemCount { get; set; }
    public int pageCount { get; set; }
    public bool hasPreviousPage { get; set; }
    public bool hasNextPage { get; set; }
}

// Класс для десериализации всего JSON-объекта
public class LeaderboardResponse
{
    public List<Score> data { get; set; }
    public Meta meta { get; set; }
}
}
