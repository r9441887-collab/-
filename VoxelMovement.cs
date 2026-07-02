using System;
using UnityEngine;

// Token: 0x02000454 RID: 1108
public class VoxelMovement : MonoBehaviour
{
	// Token: 0x06001F31 RID: 7985 RVA: 0x000DE51C File Offset: 0x000DC71C
	private void Update()
	{
		if (this.canWalk)
		{
			if (!this.canJump)
			{
				if (Mathf.Abs(this.rb2D.velocity.y) < 0.01f)
				{
					this.canJump = true;
				}
			}
			else
			{
				this.rb2D.AddForce(Vector2.up, 1);
				this.canJump = false;
			}
			Vector3 normalized = (this.targetPosition - base.transform.position).normalized;
			float num = this.moveSpeed * Time.deltaTime;
			base.transform.Translate(normalized * num);
			if (Vector3.Distance(base.transform.position, this.targetPosition) < 0.01f)
			{
				this.ReadyNextMove();
			}
		}
		if (this.isArrive)
		{
			this.arriveTime += Time.deltaTime;
			if (this.arriveTime >= this.waitTime)
			{
				this.arriveTime = 0f;
				this.isArrive = false;
				this.canWalk = true;
				this.targetPosition = this.GetRandomPosition();
				float num2 = base.transform.position.x - this.targetPosition.x;
				float num3 = base.transform.position.y - this.targetPosition.y;
				if (num2 >= 0f && num3 >= 0f)
				{
					this.Object3D.transform.rotation = Quaternion.Euler(new Vector3(0f, 135f, 0f));
					return;
				}
				if (num2 >= 0f && num3 < 0f)
				{
					this.Object3D.transform.rotation = Quaternion.Euler(new Vector3(0f, 225f, 0f));
					return;
				}
				if (num2 < 0f && num3 >= 0f)
				{
					this.Object3D.transform.rotation = Quaternion.Euler(new Vector3(0f, 45f, 0f));
					return;
				}
				if (num2 < 0f && num3 < 0f)
				{
					this.Object3D.transform.rotation = Quaternion.Euler(new Vector3(0f, 315f, 0f));
				}
			}
		}
	}

	// Token: 0x06001F32 RID: 7986 RVA: 0x000DE754 File Offset: 0x000DC954
	private Vector3 GetRandomPosition()
	{
		Vector3 vector = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, Camera.main.nearClipPlane));
		Vector3 vector2 = Camera.main.ScreenToWorldPoint(new Vector3((float)Screen.width, (float)Screen.height, Camera.main.nearClipPlane));
		float num = 1f;
		float num2 = Random.Range(vector.x + num, vector2.x - num);
		float num3 = Random.Range(vector.y + num, vector2.y - num);
		return new Vector3(num2, num3, 0f);
	}

	// Token: 0x06001F33 RID: 7987 RVA: 0x0001C226 File Offset: 0x0001A426
	public void ReadyNextMove()
	{
		this.waitTime = Random.Range(1f, 5f);
		this.canWalk = false;
		this.isArrive = true;
	}

	// Token: 0x06001F34 RID: 7988 RVA: 0x0001C24B File Offset: 0x0001A44B
	public void StopMove()
	{
		this.canWalk = false;
		this.waitTime = 9999f;
	}

	// Token: 0x06001F35 RID: 7989 RVA: 0x000DE7E8 File Offset: 0x000DC9E8
	public void SetFrontView()
	{
		if (base.transform.position.x < 0f)
		{
			this.Object3D.transform.rotation = Quaternion.Euler(new Vector3(0f, 45f, 0f));
			return;
		}
		this.Object3D.transform.rotation = Quaternion.Euler(new Vector3(0f, 135f, 0f));
	}

	// Token: 0x04001D95 RID: 7573
	public bool canWalk = true;

	// Token: 0x04001D96 RID: 7574
	public bool isArrive;

	// Token: 0x04001D97 RID: 7575
	public float arriveTime;

	// Token: 0x04001D98 RID: 7576
	public float waitTime;

	// Token: 0x04001D99 RID: 7577
	public float moveSpeed = 0.5f;

	// Token: 0x04001D9A RID: 7578
	public Vector3 targetPosition;

	// Token: 0x04001D9B RID: 7579
	public GameObject Object3D;

	// Token: 0x04001D9C RID: 7580
	public Rigidbody2D rb2D;

	// Token: 0x04001D9D RID: 7581
	public bool canJump = true;
}
