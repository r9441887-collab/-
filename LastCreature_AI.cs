using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000025 RID: 37
public class LastCreature_AI : MonoBehaviour
{
	// Token: 0x060000F1 RID: 241 RVA: 0x0002240C File Offset: 0x0002060C
	private void Start()
	{
		if (LastCreature_AI.Player == null)
		{
			LastCreature_AI.Player = SingletoneBehaviour<SystemWinionRoomManager>.Instance.Player.transform;
		}
		if (this.audioSource == null)
		{
			this.audioSource = base.GetComponent<AudioSource>();
		}
		LastCreature_AI.EnemyDie = false;
		this.originalSpeed = this._navMeshAgent.speed;
		this._enemyDie = false;
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x0000ED3A File Offset: 0x0000CF3A
	private void FixedUpdate()
	{
		if (this.MustKillPlayer)
		{
			return;
		}
		if (this.KillPlayer && !PlayerHiding.isPlayerHiding)
		{
			this.CheckPlayerIsNear();
		}
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x0000ED5A File Offset: 0x0000CF5A
	private void OnEnable()
	{
		if (this.MustKillPlayer)
		{
			if (LastCreature_AI.Player == null)
			{
				LastCreature_AI.Player = SingletoneBehaviour<SystemWinionRoomManager>.Instance.Player.transform;
			}
			base.StartCoroutine("FollowPlayer");
		}
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x0000ED91 File Offset: 0x0000CF91
	private IEnumerator FollowPlayer()
	{
		float elapsedTime = 0f;
		this.findPlayer = true;
		this.moveByNavMesh = true;
		this._navMeshAgent.speed = 20f;
		base.transform.position = this.GetRandomPointOnSphere();
		Debug.Log(Vector3.Distance(base.transform.position, LastCreature_AI.Player.position));
		this.LookAtTarget();
		while (elapsedTime <= 3f)
		{
			float num = Vector3.Distance(base.transform.position, LastCreature_AI.Player.position);
			this._navMeshAgent.SetDestination(LastCreature_AI.Player.position);
			if (num < 10f)
			{
				break;
			}
			yield return null;
			elapsedTime += Time.deltaTime;
		}
		this.TargetPoint = LastCreature_AI.Player.transform.position;
		Vector3 vector = (this.TargetPoint - base.transform.position).normalized * this.radius;
		this.ArriveAction = delegate
		{
			LastCreature_AI.ReadyAttack = true;
			base.gameObject.SetActive(false);
		};
		this._navMeshAgent.SetDestination(this.TargetPoint + vector);
		yield break;
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x00022474 File Offset: 0x00020674
	private Vector3 GetRandomPointOnSphere()
	{
		Vector3 position = LastCreature_AI.Player.position;
		Vector3 forward = LastCreature_AI.Player.forward;
		float num = Random.Range(-40f, 40f);
		Vector3 vector = (Quaternion.Euler(0f, num, 0f) * forward).normalized * this.radius;
		return position + vector;
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x0000EDA0 File Offset: 0x0000CFA0
	private void Update()
	{
		this.CheckEnemyIsDead();
		if (this.MustKillPlayer)
		{
			return;
		}
		this.MoveAnimation();
		if (this.moveByNavMesh && this.findPlayer)
		{
			this.MoveToPlayer();
		}
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x000224D8 File Offset: 0x000206D8
	public void CheckEnemyIsDead()
	{
		if (LastCreature_AI.EnemyDie && !this._enemyDie)
		{
			this._enemyDie = true;
			Debug.Log("에너미 사망");
			this.StopMovement();
			ShortcutExtensions.DOShakeRotation(base.transform, 8f, 20f, 50, 90f, true, ShakeRandomnessMode.Harmonic);
			DOVirtual.Float(0f, 1f, Random.Range(0f, 1.5f), delegate(float f)
			{
			}).OnComplete(delegate
			{
				ShortcutExtensions.DOLocalMove(base.transform, new Vector3(0f, 20f, 0f), 8f, false).SetRelative(true).SetEase(Ease.InOutBounce);
				DOVirtual.Float(0f, 20f, 8f, delegate(float value)
				{
					this._navMeshAgent.baseOffset = value;
				}).SetEase(Ease.InOutBounce).OnComplete(delegate
				{
					base.gameObject.SetActive(false);
				});
				ShortcutExtensions.DOLocalRotate(base.transform, new Vector3(30f, 0f, 0f), 4f, RotateMode.Fast).SetEase(Ease.OutQuad).SetRelative(true);
			});
		}
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x0000EDCD File Offset: 0x0000CFCD
	private void LateUpdate()
	{
		if (this.moveByNavMesh)
		{
			this.CheckArrive();
		}
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x00022580 File Offset: 0x00020780
	public void StartMovement()
	{
		this.KillPlayer = true;
		this.nearPlayer = true;
		this.findPlayer = true;
		this.nearPlayer = true;
		this.moveByNavMesh = true;
		this._navMeshAgent.ResetPath();
		this._navMeshAgent.isStopped = false;
		this._navMeshAgent.updatePosition = true;
		this._navMeshAgent.updateRotation = true;
		this.MoveToPlayer();
	}

	// Token: 0x060000FA RID: 250 RVA: 0x000225E8 File Offset: 0x000207E8
	public void StopMovement()
	{
		this.moveByNavMesh = false;
		this._navMeshAgent.ResetPath();
		this._navMeshAgent.isStopped = true;
		this._navMeshAgent.updatePosition = false;
		this._navMeshAgent.updateRotation = false;
		this._navMeshAgent.velocity = Vector3.zero;
	}

	// Token: 0x060000FB RID: 251 RVA: 0x0002263C File Offset: 0x0002083C
	private void CheckArrive()
	{
		if (!this._navMeshAgent.pathPending && this._navMeshAgent.remainingDistance <= this._navMeshAgent.stoppingDistance && (!this._navMeshAgent.hasPath || this._navMeshAgent.velocity.sqrMagnitude == 0f) && this.findPlayer)
		{
			ShortcutExtensions.DOLookAt(base.transform, LastCreature_AI.Player.position, 2f, AxisConstraint.None, null).onUpdate = delegate
			{
				Vector3 eulerAngles = base.transform.rotation.eulerAngles;
				eulerAngles.x = 0f;
				eulerAngles.z = 0f;
				base.transform.rotation = Quaternion.Euler(eulerAngles);
			};
			Action arriveAction = this.ArriveAction;
			if (arriveAction != null)
			{
				arriveAction();
			}
			Debug.Log("Player를 죽여야합니다.");
			this.findPlayer = false;
		}
	}

	// Token: 0x060000FC RID: 252 RVA: 0x0000EDDD File Offset: 0x0000CFDD
	private void MoveToPlayer()
	{
		this._navMeshAgent.acceleration = 30f;
		this._navMeshAgent.SetDestination(LastCreature_AI.Player.position);
	}

	// Token: 0x060000FD RID: 253 RVA: 0x000226FC File Offset: 0x000208FC
	private void CheckPlayerIsNear()
	{
		if (Vector3.Distance(base.transform.position, LastCreature_AI.Player.position) >= this.EnemyFarDistance)
		{
			this._navMeshAgent.speed = 8f;
		}
		else
		{
			this._navMeshAgent.speed = 4.7f;
		}
		float num = Vector3.Distance(this.EnemyDeathZones[0].position, LastCreature_AI.Player.position);
		this.MinimumDistance_Real = num;
		if (!LastCreature_AI.EnemyKilledPlayer && num < this.MinimumDistance)
		{
			Debug.Log("You Are Dead");
			SingletoneBehaviour<Chapter03_BugFace>.Instance.PlayDead();
			LastCreature_AI.EnemyKilledPlayer = true;
		}
	}

	// Token: 0x060000FE RID: 254 RVA: 0x000227A0 File Offset: 0x000209A0
	private void LookAtTarget()
	{
		base.transform.LookAt(this.TargetPoint);
		Vector3 eulerAngles = base.transform.rotation.eulerAngles;
		eulerAngles.x = 0f;
		eulerAngles.z = 0f;
		base.transform.rotation = Quaternion.Euler(eulerAngles);
	}

	// Token: 0x060000FF RID: 255 RVA: 0x000227FC File Offset: 0x000209FC
	private void MoveAnimation()
	{
		if (this._navMeshAgent.velocity.magnitude < 0.15f)
		{
			if (this.isMove)
			{
				this.isMove = false;
				if (this.EnemyState == State.Move)
				{
					this.EnemyState = State.Stop;
					return;
				}
			}
		}
		else if (!this.isMove)
		{
			this.isMove = true;
			if (this.EnemyState == State.Stop)
			{
				this.EnemyState = State.Move;
			}
		}
	}

	// Token: 0x04000147 RID: 327
	[SerializeField]
	private State EnemyState;

	// Token: 0x04000148 RID: 328
	public bool MustKillPlayer;

	// Token: 0x04000149 RID: 329
	public float radius = 30f;

	// Token: 0x0400014A RID: 330
	[Header("Patoll Transform")]
	[SerializeField]
	public NavMeshAgent _navMeshAgent;

	// Token: 0x0400014B RID: 331
	[SerializeField]
	public Vector3 TargetPoint;

	// Token: 0x0400014C RID: 332
	[Header("AI Status")]
	[SerializeField]
	public bool moveByNavMesh;

	// Token: 0x0400014D RID: 333
	[SerializeField]
	private bool isMove;

	// Token: 0x0400014E RID: 334
	[SerializeField]
	private float angleLimit = 45f;

	// Token: 0x0400014F RID: 335
	[SerializeField]
	private float distanceLimit_Find = 10f;

	// Token: 0x04000150 RID: 336
	[SerializeField]
	private float distanceLimit_Attack = 10f;

	// Token: 0x04000151 RID: 337
	[SerializeField]
	private float distanceLimit_ReAttack = 2f;

	// Token: 0x04000152 RID: 338
	[SerializeField]
	private float distanceLimit_Lost = 15f;

	// Token: 0x04000153 RID: 339
	[SerializeField]
	private bool KillPlayer;

	// Token: 0x04000154 RID: 340
	[SerializeField]
	private bool nearPlayer;

	// Token: 0x04000155 RID: 341
	[SerializeField]
	private bool findPlayer;

	// Token: 0x04000156 RID: 342
	[SerializeField]
	private bool hidePlayer;

	// Token: 0x04000157 RID: 343
	[SerializeField]
	private float minTime = 2f;

	// Token: 0x04000158 RID: 344
	[SerializeField]
	private float maxTime = 5f;

	// Token: 0x04000159 RID: 345
	public static bool EnemyKilledPlayer;

	// Token: 0x0400015A RID: 346
	[SerializeField]
	private List<Transform> EnemyDeathZones = new List<Transform>();

	// Token: 0x0400015B RID: 347
	public static bool EnemyDie;

	// Token: 0x0400015C RID: 348
	public bool _enemyDie;

	// Token: 0x0400015D RID: 349
	private static Transform Player;

	// Token: 0x0400015E RID: 350
	private float originalSpeed;

	// Token: 0x0400015F RID: 351
	public Action ArriveAction;

	// Token: 0x04000160 RID: 352
	private AudioSource audioSource;

	// Token: 0x04000161 RID: 353
	public float MinimumDistance = 1.8f;

	// Token: 0x04000162 RID: 354
	public float MinimumDistance_Real = 1.8f;

	// Token: 0x04000163 RID: 355
	public float EnemyFarDistance = 10f;

	// Token: 0x04000164 RID: 356
	public static bool ReadyAttack;
}
