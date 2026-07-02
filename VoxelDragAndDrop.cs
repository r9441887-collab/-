using System;

// Token: 0x02000453 RID: 1107
public class VoxelDragAndDrop : DragAndDrop
{
	// Token: 0x06001F2C RID: 7980 RVA: 0x0001C1D9 File Offset: 0x0001A3D9
	private void Start()
	{
		if (this.customMovement == null)
		{
			this.customMovement = base.GetComponent<VoxelMovement>();
		}
	}

	// Token: 0x06001F2D RID: 7981 RVA: 0x0000E32C File Offset: 0x0000C52C
	public override void OnMouseClickUp()
	{
	}

	// Token: 0x06001F2E RID: 7982 RVA: 0x0001C1F5 File Offset: 0x0001A3F5
	public override void OnMouseDragStart()
	{
		base.OnMouseDragStart();
		this.customMovement.StopMove();
		this.customMovement.SetFrontView();
	}

	// Token: 0x06001F2F RID: 7983 RVA: 0x0001C213 File Offset: 0x0001A413
	public override void OnMouseDragEnd()
	{
		base.OnMouseDragEnd();
		this.customMovement.ReadyNextMove();
	}

	// Token: 0x04001D94 RID: 7572
	public VoxelMovement customMovement;
}
