using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// using CSScriptLib;
// using CSScripting;
using UnityEngine.SceneManagement;

public class UniversalButtonBehaviour : MonoBehaviour
{
    //public GameObject AssignedObject;
    public Camera cam;
    private GameObject childObject;
    private TextMeshProUGUI childComponent;
    private SkinItems skin;
    private LanguageItems lang;
    private ConfigItems config;
    void Awake()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(delegate
        { // FUCK C# TUPLES BRUHHHHHHHHHHHH
            var execute = Resource.InvokeInfo[this.gameObject.name];
            Type type = Type.GetType(execute.info);
            MethodInfo method = type.GetMethod(execute.method, StaticUtils.ObjectToTypeArray(execute.args));
            if (method != null)
            {
                method.Invoke(this, execute.args);
            } else
            {
                LogHandler.Log(LogHandler.Warning, string.Format("Failure to invoke method {0}, ass info {1}", execute.method, execute.info));
            }
        });
        
        /*IScript script = (IScript)CSScript.CodeDomEvaluator.LoadCode(
            (string)Skins.ReadSkin().Properties[AssignedObject.name][Resource.keyRefrence["Execute"]]
        );
        AssignedObject.GetComponent<Button>().onClick.AddListener(delegate
        {
            script.Execute(AssignedObject);
        });*/
    }
    void OnEnable()
    {
        childObject = this.gameObject.transform.GetChild(0).gameObject;
        skin = Skins.GameSkin;
        lang = Language.ReadLang();
        config = Config.Configuration;
        childComponent = childObject.transform.GetComponent<TextMeshProUGUI>();

        //AssignedObject.transform.position = UIUtils.ToGlobalPos(
        //    (Vector3)(skin.Properties[AssignedObject.name])[Resource.keyRefrence["Pos"]],
        //    (RectOffset)(skin.Properties[AssignedObject.name])[Resource.keyRefrence["Padding"]]
        //);
        Vector3 pos = Vector3.zero;

        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            this.gameObject.GetComponent<RectTransform>(),
            UIUtils.ToGlobalPos(
            ((CustomSize)(skin.Properties[this.gameObject.name])["Pos"]).ToVector3(),
            (RectOffset)(skin.Properties[this.gameObject.name])["Padding"]),
            cam,
            out pos
        );
        this.gameObject.GetComponent<RectTransform>().position = pos;
        this.gameObject.transform.localScale = ((CustomSize)(skin.Properties[this.gameObject.name])["Scale"]).ToVector3();
        this.gameObject.transform.eulerAngles = ((CustomSize)(skin.Properties[this.gameObject.name])["RotationEuler"]).ToVector3();
        this.gameObject.transform.GetComponent<RectTransform>().sizeDelta = ((CustomSize)skin.Properties[this.gameObject.name]["Size"]).ToVector2();

        childComponent.text = lang.Language[this.gameObject.name];
        childComponent.color = (Color)(skin.Properties[this.gameObject.name])["Color"];
        childComponent.font = Skins.LoadFont((string)(skin.Properties[this.gameObject.name])["Font"]);
        childComponent.fontSize = (float)(skin.Properties[this.gameObject.name])["FontSize"];
    }
}
