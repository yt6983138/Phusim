using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundImage : MonoBehaviour
{
    //public Sprite Background;
    //public Image Background;
    public GameObject AssignedObject;
    void OnEnable()
    {
        AssignedObject.GetComponent<Image>().sprite = Skins.LoadTextureAsSprite(AssignedObject.name);
    }
    void OnGUI()
    {
    }
}
