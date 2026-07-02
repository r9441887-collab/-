using System;
using System.Text.RegularExpressions;
using UnityEngine;

// Token: 0x020000FE RID: 254
public class DummyWinionAnimator : CustomAnimator
{
	// Token: 0x06000625 RID: 1573 RVA: 0x0003705C File Offset: 0x0003525C
	private void Awake()
	{
		DummyWinionAnimator.movePower = 0.25f;
		DummyWinionAnimator.objectsCount++;
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		Match match = Regex.Match(this.spriteRenderer.sprite.texture.name, "\\d+");
		if (match.Success)
		{
			string value = match.Value;
			if (!(value == "01"))
			{
				if (!(value == "02"))
				{
					if (value == "03")
					{
						this.dummyType = DummyType.Winion02;
					}
				}
				else
				{
					this.dummyType = DummyType.Friend02;
				}
			}
			else
			{
				this.dummyType = DummyType.Friend01;
			}
		}
		DummyWinionAnimator.nextAnimation = "FrontIdle";
		this.characterState = CharacterState.FrontIdle;
		this.adjustSpeed = 0.5f;
		base.Invoke("ExecuteAction", Random.Range(0.1f, 0.5f));
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x00011F4A File Offset: 0x0001014A
	public static void PlayDummyAnimation(string animationName)
	{
		DummyWinionAnimator.nextAnimation = animationName;
		DummyWinionAnimator.changeAnimation = true;
		DummyWinionAnimator.executedCount = 0;
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x00011F5E File Offset: 0x0001015E
	public static void PlayDummyMovePosition(GameObject _target)
	{
		DummyWinionAnimator.FarAwayTarget = _target;
		DummyWinionAnimator.movePosition = true;
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x00011F6C File Offset: 0x0001016C
	public static void StopDummyMovePosition()
	{
		DummyWinionAnimator.movePosition = false;
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x00037134 File Offset: 0x00035334
	private void Update()
	{
		if (DummyWinionAnimator.playDieAnimation && this.dieWinion)
		{
			this.dieWinion = false;
			this.PlayAnimation("DeathAnim", false);
			this.EndFrameAction = delegate
			{
				this.PlayAnimation("Death", false);
				this.EndFrameAction = null;
			};
		}
		if (DummyWinionAnimator.movePosition)
		{
			float num = Vector2.Distance(base.transform.position, DummyWinionAnimator.FarAwayTarget.transform.position);
			float num2 = Mathf.InverseLerp(0f, DummyWinionAnimator.minDistance, num);
			float num3 = Mathf.InverseLerp(0f, DummyWinionAnimator.maxDistance, num);
			Vector2 vector = (base.transform.position - DummyWinionAnimator.FarAwayTarget.transform.position).normalized;
			float num4 = Random.Range(-15f, 15f);
			Vector2 vector2 = Quaternion.Euler(0f, 0f, num4) * vector;
			float num5;
			if (num <= DummyWinionAnimator.minDistance)
			{
				num5 = Mathf.Lerp(2f, 0.5f, num2);
			}
			else
			{
				num5 = Mathf.Lerp(0.5f, 0.1f, num3);
			}
			float num6 = Random.Range(0.8f, 1.2f);
			Vector2 vector3 = vector2 * (num5 * num6);
			base.transform.position += vector3 * (Time.deltaTime * DummyWinionAnimator.movePower);
		}
		if (DummyWinionAnimator.changeAnimation)
		{
			if (!this.instanceExecuted)
			{
				base.Invoke("ExecuteAction", Random.Range(0.1f, 0.5f));
				this.instanceExecuted = true;
				DummyWinionAnimator.executedCount++;
				if (DummyWinionAnimator.executedCount == DummyWinionAnimator.objectsCount)
				{
					DummyWinionAnimator.changeAnimation = false;
				}
			}
		}
		else
		{
			this.instanceExecuted = false;
		}
		if (DummyWinionAnimator.lookAt)
		{
			Vector3 vector4 = Vector3.zero;
			if (DummyWinionAnimator.target != null)
			{
				vector4 = DummyWinionAnimator.target.transform.position;
				this.CalculateWay(vector4);
				return;
			}
		}
		else if (this.characterState != CharacterState.None)
		{
			this.characterState = CharacterState.None;
		}
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x00037340 File Offset: 0x00035540
	public void ExecuteAction()
	{
		string text = DummyWinionAnimator.nextAnimation;
		if (!(text == "FrontIdle"))
		{
			if (!(text == "LeftIdle"))
			{
				if (text == "BackIdle")
				{
					this.characterState = CharacterState.BackIdle;
				}
			}
			else
			{
				this.characterState = CharacterState.SideIdle;
			}
		}
		else
		{
			this.characterState = CharacterState.FrontIdle;
		}
		this.PlayAnimation(DummyWinionAnimator.nextAnimation, false);
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x00011F74 File Offset: 0x00010174
	public override void PlayAnimation(string AnimationName, bool IndexFollowing = false)
	{
		AnimationName = AnimationName + "_" + this.dummyType.ToString();
		base.PlayAnimation(AnimationName, IndexFollowing);
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x00011F9C File Offset: 0x0001019C
	public static void SetActiveLookAt(bool value)
	{
		DummyWinionAnimator.lookAt = value;
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x00011FA4 File Offset: 0x000101A4
	public static void LookAtTarget(GameObject _target)
	{
		DummyWinionAnimator.target = _target;
		DummyWinionAnimator.SetActiveLookAt(true);
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x000373A4 File Offset: 0x000355A4
	private void CalculateWay(Vector3 targetPosition)
	{
		Vector2 vector = base.transform.position - targetPosition;
		float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		num = Mathf.Abs(num);
		this._angle = num;
		if ((num >= 0f && num <= this.sideAngle) || (num >= 180f - this.sideAngle && num <= 180f))
		{
			base.FlipSprite(vector.x <= 0f);
			if (this.characterState != CharacterState.SideIdle)
			{
				this.PlayAnimation("LeftIdle", false);
				this.characterState = CharacterState.SideIdle;
				return;
			}
		}
		else if (vector.y >= 0f)
		{
			if (this.characterState != CharacterState.FrontIdle)
			{
				this.PlayAnimation("FrontIdle", false);
				this.characterState = CharacterState.FrontIdle;
				return;
			}
		}
		else if (vector.y < 0f && this.characterState != CharacterState.BackIdle)
		{
			this.PlayAnimation("BackIdle", false);
			this.characterState = CharacterState.BackIdle;
		}
	}

	// Token: 0x040006B5 RID: 1717
	public DummyType dummyType;

	// Token: 0x040006B6 RID: 1718
	public CharacterState characterState;

	// Token: 0x040006B7 RID: 1719
	public bool instanceExecuted;

	// Token: 0x040006B8 RID: 1720
	public static bool changeAnimation = false;

	// Token: 0x040006B9 RID: 1721
	public static int objectsCount = 0;

	// Token: 0x040006BA RID: 1722
	public static int executedCount = 0;

	// Token: 0x040006BB RID: 1723
	public static string nextAnimation;

	// Token: 0x040006BC RID: 1724
	public bool dieWinion;

	// Token: 0x040006BD RID: 1725
	public static bool playDieAnimation = false;

	// Token: 0x040006BE RID: 1726
	public static bool movePosition = false;

	// Token: 0x040006BF RID: 1727
	public static GameObject FarAwayTarget;

	// Token: 0x040006C0 RID: 1728
	public static float movePower = 1f;

	// Token: 0x040006C1 RID: 1729
	public static float minDistance = 2f;

	// Token: 0x040006C2 RID: 1730
	public static float maxDistance = 8f;

	// Token: 0x040006C3 RID: 1731
	public float _angle;

	// Token: 0x040006C4 RID: 1732
	public static GameObject target;

	// Token: 0x040006C5 RID: 1733
	public static bool lookAt = false;

	// Token: 0x040006C6 RID: 1734
	public float sideAngle = 20f;
}
