using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UniversalButtonBehaviour : MonoBehaviour
{
    public GameObject AssignedObject;
    void Start()
    {
        AssignedObject.GetComponent<Button>().onClick.AddListener(delegate {
            Debug.Log("On click"); // FUCK C# TUPLES BRUHHHHHHHHHHHH
            (string space, string method, object[] args) execute = StaticUtils.ExecuteToNamedTuple(
                (List<object>)(Skins.ReadSkin().ButtonProperties[AssignedObject.name])["Execute"]
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
        } );
    }
    void OnGUI()
    {
        AssignedObject.transform.position = UIUtils.ToGlobalPos((Vector3)(Skins.ReadSkin().ButtonProperties[AssignedObject.name])["Pos"]);
        AssignedObject.transform.GetChild(0).gameObject.transform.GetComponent<TextMeshProUGUI>().text = Language.ReadLang().Language[AssignedObject.name];
        AssignedObject.transform.GetChild(0).gameObject.transform.GetComponent<TextMeshProUGUI>().color = (Color)(Skins.ReadSkin().ButtonProperties[AssignedObject.name])["Color"];
    }
}
