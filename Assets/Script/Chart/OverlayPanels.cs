using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class OverlayPanels : MonoBehaviour
{
    public GameObject LeftPanel;
    public GameObject RightPanel;

    void Start()
    {
        float chartAspect = ChartManager.ChartAspect;
        float panelWidth = (Screen.width - (Screen.height * chartAspect)) / 2;

        LeftPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(panelWidth, LeftPanel.GetComponent<RectTransform>().sizeDelta.y);
        RightPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(panelWidth, RightPanel.GetComponent<RectTransform>().sizeDelta.y);

        LeftPanel.GetComponent<Image>().color = (Color)Skins.GameSkin.Properties["PlayChart.Overlay.LeftPanel"]["Color"];
        RightPanel.GetComponent<Image>().color = (Color)Skins.GameSkin.Properties["PlayChart.Overlay.RightPanel"]["Color"];
    }
}
