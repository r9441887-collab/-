using System;
using UnityEngine;

// Token: 0x020000ED RID: 237
public class Jump : MonoBehaviour
{
	// Token: 0x14000004 RID: 4
	// (add) Token: 0x060005E0 RID: 1504 RVA: 0x000362FC File Offset: 0x000344FC
	// (remove) Token: 0x060005E1 RID: 1505 RVA: 0x00036334 File Offset: 0x00034534
	public event Action Jumped;

	// Token: 0x060005E2 RID: 1506 RVA: 0x00011B4E File Offset: 0x0000FD4E
	private void Reset()
	{
		this.groundCheck = base.GetComponentInChildren<GroundCheck>();
	}

	// Token: 0x060005E3 RID: 1507 RVA: 0x00011B5C File Offset: 0x0000FD5C
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x0003636C File Offset: 0x0003456C
	private void LateUpdate()
	{
		if (Input.GetButtonDown("Jump") && (!this.groundCheck || this.groundCheck.isGrounded))
		{
			this.rigidbody.AddForce(Vector3.up * 100f * this.jumpStrength);
			Action jumped = this.Jumped;
			if (jumped == null)
			{
				return;
			}
			jumped();
		}
	}

	// Token: 0x04000654 RID: 1620
	private Rigidbody rigidbody;

	// Token: 0x04000655 RID: 1621
	public float jumpStrength = 2f;

	// Token: 0x04000657 RID: 1623
	[SerializeField]
	[Tooltip("Prevents jumping when the transform is in mid-air.")]
	private GroundCheck groundCheck;
}
