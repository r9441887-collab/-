using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

// Token: 0x020000C5 RID: 197
public class TrailerController : MonoBehaviour
{
	// Token: 0x060004D4 RID: 1236 RVA: 0x0000E32C File Offset: 0x0000C52C
	private void Start()
	{
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x00011150 File Offset: 0x0000F350
	private IEnumerator TrailerVideo()
	{
		int index = 0;
		this.MainGroup.alpha = 1f;
		this.LogoGroup.alpha = 0f;
		yield return new WaitForSeconds(3f);
		yield return TweenExtensions.WaitForCompletion(this.LogoGroup.DOFade(1f, 0.5f));
		yield return new WaitForSeconds(0.5f);
		this.TurnOnSound.Play();
		this.FirstAnimation();
		Camera.main.orthographicSize = 1f;
		Transform transform = Camera.main.gameObject.transform;
		List<Transform> cameraPositions = this.CameraPositions;
		int num = index;
		index = num + 1;
		transform.position = cameraPositions[num].position;
		yield return new WaitForSeconds(4f);
		this.LogoGroup.alpha = 0f;
		SoundManager.Instance.Play_BGM(SoundManager.BGM.Title, true, 0f);
		SoundManager.Instance.BGM_ChangeVolume_Tween(1.5f, 1f, false);
		this.MainGroup.DOFade(0f, 0.5f);
		yield return new WaitForSeconds(2f);
		Transform transform2 = Camera.main.gameObject.transform;
		List<Transform> cameraPositions2 = this.CameraPositions;
		num = index;
		index = num + 1;
		transform2.position = cameraPositions2[num].position;
		yield return new WaitForSeconds(2f);
		Transform transform3 = Camera.main.gameObject.transform;
		List<Transform> cameraPositions3 = this.CameraPositions;
		num = index;
		index = num + 1;
		transform3.position = cameraPositions3[num].position;
		yield return new WaitForSeconds(2f);
		Transform transform4 = Camera.main.gameObject.transform;
		List<Transform> cameraPositions4 = this.CameraPositions;
		num = index;
		index = num + 1;
		transform4.position = cameraPositions4[num].position;
		this.ION.winionMovement.SetTargetPosition(this.CameraPositions[index].position, true);
		bool waitArrive = true;
		this.ION.winionBehaviour.arriveAction = delegate
		{
			this.ION.winionAnimator.SetLoop(true);
			this.ION.winionAnimator.PlayAnimation("ShakeBackHand", false);
			waitArrive = false;
		};
		this.ION.winionMovement.StartNextMove();
		yield return new WaitForSeconds(1f);
		yield return new WaitUntil(() => !waitArrive);
		yield return new WaitForSeconds(0.5f);
		this.GRID.winionAnimator.SetLoop(true);
		this.GRID.winionAnimator.PlayAnimation("ShakeHand", false);
		yield return new WaitForSeconds(1.5f);
		ShortcutExtensions.DOMove(Camera.main.gameObject.transform, new Vector3(0f, 0f, -15f), 1f, false);
		ShortcutExtensions.DOOrthoSize(Camera.main, 3f, 1f);
		yield return new WaitForSeconds(2f);
		this.ION.winionMovement.MoveToRandomPosition();
		this.ION.winionMovement.StartNextMove();
		this.BO.winionMovement.MoveToRandomPosition();
		this.BO.winionMovement.StartNextMove();
		this.GRID.winionMovement.MoveToRandomPosition();
		this.GRID.winionMovement.StartNextMove();
		yield return new WaitForSeconds(1f);
		this._DEBUG.winionMovement.MoveToRandomPosition();
		this._DEBUG.winionMovement.StartNextMove();
		yield break;
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x000328B4 File Offset: 0x00030AB4
	private void FirstAnimation()
	{
		this.ION.winionAnimator.SetLoop(true);
		this.ION.winionAnimator.PlayAnimation("ShakeHand", false);
		this.BO.winionAnimator.SetLoop(true);
		this.BO.winionAnimator.PlayAnimation("Feed", false);
		this.GRID.winionAnimator.SetLoop(true);
		this.GRID.winionAnimator.PlayAnimation("FrontIdle", false);
		this.FIX.winionAnimator.SetLoop(true);
		this.FIX.winionAnimator.PlayAnimation("Fever", false);
		this._DEBUG.winionAnimator.SetLoop(true);
		this._DEBUG.winionAnimator.PlayAnimation("Sleeping", false);
	}

	// Token: 0x04000568 RID: 1384
	public AudioSource TurnOnSound;

	// Token: 0x04000569 RID: 1385
	public CanvasGroup MainGroup;

	// Token: 0x0400056A RID: 1386
	public CanvasGroup LogoGroup;

	// Token: 0x0400056B RID: 1387
	public WinionHandler ION;

	// Token: 0x0400056C RID: 1388
	public WinionHandler BO;

	// Token: 0x0400056D RID: 1389
	public WinionHandler GRID;

	// Token: 0x0400056E RID: 1390
	public WinionHandler FIX;

	// Token: 0x0400056F RID: 1391
	public WinionHandler _DEBUG;

	// Token: 0x04000570 RID: 1392
	public List<Transform> CameraPositions;
}
