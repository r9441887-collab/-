using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000C1 RID: 193
public class WinionFolderManager : SingletoneBehaviour<WinionFolderManager>
{
	// Token: 0x060004C0 RID: 1216 RVA: 0x000319AC File Offset: 0x0002FBAC
	public void SetEnterLockWinionFolder(Winion targetFolder, Winion targetWinion, bool lockState)
	{
		this.folders[(int)targetFolder].canEnter[(int)targetWinion] = lockState;
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x000319D8 File Offset: 0x0002FBD8
	public void SetExitLockWinionFolder(Winion targetFolder, Winion targetWinion, bool lockState)
	{
		this.folders[(int)targetFolder].canExit[(int)targetWinion] = lockState;
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x00031A04 File Offset: 0x0002FC04
	public void WinionIntoFolder(WinionHandler winion, Winion folder, bool isEventMove = true, bool DragWithMouse = false)
	{
		int winionType = (int)winion.winionStatus.winionInfo.winionType;
		if (!this.folders[(int)folder].canEnter[winionType] || (!this.CanMoveFolder && !isEventMove) || (!winion.winionDragAndDrop.canDragAndDrop && DragWithMouse))
		{
			return;
		}
		winion.whichFolder = folder;
		winion.transform.position = new Vector3(0f, -1f, 0f);
		if (folder <= Winion.Debug)
		{
			winion.transform.position = new Vector3(0f, -0.072727f, 0f);
		}
		winion.SetActiveWorldWinion(false);
		winion.SetActiveUIWinion(true);
		winion.winionMovement.SetActiveMovement(false, false, false);
		this.UIWinions[winionType].GetComponent<UIWinion>().ChangedColor = false;
		this.UIWinions[winionType].transform.localScale = Vector3.one;
		this.UIWinions[winionType].transform.SetParent(this.folders[(int)folder].folder.transform);
		this.UIWinions[winionType].GetComponent<UIWinion>().UIWinionPosition();
		this.UIWinions[winionType].SetActive(true);
		SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.WinionFolderInSound, false, WinionFolderManager.MuteFolderSound ? 0f : 0.4f, 1f);
		if (winion.haveSmallTalkEmotion)
		{
			winion.ReturnDialogueEmotion(true);
			winion.HaveDialogueEmotion(true);
		}
		if (winion.haveEventEmotion)
		{
			winion.ReturnDialogueEmotion(false);
			winion.HaveDialogueEmotion(false);
		}
		this.folders[(int)folder].AddInToFolder(this.UIWinions[winionType]);
		GameObject gameObject = Object.Instantiate<GameObject>(this.winionInfo[winionType], Vector3.zero, Quaternion.identity, this.fileInfo[(int)folder].transform);
		gameObject.GetComponent<WinionFile>().SetInfo(folder, (Winion)winionType);
		this.UIWinions[winionType].GetComponent<UIWinion>().winionFile = gameObject.GetComponent<WinionFile>();
		if (GameManager.instance.gameData.curChapter == GameManager.Chapter.DesktopMode)
		{
			SingletoneBehaviour<DesktopController>.Instance.winionRoomSettings.RoomSetting(winion, folder);
			return;
		}
		if (folder == Winion.Fix)
		{
			int num = 3;
			WinionFileSelector component = SingletoneBehaviour<WinionFolderManager>.Instance.windows[num].GetComponent<WinionFileSelector>();
			if (!component.light.activeSelf)
			{
				component.UIWinionColorLightOff(false, winion, "#6B6B6B", false);
				return;
			}
			if (!winion.changeUIGradient)
			{
				component.UIWinionColorLightOff(true, winion, "#6B6B6B", false);
				return;
			}
		}
		else if (folder == Winion.Ion)
		{
			int num2 = 0;
			WinionFileSelector component2 = SingletoneBehaviour<WinionFolderManager>.Instance.windows[num2].GetComponent<WinionFileSelector>();
			if (component2.light.activeSelf)
			{
				component2.UIWinionColorLightOff(true, winion, "#6B6B6B", false);
				return;
			}
			component2.UIWinionColorLightOff(false, winion, "#6B6B6B", false);
			return;
		}
		else if (folder == Winion.Debug)
		{
			int num3 = 4;
			WinionFileSelector component3 = SingletoneBehaviour<WinionFolderManager>.Instance.windows[num3].GetComponent<WinionFileSelector>();
			if (component3.light.activeSelf)
			{
				component3.UIWinionColorLightOff(true, winion, "#6B6B6B", false);
				return;
			}
			component3.UIWinionColorLightOff(false, winion, "#6B6B6B", false);
			return;
		}
		else
		{
			SingletoneBehaviour<WinionFolderManager>.Instance.windows[(int)folder].GetComponent<WinionFileSelector>().UIWinionColorLightOff(false, winion, "#6B6B6B", false);
		}
	}

	// Token: 0x060004C3 RID: 1219 RVA: 0x00031D48 File Offset: 0x0002FF48
	public List<WinionHandler> WhoInThisFolder(Winion folder = Winion.Ion)
	{
		List<WinionHandler> list = new List<WinionHandler>();
		for (int i = 0; i < this.folders[(int)folder].winions.Count; i++)
		{
			list.Add(this.folders[(int)folder].winions[i].GetComponent<UIWinion>().winionHandler);
		}
		return list;
	}

	// Token: 0x060004C4 RID: 1220 RVA: 0x00031DA8 File Offset: 0x0002FFA8
	public void RemoveWinion_OutOfFolder(Winion folder, Winion winion)
	{
		foreach (GameObject gameObject in this.folders[(int)folder].winions)
		{
			Winion winionType = gameObject.GetComponent<UIWinion>().winionHandler.winionStatus.winionInfo.winionType;
			if (winion == winionType)
			{
				this.folders[(int)folder].winions.Remove(gameObject);
				break;
			}
		}
	}

	// Token: 0x060004C5 RID: 1221 RVA: 0x00031E38 File Offset: 0x00030038
	public void WinionIntoSpecialRoom(Winion targetWinion, Winion targetWindow, RectTransform startTransform = null)
	{
		Vector3 vector = Vector3.zero;
		if (startTransform != null)
		{
			vector = startTransform.anchoredPosition / 1200f;
		}
		WinionHandler winionHandler = this.UIWinions[(int)targetWinion].GetComponent<UIWinion>().winionHandler;
		winionHandler.whichFolder = targetWindow;
		winionHandler.transform.position = vector;
		winionHandler.SetActiveWorldWinion(false);
		winionHandler.SetActiveUIWinion(true);
		winionHandler.winionMovement.SetActiveMovement(false, false, false);
		winionHandler.winionMovement.SetMoveSpeed(MoveSpeed.ForUI, false);
		winionHandler.winionMovement.FixMoveSpeed = true;
		this.UIWinions[(int)targetWinion].transform.localScale = Vector3.one * 1.2f;
		this.UIWinions[(int)targetWinion].transform.SetParent(this.folders[(int)targetWindow].folder.transform);
		this.UIWinions[(int)targetWinion].GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
		this.UIWinions[(int)targetWinion].GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
		this.UIWinions[(int)targetWinion].transform.localPosition = Vector3.zero;
		this.UIWinions[(int)targetWinion].GetComponent<UIWinion>().SyncUIWinionToWorldWinion(true, 1200f);
		this.UIWinions[(int)targetWinion].SetActive(true);
		this.folders[(int)targetWindow].AddInToFolder(this.UIWinions[(int)targetWinion]);
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x00031FCC File Offset: 0x000301CC
	public void WinionMoveInSpecialRoom(Winion winion, Vector3 position, RectTransform targetTransform = null, Action arriveAction = null)
	{
		Winion whichFolder = GameManager.instance.GetWinionHandlers()[(int)winion].whichFolder;
		if (targetTransform != null)
		{
			position = targetTransform.localPosition;
		}
		position.x = Mathf.Clamp(position.x, -1500f, 1500f);
		WinionHandler handler = this.UIWinions[(int)winion].GetComponent<UIWinion>().winionHandler;
		handler.winionMovement.SetActiveMovement(true, false, false);
		handler.winionMovement.SetTargetPosition(position / 1200f, true);
		handler.winionBehaviour.arriveAction = delegate
		{
			handler.transform.position = position / 1200f;
			handler.winionMovement.SetActiveMovement(false, false, false);
			Action arriveAction2 = arriveAction;
			if (arriveAction2 == null)
			{
				return;
			}
			arriveAction2();
		};
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x000320B0 File Offset: 0x000302B0
	public void WinionMoveInFolder(Winion winion, Vector3 position, RectTransform targetTransform = null, Action arriveAction = null)
	{
		Winion whichFolder = GameManager.instance.GetWinionHandlers()[(int)winion].whichFolder;
		this.UIWinions[(int)winion].GetComponent<UIWinion>().SyncUIWinionToWorldWinion(true, 880f);
		this.UIWinions[(int)winion].transform.parent.GetComponent<HorizontalLayoutGroup>().enabled = false;
		if (targetTransform != null)
		{
			position = targetTransform.localPosition;
		}
		position.x = Mathf.Clamp(position.x, -1500f, 1500f);
		WinionHandler handler = this.UIWinions[(int)winion].GetComponent<UIWinion>().winionHandler;
		position /= 880f;
		position.y *= this.fValue;
		handler.winionMovement.SetActiveMovement(true, false, false);
		handler.winionMovement.SetTargetPosition(position, true);
		handler.winionBehaviour.arriveAction = delegate
		{
			handler.transform.position = position;
			handler.winionMovement.SetActiveMovement(false, false, false);
			Action arriveAction2 = arriveAction;
			if (arriveAction2 == null)
			{
				return;
			}
			arriveAction2();
		};
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x000321F8 File Offset: 0x000303F8
	public void WinionIntoBatteryCenter(WinionHandler winion)
	{
		if (!this.CanMoveFolder)
		{
			return;
		}
		winion.whichFolder = Winion.BatteryCenter;
		winion.transform.position = new Vector3(0f, -1f, 0f);
		winion.SetActiveWorldWinion(false);
		winion.SetActiveUIWinion(true);
		winion.winionMovement.SetActiveMovement(false, false, false);
		int winionType = (int)winion.winionStatus.winionInfo.winionType;
		int num = 5;
		this.UIWinions[winionType].transform.localScale = Vector3.one;
		this.UIWinions[winionType].transform.SetParent(this.folders[num].folder.transform);
		this.UIWinions[winionType].transform.localPosition = Vector3.zero;
		this.UIWinions[winionType].SetActive(true);
		this.folders[num].AddInToFolder(this.UIWinions[winionType]);
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x000322F4 File Offset: 0x000304F4
	public void WinionOutofFolder(int targetFolder, Winion targetWinion, bool ManualExit = false)
	{
		if (targetFolder == 6)
		{
			SingletoneBehaviour<WinionFolderManager>.Instance.SetUIWinionDefault(targetWinion, Winion.TrashCan);
			GameManager.instance.GetWinionHandlers()[(int)targetWinion].whichFolder = Winion.None;
			return;
		}
		if (targetFolder == 7)
		{
			this.windows[targetFolder].GetComponent<WinionFileSelector>().SystemWinionRoomColor(false, GameManager.instance.GetWinionHandlers()[(int)targetWinion]);
			SingletoneBehaviour<WinionFolderManager>.Instance.SetUIWinionDefault(targetWinion, Winion.TrashCan);
			GameManager.instance.GetWinionHandlers()[(int)targetWinion].whichFolder = Winion.None;
			return;
		}
		UIWinion uiwinion = this.folders[targetFolder].GetUIWinion(targetWinion);
		WinionFileSelector component = this.windows[targetFolder].GetComponent<WinionFileSelector>();
		uiwinion.SyncUIWinionToWorldWinion(false, 1f);
		if (component != null)
		{
			component.SetRemoveTarget(uiwinion.winionFile.gameObject);
			component.Remove(ManualExit);
		}
		GameManager.instance.GetWinionHandlers()[(int)targetWinion].whichFolder = Winion.None;
		if (this.folders[targetFolder].winions.Count > 0)
		{
			Winion winionType = GameManager.instance.GetWinionHandlers()[(int)targetWinion].winionStatus.winionInfo.winionType;
			foreach (GameObject gameObject in this.folders[targetFolder].winions)
			{
				Winion winionType2 = gameObject.GetComponent<UIWinion>().winionHandler.GetComponent<WinionStatus>().winionInfo.winionType;
				if (winionType == winionType2)
				{
					this.folders[targetFolder].winions.Remove(gameObject);
					break;
				}
			}
		}
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x000324A4 File Offset: 0x000306A4
	public void SetUIWinionDefault(Winion winion, Winion folder)
	{
		WinionHandler winionHandler = this.UIWinions[(int)winion].GetComponent<UIWinion>().winionHandler;
		UIWinion component = SingletoneBehaviour<WinionFolderManager>.Instance.UIWinions[(int)winion].GetComponent<UIWinion>();
		if (winionHandler.uiwinionColorChange)
		{
			winionHandler.uiwinionColorChange = false;
			if (winionHandler.changeUIGradient)
			{
				winionHandler.changeUIGradient = false;
				component.winionGradient.color1 = Color.white;
				component.winionGradient.color2 = Color.white;
			}
			component.winionImg.color = Color.white;
		}
		this.UIWinions[(int)winion].SetActive(false);
		this.UIWinions[(int)winion].transform.SetParent(this.uiWinion);
		this.UIWinions[(int)winion].GetComponent<UIWinion>().SyncUIWinionToWorldWinion(false, 1f);
		WinionHandler winionHandler2 = this.UIWinions[(int)winion].GetComponent<UIWinion>().winionHandler;
		winionHandler2.winionMovement.FixMoveSpeed = false;
		winionHandler2.winionMovement.SetMoveSpeed(MoveSpeed.Auto, false);
		Vector2 vector = Camera.main.WorldToScreenPoint(this.icons[(int)folder].GetComponent<RectTransform>().position);
		vector = Camera.main.ScreenToWorldPoint(vector);
		winionHandler2.transform.position = vector;
		winionHandler2.SetActiveWorldWinion(true);
		winionHandler2.SetActiveUIWinion(false);
		winionHandler.whichFolder = Winion.None;
		SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.WinionFolderOutSound, false, WinionFolderManager.MuteFolderSound ? 0f : 0.4f, 1f);
		if (winionHandler2.haveSmallTalkEmotion)
		{
			winionHandler2.ReturnDialogueEmotion(true);
			winionHandler2.HaveDialogueEmotion(true);
		}
		if (winionHandler2.haveEventEmotion)
		{
			winionHandler2.ReturnDialogueEmotion(false);
			winionHandler2.HaveDialogueEmotion(false);
		}
		if (GameManager.instance.gameData.curChapter == GameManager.Chapter.DesktopMode)
		{
			winionHandler2.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
			winionHandler2.winionAnimator.AlwaysLook(CustomAnimator.LookWay.None);
			winionHandler2.winionAnimator.SetAnimationCanChange(true);
			winionHandler2.winionBehaviour.CanArriveAction = true;
			SingletoneBehaviour<WinionDistance>.Instance.SetShakeHandState(winionHandler2.GetWinionType(), true);
			winionHandler2.winionBehaviour.SetCanInterrupt(true);
			winionHandler2.winionMovement.MoveToRandomPosition();
			winionHandler2.winionDragAndDrop.canDragAndDrop = true;
			winionHandler2.winionBehaviour.isBusy = false;
		}
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x000326FC File Offset: 0x000308FC
	public void HideAllFolder()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			base.transform.GetChild(i).gameObject.SetActive(false);
		}
	}

	// Token: 0x04000556 RID: 1366
	public List<GameObject> UIWinions = new List<GameObject>();

	// Token: 0x04000557 RID: 1367
	public List<WinionFolderData> folders = new List<WinionFolderData>();

	// Token: 0x04000558 RID: 1368
	public List<GameObject> windows = new List<GameObject>();

	// Token: 0x04000559 RID: 1369
	public List<GameObject> fileInfo = new List<GameObject>();

	// Token: 0x0400055A RID: 1370
	public List<GameObject> winionInfo = new List<GameObject>();

	// Token: 0x0400055B RID: 1371
	public List<GameObject> icons = new List<GameObject>();

	// Token: 0x0400055C RID: 1372
	public Transform uiWinion;

	// Token: 0x0400055D RID: 1373
	public bool CanMoveFolder = true;

	// Token: 0x0400055E RID: 1374
	public float fValue = 0.256f;

	// Token: 0x0400055F RID: 1375
	public static bool MuteFolderSound;
}
