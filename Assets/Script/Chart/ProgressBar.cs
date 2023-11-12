using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class ProgressBar : MonoBehaviour
{
    public Camera cam;
    public GameObject Handle;
    public GameObject Fill;

    private SkinItems skin;
    private LanguageItems lang;
    private ConfigItems config;
    private void Start()
    {
        skin = Skins.GameSkin;
        lang = Language.ReadLang();
        config = Config.Configuration;

        Vector3 pos = Vector3.zero;

        CustomSize physicalSize = (CustomSize)(skin.Properties[this.gameObject.name])["Height"];
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, physicalSize.PosY);
        this.gameObject.GetComponent<Image>().color = (Color)skin.Properties[this.gameObject.name]["UnprogressedFillColor"];

        Handle.GetComponent<RectTransform>().sizeDelta = new Vector2(physicalSize.PosY, 0);
        Handle.GetComponent<Image>().color = (Color)skin.Properties[this.gameObject.name]["HandleColor"];

        Fill.GetComponent<Image>().color = (Color)skin.Properties[this.gameObject.name]["ProgressedFillColor"];

        /*RectTransformUtility.ScreenPointToWorldPointInRectangle(
            this.gameObject.GetComponent<RectTransform>(),
            UIUtils.ToGlobalPos(
            (Vector3)(skin.Properties[this.gameObject.name])[Resource.keyRefrence["Pos"]],
            (RectOffset)(skin.Properties[this.gameObject.name])[Resource.keyRefrence["Padding"]]),
            cam,
            out pos
        );*/
    }
}
