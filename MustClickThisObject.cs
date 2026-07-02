using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

// Token: 0x020003B2 RID: 946
public class MustClickThisObject : EnterAndExit
{
	// Token: 0x06001C0B RID: 7179 RVA: 0x000CFF74 File Offset: 0x000CE174
	private void Awake()
	{
		Object component = base.GetComponent<EventTrigger>();
		this.targetPosition = base.GetComponent<RectTransform>();
		if (component == null)
		{
			base.gameObject.AddComponent<EventTrigger>();
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = 0;
			entry.callback.AddListener(delegate(BaseEventData eventData)
			{
				this.OnMouseEnter();
			});
			base.GetComponent<EventTrigger>().triggers.Add(entry);
			EventTrigger.Entry entry2 = new EventTrigger.Entry();
			entry2.eventID = 1;
			entry2.callback.AddListener(delegate(BaseEventData eventData)
			{
				this.OnMouseEnter();
			});
			base.GetComponent<EventTrigger>().triggers.Add(entry2);
		}
	}

	// Token: 0x06001C0C RID: 7180 RVA: 0x000D0014 File Offset: 0x000CE214
	public void MouseLock(float Duration, Ease ease = Ease.Linear)
	{
		Vector3 mousePosition = Input.mousePosition;
		Vector3 position = this.targetPosition.position;
		this.tween = DOVirtual.Vector3(mousePosition, position, Duration, delegate(Vector3 x)
		{
			Mouse.current.WarpCursorPosition(x);
		}).SetEase(ease).OnComplete(delegate
		{
			this.mouse_Arrival = true;
		});
	}

	// Token: 0x06001C0D RID: 7181 RVA: 0x0001A410 File Offset: 0x00018610
	public void MouseUnlock()
	{
		this.tween.Kill(false);
	}

	// Token: 0x06001C0E RID: 7182 RVA: 0x0001A41E File Offset: 0x0001861E
	public override void OnMouseEnter()
	{
		this.MouseLock(this.duration, Ease.Linear);
	}

	// Token: 0x04001968 RID: 6504
	private RectTransform targetPosition;

	// Token: 0x04001969 RID: 6505
	public float duration = 0.5f;

	// Token: 0x0400196A RID: 6506
	private Tween tween;

	// Token: 0x0400196B RID: 6507
	public bool mouse_Arrival;
}
