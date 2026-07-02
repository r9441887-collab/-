using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x0200007E RID: 126
public class MazeWindowBug : MonoBehaviour
{
	// Token: 0x0600032E RID: 814 RVA: 0x00029B20 File Offset: 0x00027D20
	private void ResetLeg()
	{
		Transform parentPivot = this.ParentPivot;
		for (int i = 0; i < 17; i++)
		{
			if (i % 2 == 1)
			{
				Debug.Log(this.ParentPivot.GetChild(1).name);
				Transform child = this.ParentPivot.GetChild(1);
				Transform child2 = child.GetChild(0);
				Transform child3 = child2.GetChild(1).GetChild(0).GetChild(0);
				Transform child4 = child2.GetChild(1).GetChild(0).GetChild(1);
				child3.localRotation = Quaternion.Euler(new Vector3(-this.startAngle, 0f, 0f));
				child4.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
				ShortcutExtensions.DOLocalRotate(child3, new Vector3(-this.endAngle, 0f, 0f), this.tweenDuration, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo).SetDelay((float)i * this.delayed * 3f + this.delayed * 3f * (float)(i % 2))
					.SetEase(Ease.InOutQuad);
				ShortcutExtensions.DOLocalRotate(child4, new Vector3(this.endAngle, 0f, 0f), this.tweenDuration, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo).SetDelay((float)i * this.delayed + this.delayed * (float)(i % 2))
					.SetEase(Ease.InOutQuad);
				Transform child5 = child.GetChild(1);
				Transform child6 = child5.GetChild(1).GetChild(0).GetChild(0);
				Transform child7 = child5.GetChild(1).GetChild(0).GetChild(1);
				child6.localRotation = Quaternion.Euler(new Vector3(this.startAngle, 0f, 0f));
				child7.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
				ShortcutExtensions.DOLocalRotate(child6, new Vector3(this.endAngle, 0f, 0f), this.tweenDuration, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo).SetDelay((float)i * this.delayed * 3f + this.delayed * 3f * (float)(i % 2) + 0.23f)
					.SetEase(Ease.InOutQuad);
				ShortcutExtensions.DOLocalRotate(child7, new Vector3(-this.endAngle, 0f, 0f), this.tweenDuration, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo).SetDelay((float)i * this.delayed + this.delayed * (float)(i % 2) + 0.23f)
					.SetEase(Ease.InOutQuad);
			}
			this.ParentPivot = this.ParentPivot.GetChild(0);
		}
		this.ParentPivot = parentPivot;
	}

	// Token: 0x0600032F RID: 815 RVA: 0x000100C6 File Offset: 0x0000E2C6
	private void Awake()
	{
		this.ResetLeg();
	}

	// Token: 0x06000330 RID: 816 RVA: 0x000100CE File Offset: 0x0000E2CE
	private void Update()
	{
		base.transform.LookAt(this.TargetTransform);
		base.transform.Translate(Vector3.forward * (this.moveSpeed * Time.deltaTime), 1);
	}

	// Token: 0x0400035B RID: 859
	[SerializeField]
	private Transform TargetTransform;

	// Token: 0x0400035C RID: 860
	[Header("타겟과 거리")]
	[SerializeField]
	private float distance;

	// Token: 0x0400035D RID: 861
	[Header("도착 거리 임계값")]
	[SerializeField]
	private float diff = 1.5f;

	// Token: 0x0400035E RID: 862
	[Header("첫번째 패트롤 좌표")]
	[SerializeField]
	private List<Transform> Positions = new List<Transform>();

	// Token: 0x0400035F RID: 863
	[Header("다리 피봇")]
	[SerializeField]
	private Transform ParentPivot;

	// Token: 0x04000360 RID: 864
	public float startAngle = 40f;

	// Token: 0x04000361 RID: 865
	public float endAngle = 100f;

	// Token: 0x04000362 RID: 866
	public float tweenDuration = 0.4f;

	// Token: 0x04000363 RID: 867
	public float delayed = 0.05f;

	// Token: 0x04000364 RID: 868
	public bool destroyArrive;

	// Token: 0x04000365 RID: 869
	public float lookAtSpeed = 1f;

	// Token: 0x04000366 RID: 870
	public float moveSpeed = 1f;
}
