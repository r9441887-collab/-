using System;
using System.Collections.Generic;
using project.Scripts.CharacterScripts;
using Unity.VisualScripting;
using UnityEngine;

// Token: 0x02000123 RID: 291
public class WinionMovement : MonoBehaviour, IHandler
{
	// Token: 0x170000CB RID: 203
	// (get) Token: 0x060006F2 RID: 1778 RVA: 0x00012859 File Offset: 0x00010A59
	// (set) Token: 0x060006F3 RID: 1779 RVA: 0x00012861 File Offset: 0x00010A61
	public WinionHandler winionHandler { get; set; }

	// Token: 0x060006F4 RID: 1780 RVA: 0x0001286A File Offset: 0x00010A6A
	private void Start()
	{
		if (this.isStop_forDebug)
		{
			this.canWalk = false;
			this.isArrive = false;
		}
		this.targetPosition = base.transform.position;
		this.previousPosition = base.transform.position;
	}

	// Token: 0x060006F5 RID: 1781 RVA: 0x000128A4 File Offset: 0x00010AA4
	private void DecreaseHP()
	{
		this.winionHandler.winionStatus.DecreaseBattery(1);
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x0003D28C File Offset: 0x0003B48C
	private void CalculateMoveDistance()
	{
		if (!this.winionHandler.winionDragAndDrop.isPickUp && this.CanCalculateDistance)
		{
			this.totalDistance += Vector3.Distance(this.previousPosition, base.transform.position);
		}
		this.previousPosition = base.transform.position;
		if (this.totalDistance >= this.currentHPDistance + this.hpDecreaseDistance)
		{
			this.currentHPDistance += this.hpDecreaseDistance;
			this.DecreaseHP();
		}
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x0003D318 File Offset: 0x0003B518
	private void Update()
	{
		if (this.winionHandler.winionStatus.winionInfo.isDischarged)
		{
			return;
		}
		if (this.CheckIsWinionOutofCamera())
		{
			base.transform.position = Vector3.zero;
			this.InitArriveTime(1.5f, 3f);
			this.ReadyNextMove();
			this.SetMoveSpeed(MoveSpeed.Auto, false);
			this.winionHandler.winionBehaviour.ArriveAction();
		}
		this.dist = base.transform.position - this.lastUpdatePos;
		this.currentSpeed = this.dist.magnitude / Time.deltaTime;
		this.lastUpdatePos = base.transform.position;
		this.isMoving = this.currentSpeed != 0f;
		this.CalculateMoveDistance();
		if (this.canWalk)
		{
			Vector3 normalized = (this.targetPosition - base.transform.position).normalized;
			float num = this.moveSpeed * Time.deltaTime;
			if (Vector3.Distance(base.transform.position, this.targetPosition) <= num)
			{
				base.transform.position = this.targetPosition;
				this.InitArriveTime(1.5f, 3f);
				this.ReadyNextMove();
				this.SetMoveSpeed(MoveSpeed.Auto, false);
				this.winionHandler.winionBehaviour.ArriveAction();
			}
			else
			{
				base.transform.Translate(normalized * num);
				if (!this.winionHandler.winionDragAndDrop.isPickUp)
				{
					this.totalDistance += Vector3.Distance(this.previousPosition, base.transform.position);
				}
				this.previousPosition = base.transform.position;
				if (!this.isArrive && !this.SpecialAnimation)
				{
					this.UpdateState();
				}
				this.canFollowing = true;
			}
		}
		if (this.isArrive && this.waitAndPlay)
		{
			this.arriveTime += Time.deltaTime;
			if (this.arriveTime >= this.waitTime)
			{
				this.arriveTime = 0f;
				this.isArrive = false;
				this.canWalk = true;
			}
		}
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x0003D534 File Offset: 0x0003B734
	public void SetActiveMovement(bool value, bool isEvent = false, bool clearTarget = false)
	{
		this.CanCalculateDistance = value;
		if (value)
		{
			if (isEvent)
			{
				this.stopEventRandomMovement = false;
			}
			this.canWalk = true;
			this.moveWinion = value;
			this.waitTime = 0f;
			this.isArrive = false;
			return;
		}
		this.moveWinion = value;
		if (clearTarget)
		{
			this.targetPosition = base.transform.position;
		}
		this.InitArriveTime(1.5f, 3f);
		this.ReadyNextMove();
		this.winionHandler.ChangeCharacterState(CharacterState.FrontIdle);
		this.waitTime = float.PositiveInfinity;
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x000128B7 File Offset: 0x00010AB7
	public void SettingPos_SetTargetPos(Transform targetTrans)
	{
		this.haveDestination = false;
		base.gameObject.transform.position = targetTrans.position;
		this.stopEventRandomMovement = true;
		this.targetPosition = targetTrans.position;
		this.SetActiveMovement(false, true, false);
	}

	// Token: 0x060006FA RID: 1786 RVA: 0x000128F2 File Offset: 0x00010AF2
	public void SetArriveDistance(float distance)
	{
		this.arriveDistance = distance;
	}

	// Token: 0x060006FB RID: 1787 RVA: 0x0003D5C0 File Offset: 0x0003B7C0
	private Vector3 GetRandomPosition()
	{
		Vector3 vector = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, Camera.main.nearClipPlane));
		Vector3 vector2 = Camera.main.ScreenToWorldPoint(new Vector3((float)Screen.width, (float)Screen.height, Camera.main.nearClipPlane));
		float num = 1f;
		int num2 = 0;
		for (;;)
		{
			float num3 = Random.Range(vector.x + num, vector2.x - num);
			float num4 = Random.Range(vector.y + num, vector2.y - num);
			Vector3 vector3;
			vector3..ctor(num3, num4, 0f);
			if (num2++ > 100)
			{
				break;
			}
			if (!SingletoneBehaviour<CheckOverlap>.Instance.IsOverlap(vector3) && !this.CheckTargetIsNear(vector3) && !this.CheckOtherWinionIsNear(vector3))
			{
				return vector3;
			}
		}
		return Vector3.zero;
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x0003D690 File Offset: 0x0003B890
	private bool CheckIsWinionOutofCamera()
	{
		if (SingletoneBehaviour<CameraFocusManager>.Instance.isZoom)
		{
			return false;
		}
		float num = 600f;
		Vector2 vector = Camera.main.ScreenToWorldPoint(new Vector3(-num, -num, Camera.main.nearClipPlane));
		Vector2 vector2 = Camera.main.ScreenToWorldPoint(new Vector3((float)Screen.width + num, (float)Screen.height + num, Camera.main.nearClipPlane));
		Vector2 vector3 = base.transform.position;
		return vector3.x < vector.x || vector3.y < vector.y || vector3.x > vector2.x || vector3.y > vector2.y;
	}

	// Token: 0x060006FD RID: 1789 RVA: 0x000128FB File Offset: 0x00010AFB
	public void ReadyNextMove()
	{
		this.StopCurrentMove();
		if (this.canInterrupt)
		{
			this.winionHandler.SetIdleByWinionStatus();
		}
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x00012916 File Offset: 0x00010B16
	public void StopCurrentMove()
	{
		this.canWalk = false;
		this.isArrive = true;
	}

	// Token: 0x060006FF RID: 1791 RVA: 0x00012926 File Offset: 0x00010B26
	public void SetNextMoveTimer(bool value)
	{
		if (!value)
		{
			this.arriveTime = 0f;
		}
		this.waitAndPlay = value;
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x0001293D File Offset: 0x00010B3D
	public void StartNextMove()
	{
		this.canWalk = true;
		this.isArrive = false;
	}

	// Token: 0x06000701 RID: 1793 RVA: 0x0001294D File Offset: 0x00010B4D
	public void InitArriveTime(float start = 1.5f, float end = 3f)
	{
		this.arriveTime = 0f;
		this.waitTime = Random.Range(start, end);
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x0003D750 File Offset: 0x0003B950
	public void UpdateState()
	{
		Vector2 vector = base.transform.position - this.targetPosition;
		float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		num = Mathf.Abs(num);
		if ((num >= 0f && num <= this._sideAngle) || (num >= 180f - this._sideAngle && num <= 180f))
		{
			this.winionHandler.winionAnimator.FlipSprite(vector.x <= 0f);
			if (this.winionHandler.characterState != CharacterState.SideWalk)
			{
				this.winionHandler.winionAnimator.PlayAnimation("LeftWalk", false);
				this.winionHandler.ChangeCharacterState(CharacterState.SideWalk);
				return;
			}
		}
		else if (vector.y >= 0f)
		{
			if (this.winionHandler.characterState != CharacterState.FrontWalk)
			{
				this.winionHandler.winionAnimator.PlayAnimation("FrontWalk", false);
				this.winionHandler.ChangeCharacterState(CharacterState.FrontWalk);
				return;
			}
		}
		else if (vector.y < 0f && this.winionHandler.characterState != CharacterState.BackWalk)
		{
			this.winionHandler.winionAnimator.PlayAnimation("BackWalk", false);
			this.winionHandler.ChangeCharacterState(CharacterState.BackWalk);
		}
	}

	// Token: 0x06000703 RID: 1795 RVA: 0x00012967 File Offset: 0x00010B67
	public Vector3 SetTargetPosition(Vector3 pos, bool isEvent = true)
	{
		this.haveDestination = true;
		if (isEvent)
		{
			this.stopEventRandomMovement = true;
		}
		pos.z = 0f;
		this.targetPosition = pos;
		return pos;
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x0001298E File Offset: 0x00010B8E
	public void MoveToRandomPosition()
	{
		if (this.winionHandler.UIWinionEnabled)
		{
			return;
		}
		this.SetTargetPosition(this.GetRandomPosition(), false);
	}

	// Token: 0x06000705 RID: 1797 RVA: 0x0003D890 File Offset: 0x0003BA90
	public void SetMoveSpeed(MoveSpeed speed, bool fixSpeed = false)
	{
		if (this.FixMoveSpeed)
		{
			return;
		}
		if (!this.FixMoveSpeed && fixSpeed)
		{
			this.FixMoveSpeed = true;
		}
		switch (speed)
		{
		case MoveSpeed.Auto:
			if (this.winionHandler.winionStatus.winionInfo.battery < 20)
			{
				this.moveSpeed = 0.25f;
				return;
			}
			if (this.winionHandler.winionStatus.winionInfo.memory > 80)
			{
				this.moveSpeed = 0.75f;
				return;
			}
			this.moveSpeed = 0.5f;
			return;
		case MoveSpeed.Slow:
			this.moveSpeed = 0.3f;
			return;
		case MoveSpeed.Normal:
			this.moveSpeed = 0.5f;
			return;
		case MoveSpeed.Fast:
			this.moveSpeed = 0.9f;
			return;
		case MoveSpeed.SuperFast:
			this.moveSpeed = 1.5f;
			return;
		case MoveSpeed.ForUI:
			this.moveSpeed = 0.1f;
			return;
		case MoveSpeed.MarketFast:
			this.moveSpeed = 1f;
			return;
		default:
			return;
		}
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x0003D978 File Offset: 0x0003BB78
	public void AvoidWinion(bool _avoid, Winion winion = Winion.None, float distance = 2f)
	{
		this.avoid = _avoid;
		this.avoidDistance = distance;
		if (!this.avoid)
		{
			if (!this.avoid)
			{
				this.avoidTarget = null;
			}
			return;
		}
		if (winion == Winion.None)
		{
			this.avoid = false;
			this.avoidTarget = null;
			return;
		}
		if (this.winionHandler.winionStatus.winionInfo.winionType == winion)
		{
			this.avoid = false;
			this.avoidTarget = null;
			return;
		}
		this.avoidTarget = GameManager.instance.GetWinionHandlers()[(int)winion].gameObject;
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x000129AC File Offset: 0x00010BAC
	public bool CheckTargetIsNear(Vector3 randomWorldPosition)
	{
		return this.avoid && !UnityObjectUtility.IsUnityNull(this.avoidTarget) && Vector3.Distance(this.avoidTarget.transform.position, randomWorldPosition) <= this.avoidDistance;
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x0003DA04 File Offset: 0x0003BC04
	public bool CheckOtherWinionIsNear(Vector3 randomWorldPosition)
	{
		List<WinionHandler> winionHandlers = GameManager.instance.GetWinionHandlers();
		for (int i = 0; i < 5; i++)
		{
			if (!winionHandlers[i].UIWinionEnabled && this.winionHandler.winionStatus.winionInfo.winionType != (Winion)i)
			{
				if (winionHandlers[i].winionMovement.haveDestination && Vector3.Distance(winionHandlers[i].winionMovement.targetPosition, randomWorldPosition) <= 0.5f)
				{
					return true;
				}
				if (Vector3.Distance(winionHandlers[i].transform.position, randomWorldPosition) <= 0.5f)
				{
					return true;
				}
			}
		}
		return this.avoid && !UnityObjectUtility.IsUnityNull(this.avoidTarget) && Vector3.Distance(this.avoidTarget.transform.position, randomWorldPosition) <= this.avoidDistance;
	}

	// Token: 0x040007CB RID: 1995
	[Header("디버그용 : 시작 시 멈춰있을까요?")]
	public bool isStop_forDebug;

	// Token: 0x040007CC RID: 1996
	[Header("위니언이 움직이고 있는 가")]
	public bool isMoving;

	// Token: 0x040007CD RID: 1997
	[Space(10f)]
	[Header("위니언 총 이동 거리")]
	public float totalDistance;

	// Token: 0x040007CE RID: 1998
	[Header("체력이 얼마마다 감소할지")]
	public float hpDecreaseDistance = 0.5f;

	// Token: 0x040007CF RID: 1999
	public float currentHPDistance;

	// Token: 0x040007D0 RID: 2000
	[Header("얼마마다 응가할지")]
	public float poopDecreaseDistance = 10f;

	// Token: 0x040007D1 RID: 2001
	public float currentPoopDistance;

	// Token: 0x040007D2 RID: 2002
	private Vector3 previousPosition;

	// Token: 0x040007D3 RID: 2003
	public float moveSpeed = 0.3f;

	// Token: 0x040007D4 RID: 2004
	public bool stopEventRandomMovement;

	// Token: 0x040007D5 RID: 2005
	public bool canWalk = true;

	// Token: 0x040007D6 RID: 2006
	public bool isArrive;

	// Token: 0x040007D7 RID: 2007
	public bool canInterrupt = true;

	// Token: 0x040007D8 RID: 2008
	public bool waitAndPlay;

	// Token: 0x040007D9 RID: 2009
	public float arriveTime;

	// Token: 0x040007DA RID: 2010
	public float waitTime;

	// Token: 0x040007DB RID: 2011
	public bool canFollowing = true;

	// Token: 0x040007DC RID: 2012
	public Vector3 targetPosition;

	// Token: 0x040007DD RID: 2013
	public float arriveDistance = 0.01f;

	// Token: 0x040007DE RID: 2014
	public bool haveDestination;

	// Token: 0x040007DF RID: 2015
	public bool CanCalculateDistance;

	// Token: 0x040007E0 RID: 2016
	private Vector3 lastUpdatePos = Vector3.zero;

	// Token: 0x040007E1 RID: 2017
	private Vector3 dist;

	// Token: 0x040007E2 RID: 2018
	private float currentSpeed;

	// Token: 0x040007E3 RID: 2019
	public bool SpecialAnimation;

	// Token: 0x040007E4 RID: 2020
	public bool moveWinion;

	// Token: 0x040007E5 RID: 2021
	public float _sideAngle = 45f;

	// Token: 0x040007E6 RID: 2022
	public bool FixMoveSpeed;

	// Token: 0x040007E7 RID: 2023
	public bool avoid;

	// Token: 0x040007E8 RID: 2024
	public GameObject avoidTarget;

	// Token: 0x040007E9 RID: 2025
	public float avoidDistance = 2f;
}
