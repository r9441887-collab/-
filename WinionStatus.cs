using System;
using project.Scripts.CharacterScripts;
using UnityEngine;

// Token: 0x02000126 RID: 294
public class WinionStatus : MonoBehaviour, IHandler
{
	// Token: 0x170000CC RID: 204
	// (get) Token: 0x0600070D RID: 1805 RVA: 0x00012A06 File Offset: 0x00010C06
	// (set) Token: 0x0600070E RID: 1806 RVA: 0x00012A0E File Offset: 0x00010C0E
	public WinionHandler winionHandler { get; set; }

	// Token: 0x0600070F RID: 1807 RVA: 0x00012A17 File Offset: 0x00010C17
	public void ChangeFriendship(float value)
	{
		this.winionInfo.friendship += value;
		this.winionInfo.friendship = Mathf.Clamp(this.winionInfo.friendship, 0f, 125f);
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x00012A51 File Offset: 0x00010C51
	public void TempLevelUp()
	{
		this.winionInfo.winionLevel++;
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x00012A66 File Offset: 0x00010C66
	public void TempLevelDown()
	{
		this.winionInfo.winionLevel--;
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x00012A7B File Offset: 0x00010C7B
	private void Update()
	{
		this.elapsedTime += Time.deltaTime;
		if (this.elapsedTime > 60f)
		{
			this.elapsedTime = 0f;
			this.ChangeFriendship(1f);
		}
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x0003DB98 File Offset: 0x0003BD98
	public void DecreaseBattery(int amount = 1)
	{
		if (this.winionInfo.battery > 0)
		{
			if (DBManager.instance.dialogueData.runNextEvent)
			{
				return;
			}
			this.winionInfo.battery -= amount;
			if (this.winionInfo.battery <= 0)
			{
				this.winionInfo.isDischarged = true;
				this.winionInfo.battery = 0;
				SystemBox.Instance.Show(new MessageConfig(DBManager.instance.GetSettingString("메세지박스", 0, 2, 0)), SystemBox.MessageType.Default, false, 4f, false, true);
				this.winionHandler.winionMovement.StopCurrentMove();
				this.winionHandler.winionMovement.SetActiveMovement(false, false, true);
				this.winionHandler.winionAnimator.PlayAnimation("Stun", false);
				this.winionHandler.winionAnimator.SetAnimationCanChange(false);
			}
		}
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x00012AB2 File Offset: 0x00010CB2
	public void EmergancyCharge()
	{
		this.winionInfo.battery = 20;
		this.winionInfo.isDischarged = false;
	}

	// Token: 0x06000715 RID: 1813 RVA: 0x0003DC7C File Offset: 0x0003BE7C
	public bool CanSleeping()
	{
		if (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - this.lastSleepTime >= 300000L)
		{
			this.CanSleep = true;
			return true;
		}
		this.CanSleep = false;
		return false;
	}

	// Token: 0x06000716 RID: 1814 RVA: 0x0003DCB8 File Offset: 0x0003BEB8
	public void SetSleeping()
	{
		this.lastSleepTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
	}

	// Token: 0x040007F5 RID: 2037
	public bool IsCharging;

	// Token: 0x040007F6 RID: 2038
	[Space]
	[Header("말풍선 위치")]
	public Transform speechBubblePos;

	// Token: 0x040007F7 RID: 2039
	public Transform speechBubbleRightPos;

	// Token: 0x040007F8 RID: 2040
	public Transform speechBubbleLeftPos;

	// Token: 0x040007F9 RID: 2041
	public WinionInfo winionInfo;

	// Token: 0x040007FA RID: 2042
	public bool changeDialogueFile;

	// Token: 0x040007FB RID: 2043
	public bool isBizit;

	// Token: 0x040007FC RID: 2044
	public bool isFriend01;

	// Token: 0x040007FD RID: 2045
	public bool isFriend02;

	// Token: 0x040007FE RID: 2046
	public bool isWinion01;

	// Token: 0x040007FF RID: 2047
	public bool isWinion02;

	// Token: 0x04000800 RID: 2048
	public bool isWatchWinion;

	// Token: 0x04000801 RID: 2049
	public bool isSystemWinion;

	// Token: 0x04000802 RID: 2050
	public bool isRescueTeam;

	// Token: 0x04000803 RID: 2051
	private float elapsedTime;

	// Token: 0x04000804 RID: 2052
	public long lastSleepTime;

	// Token: 0x04000805 RID: 2053
	public bool CanSleep;
}
