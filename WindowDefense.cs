using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200016D RID: 365
public class WindowDefense : SingletoneBehaviour<WindowDefense>
{
	// Token: 0x170000E2 RID: 226
	// (get) Token: 0x06000878 RID: 2168 RVA: 0x0001389C File Offset: 0x00011A9C
	// (set) Token: 0x06000879 RID: 2169 RVA: 0x000138A4 File Offset: 0x00011AA4
	public int HP
	{
		get
		{
			return this._HP;
		}
		set
		{
			this._HP = value;
			this.HPChanged();
		}
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x000138B3 File Offset: 0x00011AB3
	public void PlayGameOverSound()
	{
		this._gameOverSound.Play();
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x000138C0 File Offset: 0x00011AC0
	private IEnumerator CountDownPlay()
	{
		this.Score = 0;
		this.HP = 5;
		this.SetMaxHP(5);
		this._audioSource.pitch = 1f;
		float _waitTime = 1f;
		this.ScoreText.text = "3";
		this._intervals[0].PulseOnce();
		this.PlaySuccessSound(0);
		yield return new WaitForSeconds(_waitTime);
		this.ScoreText.text = "2";
		this._intervals[0].PulseOnce();
		this.PlaySuccessSound(0);
		yield return new WaitForSeconds(_waitTime);
		this.ScoreText.text = "1";
		this._intervals[0].PulseOnce();
		this.PlaySuccessSound(0);
		yield return new WaitForSeconds(_waitTime);
		this.ScoreText.text = "START!";
		this._intervals[0].PulseOnce();
		this.PlaySuccessSound(0);
		this.HP = this._HP;
		this._audioSource.Play();
		ShortcutExtensions.DOScale(this.MusicPlayer.transform, Vector3.zero, 0.1f);
		yield break;
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x000138CF File Offset: 0x00011ACF
	public void PlusScore(int score = 100)
	{
		this.Score += score;
		this.ScoreText.text = this.Score.ToString();
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x00043574 File Offset: 0x00041774
	public void PlaySuccessSound(int index = -1)
	{
		if (index == -1)
		{
			index = Random.Range(0, this._successClip.Count);
		}
		for (int i = 0; i < this._sfxSound.Count; i++)
		{
			if (!this._sfxSound[i].isPlaying)
			{
				this._sfxSound[i].clip = this._successClip[index];
				this._sfxSound[i].Play();
				return;
			}
		}
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x000435F0 File Offset: 0x000417F0
	public void PlayFailSound(int index = -1)
	{
		if (index == -1)
		{
			index = Random.Range(0, this._failClip.Count);
		}
		for (int i = 0; i < this._sfxSound.Count; i++)
		{
			if (!this._sfxSound[i].isPlaying)
			{
				this._sfxSound[i].clip = this._failClip[index];
				this._sfxSound[i].Play();
				return;
			}
		}
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x0004366C File Offset: 0x0004186C
	public void PlayHorrorSound(int index = -1)
	{
		if (index == -1)
		{
			index = Random.Range(0, this._horrorSound.Count);
		}
		for (int i = 0; i < this._horrorSound.Count; i++)
		{
			if (!this._horrorSound[i].isPlaying)
			{
				this._horrorSound[i].clip = this._horrorClip[index];
				this._horrorSound[i].Play();
				return;
			}
		}
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x000436E8 File Offset: 0x000418E8
	private void Start()
	{
		for (int i = 0; i < 16; i++)
		{
			AudioSource audioSource = ComponentHolderProtocol.AddComponent<AudioSource>(this._sfxTransform);
			audioSource.playOnAwake = false;
			audioSource.loop = false;
			this._sfxSound.Add(audioSource);
		}
		for (int j = 0; j < 3; j++)
		{
			AudioSource audioSource2 = ComponentHolderProtocol.AddComponent<AudioSource>(this._sfxTransform);
			audioSource2.playOnAwake = false;
			audioSource2.loop = false;
			this._horrorSound.Add(audioSource2);
		}
		this.dontRemoveIndex = this._intervals[0].GetObjectCount();
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x00043770 File Offset: 0x00041970
	private void Update()
	{
		this.DetectMusicStarting();
		foreach (Intervals intervals2 in this._intervals)
		{
			float num = (float)this._audioSource.timeSamples / ((float)this._audioSource.clip.frequency * intervals2.GetIntervalLength(this._bpm));
			intervals2.CheckForNewInterval(num);
		}
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x000138F5 File Offset: 0x00011AF5
	private IEnumerator HorrorMusicStart()
	{
		this.isPlayingHorror = true;
		float speed = 0.1f;
		this.PlayHorrorSound(0);
		SingletoneBehaviour<GlitchManager>.Instance.Vignette_Switch(true);
		SingletoneBehaviour<GlitchManager>.Instance.GetVignette().smoothness.value = 1f;
		SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(2f, 1f, false);
		SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(2f, 0.75f, false);
		SingletoneBehaviour<GlitchManager>.Instance.AutoLensDistortion(1f);
		this._intervals[0]._steps *= 2f;
		int num;
		for (int i = 0; i < this.hpImages.Count; i = num + 1)
		{
			if (i >= this.HP)
			{
				this._audioSource.pitch += speed * 0.4f;
				this.HP = i;
				this.hpImages[i].sprite = this.fullHeartImage;
				this.hpImages[i].gameObject.SetActive(true);
				yield return new WaitForSeconds(speed);
				speed -= 0.0025f;
				speed = Mathf.Clamp(speed, 0.02f, 0.1f);
			}
			num = i;
		}
		this._intervals[0]._steps *= 2f;
		this.SetMaxHP(this.HP);
		float pitch = this._audioSource.pitch;
		this.pitchTween = DOVirtual.Float(pitch, pitch + 0.5f, 0.5f, delegate(float value)
		{
			this._audioSource.pitch = value;
		}).SetLoops(-1, LoopType.Yoyo).OnKill(delegate
		{
			this._audioSource.pitch = 1f;
		});
		yield return null;
		yield break;
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x00013904 File Offset: 0x00011B04
	public void PlayMusic()
	{
		if (this.isPlayingGame)
		{
			return;
		}
		this.isPlayingGame = true;
		base.StartCoroutine("CountDownPlay");
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x000437D0 File Offset: 0x000419D0
	public void MusicEnd()
	{
		Intervals[] intervals = this._intervals;
		for (int i = 0; i < intervals.Length; i++)
		{
			intervals[i].SetDefault();
		}
	}

	// Token: 0x06000885 RID: 2181 RVA: 0x00013922 File Offset: 0x00011B22
	public void AddNewObjectToIntervals(GameObject obj, int index = 0)
	{
		this._intervals[index].AddObject(obj);
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x000437FC File Offset: 0x000419FC
	public void DetectMusicStarting()
	{
		if (this.beforeSample < (float)this._audioSource.timeSamples)
		{
			this.beforeSample = (float)this._audioSource.timeSamples - 1f;
			return;
		}
		if (this.beforeSample > (float)this._audioSource.timeSamples)
		{
			this.MusicEnd();
			UnityEvent initTriggerWhenMusicRestart = this.InitTriggerWhenMusicRestart;
			if (initTriggerWhenMusicRestart != null)
			{
				initTriggerWhenMusicRestart.Invoke();
			}
			this.beforeSample = (float)this._audioSource.timeSamples - 1f;
		}
	}

	// Token: 0x06000887 RID: 2183 RVA: 0x0004387C File Offset: 0x00041A7C
	public void SetMaxHP(int value = 5)
	{
		this.maxHP = value;
		for (int i = this.maxHP; i < this.hpImages.Count; i++)
		{
			this.hpImages[i].gameObject.SetActive(false);
		}
		this.HPChanged();
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x000438C8 File Offset: 0x00041AC8
	private void HPChanged()
	{
		for (int i = 0; i < this.maxHP; i++)
		{
			if (i < this.HP)
			{
				this.hpImages[i].sprite = this.fullHeartImage;
			}
			else
			{
				this.hpImages[i].sprite = this.emptyHeartImage;
			}
		}
		if (this.HP <= 0 && this.isPlayingGame)
		{
			this.isPlayingGame = false;
			this._audioSource.Stop();
			base.StartCoroutine("GameOverRoutine");
		}
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x00013932 File Offset: 0x00011B32
	private IEnumerator GameOverRoutine()
	{
		this.PlayGameOverSound();
		yield return new WaitForSeconds(0.5f);
		List<GameObject> _objects = this._intervals[0].ClearObjects();
		SingletoneBehaviour<TrashCanForMiniGame>.Instance.RemoveFile(null, false);
		int num;
		for (int i = this.dontRemoveIndex; i < _objects.Count; i = num + 1)
		{
			if (_objects[i] != null)
			{
				yield return new WaitForSeconds(0.2f);
				_objects[i].GetComponent<PurseByBeat>().RemoveSelf(false);
			}
			num = i;
		}
		_objects.RemoveAll((GameObject obj) => obj == null);
		yield return new WaitForSeconds(0.5f);
		ShortcutExtensions.DOScale(this.MusicPlayer.transform, Vector3.one * 1.5f, 0.1f);
		if (this.isPlayingHorror)
		{
			SingletoneBehaviour<GlitchManager>.Instance.Vignette_Switch(false);
			SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0.1f, 0f, false);
			SingletoneBehaviour<GlitchManager>.Instance.ResetFilmGrain();
			SingletoneBehaviour<GlitchManager>.Instance.ResetLensDistortion();
			this._intervals[0]._steps /= 4f;
			this.pitchTween.Kill(false);
			this.SetMaxHP(5);
		}
		yield break;
	}

	// Token: 0x0400095F RID: 2399
	public int _HP = 3;

	// Token: 0x04000960 RID: 2400
	public int maxHP = 5;

	// Token: 0x04000961 RID: 2401
	public List<Image> hpImages = new List<Image>();

	// Token: 0x04000962 RID: 2402
	[SerializeField]
	private Sprite fullHeartImage;

	// Token: 0x04000963 RID: 2403
	[SerializeField]
	private Sprite emptyHeartImage;

	// Token: 0x04000964 RID: 2404
	[SerializeField]
	private bool isPlayingGame;

	// Token: 0x04000965 RID: 2405
	private int dontRemoveIndex;

	// Token: 0x04000966 RID: 2406
	private float beforeSample;

	// Token: 0x04000967 RID: 2407
	public UnityEvent InitTriggerWhenMusicRestart;

	// Token: 0x04000968 RID: 2408
	[SerializeField]
	private float _bpm;

	// Token: 0x04000969 RID: 2409
	[SerializeField]
	private AudioSource _audioSource;

	// Token: 0x0400096A RID: 2410
	[SerializeField]
	private Intervals[] _intervals;

	// Token: 0x0400096B RID: 2411
	[SerializeField]
	private AudioSource _gameOverSound;

	// Token: 0x0400096C RID: 2412
	[SerializeField]
	private Transform _sfxTransform;

	// Token: 0x0400096D RID: 2413
	[SerializeField]
	private List<AudioClip> _successClip = new List<AudioClip>();

	// Token: 0x0400096E RID: 2414
	[SerializeField]
	private List<AudioClip> _failClip = new List<AudioClip>();

	// Token: 0x0400096F RID: 2415
	[SerializeField]
	private List<AudioSource> _sfxSound = new List<AudioSource>();

	// Token: 0x04000970 RID: 2416
	[SerializeField]
	private List<AudioClip> _horrorClip = new List<AudioClip>();

	// Token: 0x04000971 RID: 2417
	[SerializeField]
	private List<AudioSource> _horrorSound = new List<AudioSource>();

	// Token: 0x04000972 RID: 2418
	[SerializeField]
	private GameObject MusicPlayer;

	// Token: 0x04000973 RID: 2419
	[SerializeField]
	private TextMeshProUGUI ScoreText;

	// Token: 0x04000974 RID: 2420
	[SerializeField]
	private int Score;

	// Token: 0x04000975 RID: 2421
	public bool isPlayingHorror;

	// Token: 0x04000976 RID: 2422
	private Tween pitchTween;
}
