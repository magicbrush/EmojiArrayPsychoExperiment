using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Events;

[RequireComponent(typeof(RawImage))]
[RequireComponent(typeof(VideoPlayer))]
public class VideoDisp : MonoBehaviour {

    public int width = 256, height = 256;

    [System.Serializable]
    public class EventWithVideoClip : UnityEvent<VideoClip> { };
    public EventWithVideoClip _SendVideoClip;

    [System.Serializable]
    public class EventWithRT : UnityEvent<RenderTexture> { };
    public EventWithRT _SendVideoRT;

	public UnityEvent _Inited;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [ContextMenu("SendVideoTargetRT")]
    public void SendVideoTargetRT()
    {
        VideoPlayer player = GetVideoPlayer();
        _SendVideoRT.Invoke(player.targetTexture);
    }

    [ContextMenu("SendVideoClip")]
    public void SendVideoClip()
    {
        VideoPlayer player = GetVideoPlayer();
        _SendVideoClip.Invoke(player.clip);
    }

	[ContextMenu("Play")]
	public void Play()
	{
        VideoPlayer player = GetVideoPlayer();
        player.Play();
	}

	[ContextMenu("RePlay")]
	public void RePlay()
	{
		VideoPlayer player = GetVideoPlayer();
		if (player.isPlaying) {
			player.Stop ();
		}
		player.Play();
	}

    [ContextMenu("Stop")]
    public void Stop()
    {
        VideoPlayer player = GetVideoPlayer();
        player.Stop();
    }

	[ContextMenu("Init")]
	public void Init()
	{
        VideoPlayer player = GetVideoPlayer();
        RawImage rawImage = GetRawImage();

        RenderTexture rt = 
            new RenderTexture(
                width, height, 1, RenderTextureFormat.ARGB32);
        rt.name = player.clip.name;

        player.targetTexture = rt;
        rawImage.texture = rt;

		_Inited.Invoke ();
	}

	VideoPlayer GetVideoPlayer()
    {
        return GetComponent<VideoPlayer>();
    }

    RawImage GetRawImage()
    {
        return GetComponent<RawImage>();
    }

}
