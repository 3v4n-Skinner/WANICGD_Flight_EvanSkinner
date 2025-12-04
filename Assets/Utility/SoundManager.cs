using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/* Class: SoundManager
 * Original Author: Zev S.
 * Contributers: [Your Name]
 * Created: 11/26/24
 * Last Modified: 12/4/2024
 * 
 * Purpose: Instantiates and manages audio clips using custom packet type
 */

public static class SoundManager 
{
    /// <summary>
    /// Dictionary of packets and sources currently in play
    /// </summary>
    private static Dictionary<AudioPacket, AudioSource> ActivePackets = new Dictionary<AudioPacket, AudioSource>();

    /// <summary>
    /// Begins playing AudioPacket if not allready playing
    /// </summary>
    /// <param name="packet">Audio packet to play</param>
    public static void PlayClip(AudioPacket packet)
    {
        //Prevents multiple of the same packet from playing at once.
        if(ActivePackets.ContainsKey(packet)) return;

        //Spawn AudioSource
        GameObject soundPlayer = new GameObject();
        AudioSource source = soundPlayer.AddComponent<AudioSource>();
        soundPlayer.AddComponent<SoundPlayer>().Initialize(packet);
        soundPlayer.name = $"Audio Instance - {packet.clip.name}";

        //Configures AudioSource from packet
        source.volume = packet.volume;
        source.clip = packet.clip;
        source.outputAudioMixerGroup = packet.group;
        source.loop = packet.Looping;

        
        if (packet.PresistOnLoad) Object.DontDestroyOnLoad(soundPlayer);
        if (!packet.Looping)
        {
            Object.Destroy(soundPlayer,packet.clip.length);
        }

        source.Play();

        ActivePackets.Add(packet, source);
    }

    /// <summary>
    /// Ends audio packet Early
    /// </summary>
    /// <param name="packet">Packet to End</param>
    public static void StopClip(AudioPacket packet)
    {
        if (ActivePackets.ContainsKey(packet))
        {
            GameObject.Destroy(ActivePackets[packet]);
        }
    }

    /// <summary>
    /// Pauses or Unpauses Audio Packet if playing
    /// </summary>
    /// <param name="packet"></param>
    /// <param name="paused">True if pause, false for unpause</param>
    public static void ToggleClip(AudioPacket packet, bool paused)
    {
        if (paused)
        {
            ActivePackets[packet].Pause();
        }
        else
        {
            ActivePackets[packet].UnPause();
        }
    }

   
    public static void RemoveClip(AudioPacket packet)
    {
        if(ActivePackets.ContainsKey(packet))
        {
            ActivePackets.Remove(packet);
        }
    }
}

[System.Serializable]
public class AudioPacket
{
    public AudioClip clip;

    [Range(0,1)]
    public float volume;
    public AudioMixerGroup group;

    public bool Looping;
    public bool PresistOnLoad;

    public void Play()
    {
        SoundManager.PlayClip(this);
    }
}

 class SoundPlayer : MonoBehaviour
{
    AudioPacket packet;

    public void Initialize(AudioPacket packet)
    {
        this.packet = packet;
    }

    private void OnDestroy()
    {
        SoundManager.RemoveClip(packet);
    }
}