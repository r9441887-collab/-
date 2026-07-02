using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Token: 0x02000127 RID: 295
public class DebugWinion : SingletoneBehaviour<DebugWinion>
{
	// Token: 0x06000718 RID: 1816 RVA: 0x00012ACD File Offset: 0x00010CCD
	private void Start()
	{
		this.handlers = GameManager.instance.GetWinionHandlers();
		this.WinionDebugSetting();
	}

	// Token: 0x06000719 RID: 1817 RVA: 0x0003DCD8 File Offset: 0x0003BED8
	public void WinionDebugSetting()
	{
		if (this.handlers.Count != 0)
		{
			for (int i = 0; i < this.handlers.Count; i++)
			{
				if (this.winionList != null)
				{
					GameObject gameObject = Object.Instantiate<GameObject>(this.debugPrefab, this.winionList);
					WinionHandler winionHandler = this.handlers[i];
					WinionDataForDebug component = gameObject.GetComponent<WinionDataForDebug>();
					component.winionContents.NameText.text = winionHandler.transform.name;
					component.winionContents.LevelText.text = "Level " + winionHandler.winionStatus.winionInfo.winionLevel.ToString();
					component.handler = winionHandler;
				}
			}
		}
	}

	// Token: 0x0600071A RID: 1818 RVA: 0x0003DD94 File Offset: 0x0003BF94
	public void SetWinionDebugCenter(WinionDataForDebug wData)
	{
		WinionHandler handler = wData.handler;
		this.NameDetail.text = handler.transform.name;
		this.LevelDetail.text = "레벨 " + handler.winionStatus.winionInfo.winionLevel.ToString();
		this.FriendShipDetail.text = "친밀도 " + handler.winionStatus.winionInfo.friendship.ToString();
		this.targetHandler = handler;
	}

	// Token: 0x0600071B RID: 1819 RVA: 0x00012AE5 File Offset: 0x00010CE5
	public void Button4()
	{
		this.targetHandler == null;
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x00012AF4 File Offset: 0x00010CF4
	public void LevelUpBtn()
	{
		if (this.targetHandler == null)
		{
			return;
		}
		this.targetHandler.winionStatus.TempLevelUp();
		this.WinionDebugSetting();
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x00012B1B File Offset: 0x00010D1B
	public void LevelDownBtn()
	{
		if (this.targetHandler == null)
		{
			return;
		}
		this.targetHandler.winionStatus.TempLevelDown();
		this.WinionDebugSetting();
	}

	// Token: 0x04000806 RID: 2054
	public GameObject debugPrefab;

	// Token: 0x04000807 RID: 2055
	public List<WinionHandler> handlers;

	// Token: 0x04000808 RID: 2056
	public Transform winionList;

	// Token: 0x04000809 RID: 2057
	public TextMeshProUGUI NameDetail;

	// Token: 0x0400080A RID: 2058
	public TextMeshProUGUI LevelDetail;

	// Token: 0x0400080B RID: 2059
	public TextMeshProUGUI FriendShipDetail;

	// Token: 0x0400080C RID: 2060
	public WinionHandler targetHandler;
}
