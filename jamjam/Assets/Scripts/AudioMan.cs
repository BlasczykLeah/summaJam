using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMan : MonoBehaviour
{

    public AudioSource k1, k2, t1, t2, c1, c2, a1, a2, sword, evolve;
    int randNum;

    public static AudioMan inst;

    private void Awake()
    {
        if (inst != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            inst = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayChungus()
    {
        randNum = Random.Range(1, 3);

        if(randNum == 1)
        {
            c1.Play();
        }
        else
        {
            c2.Play();
        }
    }

    public void PlayArcane()
    {
        randNum = Random.Range(1, 3);

        if (randNum == 1)
        {
            a1.Play();
        }
        else
        {
            a2.Play();
        }
    }

    public void PlayThief()
    {
        randNum = Random.Range(1, 3);

        if (randNum == 1)
        {
            t1.Play();
        }
        else
        {
            t2.Play();
        }
    }

    public void PlayKnight()
    {
        randNum = Random.Range(1, 3);

        if (randNum == 1)
        {
            k1.Play();
        }
        else
        {
            k2.Play();
        }
    }

    public void PlaySword()
    {
        sword.Play();
    }

    public void PlayEvolve()
    {
        evolve.Play();
    }
}
