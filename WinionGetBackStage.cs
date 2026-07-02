using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

// Token: 0x0200014C RID: 332
public class WinionGetBackStage : OnlyOneComponent<WinionGetBackStage>
{
	// Token: 0x060007E9 RID: 2025 RVA: 0x000132FF File Offset: 0x000114FF
	private void OnEnable()
	{
		this.randomY = Random.Range(0.3f, 0.7f);
	}

	// Token: 0x060007EA RID: 2026 RVA: 0x00041358 File Offset: 0x0003F558
	public ValueTuple<Vector3, Vector3> GetLeftEdgePosition()
	{
		float num = Random.Range(0.3f, 0.7f);
		Vector3 vector = Camera.main.ViewportToWorldPoint(new Vector3(0f, num, Camera.main.nearClipPlane));
		Vector3 vector2 = Camera.main.ViewportToWorldPoint(new Vector3(-0.2f, num, Camera.main.nearClipPlane));
		return new ValueTuple<Vector3, Vector3>(vector, vector2);
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x000413BC File Offset: 0x0003F5BC
	public ValueTuple<Vector3, Vector3> GetRightEdgePosition()
	{
		float num = Random.Range(0.3f, 0.7f);
		Vector3 vector = Camera.main.ViewportToWorldPoint(new Vector3(1f, num, Camera.main.nearClipPlane));
		Vector3 vector2 = Camera.main.ViewportToWorldPoint(new Vector3(1.2f, num, Camera.main.nearClipPlane));
		return new ValueTuple<Vector3, Vector3>(vector, vector2);
	}

	// Token: 0x060007EC RID: 2028 RVA: 0x00013316 File Offset: 0x00011516
	public override void useStart()
	{
		base.StartCoroutine("PopSideTab");
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x00013324 File Offset: 0x00011524
	private IEnumerator PopSideTab()
	{
		WinionGetBackStage.<>c__DisplayClass6_0 CS$<>8__locals1 = new WinionGetBackStage.<>c__DisplayClass6_0();
		CS$<>8__locals1.<>4__this = this;
		base.winionHandler.winionBehaviour.CanArriveAction = false;
		base.winionHandler.winionMouseEvent.canMouseEnter = false;
		base.winionHandler.winionDragAndDrop.canDragAndDrop = false;
		bool r = Random.Range(0, 2) % 2 == 0;
		ValueTuple<Vector3, Vector3> valueTuple = (r ? this.GetLeftEdgePosition() : this.GetRightEdgePosition());
		Vector3 screenOutPosition = valueTuple.Item1;
		CS$<>8__locals1.screenOutOffsetPosition = valueTuple.Item2;
		base.winionHandler.ChangeCharacterState(CharacterState.GetBackStage);
		base.winionHandler.winionMovement.SetTargetPosition(CS$<>8__locals1.screenOutOffsetPosition, true);
		CS$<>8__locals1.isArrive = false;
		base.winionHandler.winionBehaviour.arriveAction = delegate
		{
			CS$<>8__locals1.<>4__this.transform.position = CS$<>8__locals1.screenOutOffsetPosition;
			CS$<>8__locals1.isArrive = true;
			CS$<>8__locals1.<>4__this.winionHandler.winionBehaviour.arriveAction = null;
		};
		yield return new WaitUntil(() => CS$<>8__locals1.isArrive);
		CS$<>8__locals1.isArrive = false;
		int num = Random.Range(1, 2);
		WinionGetBackStage.<>c__DisplayClass6_1 CS$<>8__locals2 = new WinionGetBackStage.<>c__DisplayClass6_1();
		CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
		switch (num)
		{
		case 0:
			ComponentHolderProtocol.AddComponent<WinionFollowMouse>(base.winionHandler);
			base.winionHandler.winionMovement.SetActiveMovement(true, false, false);
			base.winionHandler.winionBehaviour.BombObject.SetActive(true);
			break;
		case 1:
		{
			CS$<>8__locals2.image = SingletoneBehaviour<DesktopController>.Instance.InstantiateImage(base.winionHandler);
			base.winionHandler.winionBehaviour.TargetWindow = CS$<>8__locals2.image.Item2.gameObject;
			CS$<>8__locals2.image.Item2.SettingWay(r ? 1 : (-1));
			Vector3 vector = this.GetEndPosition(r);
			vector.y = screenOutPosition.y;
			base.winionHandler.winionAnimator.PlayAnimation("Pull", false);
			base.winionHandler.winionAnimator.canChangeAnimation = false;
			base.winionHandler.winionMovement.SetTargetPosition(vector, true);
			base.winionHandler.winionMovement.SetActiveMovement(true, false, false);
			base.winionHandler.winionBehaviour.arriveAction = delegate
			{
				CS$<>8__locals2.image.Item2.GetComponent<UIWindow>().CantClose = false;
				CS$<>8__locals2.CS$<>8__locals1.<>4__this.winionHandler.winionBehaviour.SetCanInterrupt(true);
				CS$<>8__locals2.CS$<>8__locals1.isArrive = true;
				CS$<>8__locals2.CS$<>8__locals1.<>4__this.winionHandler.winionBehaviour.arriveAction = null;
			};
			yield return new WaitUntil(() => CS$<>8__locals2.CS$<>8__locals1.isArrive);
			base.winionHandler.winionMovement.MoveToRandomPosition();
			base.winionHandler.winionMovement.SetActiveMovement(true, false, false);
			yield return new WaitForEndOfFrame();
			base.winionHandler.winionBehaviour.TargetWindow = null;
			CS$<>8__locals2.image.Item2.targetHandler = null;
			base.winionHandler.winionAnimator.canChangeAnimation = true;
			base.winionHandler.winionBehaviour.isBusy = false;
			base.winionHandler.winionBehaviour.CanArriveAction = true;
			break;
		}
		}
		CS$<>8__locals2 = null;
		base.winionHandler.winionMouseEvent.canMouseEnter = true;
		base.winionHandler.winionDragAndDrop.canDragAndDrop = true;
		Object.Destroy(this);
		yield break;
	}

	// Token: 0x060007EE RID: 2030 RVA: 0x00041420 File Offset: 0x0003F620
	public Vector2 GetEndPosition(bool isLeft)
	{
		if (isLeft)
		{
			return Camera.main.ScreenToWorldPoint(SingletoneBehaviour<DesktopController>.Instance.LeftInPosition.position);
		}
		return Camera.main.ScreenToWorldPoint(SingletoneBehaviour<DesktopController>.Instance.RightInPosition.position);
	}

	// Token: 0x040008CB RID: 2251
	public float offset = 1f;

	// Token: 0x040008CC RID: 2252
	public float randomY;
}
