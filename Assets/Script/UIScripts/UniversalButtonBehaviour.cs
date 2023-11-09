using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CSScriptLib;
using CSScripting;
using UnityEngine.SceneManagement;

public class UniversalButtonBehaviour : MonoBehaviour
{
    public GameObject AssignedObject;
    public Camera cam;
    private GameObject childObject;
    private TextMeshProUGUI childComponent;
    private SkinItems skin;
    private LanguageItems lang;
    private ConfigItems config;
    void Awake()
    {
        AssignedObject.GetComponent<Button>().onClick.AddListener(delegate
        { // FUCK C# TUPLES BRUHHHHHHHHHHHH
            var execute = Resource.InvokeInfo[AssignedObject.name];
            Type type = Type.GetType(execute.info);
            MethodInfo method = type.GetMethod(execute.method, StaticUtils.ObjectToTypeArray(execute.args));
            if (method != null)
            {
                method.Invoke(this, execute.args);
            } else
            {
                LogHandler.Log(LogHandler.Warning, new Exception(string.Format("Failure to invoke method {0}, ass info {1}", execute.method, execute.info)));
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
    void OnGUI()
    {
    }
    void OnEnable()
    {
        childObject = AssignedObject.transform.GetChild(0).gameObject;
        skin = Skins.ReadSkin();
        lang = Language.ReadLang();
        config = Config.ReadConfig();
        childComponent = childObject.transform.GetComponent<TextMeshProUGUI>();

        //AssignedObject.transform.position = UIUtils.ToGlobalPos(
        //    (Vector3)(skin.Properties[AssignedObject.name])[Resource.keyRefrence["Pos"]],
        //    (RectOffset)(skin.Properties[AssignedObject.name])[Resource.keyRefrence["Padding"]]
        //);
        Vector3 pos = Vector3.zero;

        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            AssignedObject.GetComponent<RectTransform>(),
            UIUtils.ToGlobalPos(
            (Vector3)(skin.Properties[AssignedObject.name])[Resource.keyRefrence["Pos"]],
            (RectOffset)(skin.Properties[AssignedObject.name])[Resource.keyRefrence["Padding"]]),
            cam,
            out pos
        );
        AssignedObject.GetComponent<RectTransform>().position = pos;
        AssignedObject.transform.localScale = (Vector3)(skin.Properties[AssignedObject.name])[Resource.keyRefrence["Scale"]];
        AssignedObject.transform.eulerAngles = (Vector3)(skin.Properties[AssignedObject.name])[Resource.keyRefrence["RotationEuler"]];
        AssignedObject.transform.GetComponent<RectTransform>().sizeDelta = ((PhysicalSize)skin.Properties[AssignedObject.name][Resource.keyRefrence["Size"]]).ToVector2();

        childComponent.text = lang.Language[AssignedObject.name];
        childComponent.color = (Color)(skin.Properties[AssignedObject.name])[Resource.keyRefrence["Color"]];
        childComponent.font = Skins.LoadFont((string)(skin.Properties[AssignedObject.name])[Resource.keyRefrence["Font"]]);
        childComponent.fontSize = (float)(skin.Properties[AssignedObject.name])[Resource.keyRefrence["FontSize"]];
    }
}
