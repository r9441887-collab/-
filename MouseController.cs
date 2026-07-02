using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

// Token: 0x020003B0 RID: 944
public class MouseController : SingletoneBehaviour<MouseController>
{
	// Token: 0x06001C02 RID: 7170 RVA: 0x0001A3D8 File Offset: 0x000185D8
	public void MustClickThisObject(GameObject target)
	{
		target.AddComponent<MustClickThisObject>();
	}

	// Token: 0x06001C03 RID: 7171 RVA: 0x0001A3E1 File Offset: 0x000185E1
	public void CantClickThisObject(GameObject target)
	{
		target.AddComponent<CantClickThisObject>();
	}

	// Token: 0x06001C04 RID: 7172 RVA: 0x000CFEB4 File Offset: 0x000CE0B4
	public void SetMousePosition(Vector3 position, float duration = 0.5f, Ease ease = Ease.Linear)
	{
		if (this.mouseTween != null)
		{
			this.mouseTween.Kill(false);
		}
		Vector3 mousePosition = Input.mousePosition;
		this.mouseTween = DOVirtual.Vector3(mousePosition, position, duration, delegate(Vector3 x)
		{
			Mouse.current.WarpCursorPosition(x);
		}).SetEase(ease);
	}

	// Token: 0x06001C05 RID: 7173 RVA: 0x000CFF10 File Offset: 0x000CE110
	public void SetMousePositionBySpeed(Vector3 position, float speed = 0.5f, Ease ease = Ease.Linear)
	{
		if (this.mouseTween != null)
		{
			this.mouseTween.Kill(false);
		}
		Vector3 mousePosition = Input.mousePosition;
		this.mouseTween = DOVirtual.Vector3(mousePosition, position, speed, delegate(Vector3 x)
		{
			Mouse.current.WarpCursorPosition(x);
		}).SetSpeedBased<Tweener>().SetEase(ease);
	}

	// Token: 0x04001962 RID: 6498
	[Space(10f)]
	[TextArea(10, 30)]
	public string description;

	// Token: 0x04001963 RID: 6499
	[Space(10f)]
	[TextArea(10, 30)]
	public string description2;

	// Token: 0x04001964 RID: 6500
	public Tween mouseTween;
}
