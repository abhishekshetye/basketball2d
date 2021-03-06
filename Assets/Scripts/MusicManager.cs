﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
	private AudioSource _audioSource;

	private void Awake()
	{
		DontDestroyOnLoad(this.transform.gameObject);
		_audioSource = GetComponent<AudioSource>();
	}

	public void PlayMusic()
	{
		if (_audioSource.isPlaying) return;
		_audioSource.Play();
	}

	public bool isPlaying(){
		return _audioSource.isPlaying;
	}

	public void StopMusic()
	{
		_audioSource.Stop();
	}

}