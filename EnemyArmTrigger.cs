using System;
using DG.Tweening;
using UnityEngine;

// Token: 0x020000C8 RID: 200
public class EnemyArmTrigger : MonoBehaviour
{
	// Token: 0x060004E1 RID: 1249 RVA: 0x00032E7C File Offset: 0x0003107C
	private void Start()
	{
		this.firstAnimation.DOPause();
		this.secondAnimation.DOPause();
		this.thirdAnimation.DOPause();
		this.firstAnimation.autoKill = false;
		this.secondAnimation.autoKill = false;
		this.thirdAnimation.autoKill = false;
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x00032ED0 File Offset: 0x000310D0
	private void OnTriggerEnter(Collider other)
	{
		if (this.firstEnter)
		{
			if (!this.EnemyTrigger && other.tag == "Player")
			{
				this.firstEnter = false;
				this.firstAnimation.tween.SetAutoKill(false);
				this.secondAnimation.tween.SetAutoKill(false);
				this.thirdAnimation.tween.SetAutoKill(false);
				this.firstAnimation.DORestart();
				this.secondAnimation.DORestart();
				this.thirdAnimation.DORestart();
				this.source.Play(0UL);
				SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.EnemyKnowPlayer, false, 1f, 1f);
				this.door.isLocked = false;
				this.door.poolDoor = false;
				this.door.Opening(false, false);
				this.door.isLocked = true;
				SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraShake(3f, 1f);
				SingletoneBehaviour<HorrorVolumeManager>.Instance.ShakeVhs(1f);
				base.Invoke("Reset", this.returnDuration);
			}
			if (this.EnemyTrigger && other.tag == "Enemy")
			{
				this.firstEnter = false;
				this.firstAnimation.tween.SetAutoKill(false);
				this.secondAnimation.tween.SetAutoKill(false);
				this.thirdAnimation.tween.SetAutoKill(false);
				this.firstAnimation.DORestart();
				this.secondAnimation.DORestart();
				this.thirdAnimation.DORestart();
				this.source.Play(0UL);
				SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.EnemyKnowPlayer, false, 1f, 1f);
				this.door.isLocked = false;
				this.door.poolDoor = false;
				this.door.Opening(false, false);
				this.door.isLocked = true;
				SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraShake(3f, 1f);
				base.Invoke("Reset", this.returnDuration);
			}
		}
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x000330EC File Offset: 0x000312EC
	public void Reset()
	{
		DOVirtual.Float(0f, 2f, 2f, delegate(float f)
		{
		}).OnComplete(delegate
		{
			this.firstEnter = true;
		});
		this.firstAnimation.tween.SetAutoKill(true);
		this.secondAnimation.tween.SetAutoKill(true);
		this.thirdAnimation.tween.SetAutoKill(true);
		this.firstAnimation.DOPlayBackwards();
		this.secondAnimation.DOPlayBackwards();
		this.thirdAnimation.DOPlayBackwards();
		this.door.isLocked = false;
		this.door.Closing(false, false);
		this.door.isLocked = true;
	}

	// Token: 0x04000578 RID: 1400
	public bool EnemyTrigger;

	// Token: 0x04000579 RID: 1401
	public float returnDuration = 3f;

	// Token: 0x0400057A RID: 1402
	public bool firstEnter = true;

	// Token: 0x0400057B RID: 1403
	public DOTweenAnimation firstAnimation;

	// Token: 0x0400057C RID: 1404
	public DOTweenAnimation secondAnimation;

	// Token: 0x0400057D RID: 1405
	public DOTweenAnimation thirdAnimation;

	// Token: 0x0400057E RID: 1406
	public DoorInteraction door;

	// Token: 0x0400057F RID: 1407
	public AudioSource source;
}
