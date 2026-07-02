using System;
using UnityEngine;

// Token: 0x02000150 RID: 336
public class WinionHangingMouse : MonoBehaviour
{
	// Token: 0x060007FC RID: 2044 RVA: 0x0001339A File Offset: 0x0001159A
	private void Start()
	{
		if (GameManager.instance.gameData.curChapter != GameManager.Chapter.DesktopMode)
		{
			Object.Destroy(this);
		}
		this.winionHandler = base.GetComponent<WinionHandler>();
		this.Hanging();
	}

	// Token: 0x060007FD RID: 2045 RVA: 0x000133C6 File Offset: 0x000115C6
	private void OnDestroy()
	{
		base.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
	}

	// Token: 0x060007FE RID: 2046 RVA: 0x0004189C File Offset: 0x0003FA9C
	private void Hanging()
	{
		this.winionHandler.winionBehaviour.isBusy = true;
		this.winionHandler.winionMouseEvent.canMouseEnter = false;
		this.winionHandler.winionDragAndDrop.canDragAndDrop = false;
		this.winionHandler.winionMovement.SetActiveMovement(false, false, false);
		this.winionHandler.winionAnimator.PlayAnimation("Hanging", false);
		base.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(-20f, 20f)));
		this.winionHandler.ChangeCharacterState(CharacterState.Hanging);
	}

	// Token: 0x060007FF RID: 2047 RVA: 0x00041940 File Offset: 0x0003FB40
	private void Update()
	{
		if (this.winionHandler.characterState != CharacterState.Hanging)
		{
			this.Hanging();
		}
		if (this.shakeCount >= this.maxShakeCount)
		{
			this.winionHandler.winionAnimator.PlayAnimation("Stun", false);
			this.winionHandler.winionAnimator.SetLoop(false);
			this.winionHandler.winionAnimator.SetAction(delegate
			{
				this.winionHandler.SetIdleByWinionStatus();
				this.winionHandler.winionMovement.canInterrupt = true;
				this.winionHandler.winionMovement.waitAndPlay = true;
				this.winionHandler.winionMouseEvent.canMouseEnter = true;
				this.winionHandler.winionMouseEvent.MouseEnter = false;
				this.winionHandler.winionDragAndDrop.canDragAndDrop = true;
				this.winionHandler.winionMovement.SetActiveMovement(true, false, false);
			});
			base.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
			this.winionHandler.winionBehaviour.isBusy = false;
			WinionHangingMouse.activeAlready = false;
			Object.Destroy(this);
			return;
		}
		base.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 20f));
		Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		vector.z = 0f;
		base.transform.position = vector;
		Vector2 vector2;
		vector2..ctor(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
		float num = 0.5f;
		if (Mathf.Abs(vector2.x - this.oldMouseAxis.x) > num || Mathf.Abs(vector2.y - this.oldMouseAxis.y) > num)
		{
			this.shake = true;
			this.shakeCount++;
		}
		else
		{
			this.shake = false;
		}
		this.oldMouseAxis = vector2;
	}

	// Token: 0x040008D9 RID: 2265
	public WinionHandler winionHandler;

	// Token: 0x040008DA RID: 2266
	public static bool activeAlready;

	// Token: 0x040008DB RID: 2267
	public bool shake;

	// Token: 0x040008DC RID: 2268
	private Vector2 oldMouseAxis;

	// Token: 0x040008DD RID: 2269
	public int shakeCount;

	// Token: 0x040008DE RID: 2270
	public int maxShakeCount = 100;
}
