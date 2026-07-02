using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000132 RID: 306
[Serializable]
public class DanceObject
{
	// Token: 0x06000751 RID: 1873 RVA: 0x0003EB4C File Offset: 0x0003CD4C
	public void PunchScaleEoni(Vector3 position)
	{
		int num = this.popCount;
		this.popCount = num + 1;
		if (num >= 5)
		{
			this.popCount = 0;
			this.Eoni.localPosition = position;
		}
		this.EoniAnimation.DORestart();
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x0003EB8C File Offset: 0x0003CD8C
	public void StartMusic()
	{
		if (this.MusicStart)
		{
			return;
		}
		DesktopController.PlayingDesktopAction = true;
		this.MusicStart = true;
		SoundManager.Instance.Play_BGM(SoundManager.BGM.ClubBGM, false, 0f);
		SoundManager.Instance.bgmPlayer.DOFade(1f, 0.5f);
		ShortcutExtensions.DOLocalMoveY(this.MirrorBall.transform, -400f, 2f, false).SetEase(Ease.Linear).SetRelative(true);
		ShortcutExtensions.DOLocalMove(this.LeftSpeaker.transform, new Vector3(400f, 400f, 0f), 2f, false).SetEase(Ease.Linear).SetRelative(true);
		ShortcutExtensions.DOLocalMove(this.RightSpeaker.transform, new Vector3(-400f, 400f, 0f), 2f, false).SetEase(Ease.Linear).SetRelative(true);
		ShortcutExtensions.DOLocalMove(this.Eoni.transform, new Vector3(0f, 600f, 0f), 2f, false).SetEase(Ease.Linear).SetRelative(true);
		this.DanceLight.DOFade(1f, 2f);
		DOVirtual.Float(0f, 1f, 15f, null).OnComplete(delegate
		{
			this.StopMusic();
		});
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x0003ECE8 File Offset: 0x0003CEE8
	public void StopMusic()
	{
		if (!this.MusicStart)
		{
			return;
		}
		SoundManager.Instance.bgmPlayer.DOFade(0f, 0.5f).OnComplete(delegate
		{
			SoundManager.Instance.bgmPlayer.Stop();
		});
		List<WinionHandler> handlers = GameManager.instance.GetWinionHandlers();
		for (int i = 0; i < handlers.Count; i++)
		{
			int index = i;
			float num = Random.Range(1f, 2f);
			DOVirtual.Float(0f, 1f, num, null).OnComplete(delegate
			{
				handlers[index].winionBehaviour.RemoveComponent<WinionDance>();
				handlers[index].winionBehaviour.RemoveComponent<WinionGetBackStage>();
				handlers[index].winionBehaviour.StopDesktopAction();
				handlers[index].ChangeCharacterState(CharacterState.None);
				handlers[index].winionBehaviour.isBusy = false;
				handlers[index].winionDragAndDrop.canDragAndDrop = true;
				handlers[index].winionMouseEvent.canMouseEnter = true;
				handlers[index].winionAnimator.canChangeAnimation = true;
				handlers[index].winionAnimator.SetLoop(false);
				handlers[index].winionBehaviour.CanArriveAction = true;
				handlers[index].winionBehaviour.SetCanInterrupt(true);
				handlers[index].SetIdleByWinionStatus();
				handlers[index].winionMovement.SetActiveMovement(true, false, false);
				handlers[index].winionMovement.MoveToRandomPosition();
				handlers[index].winionBehaviour.ArriveAction();
				SingletoneBehaviour<DesktopController>.Instance.winionRoomSettings.RoomSetting(handlers[index], handlers[index].whichFolder);
			});
		}
		DesktopController.PlayingDesktopAction = false;
		this.MusicStart = false;
		ShortcutExtensions.DOLocalMoveY(this.MirrorBall.transform, 400f, 2f, false).SetEase(Ease.Linear).SetRelative(true);
		ShortcutExtensions.DOLocalMove(this.LeftSpeaker.transform, new Vector3(-400f, -400f, 0f), 2f, false).SetEase(Ease.Linear).SetRelative(true);
		ShortcutExtensions.DOLocalMove(this.RightSpeaker.transform, new Vector3(400f, -400f, 0f), 2f, false).SetEase(Ease.Linear).SetRelative(true);
		ShortcutExtensions.DOLocalMove(this.Eoni.transform, new Vector3(0f, -600f, 0f), 2f, false).SetEase(Ease.Linear).SetRelative(true);
		this.DanceLight.DOFade(0f, 2f).OnComplete(delegate
		{
			this.SetOriginPosition();
		});
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x0003EEB8 File Offset: 0x0003D0B8
	public void GetOriginPosition()
	{
		this.MirrorBallOriginalPosition = this.MirrorBall.GetComponent<RectTransform>().anchoredPosition;
		this.LeftSpeakerOriginalPosition = this.LeftSpeaker.GetComponent<RectTransform>().anchoredPosition;
		this.RightSpeakerOriginalPosition = this.RightSpeaker.GetComponent<RectTransform>().anchoredPosition;
		this.EoniOriginalPosition = this.Eoni.GetComponent<RectTransform>().anchoredPosition;
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x0003EF34 File Offset: 0x0003D134
	public void SetOriginPosition()
	{
		this.DanceLight.alpha = 0f;
		this.MirrorBall.GetComponent<RectTransform>().anchoredPosition = this.MirrorBallOriginalPosition;
		this.LeftSpeaker.GetComponent<RectTransform>().anchoredPosition = this.LeftSpeakerOriginalPosition;
		this.RightSpeaker.GetComponent<RectTransform>().anchoredPosition = this.RightSpeakerOriginalPosition;
		this.Eoni.GetComponent<RectTransform>().anchoredPosition = this.EoniOriginalPosition;
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x00012D73 File Offset: 0x00010F73
	public void SetColor()
	{
		this.DanceLightColor.color = this.spriteColor;
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x0003EFC0 File Offset: 0x0003D1C0
	public void GetColor()
	{
		this.spriteColor = new Color32(180, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		Sequence sequence = DOTween.Sequence();
		sequence.Append(DOVirtual.Float(1f, 0f, this.colorSpeed, delegate(float f)
		{
			this.spriteColor.g = (180f + 60f * f) / 255f;
		}));
		sequence.Append(DOVirtual.Float(0f, 1f, this.colorSpeed, delegate(float f)
		{
			this.spriteColor.r = (180f + 60f * f) / 255f;
		}));
		sequence.Append(DOVirtual.Float(1f, 0f, this.colorSpeed, delegate(float f)
		{
			this.spriteColor.b = (180f + 60f * f) / 255f;
		}));
		sequence.Append(DOVirtual.Float(0f, 1f, this.colorSpeed, delegate(float f)
		{
			this.spriteColor.g = (180f + 60f * f) / 255f;
		}));
		sequence.Append(DOVirtual.Float(1f, 0f, this.colorSpeed, delegate(float f)
		{
			this.spriteColor.r = (180f + 60f * f) / 255f;
		}));
		sequence.Append(DOVirtual.Float(0f, 1f, this.colorSpeed, delegate(float f)
		{
			this.spriteColor.b = (180f + 60f * f) / 255f;
		}));
		sequence.SetLoops(-1, LoopType.Restart);
		sequence.Play<Sequence>();
	}

	// Token: 0x0400083F RID: 2111
	public bool MusicStart;

	// Token: 0x04000840 RID: 2112
	public RectTransform Eoni;

	// Token: 0x04000841 RID: 2113
	public DOTweenAnimation EoniAnimation;

	// Token: 0x04000842 RID: 2114
	public List<Transform> DancePositions;

	// Token: 0x04000843 RID: 2115
	private int popCount;

	// Token: 0x04000844 RID: 2116
	[Header("댄스 오브젝트")]
	public GameObject DanceObjectRoot;

	// Token: 0x04000845 RID: 2117
	public CanvasGroup DanceLight;

	// Token: 0x04000846 RID: 2118
	public Image DanceLightColor;

	// Token: 0x04000847 RID: 2119
	[Header("색깔 변하는 속도")]
	public float colorSpeed = 1f;

	// Token: 0x04000848 RID: 2120
	public Color spriteColor = new Color32(180, byte.MaxValue, byte.MaxValue, byte.MaxValue);

	// Token: 0x04000849 RID: 2121
	[Space(10f)]
	public GameObject MirrorBall;

	// Token: 0x0400084A RID: 2122
	public GameObject LeftSpeaker;

	// Token: 0x0400084B RID: 2123
	public GameObject RightSpeaker;

	// Token: 0x0400084C RID: 2124
	private Vector3 MirrorBallOriginalPosition;

	// Token: 0x0400084D RID: 2125
	private Vector3 LeftSpeakerOriginalPosition;

	// Token: 0x0400084E RID: 2126
	private Vector3 RightSpeakerOriginalPosition;

	// Token: 0x0400084F RID: 2127
	private Vector3 EoniOriginalPosition;
}
