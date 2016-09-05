using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource playBgm = null;
    [SerializeField]
    private AudioSource feverBgm = null;
    [SerializeField]
    private AudioSource badBgm = null;
    [SerializeField]
    private AudioSource resultBgm = null;

    public AudioSource currentBgm = null;

    // Use this for initialization
    void Start()
    {
        StartPlayTime();
    }

    public void StartPlayTime()
    {
        if (currentBgm != null)
            currentBgm.Stop();

        playBgm.Play();
        currentBgm = playBgm;
    }

    public void StartFeverTime()
    {
        if (currentBgm != null)
            currentBgm.Stop();

        feverBgm.Play();
        currentBgm = feverBgm;
    }

    public void FinishFeverTime()
    {
        if (currentBgm != null)
            currentBgm.Stop();

        currentBgm = null;
        StartPlayTime();
    }

    public void StartBadTime()
    {
        if (currentBgm != null)
            currentBgm.Stop();

        badBgm.Play();
        currentBgm = badBgm;
    }

    public void FinishBadTime()
    {
        if (currentBgm != null)
            currentBgm.Stop();

        currentBgm = null;
        StartPlayTime();
    }

    public void StartResult()
    {
        if (currentBgm != null)
            currentBgm.Stop();

        resultBgm.Play();
        currentBgm = resultBgm;
    }
}
