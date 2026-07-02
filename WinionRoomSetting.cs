using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000153 RID: 339
[Serializable]
public class WinionRoomSetting
{
	// Token: 0x0600080D RID: 2061 RVA: 0x0001344A File Offset: 0x0001164A
	public void Init()
	{
		int count = this.food.Count;
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x00041D18 File Offset: 0x0003FF18
	public void RoomSetting(WinionHandler target, Winion folder)
	{
		int winionFolderIndex = this.GetWinionFolderIndex(folder);
		Debug.Log("aa 들어옴");
		Debug.Log("aa index  " + winionFolderIndex.ToString());
		this.food[winionFolderIndex].SetActive(false);
		this.food_Left[winionFolderIndex].SetActive(false);
		this.food_Right[winionFolderIndex].SetActive(false);
		this.altogether_food_Front[winionFolderIndex].SetActive(false);
		this.altogether_food_Back[winionFolderIndex].SetActive(false);
		this.bo_food_Front[winionFolderIndex].SetActive(false);
		this.bo_food_Back[winionFolderIndex].SetActive(false);
		this.console[winionFolderIndex].SetActive(false);
		this.consoleLeft[winionFolderIndex].SetActive(false);
		this.consoleRight[winionFolderIndex].SetActive(false);
		List<WinionHandler> list = SingletoneBehaviour<WinionFolderManager>.Instance.WhoInThisFolder(folder);
		Debug.Log("aa inFolderCount  " + list.Count.ToString());
		if (list.Count > 1)
		{
			WinionHandler winionHandler = null;
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] != target)
				{
					winionHandler = list[i];
					break;
				}
			}
			if (winionHandler == null)
			{
				return;
			}
			string text = "aa anotherWinion  : ";
			WinionHandler winionHandler2 = winionHandler;
			Debug.Log(text + ((winionHandler2 != null) ? winionHandler2.ToString() : null) + "  targetWinion :  " + ((target != null) ? target.ToString() : null));
			if (winionHandler.winionStatus.winionInfo.winionType == Winion.Grid || winionHandler.winionStatus.winionInfo.winionType == Winion.Debug)
			{
				if (winionHandler.winionStatus.winionInfo.winionType == Winion.Grid && target.winionStatus.winionInfo.winionType == Winion.Ion)
				{
					winionHandler.winionAnimator.SetAnimationCanChange(true);
					this.consoleLeft[winionFolderIndex].SetActive(true);
					winionHandler.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Right);
					winionHandler.winionAnimator.PlayAnimation("LeftIdle_Flush", false);
					winionHandler.winionAnimator.SetAnimationCanChange(false);
				}
				else
				{
					winionHandler.winionAnimator.SetAnimationCanChange(true);
					this.consoleLeft[winionFolderIndex].SetActive(true);
					winionHandler.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
					winionHandler.winionAnimator.PlayAnimation("LeftIdle", false);
					winionHandler.winionAnimator.SetAnimationCanChange(false);
				}
				if (target.winionStatus.winionInfo.winionType == Winion.Grid || target.winionStatus.winionInfo.winionType == Winion.Debug)
				{
					target.winionAnimator.SetAnimationCanChange(true);
					this.consoleRight[winionFolderIndex].SetActive(true);
					target.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Right);
					target.winionAnimator.PlayAnimation("LeftIdle", false);
					target.winionAnimator.SetAnimationCanChange(false);
					return;
				}
				if (target.winionAnimator.canChangeAnimation)
				{
					this.food_Right[winionFolderIndex].SetActive(true);
					target.winionAnimator.SetAnimationCanChange(true);
					target.winionAnimator.PlayAnimation("Feed", false);
					target.winionAnimator.SetAnimationCanChange(false);
					return;
				}
			}
			else
			{
				if (target.winionStatus.winionInfo.winionType != Winion.Grid && target.winionStatus.winionInfo.winionType != Winion.Debug)
				{
					this.altogether_food_Front[winionFolderIndex].SetActive(true);
					this.altogether_food_Back[winionFolderIndex].SetActive(true);
					winionHandler.winionAnimator.SetAnimationCanChange(true);
					winionHandler.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Right);
					winionHandler.winionAnimator.PlayAnimation("Feed_Left", false);
					winionHandler.winionAnimator.SetAnimationCanChange(false);
					target.winionAnimator.SetAnimationCanChange(true);
					if (target.winionStatus.winionInfo.winionType == Winion.Fix)
					{
						target.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
						target.winionAnimator.AlwaysLook(CustomAnimator.LookWay.None);
						target.winionAnimator.PlayAnimation("Feed", false);
					}
					else
					{
						target.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
						target.winionAnimator.PlayAnimation("Feed_Left", false);
					}
					target.winionAnimator.SetAnimationCanChange(false);
					return;
				}
				this.food_Left[winionFolderIndex].SetActive(true);
				winionHandler.winionAnimator.SetAnimationCanChange(true);
				winionHandler.winionAnimator.PlayAnimation("Feed", false);
				winionHandler.winionAnimator.SetAnimationCanChange(false);
				if (target.winionStatus.winionInfo.winionType == Winion.Grid && winionHandler.winionStatus.winionInfo.winionType == Winion.Ion)
				{
					target.winionAnimator.SetAnimationCanChange(true);
					this.consoleRight[winionFolderIndex].SetActive(true);
					target.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
					target.winionAnimator.PlayAnimation("LeftIdle_Flush", false);
					target.winionAnimator.SetAnimationCanChange(false);
					return;
				}
				target.winionAnimator.SetAnimationCanChange(true);
				this.consoleRight[winionFolderIndex].SetActive(true);
				target.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Right);
				target.winionAnimator.PlayAnimation("LeftIdle", false);
				target.winionAnimator.SetAnimationCanChange(false);
				return;
			}
		}
		else
		{
			if (target.winionStatus.winionInfo.winionType == Winion.Grid || target.winionStatus.winionInfo.winionType == Winion.Debug)
			{
				this.console[winionFolderIndex].SetActive(true);
				target.winionAnimator.SetAnimationCanChange(true);
				target.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
				target.winionAnimator.PlayAnimation("LeftIdle", false);
				target.winionAnimator.SetAnimationCanChange(false);
				return;
			}
			if (target.winionStatus.winionInfo.winionType == Winion.Bo)
			{
				this.bo_food_Front[winionFolderIndex].SetActive(true);
				this.bo_food_Back[winionFolderIndex].SetActive(true);
			}
			else
			{
				this.food[winionFolderIndex].SetActive(true);
			}
			target.winionAnimator.SetAnimationCanChange(true);
			target.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
			target.winionAnimator.AlwaysLook(CustomAnimator.LookWay.None);
			target.winionAnimator.PlayAnimation("Feed", false);
			target.winionAnimator.SetAnimationCanChange(false);
		}
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x00042318 File Offset: 0x00040518
	public void OutOf_RoomSetting(Winion folder, Winion curOutWinion)
	{
		int winionFolderIndex = this.GetWinionFolderIndex(folder);
		this.food[winionFolderIndex].SetActive(false);
		this.food_Left[winionFolderIndex].SetActive(false);
		this.food_Right[winionFolderIndex].SetActive(false);
		this.altogether_food_Front[winionFolderIndex].SetActive(false);
		this.altogether_food_Back[winionFolderIndex].SetActive(false);
		this.bo_food_Front[winionFolderIndex].SetActive(false);
		this.bo_food_Back[winionFolderIndex].SetActive(false);
		this.console[winionFolderIndex].SetActive(false);
		this.consoleLeft[winionFolderIndex].SetActive(false);
		this.consoleRight[winionFolderIndex].SetActive(false);
		WinionHandler winionHandler = GameManager.instance.GetWinionHandlers()[(int)curOutWinion];
		winionHandler.winionAnimator.SetAnimationCanChange(true);
		winionHandler.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
		winionHandler.winionAnimator.AlwaysLook(CustomAnimator.LookWay.None);
		List<WinionHandler> list = SingletoneBehaviour<WinionFolderManager>.Instance.WhoInThisFolder(folder);
		if (list.Count > 0)
		{
			WinionHandler winionHandler2 = null;
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] != winionHandler)
				{
					winionHandler2 = list[i];
					break;
				}
			}
			if (winionHandler2 == null)
			{
				return;
			}
			string text = "aa curWinion   ";
			string text2 = curOutWinion.ToString();
			string text3 = " curWinionHandler  ";
			WinionHandler winionHandler3 = winionHandler;
			Debug.Log(text + text2 + text3 + ((winionHandler3 != null) ? winionHandler3.ToString() : null));
			if (winionHandler2.winionStatus.winionInfo.winionType == Winion.Grid || winionHandler2.winionStatus.winionInfo.winionType == Winion.Debug)
			{
				this.console[winionFolderIndex].SetActive(true);
				winionHandler2.winionAnimator.SetAnimationCanChange(true);
				winionHandler2.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
				winionHandler2.winionAnimator.PlayAnimation("LeftIdle", false);
				winionHandler2.winionAnimator.SetAnimationCanChange(false);
				return;
			}
			if (winionHandler2.winionStatus.winionInfo.winionType == Winion.Bo)
			{
				this.bo_food_Front[winionFolderIndex].SetActive(true);
				this.bo_food_Back[winionFolderIndex].SetActive(true);
			}
			else
			{
				this.food[winionFolderIndex].SetActive(true);
			}
			winionHandler2.winionAnimator.SetAnimationCanChange(true);
			winionHandler2.winionAnimator.AlwaysLook(CustomAnimator.LookWay.Left);
			winionHandler2.winionAnimator.AlwaysLook(CustomAnimator.LookWay.None);
			winionHandler2.winionAnimator.PlayAnimation("Feed", false);
			winionHandler2.winionAnimator.SetAnimationCanChange(false);
		}
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x00042594 File Offset: 0x00040794
	private int GetWinionFolderIndex(Winion folder)
	{
		int num = 0;
		switch (folder)
		{
		case Winion.Ion:
			num = 0;
			break;
		case Winion.Bo:
			num = 1;
			break;
		case Winion.Grid:
			num = 2;
			break;
		case Winion.Fix:
			num = 3;
			break;
		case Winion.Debug:
			num = 4;
			break;
		}
		return num;
	}

	// Token: 0x040008E4 RID: 2276
	public List<GameObject> food;

	// Token: 0x040008E5 RID: 2277
	public List<GameObject> food_Left;

	// Token: 0x040008E6 RID: 2278
	public List<GameObject> food_Right;

	// Token: 0x040008E7 RID: 2279
	public List<GameObject> altogether_food_Front;

	// Token: 0x040008E8 RID: 2280
	public List<GameObject> altogether_food_Back;

	// Token: 0x040008E9 RID: 2281
	public List<GameObject> bo_food_Front;

	// Token: 0x040008EA RID: 2282
	public List<GameObject> bo_food_Back;

	// Token: 0x040008EB RID: 2283
	public List<GameObject> console;

	// Token: 0x040008EC RID: 2284
	public List<GameObject> consoleLeft;

	// Token: 0x040008ED RID: 2285
	public List<GameObject> consoleRight;
}
