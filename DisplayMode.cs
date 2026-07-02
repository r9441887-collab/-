using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000389 RID: 905
public class DisplayMode : MonoBehaviour
{
	// Token: 0x06001AE8 RID: 6888 RVA: 0x000CB380 File Offset: 0x000C9580
	private void Start()
	{
		if (Screen.fullScreenMode == 1)
		{
			this.fullScreen_toggle.isOn = true;
			this.windowedScreen_toggle.isOn = false;
			DisplayMode.curScreenMode = ScreenMode.FullScreenWindow;
			return;
		}
		this.fullScreen_toggle.isOn = false;
		this.windowedScreen_toggle.isOn = true;
		DisplayMode.curScreenMode = ScreenMode.Window;
	}

	// Token: 0x06001AE9 RID: 6889 RVA: 0x000CB3D4 File Offset: 0x000C95D4
	private void Update()
	{
		if (!this.isHorrorScene && !this.isTitle && this.completeBlack_anim && this.uiWindow.open && !this.isStopTime)
		{
			this.curTime = Time.timeScale;
			Time.timeScale = 0f;
			this.isStopTime = true;
			this.completeBlack_anim = false;
			this.uiWindow.DisableAction = delegate
			{
				Time.timeScale = this.curTime;
				this.curTime = 0f;
				this.isStopTime = false;
				if (this.isTitle)
				{
					this.black.EndTween(0.5f).OnComplete(delegate
					{
						DBManager.instance.dialogueData.OpenBackLog = false;
						this.black.gameObject.SetActive(false);
					});
				}
				else
				{
					this.black_CanvasGroup.alpha = 1f;
					this.black_CanvasGroup.DOFade(0f, 0.5f).OnComplete(delegate
					{
						this.black_obj.SetActive(false);
						DBManager.instance.dialogueData.OpenBackLog = false;
					});
				}
				this.uiWindow.DisableAction = null;
				DBManager.instance.NoBacklogOpen_False();
				this.uiWindow.DisableAction = null;
			};
		}
	}

	// Token: 0x06001AEA RID: 6890 RVA: 0x000CB448 File Offset: 0x000C9648
	private void OnEnable()
	{
		DisplayMode.IsDisplayModeActive = true;
		if (Screen.fullScreenMode == 1)
		{
			this.fullScreen_toggle.isOn = true;
			this.windowedScreen_toggle.isOn = false;
			DisplayMode.curScreenMode = ScreenMode.FullScreenWindow;
		}
		else
		{
			this.fullScreen_toggle.isOn = false;
			this.windowedScreen_toggle.isOn = true;
			DisplayMode.curScreenMode = ScreenMode.Window;
		}
		if (!this.isHorrorScene)
		{
			if (this.isTitle)
			{
				this.black.StartTween(0.5f).OnComplete(delegate
				{
					this.completeBlack_anim = true;
				});
				return;
			}
			this.black_obj.SetActive(true);
			this.black_CanvasGroup.alpha = 0f;
			this.black_CanvasGroup.DOFade(1f, 0.5f).OnComplete(delegate
			{
				this.completeBlack_anim = true;
			});
		}
	}

	// Token: 0x06001AEB RID: 6891 RVA: 0x000CB518 File Offset: 0x000C9718
	private void OnDisable()
	{
		DisplayMode.IsDisplayModeActive = false;
		if (this.isTitle)
		{
			this.black.EndTween(0.5f).OnComplete(delegate
			{
				this.completeBlack_anim = false;
				this.black.gameObject.SetActive(false);
			});
			return;
		}
		this.black_CanvasGroup.alpha = 1f;
		this.black_CanvasGroup.DOFade(0f, 0.5f).OnComplete(delegate
		{
			this.completeBlack_anim = true;
			this.black_obj.SetActive(false);
			DBManager.instance.dialogueData.OpenBackLog = false;
		});
	}

	// Token: 0x06001AEC RID: 6892 RVA: 0x0001953E File Offset: 0x0001773E
	public void Check_fullScreen_toggle()
	{
		if (this.fullScreen_toggle.isOn)
		{
			this.fullScreen_toggle.isOn = true;
			this.windowedScreen_toggle.isOn = false;
			return;
		}
		this.fullScreen_toggle.isOn = false;
		this.windowedScreen_toggle.isOn = true;
	}

	// Token: 0x06001AED RID: 6893 RVA: 0x0001957E File Offset: 0x0001777E
	public void Check_WindowedMode_toggle()
	{
		if (this.windowedScreen_toggle.isOn)
		{
			this.fullScreen_toggle.isOn = false;
			this.windowedScreen_toggle.isOn = true;
			return;
		}
		this.fullScreen_toggle.isOn = true;
		this.windowedScreen_toggle.isOn = false;
	}

	// Token: 0x06001AEE RID: 6894 RVA: 0x000CB590 File Offset: 0x000C9790
	public void CheckBtn()
	{
		if (this.fullScreen_toggle.isOn && !this.windowedScreen_toggle.isOn)
		{
			if (DisplayMode.curScreenMode != ScreenMode.FullScreenWindow)
			{
				DisplayMode.curScreenMode = ScreenMode.FullScreenWindow;
				Screen.fullScreenMode = 1;
				Screen.SetResolution(2560, 1440, 1);
			}
			base.gameObject.SetActive(false);
			return;
		}
		if (!this.fullScreen_toggle.isOn && this.windowedScreen_toggle.isOn)
		{
			if (DisplayMode.curScreenMode != ScreenMode.Window)
			{
				DisplayMode.curScreenMode = ScreenMode.Window;
				Screen.fullScreenMode = 3;
				Screen.SetResolution(2560, 1440, 3);
			}
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x040017FD RID: 6141
	public static ScreenMode curScreenMode;

	// Token: 0x040017FE RID: 6142
	public static bool IsDisplayModeActive;

	// Token: 0x040017FF RID: 6143
	public Toggle fullScreen_toggle;

	// Token: 0x04001800 RID: 6144
	public Toggle windowedScreen_toggle;

	// Token: 0x04001801 RID: 6145
	public UIWindow uiWindow;

	// Token: 0x04001802 RID: 6146
	private bool isStopTime;

	// Token: 0x04001803 RID: 6147
	private float curTime;

	// Token: 0x04001804 RID: 6148
	private bool completeBlack_anim;

	// Token: 0x04001805 RID: 6149
	public bool isHorrorScene;

	// Token: 0x04001806 RID: 6150
	public bool isTitle;

	// Token: 0x04001807 RID: 6151
	public BlackPanelTween black;

	// Token: 0x04001808 RID: 6152
	public GameObject black_obj;

	// Token: 0x04001809 RID: 6153
	public CanvasGroup black_CanvasGroup;
}
