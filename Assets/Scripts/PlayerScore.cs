using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore
{
    public static readonly int MAX_STORED_SCORES = 10;
    public static readonly string SCORE_TOUPLE_DELIMITER = "|";
    public static readonly string SCORE_INITIAL_DELIMITER = ",";
    public static readonly string PLAYERPREFS_LIST_NAME = "top10";

    public string initials;
    public int score;
    //public long time = DateTimeOffset.Now.ToUnixTimeMilliseconds();

    public PlayerScore(string initials, int score)
    {
        this.initials = initials;
        this.score = score;
    }

    public override string ToString()
    {
        return initials + SCORE_INITIAL_DELIMITER + score.ToString();
    }

    // Serialize list of PlayerScore objects into string
    public static void saveList(PlayerScore[] scores)
    {
        string serializedList = "";
        for (int i = 0; i < scores.Length && i < MAX_STORED_SCORES; i++)
        {
            if (i == 0)
            {
                serializedList = scores[0].ToString();
            }
            else
            {
                serializedList = serializedList + SCORE_INITIAL_DELIMITER + scores[i].ToString();
            }
        }
        Debug.Log("Saving scores list " + serializedList);
        PlayerPrefs.SetString(PLAYERPREFS_LIST_NAME,serializedList);
    }
    

    //NMT,13-JBT,15-HJT,19 
    public static PlayerScore[] deserializeList(string raw)
    {
        Debug.Log("deserializing list: " + raw);
        if (raw == null || raw == "")
        {
            return new PlayerScore[0];
        }
        string[] parsed = raw.Split(SCORE_TOUPLE_DELIMITER);
        List<PlayerScore> scoresList = new List<PlayerScore>();
        for (int i = 0; i < parsed.Length; i++)
        {
            PlayerScore ps = deserialize(parsed[i]);
            scoresList.Add(ps);
        }
        return scoresList.ToArray();
    }

    //NMT,16
    public static PlayerScore deserialize(string raw)
    {
        Debug.Log("deserializing single: " + raw);
        string[] parsed = raw.Split(",");
        PlayerScore ps = new PlayerScore(parsed[0], Int32.Parse(parsed[1]));
        return ps;
    }

    //public static List<PlayerScore> sort(List<PlayerScore> scoreList)
    //{
    //    return scoreList.Sort((x, y) => int.Compare(x.score,y.score));
    //}
}
