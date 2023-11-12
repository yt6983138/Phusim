using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class test : MonoBehaviour
{
    void Start()
    {
        AudioClip clip = StaticUtils.GetAudioClip(new Uri(@"E:\Files\Phusim\test.mp3", UriKind.Absolute));

        AudioSource source = this.gameObject.GetComponent<AudioSource>();

        source.clip = clip;
        source.Play();
    }
}
