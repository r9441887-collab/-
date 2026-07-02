using System;
using System.Collections;
using project.Scripts.CharacterScripts;
using Unity.VisualScripting;
using UnityEngine;

// Token: 0x02000113 RID: 275
public class WinionFeed : MonoBehaviour, IHandler
{
	// Token: 0x170000C2 RID: 194
	// (get) Token: 0x06000697 RID: 1687 RVA: 0x00012439 File Offset: 0x00010639
	// (set) Token: 0x06000698 RID: 1688 RVA: 0x00012441 File Offset: 0x00010641
	public WinionHandler winionHandler { get; set; }

	// Token: 0x06000699 RID: 1689 RVA: 0x0003BF14 File Offset: 0x0003A114
	public void FeedWinion()
	{
		if (this.winionHandler.characterState == CharacterState.Feed)
		{
			return;
		}
		if (this.winionHandler.winionBehaviour.isBusy)
		{
			return;
		}
		if (this.winionHandler.winionStatus.winionInfo.isDischarged)
		{
			return;
		}
		base.StartCoroutine("FeedAnimation");
	}

	// Token: 0x0600069A RID: 1690 RVA: 0x0001244A File Offset: 0x0001064A
	private IEnumerator FeedAnimation()
	{
		this.winionHandler.winionBehaviour.isBusy = true;
		this.winionHandler.winionMovement.canInterrupt = false;
		this.winionHandler.winionMovement.SetTargetPosition(base.transform.position, false);
		this.winionHandler.winionMovement.SetActiveMovement(false, false, false);
		this.winionHandler.winionAnimator.PlayAnimation("Feed", false);
		this.winionHandler.ChangeCharacterState(CharacterState.Feed);
		Object.Instantiate<GameObject>(SingletoneBehaviour<ImageGetter>.Instance.Particles[0], base.transform.position, Quaternion.identity, null);
		yield return new WaitForSeconds(2f);
		this.winionHandler.ChangeCharacterState(CharacterState.None);
		this.winionHandler.winionMovement.SetActiveMovement(true, false, false);
		this.winionHandler.winionStatus.ChangeFriendship(2f);
		if (GameManager.instance.gameData.curChapter == GameManager.Chapter.DesktopMode)
		{
			if (!WinionHangingMouse.activeAlready && this.winionHandler.GetComponent<WinionGetBackStage>() == null)
			{
				WinionHangingMouse.activeAlready = true;
				WinionFollowMouse wFM = ComponentHolderProtocol.AddComponent<WinionFollowMouse>(this.winionHandler);
				wFM.arriveAction = delegate
				{
					if (GameManager.instance.gameData.curChapter == GameManager.Chapter.DesktopMode)
					{
						ComponentHolderProtocol.AddComponent<WinionHangingMouse>(wFM.transform);
					}
					wFM.arriveAction = null;
					Object.Destroy(wFM);
				};
			}
			else
			{
				this.winionHandler.winionBehaviour.isBusy = false;
				this.winionHandler.SetIdleByWinionStatus();
			}
		}
		yield break;
	}
}
