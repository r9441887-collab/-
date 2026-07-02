using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

// Token: 0x0200037A RID: 890
public class TitleTextSetting : MonoBehaviour
{
	// Token: 0x06001AA3 RID: 6819 RVA: 0x000192CF File Offset: 0x000174CF
	public void Start()
	{
		DBManager.instance.SettingContent();
		this.Title_Setting();
	}

	// Token: 0x06001AA4 RID: 6820 RVA: 0x000C4874 File Offset: 0x000C2A74
	public void Title_Setting()
	{
		this.warning_text.text = DBManager.instance.GetSettingString("경고문구", 0, 0, 0);
		this.exitWindow_title.title_Text.text = DBManager.instance.GetSettingString("나가기", 0, 0, 0);
		this.exitWindow_title.popUpText.text = DBManager.instance.GetSettingString("나가기", 0, 1, 0);
		this.exitWindow_title.yesBtn_text.text = DBManager.instance.GetSettingString("타이틀이동", 0, 3, 0);
		this.exitWindow_title.noBtn_text.text = DBManager.instance.GetSettingString("타이틀이동", 0, 4, 0);
		this.SoundSetting_Title_Text.text = DBManager.instance.GetSettingString("사운드설정", 0, 0, 0);
		this.FullSound_Text.text = DBManager.instance.GetSettingString("사운드설정", 0, 1, 0);
		this.BGMSound_Text.text = DBManager.instance.GetSettingString("사운드설정", 0, 2, 0);
		this.SFXSound_Text.text = DBManager.instance.GetSettingString("사운드설정", 0, 3, 0);
		this.Installer_text.text = DBManager.instance.GetSettingString("모드", 0, 0, 0);
		this.Installer_Already_title_text.text = DBManager.instance.GetSettingString("모드", 0, 13, 0);
		this.Installer_Already_content_text.text = DBManager.instance.GetSettingString("모드", 0, 1, 0);
		this.Installer_Already_Btn_text.text = DBManager.instance.GetSettingString("비밀번호", 0, 3, 0);
		this.readMe_content_text.text = DBManager.instance.GetSettingString("모드", 0, 2, 0);
		this.storyMode_text.text = DBManager.instance.GetSettingString("모드", 0, 12, 0);
		this.storyMode_title_text.text = DBManager.instance.GetSettingString("모드", 0, 3, 0);
		this.storyMode_content_text.text = DBManager.instance.GetSettingString("모드", 0, 4, 0);
		this.storyMode_yesBtn_text.text = DBManager.instance.GetSettingString("타이틀이동", 0, 3, 0);
		this.storyMode_noBtn_text.text = DBManager.instance.GetSettingString("타이틀이동", 0, 4, 0);
		this.desktopMode_text.text = DBManager.instance.GetSettingString("모드", 0, 5, 0);
		this.desktopMode_title_text.text = DBManager.instance.GetSettingString("모드", 0, 5, 0);
		this.desktopMode_content_text.text = DBManager.instance.GetSettingString("모드", 0, 6, 0);
		this.desktopMode_yesBtn_text.text = DBManager.instance.GetSettingString("타이틀이동", 0, 3, 0);
		this.desktopMode_noBtn_text.text = DBManager.instance.GetSettingString("타이틀이동", 0, 4, 0);
		this.nameSetting_title_text.text = DBManager.instance.GetSettingString("Start", 0, 6, 0);
		this.nameSetting_content_text.text = DBManager.instance.GetSettingString("모드", 0, 7, 0);
		this.nameSetting_InputF_text.text = DBManager.instance.GetSettingString("모드", 0, 8, 0);
		this.nameSetting_content02_text.text = DBManager.instance.GetSettingString("모드", 0, 9, 0);
		this.nameSetting_CheckBtn_text.text = DBManager.instance.GetSettingString("비밀번호", 0, 3, 0);
		this.clearData_title_text.text = DBManager.instance.GetSettingString("Start", 0, 7, 0);
		this.clearData_content00_text.text = DBManager.instance.GetSettingString("모드", 0, 10, 0);
		this.clearData_content01_text.text = DBManager.instance.GetSettingString("모드", 0, 11, 0);
		this.clearData_yesBtn_text.text = DBManager.instance.GetSettingString("타이틀이동", 0, 3, 0);
		this.clearData_noBtn_text.text = DBManager.instance.GetSettingString("타이틀이동", 0, 4, 0);
		this.SoundSetting_Title_Text_00.text = DBManager.instance.GetSettingString("사운드설정", 0, 0, 0);
		this.FullSound_Text_00.text = DBManager.instance.GetSettingString("사운드설정", 0, 1, 0);
		this.BGMSound_Text_00.text = DBManager.instance.GetSettingString("사운드설정", 0, 2, 0);
		this.SFXSound_Text_00.text = DBManager.instance.GetSettingString("사운드설정", 0, 3, 0);
		this.SoundSetting_Title_Text_00.text = DBManager.instance.GetSettingString("사운드설정", 0, 0, 0);
		this.FullSound_Text_00.text = DBManager.instance.GetSettingString("사운드설정", 0, 1, 0);
		this.BGMSound_Text_00.text = DBManager.instance.GetSettingString("사운드설정", 0, 2, 0);
		this.SFXSound_Text_00.text = DBManager.instance.GetSettingString("사운드설정", 0, 3, 0);
		string settingString = DBManager.instance.GetSettingString("Start", 0, 0, 0);
		this.start_btn_Text.DOText(settingString, 0.2f, true, ScrambleMode.None, null);
		this.goToTitle_btn_Text.text = DBManager.instance.GetSettingString("Start", 0, 4, 0);
		this.sound_btn_Text.text = DBManager.instance.GetSettingString("Start", 0, 3, 0);
		this.language_btn_Text.text = DBManager.instance.GetSettingString("Start", 0, 5, 0);
		this.text_btn_Text.text = DBManager.instance.GetSettingString("Start", 0, 2, 0);
		this.changeName_btn_Text.text = DBManager.instance.GetSettingString("Start", 0, 6, 0);
		this.InitData_btn_Text.text = DBManager.instance.GetSettingString("Start", 0, 7, 0);
		this.SettingConversation_title_Text.text = DBManager.instance.GetSettingString("Start", 0, 2, 0);
		this.TestDialoguestart_Text.text = DBManager.instance.GetSettingString("텍스트설정", 0, 0, 0);
		this.context00_Text.text = DBManager.instance.GetSettingString("텍스트설정", 0, 1, 0);
		this.context01_Text.text = DBManager.instance.GetSettingString("텍스트설정", 0, 2, 0);
		this.context02_Text.text = DBManager.instance.GetSettingString("텍스트설정", 0, 3, 0);
		this.context03_Text.text = DBManager.instance.GetSettingString("텍스트설정", 0, 4, 0);
		this.setting_InitData_btn_Text.text = DBManager.instance.GetSettingString("텍스트설정", 0, 5, 0);
		this.apply_btn_Text.text = DBManager.instance.GetSettingString("텍스트설정", 0, 6, 0);
		this.display_text.text = DBManager.instance.GetSettingString("화면설정", 0, 0, 0);
		this.display_title_text.text = DBManager.instance.GetSettingString("화면설정", 0, 0, 0);
		this.display_fullScreen_text.text = DBManager.instance.GetSettingString("화면설정", 0, 1, 0);
		this.display_WindowedScreen_text.text = DBManager.instance.GetSettingString("화면설정", 0, 2, 0);
		this.display_OKBtn_text.text = DBManager.instance.GetSettingString("비밀번호", 0, 3, 0);
		this.gallery_text.text = DBManager.instance.GetSettingString("모드", 0, 14, 0);
	}

	// Token: 0x040016F5 RID: 5877
	[Header("경고 문구 번역")]
	public TMP_Text warning_text;

	// Token: 0x040016F6 RID: 5878
	[Header("타이틀 번역")]
	public PopUpWindow exitWindow_title;

	// Token: 0x040016F7 RID: 5879
	public TMP_Text SoundSetting_Title_Text;

	// Token: 0x040016F8 RID: 5880
	public TMP_Text FullSound_Text;

	// Token: 0x040016F9 RID: 5881
	public TMP_Text BGMSound_Text;

	// Token: 0x040016FA RID: 5882
	public TMP_Text SFXSound_Text;

	// Token: 0x040016FB RID: 5883
	[Header("ingame 번역")]
	public TMP_Text Installer_text;

	// Token: 0x040016FC RID: 5884
	public TMP_Text Installer_Already_title_text;

	// Token: 0x040016FD RID: 5885
	public TMP_Text Installer_Already_content_text;

	// Token: 0x040016FE RID: 5886
	public TMP_Text Installer_Already_Btn_text;

	// Token: 0x040016FF RID: 5887
	public TMP_Text readMe_content_text;

	// Token: 0x04001700 RID: 5888
	public TMP_Text storyMode_text;

	// Token: 0x04001701 RID: 5889
	public TMP_Text storyMode_title_text;

	// Token: 0x04001702 RID: 5890
	public TMP_Text storyMode_content_text;

	// Token: 0x04001703 RID: 5891
	public TMP_Text storyMode_yesBtn_text;

	// Token: 0x04001704 RID: 5892
	public TMP_Text storyMode_noBtn_text;

	// Token: 0x04001705 RID: 5893
	public TMP_Text desktopMode_text;

	// Token: 0x04001706 RID: 5894
	public TMP_Text desktopMode_title_text;

	// Token: 0x04001707 RID: 5895
	public TMP_Text desktopMode_content_text;

	// Token: 0x04001708 RID: 5896
	public TMP_Text desktopMode_yesBtn_text;

	// Token: 0x04001709 RID: 5897
	public TMP_Text desktopMode_noBtn_text;

	// Token: 0x0400170A RID: 5898
	public TMP_Text nameSetting_title_text;

	// Token: 0x0400170B RID: 5899
	public TMP_Text nameSetting_content_text;

	// Token: 0x0400170C RID: 5900
	public TMP_Text nameSetting_InputF_text;

	// Token: 0x0400170D RID: 5901
	public TMP_Text nameSetting_content02_text;

	// Token: 0x0400170E RID: 5902
	public TMP_Text nameSetting_CheckBtn_text;

	// Token: 0x0400170F RID: 5903
	public TMP_Text clearData_title_text;

	// Token: 0x04001710 RID: 5904
	public TMP_Text clearData_content00_text;

	// Token: 0x04001711 RID: 5905
	public TMP_Text clearData_content01_text;

	// Token: 0x04001712 RID: 5906
	public TMP_Text clearData_yesBtn_text;

	// Token: 0x04001713 RID: 5907
	public TMP_Text clearData_noBtn_text;

	// Token: 0x04001714 RID: 5908
	public TMP_Text SoundSetting_Title_Text_00;

	// Token: 0x04001715 RID: 5909
	public TMP_Text FullSound_Text_00;

	// Token: 0x04001716 RID: 5910
	public TMP_Text BGMSound_Text_00;

	// Token: 0x04001717 RID: 5911
	public TMP_Text SFXSound_Text_00;

	// Token: 0x04001718 RID: 5912
	public TMP_Text start_btn_Text;

	// Token: 0x04001719 RID: 5913
	public TMP_Text goToTitle_btn_Text;

	// Token: 0x0400171A RID: 5914
	public TMP_Text sound_btn_Text;

	// Token: 0x0400171B RID: 5915
	public TMP_Text language_btn_Text;

	// Token: 0x0400171C RID: 5916
	public TMP_Text text_btn_Text;

	// Token: 0x0400171D RID: 5917
	public TMP_Text changeName_btn_Text;

	// Token: 0x0400171E RID: 5918
	public TMP_Text InitData_btn_Text;

	// Token: 0x0400171F RID: 5919
	public TMP_Text SettingConversation_title_Text;

	// Token: 0x04001720 RID: 5920
	public TMP_Text TestDialoguestart_Text;

	// Token: 0x04001721 RID: 5921
	public TMP_Text context00_Text;

	// Token: 0x04001722 RID: 5922
	public TMP_Text context01_Text;

	// Token: 0x04001723 RID: 5923
	public TMP_Text context02_Text;

	// Token: 0x04001724 RID: 5924
	public TMP_Text context03_Text;

	// Token: 0x04001725 RID: 5925
	public TMP_Text setting_InitData_btn_Text;

	// Token: 0x04001726 RID: 5926
	public TMP_Text apply_btn_Text;

	// Token: 0x04001727 RID: 5927
	public TMP_Text display_text;

	// Token: 0x04001728 RID: 5928
	public TMP_Text display_title_text;

	// Token: 0x04001729 RID: 5929
	public TMP_Text display_fullScreen_text;

	// Token: 0x0400172A RID: 5930
	public TMP_Text display_WindowedScreen_text;

	// Token: 0x0400172B RID: 5931
	public TMP_Text display_OKBtn_text;

	// Token: 0x0400172C RID: 5932
	public TMP_Text gallery_text;
}
