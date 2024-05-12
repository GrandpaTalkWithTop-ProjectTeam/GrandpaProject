using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip bgmClip;
    
    public float bgmVolume;
    AudioSource bgmPlayer;


    [Header("#Voice")]
    public AudioClip vocClip;
    public GameObject isTalkyOn;

    public float vocVolume;
    AudioSource vocPlayer;
    private bool vUp = false;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;



    public enum Sfx { Button, Hook, Jump, Slide, Footstep, Switch }


    void Awake()
    {
        instance = this;
        BGMInit();
        VOCInit();
        Init();
        AudioManager.instance.PlayBgm(true);
        
    }

    void Update()
    {
        // vocPlayer�� ��� ���� ��
        if (vocPlayer.isPlaying)
        {
            // bgmPlayer�� ������ ���������� ����
            if (bgmPlayer.volume > bgmVolume * 0.2)
                bgmPlayer.volume -= bgmVolume * 0.5f * Time.deltaTime;
        
            vUp= true;
        }
        else
        {
            if (vUp)
            {
                TalkyOnScript tos = isTalkyOn.GetComponent<TalkyOnScript>();
                tos.DisableReady();

                vUp = false;
            }

            // vocPlayer�� ��� ���� �ƴϸ� bgmPlayer�� ������ �ٽ� �ִ� �������� ����
            if (bgmPlayer.volume < bgmVolume)
                bgmPlayer.volume += bgmVolume * 0.5f * Time.deltaTime;
            else
                bgmPlayer.volume = bgmVolume;
        }
    }

    public void BGMAwake()
    {
        AudioManager.instance.PlayBgm(false);
        BGMInit();

        AudioManager.instance.PlayBgm(true);

    }

    public void VOCAwake()
    {
        AudioManager.instance.PlayVoc(false);
        VOCInit();

        AudioManager.instance.PlayVoc(true);

    }

    public void BGMSlideEvent(float value)
    {
        bgmVolume = value;
        // ����� �Ҹ� ����
        bgmPlayer.volume = bgmVolume;
    }

    public void VOCSlideEvent(float value)
    {
        vocVolume = value;
        // ����� �Ҹ� ����
        vocPlayer.volume = vocVolume;
    }

    public void SFXSlideEvent(float value)
    {
        sfxVolume = value;
        // ȿ���� �Ҹ� ����
        foreach (var player in sfxPlayers)
        {
            player.volume = sfxVolume;
        }
    }



    void BGMInit()
    {
        //����� �÷��̾� �ʱ�ȭ
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;
    }

    void VOCInit()
    {
        //����� �÷��̾� �ʱ�ȭ
        GameObject vocObject = new GameObject("VocPlayer");
        vocObject.transform.parent = transform;
        vocPlayer = vocObject.AddComponent<AudioSource>();
        vocPlayer.playOnAwake = false;
        vocPlayer.loop = false;
        vocPlayer.volume = vocVolume;
        vocPlayer.clip = vocClip;
    }

    void Init()
    {
        

        //ȿ���� �÷��̾� �ʱ�ȭ
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for(int index=0; index< sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake=false;
            

        }
    }

    public void PlayBgm(bool isPlay)
    {
        if (isPlay)
        {
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }

    public void PlayVoc(bool isPlay)
    {
        if (isPlay)
        {
            vocPlayer.Play();
        }
        else
        {
            vocPlayer.Stop();
        }
    }

    public void PlaySfx(Sfx sfx)
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;

            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].spatialBlend = 0;
            sfxPlayers[loopIndex].volume = sfxVolume;
            sfxPlayers[loopIndex].pitch = Random.Range(0.8f, 1.2f);
            sfxPlayers[loopIndex].Play();

            break;
        }
    }

    public void PlaySfx3D(Sfx sfx, GameObject sfxObject)
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].volume = sfxVolume;
            sfxPlayers[loopIndex].spatialBlend = 1;
            sfxPlayers[loopIndex].minDistance = 1;
            sfxPlayers[loopIndex].maxDistance = 20;


            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].transform.position = sfxObject.transform.position;
            sfxPlayers[loopIndex].Play();
            break;
        }
    }

    public void StopAllSfx()
    {
        foreach (var player in sfxPlayers)
        {
            player.Stop();
        }
    }

    

}
