using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x02000015 RID: 21
public class DoorInteraction : MonoBehaviour
{
	// Token: 0x0600006C RID: 108 RVA: 0x0000E6CB File Offset: 0x0000C8CB
	public void OpenDoor()
	{
		this.isLocked = false;
		this.isOpen = !this.isOpen;
		if (this.isOpen)
		{
			this.Opening(false, false);
			return;
		}
		this.Closing(false, false);
	}

	// Token: 0x0600006D RID: 109 RVA: 0x0000E6FC File Offset: 0x0000C8FC
	public void OpenDoor(bool immediately = false, bool silent = false)
	{
		this.isLocked = false;
		this.isOpen = !this.isOpen;
		if (this.isOpen)
		{
			this.Opening(immediately, silent);
			return;
		}
		this.Closing(immediately, silent);
	}

	// Token: 0x0600006E RID: 110 RVA: 0x0001FACC File Offset: 0x0001DCCC
	public void Opening(bool immediately = false, bool silent = false)
	{
		this.isLocked = false;
		this.isOpen = true;
		if (base.gameObject != null)
		{
			base.gameObject.SetActive(true);
		}
		Vector3 position = FirstPersonMovement.HorrorViewTransform.position;
		Vector3 position2 = base.transform.position;
		bool flag = Vector3.Dot((position - position2).normalized, base.transform.forward) > 0f;
		if (this.poolDoor)
		{
			flag = !flag;
		}
		if (flag)
		{
			if (!immediately)
			{
				this.tween.Kill(false);
				this.tween = ShortcutExtensions.DOLocalRotate(base.transform, new Vector3(0f, 90f, 0f), this.openDuration, this.rotateMode).SetSpeedBased<TweenerCore<Quaternion, Vector3, QuaternionOptions>>().SetEase(this.openEase)
					.OnComplete(delegate
					{
						if (this.OpenEndAction != null)
						{
							this.OpenEndAction();
						}
						this.OpenEndAction = null;
					});
			}
			else
			{
				base.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
				if (this.OpenEndAction != null)
				{
					this.OpenEndAction();
				}
				this.OpenEndAction = null;
			}
		}
		else if (!immediately)
		{
			this.tween.Kill(false);
			this.tween = ShortcutExtensions.DOLocalRotate(base.transform, new Vector3(0f, -90f, 0f), this.openDuration, this.rotateMode).SetSpeedBased<TweenerCore<Quaternion, Vector3, QuaternionOptions>>().OnComplete(delegate
			{
				if (this.OpenEndAction != null)
				{
					this.OpenEndAction();
				}
				this.OpenEndAction = null;
			});
		}
		else
		{
			base.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
			if (this.OpenEndAction != null)
			{
				this.OpenEndAction();
			}
			this.OpenEndAction = null;
		}
		if (!silent && SingletoneBehaviour<DoorSound>.Instance != null)
		{
			SingletoneBehaviour<DoorSound>.Instance.OpenDoorSound.Pause();
			SingletoneBehaviour<DoorSound>.Instance.OpenDoorSound.Play();
		}
	}

	// Token: 0x0600006F RID: 111 RVA: 0x0000E72D File Offset: 0x0000C92D
	public bool IsTweenEnd()
	{
		this.isLocked = false;
		this.isOpen = true;
		base.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
		return true;
	}

	// Token: 0x06000070 RID: 112 RVA: 0x0001FCB4 File Offset: 0x0001DEB4
	public void Closing(bool immediately = false, bool silent = false)
	{
		this.isLocked = false;
		this.isOpen = false;
		if (!immediately)
		{
			this.tween.Kill(false);
			this.tween = ShortcutExtensions.DOLocalRotate(base.transform, Vector3.zero, this.closeDuration, this.rotateMode).SetSpeedBased<TweenerCore<Quaternion, Vector3, QuaternionOptions>>().SetEase(this.closeEase)
				.OnComplete(delegate
				{
					if (this.CloseEndAction != null)
					{
						this.CloseEndAction();
					}
					this.CloseEndAction = null;
				});
		}
		else
		{
			this.tween.Kill(false);
			base.transform.localRotation = Quaternion.identity;
			if (this.CloseEndAction != null)
			{
				this.CloseEndAction();
			}
			this.CloseEndAction = null;
		}
		if (!silent && SingletoneBehaviour<DoorSound>.Instance != null)
		{
			SingletoneBehaviour<DoorSound>.Instance.CloseDoorSound.Pause();
			SingletoneBehaviour<DoorSound>.Instance.CloseDoorSound.Play();
		}
	}

	// Token: 0x06000072 RID: 114 RVA: 0x0000E789 File Offset: 0x0000C989
	public void Reset()
	{
		this.isLocked = false;
	}

	// Token: 0x06000073 RID: 115 RVA: 0x0000E792 File Offset: 0x0000C992
	private void Start()
	{
		base.gameObject.SetActive(true);
		this.isLocked = false;
		this.isOpen = true;
		base.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
	}

	// Token: 0x040000A8 RID: 168
	public bool isLocked;

	// Token: 0x040000A9 RID: 169
	public bool isOpen;

	// Token: 0x040000AA RID: 170
	public float openDuration = 0.5f;

	// Token: 0x040000AB RID: 171
	public float closeDuration = 0.5f;

	// Token: 0x040000AC RID: 172
	public RotateMode rotateMode;

	// Token: 0x040000AD RID: 173
	public Tween tween;

	// Token: 0x040000AE RID: 174
	public Ease openEase = Ease.Linear;

	// Token: 0x040000AF RID: 175
	public Ease closeEase = Ease.Linear;

	// Token: 0x040000B0 RID: 176
	public bool poolDoor;

	// Token: 0x040000B1 RID: 177
	public Action OpenEndAction;

	// Token: 0x040000B2 RID: 178
	public Action CloseEndAction;
}
