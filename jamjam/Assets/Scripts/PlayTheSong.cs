using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTheSong : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioMan.inst.PlaySong();
    }

}
