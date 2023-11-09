using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BackgroundImage : MonoBehaviour
{
    public GameObject AssignedObject;
    private SkinItems skin;
    void OnEnable()
    {
        skin = Skins.ReadSkin();

        System.Diagnostics.Stopwatch watch = new();
        watch.Start();
        Texture2D blurred = Effects.ApplyEffects(
            Skins.LoadTexture(AssignedObject.name),
            (List<Dictionary<string, object>>)skin.Properties[AssignedObject.name][Resource.keyRefrence["Effects"]]
        );
        watch.Stop();
        LogHandler.Log(
            LogHandler.Verbose, 
            new Exception(
                string.Format(
                    "Blurring background at {0} took {1}ms!", 
                    AssignedObject.name, 
                    watch.ElapsedMilliseconds.ToString()
                )
            )
        );
        AssignedObject.transform.localScale = UIUtils.CalcTextureFitScreen(blurred);
        AssignedObject.GetComponent<RectTransform>().sizeDelta = new Vector2(blurred.width, blurred.height);
        AssignedObject.GetComponent<RawImage>().texture = blurred;
    }
    void OnGUI()
    {
    }
}
