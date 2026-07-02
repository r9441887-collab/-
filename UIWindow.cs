using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020003E6 RID: 998
public class UIWindow : UIPositionMove
{
	// Token: 0x06001D15 RID: 7445 RVA: 0x0001A1B3 File Offset: 0x000183B3
	public void SetWindowPosition(Vector2 position)
	{
		base.GetComponent<RectTransform>().localPosition = position;
	}

	// Token: 0x06001D16 RID: 7446 RVA: 0x000D50DC File Offset: 0x000D32DC
	private void Awake()
	{
		if (this.getStartScale)
		{
			this._localScale = base.transform.localScale;
		}
		else
		{
			this._localScale = Vector3.one;
		}
		if (this.TitleBar != null)
		{
			if (this.TitleBar.GetComponent<Button>() == null)
			{
				this.TitleBar.AddComponent<Button>();
			}
			EventTrigger eventTrigger = this.TitleBar.GetComponent<EventTrigger>();
			if (eventTrigger == null)
			{
				eventTrigger = this.TitleBar.AddComponent<EventTrigger>();
			}
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = 2;
			entry.callback.AddListener(delegate(BaseEventData eventData)
			{
				this.GetMouseDownPosition();
			});
			EventTrigger.Entry entry2 = new EventTrigger.Entry();
			entry2.eventID = 5;
			entry2.callback.AddListener(delegate(BaseEventData eventData)
			{
				this.MouseMove();
			});
			EventTrigger.Entry entry3 = new EventTrigger.Entry();
			entry3.eventID = 3;
			entry3.callback.AddListener(delegate(BaseEventData eventData)
			{
				this.GetMouseUpPosition();
			});
			eventTrigger.triggers.Add(entry);
			eventTrigger.triggers.Add(entry2);
			eventTrigger.triggers.Add(entry3);
		}
		if (this.XButton != null)
		{
			this.XButton.tag = "TitleBar";
			EventTrigger eventTrigger2 = this.XButton.AddComponent<EventTrigger>();
			EventTrigger.Entry entry4 = new EventTrigger.Entry();
			entry4.eventID = 2;
			entry4.callback.AddListener(delegate(BaseEventData eventData)
			{
				if (this.CantClose)
				{
					SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.NegativeRetro, false, 1f, 1f);
					return;
				}
				SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.ClickSound, false, 1f, 1f);
				this.DestroyBox(false, true);
			});
			eventTrigger2.triggers.Add(entry4);
		}
	}

	// Token: 0x06001D17 RID: 7447 RVA: 0x0001AEC8 File Offset: 0x000190C8
	public virtual void CallbackEnableEnd()
	{
		this.isDoTween = false;
		Action enableAction = this.EnableAction;
		if (enableAction == null)
		{
			return;
		}
		enableAction();
	}

	// Token: 0x06001D18 RID: 7448 RVA: 0x0001AEE1 File Offset: 0x000190E1
	public virtual void CallbackDisableEnd()
	{
		this.isDestroy = false;
		Action lastCloseAction = this.LastCloseAction;
		if (lastCloseAction != null)
		{
			lastCloseAction();
		}
		if (this.setDisable)
		{
			base.gameObject.SetActive(false);
			return;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06001D19 RID: 7449 RVA: 0x0001AF1B File Offset: 0x0001911B
	private void OnEnable()
	{
		if (this.isDoTween)
		{
			return;
		}
		if (this.m_icon == Icon.DialogueOption && SingletoneBehaviour<IconManager>.Instance != null)
		{
			SingletoneBehaviour<IconManager>.Instance.CheckActive(this.m_icon, true);
		}
		base.StartCoroutine("EnableRoutine");
	}

	// Token: 0x06001D1A RID: 7450 RVA: 0x000D524C File Offset: 0x000D344C
	public void StartEnableRoutine()
	{
		this.open = false;
		Vector3 lastScale = base.transform.localScale;
		TweenCallback<float> <>9__2;
		TweenCallback <>9__3;
		DOVirtual.Float(lastScale.x, lastScale.x * 1.05f, 0.15f, delegate(float value)
		{
			this.transform.localScale = Vector3.one * value;
		}).SetEase(Ease.OutExpo).OnComplete(delegate
		{
			float num = lastScale.x * 1.05f;
			float x = lastScale.x;
			float num2 = 0.1f;
			TweenCallback<float> tweenCallback;
			if ((tweenCallback = <>9__2) == null)
			{
				tweenCallback = (<>9__2 = delegate(float value)
				{
					this.transform.localScale = Vector3.one * value;
				});
			}
			Tweener tweener = DOVirtual.Float(num, x, num2, tweenCallback).SetEase(Ease.Linear);
			TweenCallback tweenCallback2;
			if ((tweenCallback2 = <>9__3) == null)
			{
				tweenCallback2 = (<>9__3 = delegate
				{
					this.transform.localScale = lastScale;
					this.open = true;
				});
			}
			tweener.OnComplete(tweenCallback2);
		});
	}

	// Token: 0x06001D1B RID: 7451 RVA: 0x0001AF5A File Offset: 0x0001915A
	private IEnumerator EnableRoutine()
	{
		this.isDoTween = true;
		this.open = false;
		base.transform.localScale = this._localScale / 2f;
		DOVirtual.Float(this._localScale.x / 2f, this._localScale.x * 1.03f, 0.15f, delegate(float value)
		{
			base.transform.localScale = Vector3.one * value;
		}).SetEase(Ease.OutExpo).OnComplete(delegate
		{
			DOVirtual.Float(this._localScale.x * 1.03f, this._localScale.x, 0.1f, delegate(float value)
			{
				base.transform.localScale = Vector3.one * value;
			}).SetEase(Ease.Linear).OnComplete(delegate
			{
				this.isDoTween = false;
				this.open = true;
				this.CallbackEnableEnd();
			});
		});
		yield return null;
		yield break;
	}

	// Token: 0x06001D1C RID: 7452 RVA: 0x000D52CC File Offset: 0x000D34CC
	public void DestroyBox(bool closeForce = false, bool manualClose = false)
	{
		Action disableAction = this.DisableAction;
		if (disableAction != null)
		{
			disableAction();
		}
		if (closeForce)
		{
			this.CloseFolder();
			return;
		}
		if (this.CantCloseWhileEvent && GameManager.RunningFixMemory && manualClose)
		{
			SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.NegativeRetro, false, 1f, 1f);
			return;
		}
		Winion winion = Winion.None;
		if (this.m_icon == Icon.Folder_Ion)
		{
			winion = Winion.Ion;
		}
		else if (this.m_icon == Icon.Folder_Bo)
		{
			winion = Winion.Bo;
		}
		else if (this.m_icon == Icon.Folder_Grid)
		{
			winion = Winion.Grid;
		}
		else if (this.m_icon == Icon.Folder_Fix)
		{
			winion = Winion.Fix;
		}
		else if (this.m_icon == Icon.Folder_Debug)
		{
			winion = Winion.Debug;
		}
		if (winion != Winion.None && SingletoneBehaviour<WinionFolderManager>.Instance.WhoInThisFolder(winion).Count != 0)
		{
			List<WinionHandler> list = SingletoneBehaviour<WinionFolderManager>.Instance.WhoInThisFolder(winion);
			bool flag = false;
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].dialogue_ing)
				{
					flag = true;
					break;
				}
				if (list[i].winionStatus.winionInfo.winionType == DBManager.instance.dialogueData.curDialogue_Winion)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.NegativeRetro, false, 1f, 1f);
				return;
			}
		}
		if (this.isDestroy)
		{
			return;
		}
		this.isDestroy = true;
		DOVirtual.Float(this._localScale.x, this._localScale.x * 1.03f, 0.15f, delegate(float value)
		{
			base.transform.localScale = Vector3.one * value;
		}).SetEase(Ease.OutExpo).SetLoops(2, LoopType.Yoyo)
			.OnComplete(delegate
			{
				DOVirtual.Float(this._localScale.x * 1.03f, 0f, 0.1f, delegate(float value)
				{
					base.transform.localScale = Vector3.one * value;
				}).SetEase(Ease.Linear).OnComplete(delegate
				{
					this.CloseFolder();
				});
			});
	}

	// Token: 0x06001D1D RID: 7453 RVA: 0x000D545C File Offset: 0x000D365C
	public void CloseFolder()
	{
		this.CallbackDisableEnd();
		if (this.SetDefaultTransform)
		{
			SingletoneBehaviour<MouseRaycast>.Instance.SetSecondLayer(base.gameObject);
		}
		if (!this.SetDefaultTransform && this.ParentTransform != null)
		{
			base.transform.SetParent(this.ParentTransform);
		}
		base.transform.localScale = this._localScale;
		if (SingletoneBehaviour<IconManager>.Instance != null)
		{
			SingletoneBehaviour<IconManager>.Instance.CheckActive(this.m_icon, false);
		}
		this.open = false;
	}

	// Token: 0x04001B22 RID: 6946
	public Icon m_icon;

	// Token: 0x04001B23 RID: 6947
	public GameObject TitleBar;

	// Token: 0x04001B24 RID: 6948
	public GameObject XButton;

	// Token: 0x04001B25 RID: 6949
	public bool CantCloseWhileEvent;

	// Token: 0x04001B26 RID: 6950
	public bool CantClose;

	// Token: 0x04001B27 RID: 6951
	public bool open;

	// Token: 0x04001B28 RID: 6952
	public bool isDestroy;

	// Token: 0x04001B29 RID: 6953
	public bool isDoTween;

	// Token: 0x04001B2A RID: 6954
	public bool setDisable;

	// Token: 0x04001B2B RID: 6955
	public Ease ease = Ease.OutExpo;

	// Token: 0x04001B2C RID: 6956
	public float duration = 0.15f;

	// Token: 0x04001B2D RID: 6957
	public Vector3 _localScale;

	// Token: 0x04001B2E RID: 6958
	public bool SetDefaultTransform = true;

	// Token: 0x04001B2F RID: 6959
	public Transform ParentTransform;

	// Token: 0x04001B30 RID: 6960
	public bool getStartScale;

	// Token: 0x04001B31 RID: 6961
	public Action EnableAction;

	// Token: 0x04001B32 RID: 6962
	public Action DisableAction;

	// Token: 0x04001B33 RID: 6963
	public Action LastCloseAction;
}
