using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200002F RID: 47
public class Chapter03_BugFace : SingletoneBehaviour<Chapter03_BugFace>
{
	// Token: 0x06000131 RID: 305 RVA: 0x0000F003 File Offset: 0x0000D203
	public IEnumerator PlayerDeadRoutine()
	{
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerMovementActive(false);
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraActive(false, true);
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraShake(5f, 3.5f);
		ShortcutExtensions.DOShakePosition(base.transform, 3.5f, 2f, 50, 90f, false, true, ShakeRandomnessMode.Harmonic);
		this.BugFace.SetActive(true);
		this.values.Clear();
		SingletoneBehaviour<HorrorVolumeManager>.Instance.ShakeVhs(3f);
		Transform transform = this.BugFace.transform;
		for (int i = 0; i < 3; i++)
		{
			this.values.Add(transform.localPosition);
			this.values.Add(transform.localRotation.eulerAngles);
			this.values.Add(transform.localScale);
			transform = transform.GetChild(0);
		}
		SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.EnemyKnowPlayer, false, 1f, 1f);
		transform = this.BugFace.transform;
		ShortcutExtensions.DOScale(transform, 0.75f, 1f);
		transform = transform.GetChild(0);
		ShortcutExtensions.DOShakePosition(transform, 1f, 0.7f, 40, 90f, false, true, ShakeRandomnessMode.Harmonic).SetEase(Ease.OutQuad);
		transform = transform.GetChild(0);
		ShortcutExtensions.DOLocalRotate(transform, new Vector3(0f, 0f, -10f), 0.3f, RotateMode.Fast);
		SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.Last3DCreatureSound, false, 1f, 1f);
		yield return new WaitForSeconds(2f);
		yield return TweenExtensions.WaitForCompletion(this.MainScreen.DOColor(Color.black, 0.3f));
		if (MyPcWindowResolution.chapter == HorrorChapter.Chapter4)
		{
			SingletoneBehaviour<HorrorSceneManager>.Instance.Player.transform.position = new Vector3(146f, 100f, 52f);
			SingletoneBehaviour<HorrorSceneManager>.Instance.Player.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
			SingletoneBehaviour<Last3D_Manager>.Instance.Reset();
		}
		else
		{
			int num = 219;
			int num2 = 239;
			if (EnemyAI_SystemWinionRoom.deadPointChange)
			{
				num = Random.Range(50, 200);
				num2 = Random.Range(120, 200);
			}
			SingletoneBehaviour<HorrorSceneManager>.Instance.Player.transform.position = new Vector3((float)num, 100f, (float)num2);
			SingletoneBehaviour<HorrorSceneManager>.Instance.Player.transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
			SingletoneBehaviour<EnemyAI_SystemWinionRoom>.Instance.EnemyList[2].SetRandomPositionNearPlayer();
		}
		transform = this.BugFace.transform;
		for (int j = 0; j < 3; j++)
		{
			transform.localPosition = this.values[j * 3];
			transform.localRotation = Quaternion.Euler(this.values[1 + j * 3]);
			transform.localScale = this.values[2 + j * 3];
			transform = transform.GetChild(0);
		}
		BallCreature_AI.EnemyKilledPlayer = false;
		LastCreature_AI.EnemyKilledPlayer = false;
		this.BugFace.SetActive(false);
		yield return new WaitForSeconds(1f);
		yield return TweenExtensions.WaitForCompletion(this.MainScreen.DOColor(Color.white, 0.3f));
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerMovementActive(true);
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraActive(true, true);
		yield break;
	}

	// Token: 0x06000132 RID: 306 RVA: 0x0000F012 File Offset: 0x0000D212
	public void PlayDead()
	{
		base.StartCoroutine("PlayerDeadRoutine");
	}

	// Token: 0x06000133 RID: 307 RVA: 0x0000E32C File Offset: 0x0000C52C
	private void Update()
	{
	}

	// Token: 0x04000196 RID: 406
	public GameObject BugFace;

	// Token: 0x04000197 RID: 407
	public List<Vector3> values;

	// Token: 0x04000198 RID: 408
	public RawImage MainScreen;
}
