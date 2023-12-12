using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    void Start()
    {
        /*AudioClip clip = Utils.GetAudioClip(new Uri(@"E:\Files\Phusim\test.mp3", UriKind.Absolute));

        AudioSource source = this.gameObject.GetComponent<AudioSource>();

        source.clip = clip;
        source.Play();*/
        //List<ChartMeta> test = ChartManager.LoadMetaFromDifferentSimulator(@"E:\Files\Phusim\info.yml");
        //Debug.Log(test.ToString<ChartMeta>());
        GameObject canvas = GameObject.Find("PlayChart.MainChart");
        ChartManager.InitializeEverything(
            ChartManager.LoadChart(@"E:\Files\Phusim\testchart\Chart_IN.json"),
            Utils.GetAudioClip(new Uri(@"E:\Files\Phusim\testchart\test.mp3", UriKind.Absolute)),
            canvas.GetComponent<AudioSource>(),
            UIUtils.LoadTexture(@"E:\Files\Phusim\testchart\Illustration.png"),
            new ChartMeta() { },
            canvas,
            GameObject.Find("Main Camera").GetComponent<Camera>(),
            null
        );
        NoteTextureManager.Initialize(
            tap: UIUtils.Create9SlicedQuick(UIUtils.LoadTexture(@"E:\Files\Phusim\testchart\click.png"), new Vector4(219, 49, 219, 49)), //
            tapMl: UIUtils.Create9SlicedQuick(UIUtils.LoadTexture(@"E:\Files\Phusim\testchart\click_mh.png"), new Vector4(88, 124, 88, 124)), //
            drag: UIUtils.Create9SlicedQuick(UIUtils.LoadTexture(@"E:\Files\Phusim\testchart\drag.png"), new Vector4(134, 24, 134, 24)), //
            dragMl: UIUtils.Create9SlicedQuick(UIUtils.LoadTexture(@"E:\Files\Phusim\testchart\drag_mh.png"), new Vector4(210, 99, 210, 99)), //
            flick: UIUtils.Create9SlicedQuick(UIUtils.LoadTexture(@"E:\Files\Phusim\testchart\flick.png"), new Vector4(144, 85, 144, 85)), //
            flickMl: UIUtils.Create9SlicedQuick(UIUtils.LoadTexture(@"E:\Files\Phusim\testchart\flick_mh.png"), new Vector4(219, 160, 219, 160)), //
            TF: UIUtils.Create9SlicedQuick(UIUtils.LoadTexture(@"E:\Files\Phusim\testchart\Illustration.png"), new Vector4(144, 85, 144, 85)), //
            TFMl: UIUtils.Create9SlicedQuick(UIUtils.LoadTexture(@"E:\Files\Phusim\testchart\Illustration.png"), new Vector4(219, 160, 219, 160)), //
            hold: UIUtils.Create9SlicedQuick(UIUtils.LoadTexture(@"E:\Files\Phusim\testchart\Illustration.png"), new Vector4(88, 55, 88, 55)), //
            holdMl: UIUtils.Create9SlicedQuick(UIUtils.LoadTexture(@"E:\Files\Phusim\testchart\Illustration.png"), new Vector4(163, 130, 163, 130)) //
        );
        ChartManager.CurrentChart.Process(ChartManager.Canvas);
        ChartManager.Start();
    }
    private void FixedUpdate()
    {
        ChartManager.CurrentChart.Update((int)ChartManager.Timer.ElapsedMilliseconds);
    }
}
