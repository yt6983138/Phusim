using Polenter.Serialization;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Init : MonoBehaviour
{
    public GameObject AssignedInitObject = null;
    void Start()
    {
        AssignedInitObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        Serializer.binSettings = new SharpSerializerBinarySettings(BinarySerializationMode.SizeOptimized);
        AssignedInitObject.GetComponent<TextMeshProUGUI>().SetText(string.Format(Resource.LoadingScreenTexts["Loading"], Resource.LoadingScreenTexts["Config"], ""));
        try
        {
            Config.Init();
            LogHandler.Init();
        }
        catch (Exception e)
        {
            LogHandler.Init();
            LogHandler.Log(LogHandler.Error, e);
        }
        AssignedInitObject.GetComponent<TextMeshProUGUI>().SetText(string.Format(Resource.LoadingScreenTexts["Loading"], Resource.LoadingScreenTexts["Lang"], ""));
        try
        {
            Language.Init();
        }
        catch (Exception e)
        {
            LogHandler.Log(LogHandler.Error, e);
        }
        AssignedInitObject.GetComponent<TextMeshProUGUI>().SetText(string.Format(Resource.LoadingScreenTexts["Loading"], Resource.LoadingScreenTexts["Skin"], ""));
        try
        {
            Skins.Init();
        }
        catch (Exception e)
        {
            LogHandler.Log(LogHandler.Error, e);
        }
        AssignedInitObject.GetComponent<TextMeshProUGUI>().SetText(string.Format(Resource.LoadingScreenTexts["Loading"], Resource.LoadingScreenTexts["Scene.MainUI"], ""));
        SceneManager.LoadScene("MainUI");
    }
}