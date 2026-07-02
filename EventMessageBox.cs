using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000371 RID: 881
public class EventMessageBox : MonoBehaviour
{
	// Token: 0x06001A84 RID: 6788 RVA: 0x000C3488 File Offset: 0x000C1688
	public void Setting()
	{
		switch (this.type)
		{
		case EventMessageBox.EventMessageType.WinionRegistrationError:
			this.titleText.text = DBManager.instance.GetSettingString("메세지박스", 0, 0, 0);
			this.contextText.text = DBManager.instance.GetSettingString("c1_e6_0", 0, 0, 1);
			this.OKBtn_text.text = DBManager.instance.GetSettingString("비밀번호", 0, 3, 0);
			return;
		case EventMessageBox.EventMessageType.WinionFixError:
			this.titleText.text = DBManager.instance.GetSettingString("메세지박스", 0, 0, 0);
			this.contextText.text = DBManager.instance.GetSettingString("c2_e4_1", 0, 0, 1);
			this.OKBtn_text.text = DBManager.instance.GetSettingString("비밀번호", 0, 3, 0);
			return;
		case EventMessageBox.EventMessageType.WinionIONError:
			this.titleText.text = DBManager.instance.GetSettingString("c2_e4_1", 0, 1, 1);
			this.contextText.text = DBManager.instance.GetSettingString("c2_e4_1", 0, 2, 1);
			this.OKBtn_text.text = DBManager.instance.GetSettingString("비밀번호", 0, 3, 0);
			return;
		case EventMessageBox.EventMessageType.VirusError:
			this.titleText.text = DBManager.instance.GetSettingString("c2_e4_1", 0, 3, 1);
			this.contextText.text = DBManager.instance.GetSettingString("c2_e4_1", 0, 0, 1);
			this.OKBtn_text.text = DBManager.instance.GetSettingString("비밀번호", 0, 3, 0);
			return;
		case EventMessageBox.EventMessageType.BizitError:
			this.titleText.text = DBManager.instance.GetSettingString("c2_e4_1", 0, 4, 1);
			this.contextText.text = DBManager.instance.GetSettingString("c2_e4_1", 0, 5, 1);
			this.OKBtn_text.text = DBManager.instance.GetSettingString("비밀번호", 0, 3, 0);
			return;
		case EventMessageBox.EventMessageType.IONSystemError:
			this.titleText.text = DBManager.instance.GetSettingString("메세지박스", 0, 0, 0);
			this.contextText.text = DBManager.instance.GetSettingString("c2_e4_1", 0, 6, 1);
			this.OKBtn_text.text = DBManager.instance.GetSettingString("비밀번호", 0, 3, 0);
			return;
		case EventMessageBox.EventMessageType.Tutorial:
		case EventMessageBox.EventMessageType.IONSystemError_02:
			break;
		case EventMessageBox.EventMessageType.SystemError:
			this.titleText.text = DBManager.instance.GetSettingString("메세지박스", 0, 0, 0);
			this.contextText.text = DBManager.instance.GetSettingString("메세지박스", 0, 1, 0);
			this.OKBtn_text.text = DBManager.instance.GetSettingString("비밀번호", 0, 4, 0);
			break;
		default:
			return;
		}
	}

	// Token: 0x06001A85 RID: 6789 RVA: 0x000C3730 File Offset: 0x000C1930
	public void StartEnableRoutine()
	{
		this.Setting();
		this.isDestroy = false;
		SoundManager.instance.Play_SfxSound_2(this.MessageType, false, 0.6f, 1f);
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
				});
			}
			tweener.OnComplete(tweenCallback2);
		});
	}

	// Token: 0x06001A86 RID: 6790 RVA: 0x000C37D0 File Offset: 0x000C19D0
	public void DestroyBox()
	{
		if (this.isDestroy)
		{
			return;
		}
		SoundManager.instance.Play_SfxSound(SoundManager.SfxSound.ClickSound, false, 1f, 1f);
		this.isDestroy = true;
		Vector3 _localScale = base.transform.localScale;
		TweenCallback<float> <>9__2;
		TweenCallback <>9__3;
		DOVirtual.Float(_localScale.x, _localScale.x * 1.03f, 0.15f, delegate(float value)
		{
			this.transform.localScale = Vector3.one * value;
		}).SetEase(Ease.OutExpo).SetLoops(2, LoopType.Yoyo)
			.OnComplete(delegate
			{
				float num = _localScale.x * 1.03f;
				float num2 = 0f;
				float num3 = 0.1f;
				TweenCallback<float> tweenCallback;
				if ((tweenCallback = <>9__2) == null)
				{
					tweenCallback = (<>9__2 = delegate(float value)
					{
						this.transform.localScale = Vector3.one * value;
					});
				}
				Tweener tweener = DOVirtual.Float(num, num2, num3, tweenCallback).SetEase(Ease.Linear);
				TweenCallback tweenCallback2;
				if ((tweenCallback2 = <>9__3) == null)
				{
					tweenCallback2 = (<>9__3 = delegate
					{
						this.transform.localScale = Vector3.one;
						if (this.destroyObj == null)
						{
							this.gameObject.SetActive(false);
							return;
						}
						this.destroyObj.SetActive(false);
					});
				}
				tweener.OnComplete(tweenCallback2);
			});
	}

	// Token: 0x040016AC RID: 5804
	public SoundManager.SfxSound_2 MessageType = SoundManager.SfxSound_2.ErrorPopUpMessage;

	// Token: 0x040016AD RID: 5805
	public TMP_Text titleText;

	// Token: 0x040016AE RID: 5806
	public TMP_Text contextText;

	// Token: 0x040016AF RID: 5807
	public Button OKBtn;

	// Token: 0x040016B0 RID: 5808
	public TMP_Text OKBtn_text;

	// Token: 0x040016B1 RID: 5809
	public GameObject destroyObj;

	// Token: 0x040016B2 RID: 5810
	private bool isDestroy;

	// Token: 0x040016B3 RID: 5811
	public EventMessageBox.EventMessageType type;

	// Token: 0x02000372 RID: 882
	public enum EventMessageType
	{
		// Token: 0x040016B5 RID: 5813
		WinionRegistrationError,
		// Token: 0x040016B6 RID: 5814
		WinionFixError,
		// Token: 0x040016B7 RID: 5815
		WinionIONError,
		// Token: 0x040016B8 RID: 5816
		VirusError,
		// Token: 0x040016B9 RID: 5817
		BizitError,
		// Token: 0x040016BA RID: 5818
		IONSystemError,
		// Token: 0x040016BB RID: 5819
		Tutorial,
		// Token: 0x040016BC RID: 5820
		IONSystemError_02,
		// Token: 0x040016BD RID: 5821
		SystemError
	}
}
