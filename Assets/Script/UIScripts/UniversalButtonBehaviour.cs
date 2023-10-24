using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UniversalButtonBehaviour : MonoBehaviour
{
    public GameObject AssignedObject;
    private GameObject childObject;
    private TextMeshProUGUI childComponent;
    private SkinItems skin;
    private LanguageItems lang;
    private ConfigItems config;
    void Awake()
    {
        AssignedObject.GetComponent<Button>().onClick.AddListener(delegate
        {
            Debug.Log("On click"); // FUCK C# TUPLES BRUHHHHHHHHHHHH
            (string space, string method, object[] args) execute = StaticUtils.ExecuteToNamedTuple(
                (List<object>)(Skins.ReadSkin().Properties[AssignedObject.name])["Execute"]
            );
            Debug.Log(execute);
            Type type = Type.GetType(execute.space);
            Debug.Log(type);
            MethodInfo method = type.GetMethod(execute.method, StaticUtils.ObjectToTypeArray(execute.args));
            Debug.Log(method);
            if (method != null)
            {
                method.Invoke(this, execute.args);
            }
        });
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

        AssignedObject.transform.position = UIUtils.ToGlobalPos(
            (Vector3)(skin.Properties[AssignedObject.name])["Pos"],
            (RectOffset)(skin.Properties[AssignedObject.name])["Padding"]
        );
        AssignedObject.transform.localScale = (Vector3)(skin.Properties[AssignedObject.name])["Scale"];
        AssignedObject.transform.GetComponent<RectTransform>().sizeDelta = ((PhysicalSize)skin.Properties[AssignedObject.name]["Size"]).to2dPixel();

        childComponent.text = lang.Language[AssignedObject.name];
        childComponent.color = (Color)(skin.Properties[AssignedObject.name])["Color"];
        childComponent.font = Skins.LoadFont((string)(skin.Properties[AssignedObject.name])["Font"]);
    }
}
