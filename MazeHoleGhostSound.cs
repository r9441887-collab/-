using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

// Token: 0x02000076 RID: 118
public class MazeHoleGhostSound : MonoBehaviour
{
	// Token: 0x060002F6 RID: 758 RVA: 0x0000FE77 File Offset: 0x0000E077
	private void Start()
	{
		this.collider = base.GetComponent<Collider>();
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x00029118 File Offset: 0x00027318
	private void Update()
	{
		Bounds bounds = this.collider.bounds;
		this.cameraFrustum = GeometryUtility.CalculateFrustumPlanes(SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCamera);
		if (GeometryUtility.TestPlanesAABB(this.cameraFrustum, bounds))
		{
			if (this.firstEnter)
			{
				Debug.Log(base.gameObject.name + " became visible.");
				base.StartCoroutine("PlayLastSound");
			}
			this.firstEnter = false;
		}
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x0000FE85 File Offset: 0x0000E085
	private IEnumerator PlayLastSound()
	{
		MazeHoleGhostSound.ApeearGhost = true;
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraActive(false, true);
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerMovementActive(false);
		SingletoneBehaviour<HorrorSceneManager>.Instance.Player.GetComponent<Rigidbody>().constraints = 126;
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.EnemyKnowPlayer, false, 1f, 1f);
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraShake(5f, 1f);
		yield return TweenExtensions.WaitForCompletion(ShortcutExtensions.DOLookAt(SingletoneBehaviour<HorrorSceneManager>.Instance.Player.transform, this.LookTargetObject.transform.position, 0.5f, AxisConstraint.None, null));
		yield return new WaitForSeconds(0.5f);
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraActive(true, true);
		SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerMovementActive(true);
		SingletoneBehaviour<HorrorSceneManager>.Instance.Player.GetComponent<Rigidbody>().constraints = 80;
		MazeHoleGhostSound.ApeearGhost = false;
		yield break;
	}

	// Token: 0x04000336 RID: 822
	public bool firstEnter = true;

	// Token: 0x04000337 RID: 823
	public GameObject LookTargetObject;

	// Token: 0x04000338 RID: 824
	private Collider collider;

	// Token: 0x04000339 RID: 825
	private Plane[] cameraFrustum;

	// Token: 0x0400033A RID: 826
	public static bool ApeearGhost;
}
