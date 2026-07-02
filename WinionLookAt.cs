using System;
using project.Scripts.CharacterScripts;
using UnityEngine;

// Token: 0x02000121 RID: 289
public class WinionLookAt : MonoBehaviour, IHandler
{
	// Token: 0x170000C9 RID: 201
	// (get) Token: 0x060006E3 RID: 1763 RVA: 0x000127D8 File Offset: 0x000109D8
	// (set) Token: 0x060006E4 RID: 1764 RVA: 0x000127E0 File Offset: 0x000109E0
	public WinionHandler winionHandler { get; set; }

	// Token: 0x060006E5 RID: 1765 RVA: 0x000127E9 File Offset: 0x000109E9
	public bool IsWinionLookTarget()
	{
		return this.lookAt;
	}

	// Token: 0x060006E6 RID: 1766 RVA: 0x000127F1 File Offset: 0x000109F1
	public void SetActiveLookAt(bool value)
	{
		this.lookAt = value;
		this.winionHandler.ChangeCharacterState(CharacterState.None);
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x00012806 File Offset: 0x00010A06
	public void LookAtMouse(bool value)
	{
		this.targetIsMouse = value;
		this.lookAt = value;
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x00012816 File Offset: 0x00010A16
	public void LookAtTarget(GameObject target)
	{
		this.target = target;
		this.SetActiveLookAt(true);
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x0003CCD8 File Offset: 0x0003AED8
	private void Update()
	{
		if (!this.canLook)
		{
			return;
		}
		if (this.winionHandler.winionMovement.isMoving)
		{
			return;
		}
		if (this.winionHandler.winionDragAndDrop.isPickUp)
		{
			return;
		}
		if (this.lookAt)
		{
			Vector3 vector = Vector3.zero;
			if (this.targetIsMouse)
			{
				vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				this.CalculateWay(vector);
				return;
			}
			if (this.target != null)
			{
				vector = this.target.transform.position;
				this.CalculateWay(vector);
			}
		}
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x0003CD68 File Offset: 0x0003AF68
	private void CalculateWay(Vector3 targetPosition)
	{
		Vector2 vector = base.transform.position - targetPosition;
		float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		num = Mathf.Abs(num);
		this._angle = num;
		if ((num >= 0f && num <= this.sideAngle) || (num >= 180f - this.sideAngle && num <= 180f))
		{
			this.winionHandler.winionAnimator.FlipSprite(vector.x <= 0f);
			if (this.winionHandler.characterState != CharacterState.SideIdle)
			{
				this.winionHandler.winionAnimator.PlayAnimation("LeftIdle", false);
				this.winionHandler.ChangeCharacterState(CharacterState.SideIdle);
				return;
			}
		}
		else if (vector.y >= 0f)
		{
			if (this.winionHandler.characterState != CharacterState.FrontIdle)
			{
				this.winionHandler.winionAnimator.PlayAnimation("FrontIdle", false);
				this.winionHandler.ChangeCharacterState(CharacterState.FrontIdle);
				return;
			}
		}
		else if (vector.y < 0f && this.winionHandler.characterState != CharacterState.BackIdle)
		{
			this.winionHandler.winionAnimator.PlayAnimation("BackIdle", false);
			this.winionHandler.ChangeCharacterState(CharacterState.BackIdle);
		}
	}

	// Token: 0x040007BE RID: 1982
	public bool canLook = true;

	// Token: 0x040007BF RID: 1983
	[SerializeField]
	private GameObject target;

	// Token: 0x040007C0 RID: 1984
	[SerializeField]
	public bool lookAt;

	// Token: 0x040007C1 RID: 1985
	[SerializeField]
	private bool targetIsMouse;

	// Token: 0x040007C2 RID: 1986
	[SerializeField]
	private float _angle;

	// Token: 0x040007C3 RID: 1987
	public float sideAngle = 20f;
}
