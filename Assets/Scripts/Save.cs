using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Save
{

    public static void OnSaveInt(string name, int value)
    {
        PlayerPrefs.SetInt(name, value);
    }
    public static void OnSaveFloat(string name, float value)
    {
        PlayerPrefs.SetFloat(name, value);
    }
    public static void OnSaveString(string name, string value)
    {
        PlayerPrefs.SetString(name, value);
    }
    public static int LoadLevelProgress()
    {
        if (PlayerPrefs.HasKey("levelProgress"))
        {
            return (PlayerPrefs.GetInt("levelProgress"));
        }
        else return 1;
    }
}
