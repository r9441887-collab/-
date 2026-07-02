using System;
using UnityEngine;

// Token: 0x020000E7 RID: 231
public class Crouch : MonoBehaviour
{
	// Token: 0x170000AF RID: 175
	// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00011924 File Offset: 0x0000FB24
	// (set) Token: 0x060005B4 RID: 1460 RVA: 0x0001192C File Offset: 0x0000FB2C
	public bool IsCrouched { get; private set; }

	// Token: 0x14000001 RID: 1
	// (add) Token: 0x060005B5 RID: 1461 RVA: 0x00035920 File Offset: 0x00033B20
	// (remove) Token: 0x060005B6 RID: 1462 RVA: 0x00035958 File Offset: 0x00033B58
	public event Action CrouchStart;

	// Token: 0x14000002 RID: 2
	// (add) Token: 0x060005B7 RID: 1463 RVA: 0x00035990 File Offset: 0x00033B90
	// (remove) Token: 0x060005B8 RID: 1464 RVA: 0x000359C8 File Offset: 0x00033BC8
	public event Action CrouchEnd;

	// Token: 0x060005B9 RID: 1465 RVA: 0x00011935 File Offset: 0x0000FB35
	private void Reset()
	{
		this.movement = base.GetComponentInParent<FirstPersonMovement>();
		this.headToLower = this.movement.GetComponentInChildren<Camera>().transform;
		this.colliderToLower = this.movement.GetComponentInChildren<CapsuleCollider>();
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x00035A00 File Offset: 0x00033C00
	private void LateUpdate()
	{
		if (Input.GetKey(this.key))
		{
			if (this.headToLower)
			{
				if (this.defaultHeadYLocalPosition == null)
				{
					this.defaultHeadYLocalPosition = new float?(this.headToLower.localPosition.y);
				}
				this.headToLower.localPosition = new Vector3(this.headToLower.localPosition.x, this.crouchYHeadPosition, this.headToLower.localPosition.z);
			}
			if (this.colliderToLower)
			{
				if (this.defaultColliderHeight == null)
				{
					this.defaultColliderHeight = new float?(this.colliderToLower.height);
				}
				float num;
				if (this.defaultHeadYLocalPosition != null)
				{
					num = this.defaultHeadYLocalPosition.Value - this.crouchYHeadPosition;
				}
				else
				{
					num = this.defaultColliderHeight.Value * 0.5f;
				}
				this.colliderToLower.height = Mathf.Max(this.defaultColliderHeight.Value - num, 0f);
				this.colliderToLower.center = Vector3.up * this.colliderToLower.height * 0.5f;
			}
			if (!this.IsCrouched)
			{
				this.IsCrouched = true;
				this.SetSpeedOverrideActive(true);
				Action crouchStart = this.CrouchStart;
				if (crouchStart == null)
				{
					return;
				}
				crouchStart();
				return;
			}
		}
		else if (this.IsCrouched)
		{
			if (this.headToLower)
			{
				this.headToLower.localPosition = new Vector3(this.headToLower.localPosition.x, this.defaultHeadYLocalPosition.Value, this.headToLower.localPosition.z);
			}
			if (this.colliderToLower)
			{
				this.colliderToLower.height = this.defaultColliderHeight.Value;
				this.colliderToLower.center = Vector3.up * this.colliderToLower.height * 0.5f;
			}
			this.IsCrouched = false;
			this.SetSpeedOverrideActive(false);
			Action crouchEnd = this.CrouchEnd;
			if (crouchEnd == null)
			{
				return;
			}
			crouchEnd();
		}
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x00035C24 File Offset: 0x00033E24
	private void SetSpeedOverrideActive(bool state)
	{
		if (!this.movement)
		{
			return;
		}
		if (state)
		{
			if (!this.movement.speedOverrides.Contains(new Func<float>(this.SpeedOverride)))
			{
				this.movement.speedOverrides.Add(new Func<float>(this.SpeedOverride));
				return;
			}
		}
		else if (this.movement.speedOverrides.Contains(new Func<float>(this.SpeedOverride)))
		{
			this.movement.speedOverrides.Remove(new Func<float>(this.SpeedOverride));
		}
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x0001196A File Offset: 0x0000FB6A
	private float SpeedOverride()
	{
		return this.movementSpeed;
	}

	// Token: 0x04000625 RID: 1573
	public KeyCode key = 306;

	// Token: 0x04000626 RID: 1574
	[Header("Slow Movement")]
	[Tooltip("Movement to slow down when crouched.")]
	public FirstPersonMovement movement;

	// Token: 0x04000627 RID: 1575
	[Tooltip("Movement speed when crouched.")]
	public float movementSpeed = 2f;

	// Token: 0x04000628 RID: 1576
	[Header("Low Head")]
	[Tooltip("Head to lower when crouched.")]
	public Transform headToLower;

	// Token: 0x04000629 RID: 1577
	[HideInInspector]
	public float? defaultHeadYLocalPosition;

	// Token: 0x0400062A RID: 1578
	public float crouchYHeadPosition = 1f;

	// Token: 0x0400062B RID: 1579
	[Tooltip("Collider to lower when crouched.")]
	public CapsuleCollider colliderToLower;

	// Token: 0x0400062C RID: 1580
	[HideInInspector]
	public float? defaultColliderHeight;
}
