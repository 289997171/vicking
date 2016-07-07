
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 挂载到主角摄像机，或者UI Canvas上，实现2D音效
/// </summary>
public class SoundController : MonoBehaviour
{
    private bool bPause;

    private List<AudioSource> audios = new List<AudioSource>();
    
    private int findFreeAudio()
    {
        int pos = -1;
        for (int i = 0; i < audios.Count; ++i)
        {
            if (audios[i] == null)
                continue;
            if (!audios[i].isPlaying)
            {
                pos = i;
                break;
            }
        }
        return pos;
    }

    /// <summary>
    /// 判断是否有重复的声音正在播放,有就重新播放
    /// </summary>
    /// <param name="audioname">name</param>
    /// <returns></returns>
    bool NotHaveSameVoice(string audioname)
    {
        for (int i = 0; i < audios.Count; ++i)
        {
            if (audios[i] == null)
                continue;
            if (audios[i].clip == null)
                continue;
            if (audios[i].clip.name == audioname && audios[i].isPlaying)
            {
                return false;
            }
        }
        return true;
    }

    public bool PlaySound(AudioClip chip, bool bLoop, bool bForce)
    {
        if (chip == null)
            return false;
        if (!NotHaveSameVoice(chip.name) && !bForce)
            return false;
        int Pos = findFreeAudio();
        AudioSource pAudio;
        if (Pos == -1)
        {
            pAudio = gameObject.AddComponent<AudioSource>();
            audios.Add(pAudio);
        }
        else
        {
            pAudio = audios[Pos];
            //DestroyImmediate(pAudio.clip);
            //Destroy(pAudio.clip);
        }
        pAudio.clip = chip;
        pAudio.volume = 1f;
        pAudio.loop = bLoop;
        pAudio.Play();
        return true;
    }

    public void PausePlay()
    {
        for (int i = 0; i < audios.Count; ++i)
        {
            if (audios[i] == null)
                continue;
            if (audios[i].isPlaying)
            {
                bPause = true;
                audios[i].Pause();
            }
            else
            {
                Destroy(audios[i]);
                audios.RemoveAt(i);
            }
        }
    }

    public void StopSound(string audioname)
    {
        for (int i = 0; i < audios.Count; ++i)
        {
            if (audios[i] == null || audios[i].clip == null)
                continue;
            if (audios[i].clip.name == audioname)
            {
                Destroy(audios[i]);
                audios.RemoveAt(i);
                return;
            }
        }
    }

    public void CanclePause()
    {
        if (!bPause)
            return;
        for (int i = 0; i < audios.Count; ++i)
        {
            if (audios[i] != null)
                audios[i].Play();
        }
    }

    public void StopAllSound()
    {
        for (int i = 0; i < audios.Count; ++i)
            audios[i].volume = 0f;
    }

    public void CancelMute()
    {
        for (int i = 0; i < audios.Count; ++i)
        {
            if (audios[i] != null && audios[i].isPlaying)
            {
                audios[i].volume = 1f;
            }
        }
    }

}
