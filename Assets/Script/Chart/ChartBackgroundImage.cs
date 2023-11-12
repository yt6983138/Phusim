using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChartBackgroundImage : MonoBehaviour
{
    //public GameObject AssignedObject;
    private SkinItems skin;
    void Start()
    {
        skin = Skins.GameSkin;

        System.Diagnostics.Stopwatch watch = new();
        watch.Start();
        Texture2D blurred = Effects.ApplyEffects(
            Skins.LoadTexture(this.gameObject.name),
            (List<Dictionary<string, object>>)skin.Properties[this.gameObject.name]["Effects"]
        );
        watch.Stop();
        LogHandler.Log(
            LogHandler.Verbose, 
            string.Format("Blurring background at {0} took {1}ms!", this.gameObject.name, watch.ElapsedMilliseconds.ToString())
        );
        this.gameObject.transform.localScale = UIUtils.CalcTextureFitScreen(blurred);
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(blurred.width, blurred.height);
        this.gameObject.GetComponent<RawImage>().texture = blurred;
    }
}
