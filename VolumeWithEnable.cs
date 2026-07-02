using System;
using UnityEngine;

// Token: 0x020003EB RID: 1003
public class VolumeWithEnable : MonoBehaviour
{
	// Token: 0x06001D41 RID: 7489 RVA: 0x0001B080 File Offset: 0x00019280
	private void OnEnable()
	{
		if (this.functionName == "")
		{
			this.functionName = base.gameObject.name;
		}
		base.transform.SendMessage(this.functionName);
	}

	// Token: 0x06001D42 RID: 7490 RVA: 0x000D5C68 File Offset: 0x000D3E68
	private void DeliciousLunchImage()
	{
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
		SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.5f, 0.35f, false);
		SingletoneBehaviour<GlitchManager>.Instance.GetChromaticAberration();
		this.chromaticAberration_intensity_originValue = SingletoneBehaviour<GlitchManager>.Instance.chromaticAberration.intensity.value;
		SingletoneBehaviour<GlitchManager>.Instance.TweenChromaticAberration(0.5f, this.chromaticAberration_intensity_originValue, 0.65f, false);
		SingletoneBehaviour<GlitchManager>.Instance.GetVhsVolume();
		this.vhs_weight_originValue = SingletoneBehaviour<GlitchManager>.Instance.vhsVol._weight.value;
		SingletoneBehaviour<GlitchManager>.Instance.TweenVhsVolume_weight(0.5f, 0.5f);
	}

	// Token: 0x06001D43 RID: 7491 RVA: 0x000D5DEC File Offset: 0x000D3FEC
	private void DeliciousLunchImage_Exit()
	{
		SoundManager.instance.BGM_ChangePitch(0.5f, this.origin_BGM_Pitch);
		SoundManager.instance.BGM_ChangeVolume_Tween(0.5f, this.origin_BGM_Volume, false);
		SingletoneBehaviour<ConsoleController>.Instance.BlackFade(0.5f, false, 0.6f);
		SingletoneBehaviour<MailManager>.Instance.FadeBlack(false, 0.5f);
		SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0.5f, this.vignetteVol_originValue, false);
		SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.5f, this.flimGrain_intensity_originValue, false);
		SingletoneBehaviour<GlitchManager>.Instance.TweenChromaticAberration(0.5f, 0.65f, this.chromaticAberration_intensity_originValue, false);
		SingletoneBehaviour<GlitchManager>.Instance.TweenVhsVolume_weight(0.5f, this.vhs_weight_originValue);
	}

	// Token: 0x06001D44 RID: 7492 RVA: 0x000D5EAC File Offset: 0x000D40AC
	private void MyDreamImage()
	{
		this.origin_BGM_Pitch = SoundManager.instance.bgmPlayer.pitch;
		this.origin_BGM_Volume = SoundManager.instance.bgmPlayer.volume;
		SoundManager.instance.BGM_ChangePitch(0.5f, 0.2f);
		SoundManager.instance.BGM_ChangeVolume_Tween(0.5f, 0.4f, false);
		SingletoneBehaviour<ConsoleController>.Instance.BlackFade(0.5f, true, 0.6f);
		SingletoneBehaviour<MailManager>.Instance.FadeBlack(true, 0.5f);
		SingletoneBehaviour<GlitchManager>.Instance.Vignette_Switch(true);
		SingletoneBehaviour<GlitchManager>.Instance.ChangeVignette_Color("Black");
		this.vignetteVol_originValue = SingletoneBehaviour<GlitchManager>.Instance.vignette.intensity.value;
		SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0.5f, 0.7f, false);
		SingletoneBehaviour<GlitchManager>.Instance.GetFilmGrain();
		this.flimGrain_intensity_originValue = SingletoneBehaviour<GlitchManager>.Instance.filmGrain.intensity.value;
		SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.5f, 0.35f, false);
	}

	// Token: 0x06001D45 RID: 7493 RVA: 0x000D5FB8 File Offset: 0x000D41B8
	private void MyDreamImage_Exit()
	{
		SoundManager.instance.BGM_ChangePitch(0.5f, this.origin_BGM_Pitch);
		SoundManager.instance.BGM_ChangeVolume_Tween(0.5f, this.origin_BGM_Volume, false);
		SingletoneBehaviour<ConsoleController>.Instance.BlackFade(0.5f, false, 0.6f);
		SingletoneBehaviour<MailManager>.Instance.FadeBlack(false, 0.5f);
		SingletoneBehaviour<GlitchManager>.Instance.TweenVignetteVol_Intensity(0.5f, this.vignetteVol_originValue, false);
		SingletoneBehaviour<GlitchManager>.Instance.TweenFilmGrainVol_Intensity(0.5f, this.flimGrain_intensity_originValue, false);
	}

	// Token: 0x06001D46 RID: 7494 RVA: 0x000D6044 File Offset: 0x000D4244
	private void DancingFix()
	{
		SingletoneBehaviour<ConsoleController>.Instance.BlackFade(0.5f, true, 0.6f);
		SingletoneBehaviour<MailManager>.Instance.FadeBlack(true, 0.5f);
		this.origin_BGM_Volume = SoundManager.instance.bgmPlayer.volume;
		SoundManager.instance.BGM_ChangeVolume_Tween(2f, 0f, false);
		this.audioSource_Sound = SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.WinionVirusRemix, false, 0f, 1f);
		SoundManager.instance.SfxSoundTween(this.audioSource_Sound, 1f, 2f, true, false);
	}

	// Token: 0x06001D47 RID: 7495 RVA: 0x000D60DC File Offset: 0x000D42DC
	private void DancingFix_Exit()
	{
		SingletoneBehaviour<ConsoleController>.Instance.BlackFade(0.5f, false, 0.6f);
		SingletoneBehaviour<MailManager>.Instance.FadeBlack(false, 0.5f);
		SoundManager.instance.BGM_ChangeVolume_Tween(0.5f, this.origin_BGM_Volume, false);
		this.audioSource_Sound.Stop();
		this.audioSource_Sound.clip = null;
		this.audioSource_Sound.volume = 1f;
	}

	// Token: 0x06001D48 RID: 7496 RVA: 0x0001B0B6 File Offset: 0x000192B6
	private void MySelfie()
	{
		SingletoneBehaviour<ConsoleController>.Instance.BlackFade(0.5f, true, 0.6f);
		SingletoneBehaviour<MailManager>.Instance.FadeBlack(true, 0.5f);
	}

	// Token: 0x06001D49 RID: 7497 RVA: 0x0001B0DD File Offset: 0x000192DD
	private void MySelfie_Exit()
	{
		SingletoneBehaviour<ConsoleController>.Instance.BlackFade(0.5f, false, 0.6f);
		SingletoneBehaviour<MailManager>.Instance.FadeBlack(false, 0.5f);
	}

	// Token: 0x06001D4A RID: 7498 RVA: 0x0001B0B6 File Offset: 0x000192B6
	private void AHappyNap()
	{
		SingletoneBehaviour<ConsoleController>.Instance.BlackFade(0.5f, true, 0.6f);
		SingletoneBehaviour<MailManager>.Instance.FadeBlack(true, 0.5f);
	}

	// Token: 0x06001D4B RID: 7499 RVA: 0x0001B0DD File Offset: 0x000192DD
	private void AHappyNap_Exit()
	{
		SingletoneBehaviour<ConsoleController>.Instance.BlackFade(0.5f, false, 0.6f);
		SingletoneBehaviour<MailManager>.Instance.FadeBlack(false, 0.5f);
	}

	// Token: 0x06001D4C RID: 7500 RVA: 0x0001B0B6 File Offset: 0x000192B6
	private void IonAndGrid()
	{
		SingletoneBehaviour<ConsoleController>.Instance.BlackFade(0.5f, true, 0.6f);
		SingletoneBehaviour<MailManager>.Instance.FadeBlack(true, 0.5f);
	}

	// Token: 0x06001D4D RID: 7501 RVA: 0x0001B0DD File Offset: 0x000192DD
	private void IonAndGrid_Exit()
	{
		SingletoneBehaviour<ConsoleController>.Instance.BlackFade(0.5f, false, 0.6f);
		SingletoneBehaviour<MailManager>.Instance.FadeBlack(false, 0.5f);
	}

	// Token: 0x06001D4E RID: 7502 RVA: 0x0001B0B6 File Offset: 0x000192B6
	private void PreciousFriends()
	{
		SingletoneBehaviour<ConsoleController>.Instance.BlackFade(0.5f, true, 0.6f);
		SingletoneBehaviour<MailManager>.Instance.FadeBlack(true, 0.5f);
	}

	// Token: 0x06001D4F RID: 7503 RVA: 0x0001B0DD File Offset: 0x000192DD
	private void PreciousFriends_Exit()
	{
		SingletoneBehaviour<ConsoleController>.Instance.BlackFade(0.5f, false, 0.6f);
		SingletoneBehaviour<MailManager>.Instance.FadeBlack(false, 0.5f);
	}

	// Token: 0x06001D50 RID: 7504 RVA: 0x0001B104 File Offset: 0x00019304
	private void GroupSelfie()
	{
		SingletoneBehaviour<ConsoleController>.Instance.BlackFade(0.5f, true, 0.6f);
		SoundManager.instance.BGM_ChangePitch(0.5f, 0.75f);
		SingletoneBehaviour<MailManager>.Instance.FadeBlack(true, 0.5f);
	}

	// Token: 0x06001D51 RID: 7505 RVA: 0x0001B13F File Offset: 0x0001933F
	private void GroupSelfie_Exit()
	{
		SingletoneBehaviour<ConsoleController>.Instance.BlackFade(0.5f, false, 0.6f);
		SoundManager.instance.BGM_ChangePitch(0.5f, 1f);
		SingletoneBehaviour<MailManager>.Instance.FadeBlack(false, 0.5f);
	}

	// Token: 0x06001D52 RID: 7506 RVA: 0x000D614C File Offset: 0x000D434C
	private void OnDisable()
	{
		if (this.functionName == "")
		{
			this.functionName = base.gameObject.name;
		}
		string text = this.functionName;
		uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
		if (num <= 590082347U)
		{
			if (num <= 453590742U)
			{
				if (num != 159804029U)
				{
					if (num != 453590742U)
					{
						return;
					}
					if (!(text == "IonAndGrid"))
					{
						return;
					}
					this.IonAndGrid_Exit();
					return;
				}
				else
				{
					if (!(text == "AHappyNap"))
					{
						return;
					}
					this.AHappyNap_Exit();
					return;
				}
			}
			else if (num != 510655808U)
			{
				if (num != 590082347U)
				{
					return;
				}
				if (!(text == "DeliciousLunchImage"))
				{
					return;
				}
				this.DeliciousLunchImage_Exit();
				return;
			}
			else
			{
				if (!(text == "GroupSelfie"))
				{
					return;
				}
				this.GroupSelfie_Exit();
				return;
			}
		}
		else if (num <= 3203960475U)
		{
			if (num != 1925535887U)
			{
				if (num != 3203960475U)
				{
					return;
				}
				if (!(text == "MySelfie"))
				{
					return;
				}
				this.MySelfie_Exit();
				return;
			}
			else
			{
				if (!(text == "MyDreamImage"))
				{
					return;
				}
				this.MyDreamImage_Exit();
				return;
			}
		}
		else if (num != 3604154240U)
		{
			if (num != 3830542554U)
			{
				return;
			}
			if (!(text == "DancingFix"))
			{
				return;
			}
			this.DancingFix_Exit();
			return;
		}
		else
		{
			if (!(text == "PreciousFriends"))
			{
				return;
			}
			this.PreciousFriends_Exit();
			return;
		}
	}

	// Token: 0x04001B4A RID: 6986
	public string functionName = "";

	// Token: 0x04001B4B RID: 6987
	public float origin_BGM_Volume;

	// Token: 0x04001B4C RID: 6988
	public float origin_BGM_Pitch;

	// Token: 0x04001B4D RID: 6989
	public float vignetteVol_originValue;

	// Token: 0x04001B4E RID: 6990
	public float flimGrain_intensity_originValue;

	// Token: 0x04001B4F RID: 6991
	public float chromaticAberration_intensity_originValue;

	// Token: 0x04001B50 RID: 6992
	public float vhs_weight_originValue;

	// Token: 0x04001B51 RID: 6993
	private AudioSource audioSource_Sound;
}
