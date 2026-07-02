using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

// Token: 0x020000A3 RID: 163
public class WindowBugMovement : MonoBehaviour
{
	// Token: 0x06000412 RID: 1042 RVA: 0x0002CF08 File Offset: 0x0002B108
	private void OnEnable()
	{
		this.tailRigging.weight = 1f;
		this.setWeightZero = false;
		for (int i = 0; i < this.audioSources.Length; i++)
		{
			this.audioSources[i].loop = true;
			this.audioSources[i].pitch = 1.5f + 0.1f * (float)i;
			this.audioSources[i].volume = 1f;
			this.audioSources[i].Play();
		}
		this.playVoice = true;
		this.voiceRoutine = base.StartCoroutine("VoiceRoutine");
		this.ResetLeg();
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x00010978 File Offset: 0x0000EB78
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

	// Token: 0x06000414 RID: 1044 RVA: 0x0002CFA8 File Offset: 0x0002B1A8
	private void OnDisable()
	{
		Debug.Log("Enemy Disable Here");
		this.tailRigging.weight = 0f;
		this.setWeightZero = true;
		base.transform.position = this.targets[0].position;
		this.audioSources[0].GetComponent<FadeOutSound>().PlayFadeOut(delegate
		{
			for (int k = 0; k < this.audioSources.Length; k++)
			{
				this.audioSources[k].Stop();
			}
		});
		this.voiceSources.GetComponent<FadeOutSound>().PlayFadeOut(delegate
		{
			this.playVoice = false;
			if (this.voiceRoutine != null)
			{
				base.StopCoroutine(this.voiceRoutine);
			}
			this.voiceRoutine = null;
		});
		Transform transform = this.ParentPivot;
		for (int i = 0; i < 17; i++)
		{
			transform.transform.localPosition = new Vector3(0.8f, 0f, 0f);
			transform = transform.GetChild(0);
		}
		for (int j = 0; j < this.tweens.Count; j++)
		{
			this.tweens[j].Kill(false);
		}
		this.tweens.Clear();
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x0002D09C File Offset: 0x0002B29C
	private void ResetLeg()
	{
		for (int i = 0; i < this.tweens.Count; i++)
		{
			this.tweens[i].Kill(false);
		}
		this.tweens.Clear();
		Transform parentPivot = this.ParentPivot;
		for (int j = 0; j < 17; j++)
		{
			if (j % 2 == 1)
			{
				Transform child = this.ParentPivot.GetChild(1);
				Transform child2 = child.GetChild(0);
				Transform child3 = child2.GetChild(1).GetChild(0).GetChild(0);
				Transform child4 = child2.GetChild(1).GetChild(0).GetChild(1);
				child3.localRotation = Quaternion.Euler(new Vector3(-this.startAngle, 0f, 0f));
				child4.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
				this.tweens.Add(ShortcutExtensions.DOLocalRotate(child3, new Vector3(-this.endAngle, 0f, 0f), this.tweenDuration, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo).SetDelay((float)j * this.delayed * 3f + this.delayed * 3f * (float)(j % 2))
					.SetEase(Ease.InOutQuad));
				this.tweens.Add(ShortcutExtensions.DOLocalRotate(child4, new Vector3(this.endAngle, 0f, 0f), this.tweenDuration, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo).SetDelay((float)j * this.delayed + this.delayed * (float)(j % 2))
					.SetEase(Ease.InOutQuad));
				Transform child5 = child.GetChild(1);
				Transform child6 = child5.GetChild(1).GetChild(0).GetChild(0);
				Transform child7 = child5.GetChild(1).GetChild(0).GetChild(1);
				child6.localRotation = Quaternion.Euler(new Vector3(this.startAngle, 0f, 0f));
				child7.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
				this.tweens.Add(ShortcutExtensions.DOLocalRotate(child6, new Vector3(this.endAngle, 0f, 0f), this.tweenDuration, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo).SetDelay((float)j * this.delayed * 3f + this.delayed * 3f * (float)(j % 2) + 0.23f)
					.SetEase(Ease.InOutQuad));
				this.tweens.Add(ShortcutExtensions.DOLocalRotate(child7, new Vector3(-this.endAngle, 0f, 0f), this.tweenDuration, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo).SetDelay((float)j * this.delayed + this.delayed * (float)(j % 2) + 0.23f)
					.SetEase(Ease.InOutQuad));
			}
			this.ParentPivot = this.ParentPivot.GetChild(0);
		}
		this.ParentPivot = parentPivot;
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x00010987 File Offset: 0x0000EB87
	private void Awake()
	{
		this._agent = base.GetComponent<NavMeshAgent>();
		this.GetPositions(0);
		this.ResetLeg();
		this.startPoint = base.transform.position;
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x0002D380 File Offset: 0x0002B580
	public void GetPositions(int index)
	{
		this.lastPositionIndex = index;
		this.targets.Clear();
		for (int i = 0; i < this.Positions[index].childCount; i++)
		{
			this.targets.Add(this.Positions[index].GetChild(i));
		}
		base.transform.position = this.targets[0].position;
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x0002D3F4 File Offset: 0x0002B5F4
	public void Reset()
	{
		this.StopMove();
		this.targetIsPlayer = false;
		this.ArriveToPlayer = false;
		this.Face.transform.GetChild(0).localPosition = new Vector3(0f, 0f, 0f);
		this.Face.transform.localRotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
		DOVirtual.Float(1f, 0f, 0.3f, delegate(float f)
		{
			this.tailRigging.weight = f;
		}).OnComplete(delegate
		{
			base.transform.position = this.startPoint;
			this.GetPositions(0);
			this.setWeightZero = true;
			base.gameObject.SetActive(false);
		});
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x000109B3 File Offset: 0x0000EBB3
	public void StartMove()
	{
		if (this._agent != null)
		{
			this._agent.speed = 10f;
		}
		this.PositionIndex = 0;
		this.Movement = true;
		Debug.Log("Enemy를 재생합니다");
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x000109EB File Offset: 0x0000EBEB
	public void StopMove()
	{
		if (this._agent != null)
		{
			this._agent.speed = 0f;
		}
		this.PositionIndex = 0;
		this.Movement = false;
		Debug.Log("Enemy를 정지합니다");
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x0002D49C File Offset: 0x0002B69C
	private void Update()
	{
		Vector3 vector = this.Player.transform.position - base.transform.position;
		if (Vector3.Angle(base.transform.forward, vector) <= this.fieldOfView / 2f)
		{
			if (!this.targetIsPlayer && !this.lookAtPlayer)
			{
				SingletoneBehaviour<HorrorSceneManager>.Instance.Eyes.GetComponent<EyesOnMe>().TryOpenEyes();
			}
			this.lookAtPlayer = true;
		}
		else
		{
			if (this.lookAtPlayer)
			{
				SingletoneBehaviour<HorrorSceneManager>.Instance.Eyes.GetComponent<EyesOnMe>().TryCloseEyes(false);
			}
			this.lookAtPlayer = false;
		}
		if (this.targetIsPlayer)
		{
			Quaternion quaternion = Quaternion.LookRotation(this.Player.transform.position - this.Face.transform.position);
			this.Face.transform.rotation = Quaternion.Slerp(this.Face.transform.rotation, quaternion, this.rotationSpeed * Time.deltaTime);
		}
		if (this.ArriveToPlayer)
		{
			this._agent.speed = 0f;
			this._agent.velocity = Vector3.zero;
			SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCamera.fieldOfView = 80f;
			SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerOutlineCamera.fieldOfView = 80f;
			Quaternion quaternion2 = Quaternion.LookRotation(this.Face.transform.position - this.Player.transform.position);
			SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCamera.transform.rotation = Quaternion.Slerp(this.Player.transform.rotation, quaternion2, 500f * Time.deltaTime);
		}
		if (this.setWeightZero)
		{
			this.tailRigging.weight = 1f;
			this.setWeightZero = false;
		}
		if (this.targetIsPlayer)
		{
			this.distance = Vector3.Distance(base.transform.position, this.Player.transform.position);
			if (this.distance <= this.diff && !this.ArriveToPlayer)
			{
				this.ArriveToPlayer = true;
				SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.EnemyCrying, false, 0.7f, 1.1f);
				ShortcutExtensions.DOLocalMove(this.Face.transform.GetChild(0), new Vector3(0f, 0f, 2f), 0.5f, false);
				this.Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
				SingletoneBehaviour<HorrorSceneManager>.Instance.PlayerCameraActive(false, true);
				this.Player.GetComponent<FirstPersonMovement>().enabled = false;
				SingletoneBehaviour<HorrorSceneManager>.Instance.SetChapter(HorrorChapter.Chapter1);
			}
		}
		else
		{
			this.distance = Vector3.Distance(base.transform.position, this.targets[this.PositionIndex].position);
			if (this.distance <= this.diff)
			{
				this.tailRigging.weight = 0f;
				this.setWeightZero = true;
				int num = this.PositionIndex + 1;
				this.PositionIndex = num;
				if (num >= this.targets.Count)
				{
					this.PositionIndex = 0;
					if (this.ResetArrive)
					{
						this.Reset();
					}
				}
				if (this.destroyArrive)
				{
					base.gameObject.SetActive(false);
				}
			}
		}
		if (this.distance > this.diff)
		{
			if (this.targetIsPlayer)
			{
				this._agent.speed = 150f;
				this._agent.SetDestination(this.Player.transform.position);
				return;
			}
			if (this.Movement)
			{
				this._agent.speed = 10f;
			}
			this._agent.SetDestination(this.targets[this.PositionIndex].position);
		}
	}

	// Token: 0x04000440 RID: 1088
	public NavMeshAgent _agent;

	// Token: 0x04000441 RID: 1089
	[Header("얼굴")]
	[SerializeField]
	private GameObject Face;

	// Token: 0x04000442 RID: 1090
	[SerializeField]
	private GameObject BodyModel;

	// Token: 0x04000443 RID: 1091
	[Header("꼬리 리깅")]
	[SerializeField]
	private Rig tailRigging;

	// Token: 0x04000444 RID: 1092
	[Header("플레이어 관련")]
	[SerializeField]
	public bool targetIsPlayer;

	// Token: 0x04000445 RID: 1093
	[SerializeField]
	private GameObject Player;

	// Token: 0x04000446 RID: 1094
	[SerializeField]
	private bool ArriveToPlayer;

	// Token: 0x04000447 RID: 1095
	[Header("타겟과 거리")]
	[SerializeField]
	private float distance;

	// Token: 0x04000448 RID: 1096
	[Header("도착 거리 임계값")]
	[SerializeField]
	private float diff = 1.5f;

	// Token: 0x04000449 RID: 1097
	[Header("첫번째 패트롤 좌표")]
	[SerializeField]
	private List<Transform> Positions = new List<Transform>();

	// Token: 0x0400044A RID: 1098
	[Header("패드롤 좌표 인덱스")]
	[SerializeField]
	private int PositionIndex;

	// Token: 0x0400044B RID: 1099
	private List<Transform> targets = new List<Transform>();

	// Token: 0x0400044C RID: 1100
	[Header("다리 피봇")]
	[SerializeField]
	private Transform ParentPivot;

	// Token: 0x0400044D RID: 1101
	public float startAngle = 40f;

	// Token: 0x0400044E RID: 1102
	public float endAngle = 100f;

	// Token: 0x0400044F RID: 1103
	public float tweenDuration = 0.4f;

	// Token: 0x04000450 RID: 1104
	public float delayed = 0.05f;

	// Token: 0x04000451 RID: 1105
	private Vector3 startPoint;

	// Token: 0x04000452 RID: 1106
	public bool destroyArrive;

	// Token: 0x04000453 RID: 1107
	public AudioSource[] audioSources;

	// Token: 0x04000454 RID: 1108
	public AudioSource voiceSources;

	// Token: 0x04000455 RID: 1109
	public AudioClip[] voiceClips;

	// Token: 0x04000456 RID: 1110
	private Coroutine voiceRoutine;

	// Token: 0x04000457 RID: 1111
	private bool playVoice;

	// Token: 0x04000458 RID: 1112
	private List<Tween> tweens = new List<Tween>();

	// Token: 0x04000459 RID: 1113
	public int lastPositionIndex;

	// Token: 0x0400045A RID: 1114
	private bool setWeightZero;

	// Token: 0x0400045B RID: 1115
	public float rotationSpeed = 2f;

	// Token: 0x0400045C RID: 1116
	public bool ResetArrive;

	// Token: 0x0400045D RID: 1117
	public bool Movement;

	// Token: 0x0400045E RID: 1118
	public float fieldOfView = 45f;

	// Token: 0x0400045F RID: 1119
	public bool lookAtPlayer;

	// Token: 0x04000460 RID: 1120
	public float acceleration = 8f;

	// Token: 0x04000461 RID: 1121
	public float deceleration = 60f;

	// Token: 0x04000462 RID: 1122
	public float closeEnoughMeters = 10f;
}
