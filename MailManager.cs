using System;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200040B RID: 1035
public class MailManager : SingletoneBehaviour<MailManager>
{
	// Token: 0x06001DE0 RID: 7648 RVA: 0x0001B632 File Offset: 0x00019832
	public void ScrollUp()
	{
		this.scroll.verticalNormalizedPosition = 1f;
	}

	// Token: 0x06001DE1 RID: 7649 RVA: 0x0001B644 File Offset: 0x00019844
	public void ScrollDown()
	{
		this.scroll_Preview.verticalNormalizedPosition = 0f;
	}

	// Token: 0x06001DE2 RID: 7650 RVA: 0x000D8898 File Offset: 0x000D6A98
	public void SetMailContent(MailObject mailObject)
	{
		this.mailContent.SetActive(true);
		this.mailContents.ProfileImage.GetComponent<CustomAnimator>().PlayAnimation(mailObject.data.animationIndex);
		RectTransform component = this.mailContents.ProfileImage.GetComponent<RectTransform>();
		component.localScale = Vector3.one * mailObject.data.scale;
		component.localPosition = new Vector3(component.localPosition.x, mailObject.data.posY, component.localPosition.z);
		this.mailContents.NameText.text = mailObject.data.name;
		this.mailContents.TitleText.text = "";
		this.mailContents.DateText.text = mailObject.data.date;
		this.mailContents.ContentText.text = "<size=40>" + mailObject.data.title + "</size>\n\n";
		TextMeshProUGUI contentText = this.mailContents.ContentText;
		contentText.text += mailObject.data.content;
		this.mailContents.FileNameText.text = mailObject.data.fileName;
		base.StartCoroutine("SetHeight");
		if (mailObject.data.fileName == "" || mailObject.data.fileName.Length <= 0)
		{
			this.mailContents.FileDownload.enabled = false;
			return;
		}
		this.mailContents.FileDownload.enabled = true;
	}

	// Token: 0x06001DE3 RID: 7651 RVA: 0x0001B656 File Offset: 0x00019856
	private IEnumerator SetHeight()
	{
		yield return new WaitForEndOfFrame();
		Vector2 sizeDelta = this.mailContent.GetComponent<RectTransform>().sizeDelta;
		sizeDelta.y = this.mailContents.ContentText.renderedHeight + 50f;
		this.mailContent.GetComponent<RectTransform>().sizeDelta = sizeDelta;
		yield break;
	}

	// Token: 0x06001DE4 RID: 7652 RVA: 0x000D8A38 File Offset: 0x000D6C38
	private void Awake()
	{
		if (this.autoLoad)
		{
			this.mailList.Load();
		}
		else
		{
			this.mailList.Clear();
		}
		foreach (MailSlot mailSlot in this.mailList.Container)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.mailObject, Vector3.zero, Quaternion.identity, this.previewContent);
			gameObject.transform.localScale = Vector3.one;
			gameObject.GetComponent<MailObject>().SetData(mailSlot.item.data, mailSlot.isRead);
		}
	}

	// Token: 0x06001DE5 RID: 7653 RVA: 0x0000E32C File Offset: 0x0000C52C
	private void Start()
	{
	}

	// Token: 0x06001DE6 RID: 7654 RVA: 0x000D8AF0 File Offset: 0x000D6CF0
	public void SetMailBrokenStatus(bool enable)
	{
		GameObject window = SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.MailBox);
		if (window.activeSelf)
		{
			window.GetComponent<UIWindow>().DestroyBox(true, false);
		}
		this.BrokenImage.SetActive(enable);
	}

	// Token: 0x06001DE7 RID: 7655 RVA: 0x0001B665 File Offset: 0x00019865
	public void SetReadMail(int id)
	{
		this.mailList.SetReadMail(id);
	}

	// Token: 0x06001DE8 RID: 7656 RVA: 0x0001B673 File Offset: 0x00019873
	public void AddNewMailDelay(int id = -1)
	{
		base.StartCoroutine("WaitChapterSetter", id);
	}

	// Token: 0x06001DE9 RID: 7657 RVA: 0x0001B687 File Offset: 0x00019887
	private IEnumerator WaitChapterSetter(int id = -1)
	{
		yield return new WaitUntil(() => ChapterSetter.ChapterSetEnd >= 0);
		this.AddNewMail(id, false);
		yield break;
	}

	// Token: 0x06001DEA RID: 7658 RVA: 0x000D8B2C File Offset: 0x000D6D2C
	public void AddNewMail(int id = -1, bool mute = false)
	{
		MailSlot mailSlot = this.mailList.AddItem(id);
		if (mailSlot == null)
		{
			return;
		}
		GameObject gameObject = Object.Instantiate<GameObject>(this.mailObject, Vector3.zero, Quaternion.identity, this.previewContent);
		gameObject.transform.localScale = Vector3.one;
		gameObject.GetComponent<MailObject>().SetData(mailSlot.item.data, mute || mailSlot.isRead);
		if (!mute)
		{
			this.NewMailAnimation();
		}
	}

	// Token: 0x06001DEB RID: 7659 RVA: 0x000D8BA0 File Offset: 0x000D6DA0
	private void Update()
	{
		if (GameManager.instance.gameData.Bo.blockDialogue)
		{
			if (this.playingTween && !this.waitMailAnimation)
			{
				this.waitMailAnimation = true;
				base.transform.GetChild(1).GetChild(0).localScale = Vector3.one;
				this.tween.Kill(false);
				return;
			}
		}
		else if (this.waitMailAnimation)
		{
			this.waitMailAnimation = false;
			this.NewMailAnimation();
		}
	}

	// Token: 0x06001DEC RID: 7660 RVA: 0x000D8C18 File Offset: 0x000D6E18
	public void NewMailAnimation()
	{
		this.notReadMail = true;
		if (this.tween != null && this.tween.IsPlaying())
		{
			this.playingTween = false;
			return;
		}
		Transform ChildIcon = base.transform.GetChild(1).GetChild(0);
		ChildIcon.localScale = Vector3.one;
		Sequence sequence = DOTween.Sequence();
		sequence.Pause<Sequence>();
		sequence.OnPlay(delegate
		{
			this.playingTween = true;
		});
		sequence.Append(ShortcutExtensions.DOScaleX(ChildIcon, 1.15f, 0.15f).SetEase(Ease.OutBack).SetLoops(2, LoopType.Yoyo));
		sequence.Join(ShortcutExtensions.DOScaleY(ChildIcon, 1.35f, 0.15f).SetEase(Ease.OutBack).SetLoops(2, LoopType.Yoyo));
		sequence.AppendInterval(0.2f);
		sequence.Append(ShortcutExtensions.DOScaleX(ChildIcon, 1.15f, 0.15f).SetEase(Ease.OutBack).SetLoops(2, LoopType.Yoyo));
		sequence.Join(ShortcutExtensions.DOScaleY(ChildIcon, 1.35f, 0.15f).SetEase(Ease.OutBack).SetLoops(2, LoopType.Yoyo));
		sequence.AppendInterval(1f);
		sequence.Play<Sequence>();
		sequence.onComplete = delegate
		{
			this.playingTween = false;
			ChildIcon.localScale = Vector3.one;
			if (this.notReadMail)
			{
				sequence.Restart(true, -1f);
			}
		};
		this.tween = sequence;
	}

	// Token: 0x06001DED RID: 7661 RVA: 0x000D8DBC File Offset: 0x000D6FBC
	public void OpenMail()
	{
		this.SetMailBrokenStatus(GameManager.SystemBroken);
		if (GameManager.SystemBroken)
		{
			this.BrokenMailEnable();
		}
		this.notReadMail = false;
		this.playingTween = false;
		this.waitMailAnimation = false;
		this.tween.Kill(false);
		base.transform.GetChild(1).GetChild(0).localScale = Vector3.one;
	}

	// Token: 0x06001DEE RID: 7662 RVA: 0x000D8E20 File Offset: 0x000D7020
	public void ReadMail(GameObject lastMail)
	{
		if (this.LastMail != null)
		{
			this.LastMail.GetComponent<Image>().color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		}
		this.LastMail = lastMail;
		this.LastMail.GetComponent<Image>().color = new Color32(200, 200, 200, byte.MaxValue);
	}

	// Token: 0x06001DEF RID: 7663 RVA: 0x000D8EA0 File Offset: 0x000D70A0
	public void FadeBlack(bool fadeOut = true, float duration = 0.5f)
	{
		if (this.LastTween != null)
		{
			this.LastTween.Kill(false);
		}
		if (fadeOut)
		{
			this.black.SetActive(true);
			this.black_CanvasGroup.alpha = 0f;
			this.LastTween = this.black_CanvasGroup.DOFade(1f, duration);
			return;
		}
		this.black.SetActive(true);
		this.black_CanvasGroup.alpha = 1f;
		this.LastTween = this.black_CanvasGroup.DOFade(0f, duration).OnComplete(delegate
		{
			this.black.SetActive(false);
		});
	}

	// Token: 0x06001DF0 RID: 7664 RVA: 0x000D8F3C File Offset: 0x000D713C
	private void BrokenMailEnable()
	{
		this.changeVolume = true;
		this.origin_BGM_Pitch = SoundManager.instance.bgmPlayer.pitch;
		this.origin_BGM_Volume = SoundManager.instance.bgmPlayer.volume;
		SoundManager.instance.BGM_ChangePitch(0.5f, -0.6f);
		SoundManager.instance.BGM_ChangeVolume_Tween(0.5f, 0.6f, false);
		SingletoneBehaviour<ConsoleController>.Instance.BlackFade(0.5f, true, 0.6f);
		SingletoneBehaviour<MailManager>.Instance.FadeBlack(true, 0.5f);
		SingletoneBehaviour<GlitchManager>.Instance.Vignette_Switch(true);
		SingletoneBehaviour<GlitchManager>.Instance.ChangeVignette_Color("Black");
		this.vignetteVol_originValue = SingletoneBehaviour<GlitchManager>.Instance.vignette.intensity.value;
		SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0.5f, 0.7f, false);
		SingletoneBehaviour<GlitchManager>.Instance.GetFilmGrain();
		this.flimGrain_intensity_originValue = SingletoneBehaviour<GlitchManager>.Instance.filmGrain.intensity.value;
		this.flimGrain_response_originValue = SingletoneBehaviour<GlitchManager>.Instance.filmGrain.response.value;
		SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.5f, 1f, false);
		SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Response(0.5f, 0f);
		SingletoneBehaviour<GlitchManager>.Instance.GetChromaticAberration();
		this.chromaticAberration_intensity_originValue = SingletoneBehaviour<GlitchManager>.Instance.chromaticAberration.intensity.value;
		SingletoneBehaviour<GlitchManager>.Instance.TweenChromaticAberration(0.5f, this.chromaticAberration_intensity_originValue, 0.65f, false);
		SingletoneBehaviour<GlitchManager>.Instance.GetVhsVolume();
		this.vhs_weight_originValue = SingletoneBehaviour<GlitchManager>.Instance.vhsVol._weight.value;
		SingletoneBehaviour<GlitchManager>.Instance.TweenVhsVolume_weight(0.5f, 0.5f);
		SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.MailBox);
		base.Invoke("BrokenMailDisable", 2f);
	}

	// Token: 0x06001DF1 RID: 7665 RVA: 0x000D9114 File Offset: 0x000D7314
	private void BrokenMailDisable()
	{
		if (this.changeVolume)
		{
			this.changeVolume = false;
			SoundManager.instance.BGM_ChangePitch(0.5f, this.origin_BGM_Pitch);
			SoundManager.instance.BGM_ChangeVolume_Tween(0.5f, this.origin_BGM_Volume, false);
			SingletoneBehaviour<ConsoleController>.Instance.BlackFade(0.5f, false, 0.6f);
			this.FadeBlack(false, 0.5f);
			SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0.5f, this.vignetteVol_originValue, false);
			SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.5f, this.flimGrain_intensity_originValue, false);
			SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Response(0.5f, this.flimGrain_response_originValue);
			SingletoneBehaviour<GlitchManager>.Instance.TweenChromaticAberration(0.5f, 0.65f, this.chromaticAberration_intensity_originValue, false);
			SingletoneBehaviour<GlitchManager>.Instance.TweenVhsVolume_weight(0.5f, this.vhs_weight_originValue);
			GameObject window = SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.MailBox);
			if (window.activeSelf)
			{
				window.GetComponent<UIWindow>().DestroyBox(false, false);
			}
		}
	}

	// Token: 0x04001BFD RID: 7165
	public GameObject BrokenImage;

	// Token: 0x04001BFE RID: 7166
	public bool TestMail;

	// Token: 0x04001BFF RID: 7167
	public bool autoSave;

	// Token: 0x04001C00 RID: 7168
	public bool autoLoad;

	// Token: 0x04001C01 RID: 7169
	public MailList mailList;

	// Token: 0x04001C02 RID: 7170
	[SerializeField]
	private GameObject mailObject;

	// Token: 0x04001C03 RID: 7171
	[SerializeField]
	private MailContents mailContents;

	// Token: 0x04001C04 RID: 7172
	[SerializeField]
	public Transform previewContent;

	// Token: 0x04001C05 RID: 7173
	[SerializeField]
	public GameObject mailContent;

	// Token: 0x04001C06 RID: 7174
	private bool notReadMail = true;

	// Token: 0x04001C07 RID: 7175
	private Tween tween;

	// Token: 0x04001C08 RID: 7176
	public ScrollRect scroll;

	// Token: 0x04001C09 RID: 7177
	public ScrollRect scroll_Preview;

	// Token: 0x04001C0A RID: 7178
	public GameObject black;

	// Token: 0x04001C0B RID: 7179
	public CanvasGroup black_CanvasGroup;

	// Token: 0x04001C0C RID: 7180
	public bool isReservationMail;

	// Token: 0x04001C0D RID: 7181
	public int GetMailId = 2;

	// Token: 0x04001C0E RID: 7182
	public bool waitMailAnimation;

	// Token: 0x04001C0F RID: 7183
	public bool playingTween;

	// Token: 0x04001C10 RID: 7184
	public GameObject LastMail;

	// Token: 0x04001C11 RID: 7185
	public Tween LastTween;

	// Token: 0x04001C12 RID: 7186
	public float origin_BGM_Volume;

	// Token: 0x04001C13 RID: 7187
	public float origin_BGM_Pitch;

	// Token: 0x04001C14 RID: 7188
	public float vignetteVol_originValue;

	// Token: 0x04001C15 RID: 7189
	public float flimGrain_intensity_originValue;

	// Token: 0x04001C16 RID: 7190
	public float flimGrain_response_originValue;

	// Token: 0x04001C17 RID: 7191
	public float chromaticAberration_intensity_originValue;

	// Token: 0x04001C18 RID: 7192
	public float vhs_weight_originValue;

	// Token: 0x04001C19 RID: 7193
	public bool changeVolume;
}
