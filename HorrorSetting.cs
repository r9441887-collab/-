using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;

// Token: 0x02000018 RID: 24
public class HorrorSetting : SingletoneBehaviour<HorrorSetting>
{
	// Token: 0x06000084 RID: 132 RVA: 0x0000E8B7 File Offset: 0x0000CAB7
	private IEnumerator TranslateMessage()
	{
		yield return new WaitUntil(() => DBManager.instance != null);
		for (int i = 0; i < this.texts.Count; i++)
		{
			this.texts[i].text = DBManager.instance.GetSettingString("3D_0", 0, i, 1);
		}
		yield break;
	}

	// Token: 0x06000085 RID: 133 RVA: 0x0001FEC4 File Offset: 0x0001E0C4
	public void OpenSystemWinionRoom()
	{
		this.SystemWinionDoor.isLocked = true;
		this.SystemWinionDoor.OpenDoor();
		if (this.SomethinsIsWrong_SystemWinionRoom == 0)
		{
			SingletoneBehaviour<PopUpMessage>.Instance.PopByIndex(3);
			return;
		}
		if (this.SomethinsIsWrong_SystemWinionRoom == 1)
		{
			SingletoneBehaviour<PopUpMessage>.Instance.PopByIndex(4);
		}
	}

	// Token: 0x06000086 RID: 134 RVA: 0x0001FF10 File Offset: 0x0001E110
	public void OpenGreenFieldRoom()
	{
		this.GreenFieldDoor.isLocked = true;
		this.GreenFieldDoor.OpenDoor();
		if (this.PlayFizzleSound)
		{
			int num = Random.Range(0, this.FizzleClips.Count);
			float num2 = Random.Range(0.9f, 1.15f);
			this.FizzleSource.clip = this.FizzleClips[num];
			this.FizzleSource.pitch = num2;
			this.FizzleSource.Play();
		}
		if (this.SomethinsIsWrong_GreenField)
		{
			SingletoneBehaviour<PopUpMessage>.Instance.PopByIndex(4);
		}
	}

	// Token: 0x06000087 RID: 135 RVA: 0x0000E8C6 File Offset: 0x0000CAC6
	public void SetBlood(bool value)
	{
		this.bloods[0].SetActive(value);
	}

	// Token: 0x06000088 RID: 136 RVA: 0x0000E8DA File Offset: 0x0000CADA
	public void LockTrashDoor(bool value)
	{
		this.TrashDoor.isLocked = value;
	}

	// Token: 0x06000089 RID: 137 RVA: 0x0000E8E8 File Offset: 0x0000CAE8
	public void OpenTrashDoor()
	{
		if (this.TrashDoor.isLocked)
		{
			SingletoneBehaviour<PopUpMessage>.Instance.PopByIndex(6);
		}
		this.TrashDoor.OpenDoor();
	}

	// Token: 0x0600008A RID: 138 RVA: 0x0000E90D File Offset: 0x0000CB0D
	private IEnumerator PlayVideo()
	{
		this.VideoEnd = false;
		this.PlayerMovement.enabled = false;
		yield return TweenExtensions.WaitForCompletion(this.VideoGroup.DOFade(1f, 0.5f));
		this.Tutorial_CutScene_GlitchON();
		this.cutScene_obj.SetActive(true);
		this.win_S_Mark.SetActive(true);
		this.win_S_Mark_CanvasGroup.alpha = 0f;
		yield return new WaitForSeconds(2f);
		this.canStop = true;
		this.skipText.SetActive(true);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.EggBreakSound_3, false, 1f, 1f);
		if (SoundManager.LastBGM != SoundManager.BGM.HorrorBGM_01)
		{
			this.lastBGM = SoundManager.LastBGM;
		}
		SoundManager.Instance.Play_BGM(SoundManager.BGM.HorrorBGM_01, true, 0f);
		SoundManager.Instance.bgmPlayer.pitch = 0.9f;
		this.VolumeTween = SoundManager.Instance.bgmPlayer.DOFade(0.3f, 2f);
		yield return TweenExtensions.WaitForCompletion(this.win_S_Mark_CanvasGroup.DOFade(1f, 2f));
		yield return new WaitForSeconds(3f);
		if (HorrorSetting.PlayHorrorVideo)
		{
			SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.Zizizic4, true, 0.8f, 1f);
			yield return new WaitForSeconds(1f);
			SoundManager.Instance.Stop_SfxSound_2(SoundManager.SfxSound_2.Zizizic4, 0.1f);
			base.StartCoroutine("PitchTween");
		}
		else
		{
			yield return new WaitForSeconds(1f);
		}
		yield return TweenExtensions.WaitForCompletion(this.win_S_Mark_CanvasGroup.DOFade(0f, 2f));
		this.win_S_Mark.SetActive(false);
		this.tutorial_Text_OBJ.SetActive(true);
		this.tutorial_Text.text = "";
		this.tutorial_Text_canvasGroup.alpha = 0f;
		int text_index = 0;
		this.tutorial_img_obj.SetActive(true);
		int num2;
		for (int i = 0; i < this.tutorial_img_List.Count; i = num2 + 1)
		{
			this.tutorial_img_List[i].SetActive(true);
			this.tutorial_img_List_CanvasGroup[i].alpha = 0f;
			SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.CaptureSound_2, false, 1f, Random.Range(0.9f, 1.1f));
			this.tutorial_img_List_CanvasGroup[i].DOFade(1f, 2f);
			int finishIndex = 1;
			if (i == 4)
			{
				finishIndex = 3;
			}
			else
			{
				finishIndex = 1;
			}
			for (int j = 0; j < finishIndex; j = num2 + 1)
			{
				DBManager.instance.dialogueController.endChat = false;
				this.tutorial_Text.text = "";
				yield return TweenExtensions.WaitForCompletion(this.tutorial_Text_canvasGroup.DOFade(1f, 1f));
				DBManager instance = DBManager.instance;
				string text = "c0_e0_0";
				int num = 0;
				num2 = text_index;
				text_index = num2 + 1;
				string settingString = instance.GetSettingString(text, num, num2, 1);
				DBManager.instance.chatController.Chat_Obect_TmpText_ChangeSpeed(this.tutorial_Text, settingString, false, false, 0.05f);
				yield return new WaitUntil(() => DBManager.instance.dialogueController.endChat);
				DBManager.instance.dialogueController.endChat = false;
				yield return new WaitForSeconds(1.5f);
				if (HorrorSetting.PlayHorrorVideo && i == 0)
				{
					base.StartCoroutine("PitchTween");
					SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.FuseSound_1, false, 1f, 1f);
					this.HorrorCutScene[0].SetActive(true);
					this.HorrorTween();
					yield return new WaitForSeconds(0.5f);
				}
				if (HorrorSetting.PlayHorrorVideo && i == 1)
				{
					base.StartCoroutine("PitchTween");
					SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.FuseSound_1, false, 1f, 1f);
					this.HorrorCutScene[0].SetActive(true);
					this.HorrorTween();
					yield return new WaitForSeconds(0.5f);
				}
				if (HorrorSetting.PlayHorrorVideo && i == 3)
				{
					base.StartCoroutine("PitchTween");
					SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.FuseSound_2, false, 1f, 1f);
					this.HorrorCutScene[2].SetActive(true);
					this.ScaleTween();
				}
				if (HorrorSetting.PlayHorrorVideo && i == 4 && j == 1)
				{
					base.StartCoroutine("PitchTween");
					this.HorrorCutScene[1].SetActive(true);
					this.HorrorTween();
					this.ScaleTween();
					yield return new WaitForSeconds(1f);
					SoundManager.Instance.bgmPlayer.volume = 0f;
					SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.Zizizic3, true, 0.8f, 1f);
					yield return new WaitForSeconds(3f);
					this.Black.SetActive(true);
					SoundManager.Instance.Stop_SfxSound_2(SoundManager.SfxSound_2.Zizizic3, 0.1f);
					yield return new WaitForSeconds(1f);
					this.Black.SetActive(false);
					SoundManager.Instance.bgmPlayer.volume = 0.3f;
					this.HorrorCutScene[1].SetActive(false);
				}
				if (HorrorSetting.PlayHorrorVideo && i == 5)
				{
					base.StartCoroutine("PitchTween");
					SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.FuseSound_2, false, 1f, 1f);
					this.HorrorCutScene[2].SetActive(true);
					this.ScaleTween();
				}
				if (HorrorSetting.PlayHorrorVideo && i == 6)
				{
					SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.AliveHere, false, 1f, 1f);
				}
				if (i == 4 && j < finishIndex - 1)
				{
					yield return TweenExtensions.WaitForCompletion(this.tutorial_Text_canvasGroup.DOFade(0f, 2f));
					this.tutorial_Text.text = "";
				}
				else
				{
					this.tutorial_Text_canvasGroup.DOFade(0f, 2f);
				}
				num2 = j;
			}
			if (HorrorSetting.PlayHorrorVideo && i == 0)
			{
				yield return new WaitForSeconds(0.5f);
				this.HorrorCutScene[0].SetActive(false);
			}
			if (HorrorSetting.PlayHorrorVideo && i == 1)
			{
				yield return new WaitForSeconds(0.5f);
				this.HorrorCutScene[0].SetActive(false);
			}
			if (HorrorSetting.PlayHorrorVideo && i == 3)
			{
				yield return new WaitForSeconds(0.6f);
				this.HorrorCutScene[2].SetActive(false);
			}
			if (HorrorSetting.PlayHorrorVideo && i == 5)
			{
				yield return new WaitForSeconds(0.6f);
				this.HorrorCutScene[2].SetActive(false);
			}
			if (HorrorSetting.PlayHorrorVideo && i == 8)
			{
				this.HorrorCutScene[3].SetActive(true);
				SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.Hit_metallic, false, 1f, 1f);
				this.HorrorCutScene[3].GetComponent<CustomAnimator>().PlayAnimation("OpenEyes", false);
				this.HorrorCutScene[3].GetComponent<CustomAnimator>().SetLoop(false);
				this.HorrorCutScene[3].GetComponent<CustomAnimator>().EndFrameAction = delegate
				{
					this.VideoEnd = true;
				};
				break;
			}
			yield return TweenExtensions.WaitForCompletion(this.tutorial_img_List_CanvasGroup[i].DOFade(0f, 2f));
			this.tutorial_Text.text = "";
			this.tutorial_Text_canvasGroup.alpha = 0f;
			this.tutorial_img_List[i].SetActive(false);
			num2 = i;
		}
		if (HorrorSetting.PlayHorrorVideo)
		{
			yield return new WaitUntil(() => this.VideoEnd);
			this.Tutorial_CutScene_GlitchOFF();
		}
		this.canStop = false;
		this.skipText.SetActive(false);
		this.tutorial_Text_OBJ.SetActive(false);
		this.tutorial_Text.text = "";
		this.tutorial_Text_canvasGroup.alpha = 0f;
		this.tutorial_img_obj.SetActive(false);
		HorrorSetting.isPlayingVideo = false;
		HorrorSetting.canPlayVideo = false;
		this.VideoGroup.DOFade(0f, 0.5f);
		yield return TweenExtensions.WaitForCompletion(SoundManager.Instance.bgmPlayer.DOFade(0f, 0.5f));
		SoundManager.Instance.Play_BGM(this.lastBGM, true, 0f);
		this.Tutorial_CutScene_GlitchOFF();
		this.playVideo_co = null;
		this.PlayerMovement.enabled = true;
		DBManager.instance.NoBacklogOpen_False();
		yield break;
	}

	// Token: 0x0600008B RID: 139 RVA: 0x0001FFA0 File Offset: 0x0001E1A0
	public void PlayWinSVideo()
	{
		if (HorrorSetting.isPlayingVideo)
		{
			return;
		}
		if (HorrorSetting.canPlayVideo)
		{
			HorrorSetting.isPlayingVideo = true;
			DBManager.instance.dialogueData.NoBacklogOpen = true;
			SingletoneBehaviour<GlitchManager>.Instance.GetFilmGrain();
			this.originFilmIntensity = SingletoneBehaviour<GlitchManager>.Instance.filmGrain.intensity.value;
			SingletoneBehaviour<GlitchManager>.Instance.GetVignette();
			this.originVignetteIntensity = SingletoneBehaviour<GlitchManager>.Instance.vignette.intensity.value;
			this.playVideo_co = base.StartCoroutine(this.PlayVideo());
			Debug.Log("비디오 재생");
			return;
		}
		HorrorSetting.canPlayVideo = true;
		if (!HorrorSetting.PlayHorrorVideo)
		{
			SingletoneBehaviour<PopUpMessage>.Instance.PopByIndex(1);
			return;
		}
		SingletoneBehaviour<PopUpMessage>.Instance.PopByIndex(5);
	}

	// Token: 0x0600008C RID: 140 RVA: 0x0000E91C File Offset: 0x0000CB1C
	public void MouseExit()
	{
		HorrorSetting.canPlayVideo = false;
		SingletoneBehaviour<PopUpMessage>.Instance.PopDown();
	}

	// Token: 0x0600008D RID: 141 RVA: 0x0002005C File Offset: 0x0001E25C
	public void PlayWinSVideoExit()
	{
		if (HorrorSetting.isPlayingVideo && this.canStop)
		{
			HorrorSetting.isPlayingVideo = false;
			this.canStop = false;
			DBManager.instance.dialogueController.endChat = false;
			this.skipText.SetActive(false);
			base.StopCoroutine(this.playVideo_co);
			this.win_S_Mark.SetActive(false);
			this.win_S_Mark_CanvasGroup.alpha = 0f;
			this.tutorial_Text_OBJ.SetActive(false);
			this.tutorial_Text.text = "";
			this.tutorial_Text_canvasGroup.alpha = 0f;
			this.tutorial_img_obj.SetActive(false);
			DOTween.Kill(this.tutorial_Text_canvasGroup, false);
			for (int i = 0; i < this.tutorial_img_List_CanvasGroup.Count; i++)
			{
				DOTween.Kill(this.tutorial_img_List_CanvasGroup[i], false);
			}
			DOTween.Kill(this.win_S_Mark_CanvasGroup, false);
			HorrorSetting.isPlayingVideo = false;
			HorrorSetting.canPlayVideo = false;
			this.VideoGroup.DOFade(0f, 0.5f);
			this.VolumeTween.Kill(false);
			SoundManager.Instance.bgmPlayer.DOFade(0f, 0.5f).OnComplete(delegate
			{
				SoundManager.Instance.Play_BGM(this.lastBGM, true, 0f);
			});
			this.PlayerMovement.enabled = true;
			this.Tutorial_CutScene_GlitchOFF();
			DBManager.instance.NoBacklogOpen_False();
			SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.5f, this.originFilmIntensity, false);
			SingletoneBehaviour<GlitchManager>.Instance.Vignette_Switch(true);
			SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0.5f, this.originVignetteIntensity, false);
		}
	}

	// Token: 0x0600008E RID: 142 RVA: 0x0000E92E File Offset: 0x0000CB2E
	private void Update()
	{
		if (Input.GetKeyDown(120))
		{
			this.PlayWinSVideoExit();
		}
	}

	// Token: 0x0600008F RID: 143 RVA: 0x000201F8 File Offset: 0x0001E3F8
	public void SetChapterHorrorSetting(int currEvent)
	{
		this.SetDefault();
		if (currEvent == 100)
		{
			this.SomethinsIsWrong_SystemWinionRoom = 0;
			this.HumbleSource.Play();
		}
		else if (currEvent == 102)
		{
			this.ServerSource.Play();
			this.PlayFizzleSound = true;
		}
		else if (currEvent == 104)
		{
			this.SomethinsIsWrong_SystemWinionRoom = 1;
			this.HumbleSource.Play();
			this.ServerSource.Play();
		}
		else if (currEvent == 110)
		{
			this.ServerSource.Play();
			this.PlayFizzleSound = true;
			this.SomethinsIsWrong_GreenField = true;
		}
		else if (currEvent == 105 || currEvent == 106)
		{
			this.SetBlood(true);
		}
		else
		{
			this.SetDefault();
		}
		if (currEvent >= 210)
		{
			this.LockTrashDoor(true);
		}
		if (currEvent >= 206 && currEvent < 312)
		{
			HorrorSetting.PlayHorrorVideo = true;
			this.bloods[1].SetActive(true);
			return;
		}
		HorrorSetting.PlayHorrorVideo = false;
		this.bloods[1].SetActive(false);
	}

	// Token: 0x06000090 RID: 144 RVA: 0x000202EC File Offset: 0x0001E4EC
	public void SetDefault()
	{
		this.HumbleSource.Stop();
		this.ServerSource.Stop();
		this.PlayFizzleSound = false;
		this.SomethinsIsWrong_SystemWinionRoom = -1;
		this.SomethinsIsWrong_GreenField = false;
		this.LockTrashDoor(false);
		this.SetBlood(false);
		this.bloods[1].SetActive(false);
		HorrorSetting.PlayHorrorVideo = false;
	}

	// Token: 0x06000091 RID: 145 RVA: 0x0000E93F File Offset: 0x0000CB3F
	private void Awake()
	{
		this.SetDefault();
		base.StartCoroutine("TranslateMessage");
	}

	// Token: 0x06000092 RID: 146 RVA: 0x0000E953 File Offset: 0x0000CB53
	public void PopDown()
	{
		SingletoneBehaviour<PopUpMessage>.Instance.PopDown();
	}

	// Token: 0x06000093 RID: 147 RVA: 0x0002034C File Offset: 0x0001E54C
	public void HorrorTween()
	{
		SingletoneBehaviour<GlitchManager>.Instance.GetAsciiVolume();
		SingletoneBehaviour<GlitchManager>.Instance.asciiVol.m_Scale.value = 0.4f;
		DOVirtual.Float(0.4f, 1f, 0.3f, delegate(float f)
		{
			SingletoneBehaviour<GlitchManager>.Instance.asciiVol.m_Scale.value = f;
		});
	}

	// Token: 0x06000094 RID: 148 RVA: 0x0000E95F File Offset: 0x0000CB5F
	public void ScaleTween()
	{
		ShortcutExtensions.DOShakeScale(this.cutScene_obj.transform.GetChild(0), 0.3f, 2f, 50, 90f, false, ShakeRandomnessMode.Full).SetEase(Ease.Linear);
	}

	// Token: 0x06000095 RID: 149 RVA: 0x0000E991 File Offset: 0x0000CB91
	public IEnumerator PitchTween()
	{
		float num = SoundManager.instance.bgmPlayer.pitch;
		float num2 = Random.Range(0.5f, 1.2f);
		int num3;
		for (int i = 0; i < 4; i = num3 + 1)
		{
			num = SoundManager.instance.bgmPlayer.pitch;
			num2 = Random.Range(0.5f, 1.2f);
			yield return TweenExtensions.WaitForCompletion(DOVirtual.Float(num, num2, 0.2f, delegate(float f)
			{
				SoundManager.instance.bgmPlayer.pitch = f;
			}));
			num3 = i;
		}
		num = SoundManager.instance.bgmPlayer.pitch;
		num2 = 0.9f;
		DOVirtual.Float(num, num2, 0.1f, delegate(float f)
		{
			SoundManager.instance.bgmPlayer.pitch = f;
		});
		yield break;
	}

	// Token: 0x06000096 RID: 150 RVA: 0x000203B4 File Offset: 0x0001E5B4
	public void Tutorial_CutScene_GlitchON()
	{
		SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.5f, 0.45f, false);
		SingletoneBehaviour<GlitchManager>.Instance.Vignette_Switch(true);
		SingletoneBehaviour<GlitchManager>.Instance.ChangeVignette_Color("Black");
		SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0.5f, 0.45f, false);
		SingletoneBehaviour<GlitchManager>.Instance.TweenPixelationVol(0.5f, 1f, 0.9f, false);
		SingletoneBehaviour<GlitchManager>.Instance.LensDistortion_Switch(true);
		SingletoneBehaviour<GlitchManager>.Instance.lensDistortion.intensity.value = 0.7f;
		SingletoneBehaviour<GlitchManager>.Instance.lensDistortion.xMultiplier.value = 1f;
		SingletoneBehaviour<GlitchManager>.Instance.lensDistortion.yMultiplier.value = 1f;
		SingletoneBehaviour<GlitchManager>.Instance.lensDistortion.scale.value = 1.2f;
		SingletoneBehaviour<GlitchManager>.Instance.TweenVhsVolume_weight(0.5f, 0.5f);
	}

	// Token: 0x06000097 RID: 151 RVA: 0x000204A8 File Offset: 0x0001E6A8
	public void Tutorial_CutScene_GlitchOFF()
	{
		this.VideoEnd = false;
		for (int i = 0; i < this.tutorial_img_List.Count; i++)
		{
			this.tutorial_img_List[i].SetActive(false);
		}
		for (int j = 0; j < this.HorrorCutScene.Count; j++)
		{
			this.HorrorCutScene[j].SetActive(false);
		}
		SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.1f, this.originFilmIntensity, false);
		SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0.1f, this.originVignetteIntensity, false);
		SingletoneBehaviour<GlitchManager>.Instance.TweenPixelationVol(0.1f, 0.9f, 1f, false);
		SingletoneBehaviour<GlitchManager>.Instance.lensDistortion.intensity.value = 0f;
		SingletoneBehaviour<GlitchManager>.Instance.lensDistortion.xMultiplier.value = 0f;
		SingletoneBehaviour<GlitchManager>.Instance.lensDistortion.yMultiplier.value = 0f;
		SingletoneBehaviour<GlitchManager>.Instance.lensDistortion.scale.value = 1f;
		SingletoneBehaviour<GlitchManager>.Instance.TweenVhsVolume_weight(0.1f, 0f);
	}

	// Token: 0x040000BD RID: 189
	public FirstPersonMovement PlayerMovement;

	// Token: 0x040000BE RID: 190
	public GameObject Black;

	// Token: 0x040000BF RID: 191
	public List<TMP_Text> texts;

	// Token: 0x040000C0 RID: 192
	public AudioSource HumbleSource;

	// Token: 0x040000C1 RID: 193
	public AudioSource FizzleSource;

	// Token: 0x040000C2 RID: 194
	public List<AudioClip> FizzleClips;

	// Token: 0x040000C3 RID: 195
	public AudioSource EnemySource;

	// Token: 0x040000C4 RID: 196
	public AudioSource ServerSource;

	// Token: 0x040000C5 RID: 197
	public DoorInteraction SystemWinionDoor;

	// Token: 0x040000C6 RID: 198
	public int SomethinsIsWrong_SystemWinionRoom = -1;

	// Token: 0x040000C7 RID: 199
	public DoorInteraction GreenFieldDoor;

	// Token: 0x040000C8 RID: 200
	public bool SomethinsIsWrong_GreenField;

	// Token: 0x040000C9 RID: 201
	public bool PlayFizzleSound;

	// Token: 0x040000CA RID: 202
	public List<GameObject> bloods = new List<GameObject>();

	// Token: 0x040000CB RID: 203
	public DoorInteraction TrashDoor;

	// Token: 0x040000CC RID: 204
	public static bool PlayHorrorVideo;

	// Token: 0x040000CD RID: 205
	public static bool isPlayingVideo;

	// Token: 0x040000CE RID: 206
	public static bool canPlayVideo;

	// Token: 0x040000CF RID: 207
	public GameObject win_S_Mark;

	// Token: 0x040000D0 RID: 208
	public CanvasGroup win_S_Mark_CanvasGroup;

	// Token: 0x040000D1 RID: 209
	public GameObject cutScene_obj;

	// Token: 0x040000D2 RID: 210
	public GameObject tutorial_Text_OBJ;

	// Token: 0x040000D3 RID: 211
	public CanvasGroup tutorial_Text_canvasGroup;

	// Token: 0x040000D4 RID: 212
	public TMP_Text tutorial_Text;

	// Token: 0x040000D5 RID: 213
	public GameObject tutorial_img_obj;

	// Token: 0x040000D6 RID: 214
	public List<GameObject> tutorial_img_List;

	// Token: 0x040000D7 RID: 215
	public List<CanvasGroup> tutorial_img_List_CanvasGroup;

	// Token: 0x040000D8 RID: 216
	public CanvasGroup VideoGroup;

	// Token: 0x040000D9 RID: 217
	private SoundManager.BGM lastBGM;

	// Token: 0x040000DA RID: 218
	public Tween VolumeTween;

	// Token: 0x040000DB RID: 219
	public List<GameObject> HorrorCutScene;

	// Token: 0x040000DC RID: 220
	public bool VideoEnd;

	// Token: 0x040000DD RID: 221
	public float originFilmIntensity;

	// Token: 0x040000DE RID: 222
	public float originVignetteIntensity;

	// Token: 0x040000DF RID: 223
	private Coroutine playVideo_co;

	// Token: 0x040000E0 RID: 224
	private bool canStop;

	// Token: 0x040000E1 RID: 225
	public GameObject skipText;
}
