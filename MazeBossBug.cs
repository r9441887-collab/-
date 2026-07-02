using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Animations.Rigging;

// Token: 0x02000068 RID: 104
public class MazeBossBug : MonoBehaviour
{
	// Token: 0x060002AD RID: 685 RVA: 0x00027A98 File Offset: 0x00025C98
	private void OnEnable()
	{
		this.timer = 0f;
		this.firstTick = false;
		for (int i = 0; i < this.audioSources.Length; i++)
		{
			this.audioSources[i].loop = true;
			this.audioSources[i].pitch = 1.5f + 0.1f * (float)i;
			this.audioSources[i].volume = 1f;
			this.audioSources[i].Play();
		}
		this.playVoice = true;
		this.voiceRoutine = base.StartCoroutine("VoiceRoutine");
		this.audioSources[0].transform.GetComponent<FadeOutSound>().origianalTransform = base.transform;
		this.voiceSources.transform.GetComponent<FadeOutSound>().origianalTransform = base.transform;
		this.footstepRoutine = base.StartCoroutine("FootstepRoutine");
	}

	// Token: 0x060002AE RID: 686 RVA: 0x0000FBD1 File Offset: 0x0000DDD1
	private IEnumerator FootstepRoutine()
	{
		this.chaseBGM = SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.ChaseBGM1, true, 0.3f, 1f);
		for (;;)
		{
			SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.EnemyFootStep1, false, 0.4f, Random.Range(1f, 1.4f));
			yield return new WaitForSeconds(0.22f);
			SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.EnemyFootStep2, false, 0.4f, Random.Range(1f, 1.4f));
			yield return new WaitForSeconds(0.22f);
		}
		yield break;
	}

	// Token: 0x060002AF RID: 687 RVA: 0x0000FBE0 File Offset: 0x0000DDE0
	private IEnumerator VoiceRoutine()
	{
		while (this.playVoice)
		{
			this.voiceSources.clip = this.voiceClips[Random.Range(0, this.voiceClips.Length)];
			this.voiceSources.pitch = Random.Range(0.35f, 0.5f);
			this.voiceSources.volume = 1f;
			this.voiceSources.Play();
			yield return new WaitUntil(() => !this.voiceSources.isPlaying);
			yield return new WaitForSeconds(Random.Range(0.2f, 0.5f));
		}
		yield break;
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x00027B74 File Offset: 0x00025D74
	private void OnDisable()
	{
		SoundManager.Instance.Stop_SfxSound_2(SoundManager.SfxSound_2.ChaseBGM1, 0.5f);
		base.StopCoroutine(this.footstepRoutine);
		this.audioSources[0].GetComponent<FadeOutSound>().PlayFadeOut(delegate
		{
			for (int j = 0; j < this.audioSources.Length; j++)
			{
				this.audioSources[j].Stop();
			}
		});
		this.voiceSources.GetComponent<FadeOutSound>().PlayFadeOut(delegate
		{
			this.playVoice = false;
			base.StopCoroutine(this.voiceRoutine);
			this.voiceRoutine = null;
		});
		Transform transform = this.ParentPivot;
		for (int i = 0; i < 17; i++)
		{
			transform.transform.localPosition = new Vector3(0.8f, 0f, 0f);
			transform = transform.GetChild(0);
		}
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x00027C14 File Offset: 0x00025E14
	private void ResetLeg()
	{
		Transform parentPivot = this.ParentPivot;
		for (int i = 0; i < 17; i++)
		{
			if (i % 2 == 1)
			{
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

	// Token: 0x060002B2 RID: 690 RVA: 0x0000FBEF File Offset: 0x0000DDEF
	private void Awake()
	{
		this.ResetLeg();
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.BlackSkyMusic, true, 0.8f, 1f);
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x00027E90 File Offset: 0x00026090
	private void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer <= 1f && this.setWeightZero)
		{
			this.tailRigging.weight = 1f;
			this.setWeightZero = false;
		}
		if (this.timer >= 5f)
		{
			this.tailRigging.weight = 0f;
			this.setWeightZero = true;
			this.timer = 0f;
			if (!this.firstTick)
			{
				this.firstTick = true;
			}
		}
		base.transform.LookAt(SingletoneBehaviour<HorrorSceneManager>.Instance.Player.transform.position);
		Vector3 eulerAngles = base.transform.rotation.eulerAngles;
		eulerAngles.x = 0f;
		eulerAngles.z = 0f;
		base.transform.rotation = Quaternion.Euler(eulerAngles);
		if (!this.CanMove)
		{
			return;
		}
		if (SingletoneBehaviour<MazeController>.Instance.doorSnapper.Count > this.followIndex)
		{
			Transform transform = SingletoneBehaviour<MazeController>.Instance.doorSnapper[this.followIndex].transform;
			if (transform != null && this.firstTick)
			{
				float num = Vector3.Distance(transform.position, base.transform.position);
				this.moveSpeed = Mathf.Clamp(num, 3f, num - 0.2f);
			}
			if (Vector3.Distance(transform.position, base.transform.position) >= 10f)
			{
				Debug.Log("Long Distance Cancled");
				this.followIndex++;
				return;
			}
			base.transform.position = Vector3.MoveTowards(base.transform.position, transform.position, this.moveSpeed * Time.deltaTime);
			if (Vector3.Distance(transform.position, base.transform.position) <= 0.1f)
			{
				this.followIndex++;
				return;
			}
		}
		else if (SingletoneBehaviour<MazeController>.Instance.doorSnapper.Count <= this.followIndex)
		{
			Transform transform = SingletoneBehaviour<HorrorSceneManager>.Instance.Player.transform;
			if (transform != null && this.firstTick)
			{
				float num2 = Vector3.Distance(transform.position, base.transform.position);
				this.moveSpeed = Mathf.Clamp(num2, 3f, num2 - 1f);
			}
			this.distanceFromPlayer = Vector3.Distance(transform.position, base.transform.position);
			if (this.distanceFromPlayer > this.arriveDiff)
			{
				base.transform.position = Vector3.MoveTowards(base.transform.position, transform.position, this.moveSpeed * Time.deltaTime);
			}
			this.distanceFromPlayer = Vector3.Distance(transform.position, base.transform.position);
			if (this.distanceFromPlayer <= this.arriveDiff && this.killPlayer && !this.ArriveToPlayer)
			{
				this.ArriveToPlayer = true;
				this.moveSpeed = 0f;
				Vector3 diff = this.Diff;
				Vector3 vector = SingletoneBehaviour<HorrorSceneManager>.Instance.Player.transform.TransformDirection(diff);
				Vector3 vector2 = SingletoneBehaviour<HorrorSceneManager>.Instance.Player.transform.position + vector;
				base.transform.position = vector2;
				SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraActive(false, true);
				Quaternion quaternion = Quaternion.LookRotation(this.Face.transform.position - SingletoneBehaviour<HorrorSceneManager>.Instance.Player.transform.position);
				SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCamera.transform.rotation = Quaternion.Slerp(SingletoneBehaviour<HorrorSceneManager>.Instance.Player.transform.rotation, quaternion, 500f * Time.deltaTime);
				this.faceObject.SetActive(false);
				SingletoneBehaviour<HorrorSceneManager>.Instance.WindowBugFace.SetActive(true);
				SingletoneBehaviour<HorrorSceneManager>.Instance.SetChapter(HorrorChapter.Chapter2);
				SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.EnemyCrying, false, 0.7f, 1.1f);
				SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.HitPiano, false, 0.7f, 1.1f);
			}
		}
	}

	// Token: 0x060002B4 RID: 692 RVA: 0x0000FC0F File Offset: 0x0000DE0F
	public void Reset()
	{
		this.faceObject.SetActive(true);
		this.killPlayer = true;
		this.ArriveToPlayer = false;
		this.moveSpeed = 3f;
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x0000FC36 File Offset: 0x0000DE36
	public void DestroySelf()
	{
		this.killPlayer = false;
		base.StartCoroutine("DestroySelfRoutine");
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x0000FC4B File Offset: 0x0000DE4B
	private IEnumerator DestroySelfRoutine()
	{
		yield return new WaitUntil(() => this.distanceFromPlayer <= 4f);
		this.moveSpeed = 0f;
		new List<Transform>().Clear();
		SingletoneBehaviour<HorrorSceneManager>.Instance.ShakeCamera(2f);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.RetroBeep_6, false, 1f, 1f);
		int num;
		for (int i = this.Positions.Count - 1; i >= 0; i = num - 1)
		{
			yield return new WaitForSeconds(0.1f);
			SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.RetroBeep_1, false, 0.8f, 1f);
			this.ParticleParentPivot[i].SetParent(null);
			this.ParticleParentPivot[i].GetComponent<ParticleCallback>().originalParent = this.ParticleParentPivot[i].parent;
			this.ParticleParentPivot[i].GetComponent<ParticleSystem>().Play();
			this.Positions[i].gameObject.SetActive(false);
			num = i;
		}
		yield return new WaitForSeconds(0.5f);
		SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.MonsterDie, false, 1f, 1f);
		yield return new WaitForSeconds(2f);
		StairsEnter.gameEnd = true;
		yield return new WaitUntil(() => DBManager.instance.dialogueController.CanPlayingDialogue_3D());
		HorrorSceneManager.GameNum = 0;
		HorrorSceneManager.dialogueNum = 12;
		DBManager.instance.dialogueController.StartNextDialogue_3D();
		SingletoneBehaviour<HorrorSceneManager>.Instance.First3DGameEnd();
		base.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x040002DA RID: 730
	[SerializeField]
	private Transform TargetTransform;

	// Token: 0x040002DB RID: 731
	[Header("꼬리 리깅")]
	[SerializeField]
	public Rig tailRigging;

	// Token: 0x040002DC RID: 732
	private bool setWeightZero;

	// Token: 0x040002DD RID: 733
	[Header("얼굴")]
	[SerializeField]
	private GameObject faceObject;

	// Token: 0x040002DE RID: 734
	[Header("타겟과 거리")]
	[SerializeField]
	private float distance;

	// Token: 0x040002DF RID: 735
	[Header("도착 거리 임계값")]
	[SerializeField]
	private float arriveDiff = 1.5f;

	// Token: 0x040002E0 RID: 736
	[Header("첫번째 패트롤 좌표")]
	[SerializeField]
	private List<Transform> Positions = new List<Transform>();

	// Token: 0x040002E1 RID: 737
	[SerializeField]
	private List<Transform> ParticleParentPivot = new List<Transform>();

	// Token: 0x040002E2 RID: 738
	[Header("다리 피봇")]
	[SerializeField]
	private Transform ParentPivot;

	// Token: 0x040002E3 RID: 739
	public float startAngle = 40f;

	// Token: 0x040002E4 RID: 740
	public float endAngle = 100f;

	// Token: 0x040002E5 RID: 741
	public float tweenDuration = 0.4f;

	// Token: 0x040002E6 RID: 742
	public float delayed = 0.05f;

	// Token: 0x040002E7 RID: 743
	public bool destroySelf;

	// Token: 0x040002E8 RID: 744
	public bool killPlayer = true;

	// Token: 0x040002E9 RID: 745
	public float timer;

	// Token: 0x040002EA RID: 746
	public bool firstTick;

	// Token: 0x040002EB RID: 747
	[Header("얼굴")]
	[SerializeField]
	private GameObject Face;

	// Token: 0x040002EC RID: 748
	[SerializeField]
	private bool ArriveToPlayer;

	// Token: 0x040002ED RID: 749
	public AudioSource[] audioSources;

	// Token: 0x040002EE RID: 750
	public AudioSource voiceSources;

	// Token: 0x040002EF RID: 751
	public AudioClip[] voiceClips;

	// Token: 0x040002F0 RID: 752
	private Coroutine voiceRoutine;

	// Token: 0x040002F1 RID: 753
	private bool playVoice;

	// Token: 0x040002F2 RID: 754
	private Coroutine footstepRoutine;

	// Token: 0x040002F3 RID: 755
	private AudioSource chaseBGM;

	// Token: 0x040002F4 RID: 756
	public float lookAtSpeed = 1f;

	// Token: 0x040002F5 RID: 757
	public float moveSpeed = 1f;

	// Token: 0x040002F6 RID: 758
	public int followIndex;

	// Token: 0x040002F7 RID: 759
	public float distanceFromPlayer;

	// Token: 0x040002F8 RID: 760
	public Vector3 Diff = new Vector3(0.5f, 0f, 0.3f);

	// Token: 0x040002F9 RID: 761
	public bool CanMove = true;
}
