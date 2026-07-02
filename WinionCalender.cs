using System;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;

// Token: 0x0200043D RID: 1085
[Serializable]
public class WinionCalender : SingletoneBehaviour<WinionCalender>
{
	// Token: 0x06001ED9 RID: 7897 RVA: 0x0001BEE3 File Offset: 0x0001A0E3
	public void SetTitle(string title = "")
	{
		this.loadSavedTitle = true;
		this.savedTitle = title;
	}

	// Token: 0x06001EDA RID: 7898 RVA: 0x0001BEF3 File Offset: 0x0001A0F3
	public IEnumerator SetTitle(string title = "", string subtitle = "")
	{
		if (this.tweenActive)
		{
			yield break;
		}
		this.currentDay++;
		this.tweenActive = true;
		this.titleTMP.text = (this.loadSavedTitle ? this.savedTitle : title);
		this.titleTMP.transform.localScale = Vector3.one * 1.4f;
		this.subTitleTMP.text = "";
		this.loadSavedTitle = false;
		this.savedTitle = "";
		yield return null;
		DBManager.instance.NextDay = true;
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(this.fadeDuration, 0f, null, null, 1f);
		yield return new WaitForSeconds(this.fadeDuration * 2f);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeOut(this.fadeDuration, 0f, null, this.titleTMP.GetComponent<CanvasGroup>(), 1f);
		this.AutoSaveGroup.DOFade(1f, 0.5f);
		this.CircleGroup.DOFade(1f, 0.5f);
		TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore = this.titleTMP.DOScale(1.2f, this.fadeDuration);
		yield return TweenExtensions.WaitForCompletion(tweenerCore);
		if (this.fadeAction != null)
		{
			this.fadeActionEnd = false;
		}
		else
		{
			this.fadeActionEnd = true;
		}
		Action action = this.fadeEventAction;
		if (action != null)
		{
			action();
		}
		Action action2 = this.fadeAction;
		if (action2 != null)
		{
			action2();
		}
		yield return TweenExtensions.WaitForCompletion(this.subTitleTMP.DOText(subtitle, this.speed, true, ScrambleMode.None, null).SetSpeedBased<TweenerCore<string, string, StringOptions>>());
		yield return new WaitForSeconds(3f);
		yield return new WaitUntil(() => this.fadeActionEnd);
		this.AutoSaveGroup.DOFade(0f, 0.5f);
		this.CircleGroup.DOFade(0f, 0.5f);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(this.fadeDuration, 0f, null, this.titleTMP.GetComponent<CanvasGroup>());
		TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore2 = this.titleTMP.DOScale(1.4f, this.fadeDuration);
		yield return TweenExtensions.WaitForCompletion(tweenerCore2);
		SingletoneBehaviour<FadeInAndOut>.Instance.FadeIn(this.fadeDuration, 0f, null, null);
		yield return new WaitUntil(() => !SingletoneBehaviour<FadeInAndOut>.Instance.isBlack);
		yield return null;
		this.tweenActive = false;
		DBManager.instance.NextDay = false;
		if (!this.doubleLock_NoBackLog)
		{
			DBManager.instance.NoBacklogOpen_False();
		}
		yield break;
	}

	// Token: 0x06001EDB RID: 7899 RVA: 0x0001BF10 File Offset: 0x0001A110
	public void NextDay(string title = "", string subtitle = "")
	{
		DBManager.instance.dialogueData.NoBacklogOpen = true;
		base.StartCoroutine(this.SetTitle(title, subtitle));
	}

	// Token: 0x04001D24 RID: 7460
	[Space]
	[Header("전원 버튼")]
	public PowerBtn powerBtn;

	// Token: 0x04001D25 RID: 7461
	[Space]
	public int currentDay;

	// Token: 0x04001D26 RID: 7462
	public bool forDebug;

	// Token: 0x04001D27 RID: 7463
	public Action fadeEventAction;

	// Token: 0x04001D28 RID: 7464
	public Action fadeAction;

	// Token: 0x04001D29 RID: 7465
	public bool fadeActionEnd;

	// Token: 0x04001D2A RID: 7466
	[Space]
	[Header("================================")]
	[Space]
	[Space]
	[SerializeField]
	private string testTitle;

	// Token: 0x04001D2B RID: 7467
	[SerializeField]
	private string testSubTitle;

	// Token: 0x04001D2C RID: 7468
	[SerializeField]
	private TextMeshProUGUI titleTMP;

	// Token: 0x04001D2D RID: 7469
	[SerializeField]
	private TextMeshProUGUI subTitleTMP;

	// Token: 0x04001D2E RID: 7470
	[SerializeField]
	private float speed = 10f;

	// Token: 0x04001D2F RID: 7471
	[SerializeField]
	private float fadeDuration = 1f;

	// Token: 0x04001D30 RID: 7472
	public bool tweenActive;

	// Token: 0x04001D31 RID: 7473
	private bool loadSavedTitle;

	// Token: 0x04001D32 RID: 7474
	private string savedTitle = "";

	// Token: 0x04001D33 RID: 7475
	[SerializeField]
	private CanvasGroup AutoSaveGroup;

	// Token: 0x04001D34 RID: 7476
	[SerializeField]
	private CanvasGroup CircleGroup;

	// Token: 0x04001D35 RID: 7477
	public bool doubleLock_NoBackLog;
}
