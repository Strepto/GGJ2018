using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Transmission;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioSource BoyMusic;
    public AudioSource GirlMusic;

    [Range(0.0f,1.0f)]
    public float MaxVolume = 0.75f;

    private PlayerController playerController;
    
	void Start () {
        playerController = PlayerController.Instance;
	}

    //public void MuteAll()
    //{
    //    BoyMusic.volume = 0;
    //    GirlMusic.volume = 0;
    //}
	

    public void SwitchGenderSounds(PlayerController.PlayerState gender)
    {
        if(gender == PlayerController.PlayerState.Boy)
        {
            StartCoroutine(FadeVolume(GirlMusic,BoyMusic , 1.4f));
        }
        if (gender == PlayerController.PlayerState.Girl)
        {
            StartCoroutine(FadeVolume(BoyMusic, GirlMusic, 1.4f));
        }
    }


    public IEnumerator FadeVolume(AudioSource toMute, AudioSource toUnmute, float time)
    {
        float elapsedTime = 0;
        float initalMute = toMute.volume;
        while (elapsedTime < time)
        {
            float value = Mathf.Lerp(0, MaxVolume, (elapsedTime / time));
            float value2 = Mathf.Lerp(initalMute, 0, (elapsedTime / time));
            toUnmute.volume = value;
            toMute.volume = value2;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

}
