using System;
using DG.Tweening;
using UnityEngine;

// Token: 0x020000A1 RID: 161
public class WindowBugFace : MonoBehaviour
{
	// Token: 0x0600040B RID: 1035 RVA: 0x0002CE1C File Offset: 0x0002B01C
	private void OnEnable()
	{
		this.originalPosition = base.transform.localPosition;
		this.originalRotation = base.transform.localRotation;
		Vector3 facePositionLocalPosition = this.facePosition.localPosition;
		Quaternion facePositionLocalRotation = this.facePosition.localRotation;
		ShortcutExtensions.DOPunchPosition(this.facePosition.transform, new Vector3(0.2f, 0.2f, 0.2f), 2f, 50, 0f, false).onComplete = delegate
		{
			this.facePosition.localPosition = facePositionLocalPosition;
			this.facePosition.localRotation = facePositionLocalRotation;
		};
		ShortcutExtensions.DOLocalMove(base.transform, new Vector3(0f, -3f, -1f), 0.75f, false);
		ShortcutExtensions.DOLocalRotate(base.transform, new Vector3(-20f, 0f, 0f), 0.75f, RotateMode.Fast);
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x00010926 File Offset: 0x0000EB26
	private void OnDisable()
	{
		base.transform.localPosition = this.originalPosition;
		base.transform.localRotation = this.originalRotation;
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x0000E32C File Offset: 0x0000C52C
	private void Start()
	{
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x0000E32C File Offset: 0x0000C52C
	private void Update()
	{
	}

	// Token: 0x0400043A RID: 1082
	public Transform facePosition;

	// Token: 0x0400043B RID: 1083
	private Vector3 originalPosition;

	// Token: 0x0400043C RID: 1084
	private Quaternion originalRotation;
}
