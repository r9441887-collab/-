using System;
using UnityEngine;

// Token: 0x02000148 RID: 328
public class WinionDance : MonoBehaviour
{
	// Token: 0x060007DC RID: 2012 RVA: 0x00040C58 File Offset: 0x0003EE58
	private void Start()
	{
		if (base.GetComponents<WinionDance>().Length > 1)
		{
			this.silentDestroy = true;
			Object.Destroy(this);
		}
		this.winionHandler = base.GetComponent<WinionHandler>();
		if (this.winionHandler.winionBehaviour.isBusy)
		{
			this.silentDestroy = true;
			Object.Destroy(this);
		}
		GameObject bombObject = this.winionHandler.winionBehaviour.BombObject;
		if (bombObject != null)
		{
			bombObject.SetActive(false);
		}
		if (this.winionHandler.winionBehaviour.TargetWindow != null)
		{
			this.winionHandler.winionBehaviour.TargetWindow.SetActive(false);
		}
		this.winionHandler.winionBehaviour.StopDesktopAction();
		this.winionHandler.winionBehaviour.SetCanInterrupt(false);
		this.winionHandler.winionBehaviour.CanArriveAction = false;
		this.winionHandler.winionDragAndDrop.canDragAndDrop = false;
		int winionType = (int)this.winionHandler.winionStatus.winionInfo.winionType;
		Vector3 position = SingletoneBehaviour<DesktopController>.Instance.danceObject.DancePositions[winionType].position;
		this.winionHandler.winionMovement.SetTargetPosition(position, true);
		this.winionHandler.winionAnimator.canChangeAnimation = true;
		this.winionHandler.winionAnimator.SetLoop(false);
		this.winionHandler.winionMovement.SetMoveSpeed(MoveSpeed.SuperFast, true);
		this.winionHandler.winionMovement.SetActiveMovement(true, false, false);
		this.winionHandler.ChangeCharacterState(CharacterState.None);
		this.winionHandler.winionBehaviour.arriveAction = delegate
		{
			this.winionHandler.winionMovement.FixMoveSpeed = false;
			this.winionHandler.winionMovement.SetMoveSpeed(MoveSpeed.Auto, false);
			this.winionHandler.winionMovement.SetActiveMovement(false, false, false);
			this.winionHandler.winionBehaviour.SetCanInterrupt(false);
			this.winionHandler.winionBehaviour.arriveAction = null;
			this.winionHandler.ChangeCharacterState(CharacterState.Dancing);
			this.winionHandler.winionAnimator.PlayAnimation("DanceWithMusic", false);
		};
	}

	// Token: 0x040008C2 RID: 2242
	public WinionHandler winionHandler;

	// Token: 0x040008C3 RID: 2243
	public bool silentDestroy;
}
