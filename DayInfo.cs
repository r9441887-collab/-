using System;
using Unity.VisualScripting;

// Token: 0x0200043C RID: 1084
public static class DayInfo
{
	// Token: 0x06001ED7 RID: 7895 RVA: 0x000DCC74 File Offset: 0x000DAE74
	public static void SetLanguage()
	{
		if (DBManager.instance != null && !UnityObjectUtility.IsUnityNull(DBManager.instance))
		{
			DayInfo.NextDay = DBManager.instance.GetSettingString("NextDay", 0, 0, 0);
			DayInfo.TwoDay = DBManager.instance.GetSettingString("NextDay", 0, 1, 0);
			DayInfo.Dawn = DBManager.instance.GetSettingString("NextDay", 0, 2, 0);
			DayInfo.AndNextDay = DBManager.instance.GetSettingString("NextDay", 0, 3, 0);
			DayInfo.FewDaysAgo = DBManager.instance.GetSettingString("NextDay", 0, 4, 0);
			DayInfo.FewDaysAfter = DBManager.instance.GetSettingString("NextDay", 0, 5, 0);
			DayInfo.MemoryRecovering = DBManager.instance.GetSettingString("NextDay", 0, 6, 0);
			DayInfo.MemoryComplete = DBManager.instance.GetSettingString("NextDay", 0, 7, 0);
			DayInfo.AutoSaving = DBManager.instance.GetSettingString("NextDay", 0, 8, 0);
			DayInfo.AfterHours = DBManager.instance.GetSettingString("NextDay", 0, 9, 0);
		}
	}

	// Token: 0x04001D1A RID: 7450
	public static string NextDay = "";

	// Token: 0x04001D1B RID: 7451
	public static string Dawn = "";

	// Token: 0x04001D1C RID: 7452
	public static string TwoDay = "";

	// Token: 0x04001D1D RID: 7453
	public static string AndNextDay = "";

	// Token: 0x04001D1E RID: 7454
	public static string FewDaysAgo = "";

	// Token: 0x04001D1F RID: 7455
	public static string FewDaysAfter = "";

	// Token: 0x04001D20 RID: 7456
	public static string MemoryRecovering = "";

	// Token: 0x04001D21 RID: 7457
	public static string MemoryComplete = "";

	// Token: 0x04001D22 RID: 7458
	public static string AutoSaving = "";

	// Token: 0x04001D23 RID: 7459
	public static string AfterHours = "";
}
