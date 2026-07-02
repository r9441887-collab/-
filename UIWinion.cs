using System;
using Coffee.UIEffects;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000BC RID: 188
public class UIWinion : MonoBehaviour
{
	// Token: 0x0600049E RID: 1182 RVA: 0x00030CF8 File Offset: 0x0002EEF8
	private void OnEnable()
	{
		if (this.UI_Element == null)
		{
			this.UI_Element = base.GetComponent<RectTransform>();
		}
		this.winionHandler.uiWinion = base.gameObject;
		this.winionHandler.uiWinionSpeechBubblePos = this.speechBubblePos;
		this.winionHandler.uiWinionSpeechBubblePos_Right = this.speechBubbleRightPos;
		this.winionHandler.uiWinionSpeechBubblePos_Left = this.speechBubbleLeftPos;
		Action onEnableAction = this.OnEnableAction;
		if (onEnableAction == null)
		{
			return;
		}
		onEnableAction();
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x00030D74 File Offset: 0x0002EF74
	private void Update()
	{
		float num = ((this.winionHandler.winionStatus.winionInfo.winionType == Winion.Bo) ? 1.27f : 1.2f);
		base.transform.localScale = new Vector3((float)(this.winionHandler.winionAnimator.isRight ? (-1) : 1), 1f, 1f) * num;
		this.bubbleParent.localScale = new Vector3((float)(this.winionHandler.winionAnimator.isRight ? (-1) : 1), 1f, 1f);
		if (this.SyncPosition)
		{
			this.UIWinionSyncPosition();
		}
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x00030E1C File Offset: 0x0002F01C
	public void SyncUIWinionToWorldWinion(bool value = false, float syncScale = 1f)
	{
		if (this.UI_Element == null)
		{
			this.UI_Element = base.GetComponent<RectTransform>();
			this.winionHandler.uiWinion = base.gameObject;
			this.winionHandler.uiWinionSpeechBubblePos = this.speechBubblePos;
			this.winionHandler.uiWinionSpeechBubblePos_Right = this.speechBubbleRightPos;
			this.winionHandler.uiWinionSpeechBubblePos_Left = this.speechBubbleLeftPos;
		}
		this.UI_Element.anchoredPosition = this.winionHandler.transform.position * syncScale;
		this.SyncPosition = value;
		this.SyncScale = syncScale;
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x00010FD7 File Offset: 0x0000F1D7
	public void UIWinionSyncPosition()
	{
		this.UI_Element.anchoredPosition = this.winionHandler.transform.position * this.SyncScale;
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x00030EBC File Offset: 0x0002F0BC
	public void UIWinionPosition()
	{
		float num = ((this.winionHandler.winionStatus.winionInfo.winionType == Winion.Bo) ? 1.27f : 1.2f);
		base.transform.localScale = new Vector3((float)(this.winionHandler.winionAnimator.isRight ? (-1) : 1), 1f, 1f) * num;
		this.bubbleParent.localScale = new Vector3((float)(this.winionHandler.winionAnimator.isRight ? (-1) : 1), 1f, 1f);
		if (this.UI_Element == null)
		{
			this.UI_Element = base.GetComponent<RectTransform>();
		}
		RectTransform component = ScreenCanvas.Instance.GetComponent<RectTransform>();
		Vector2 vector = Camera.main.WorldToViewportPoint(this.winionHandler.transform.position);
		Vector2 vector2;
		vector2..ctor(vector.x * component.sizeDelta.x - component.sizeDelta.x * 0.5f, vector.y * component.sizeDelta.y - component.sizeDelta.y * 0.5f);
		this.UI_Element.anchoredPosition = vector2;
	}

	// Token: 0x04000524 RID: 1316
	[Header("위니언 핸들러")]
	public WinionHandler winionHandler;

	// Token: 0x04000525 RID: 1317
	[Space]
	[Header("말풍선 위치")]
	public Transform bubbleParent;

	// Token: 0x04000526 RID: 1318
	public Transform speechBubblePos;

	// Token: 0x04000527 RID: 1319
	public Transform speechBubbleRightPos;

	// Token: 0x04000528 RID: 1320
	public Transform speechBubbleLeftPos;

	// Token: 0x04000529 RID: 1321
	public Image winionImg;

	// Token: 0x0400052A RID: 1322
	public UIGradient winionGradient;

	// Token: 0x0400052B RID: 1323
	public WinionFile winionFile;

	// Token: 0x0400052C RID: 1324
	public Vector2 position;

	// Token: 0x0400052D RID: 1325
	private RectTransform UI_Element;

	// Token: 0x0400052E RID: 1326
	public Action OnEnableAction;

	// Token: 0x0400052F RID: 1327
	public bool ChangedColor;

	// Token: 0x04000530 RID: 1328
	public bool SyncPosition;

	// Token: 0x04000531 RID: 1329
	public float SyncScale = 1f;
}
