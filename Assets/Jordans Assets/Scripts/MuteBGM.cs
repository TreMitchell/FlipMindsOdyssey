using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MuteBGM : MonoBehaviour
{
    public void MuteHandler(bool mute)
    {
        if (mute)
        {
            AudioListener.volume = 0;

        }
        else
        {
            AudioListener.volume = 1;
        }
    }
}