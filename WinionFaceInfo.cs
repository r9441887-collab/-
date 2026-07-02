using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000382 RID: 898
[Serializable]
public class WinionFaceInfo
{
	// Token: 0x06001AB8 RID: 6840 RVA: 0x000C5500 File Offset: 0x000C3700
	public void InRecycleBin_FaceSetting(Winion winion, bool _inRecycleBin)
	{
		if (winion == Winion.Ion)
		{
			this.Ion_inRecycleBin = _inRecycleBin;
			this.Ion_RecycleBinFilter.SetActive(_inRecycleBin);
			return;
		}
		if (winion == Winion.Bo)
		{
			this.Bo_inRecycleBin = _inRecycleBin;
			this.Bo_RecycleBinFilter.SetActive(_inRecycleBin);
			return;
		}
		if (winion == Winion.Grid)
		{
			this.Grid_inRecycleBin = _inRecycleBin;
			this.Grid_RecycleBinFilter.SetActive(_inRecycleBin);
		}
	}

	// Token: 0x06001AB9 RID: 6841 RVA: 0x000C5554 File Offset: 0x000C3754
	public void InSystemWinionRoom_FaceSetting(Winion winion, bool _inSystemWinionRoom)
	{
		if (winion == Winion.Ion)
		{
			if (_inSystemWinionRoom)
			{
				SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SettingUnderGrident("#989898");
			}
			else
			{
				SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SettingUnderGrident("#FFFFFF");
			}
			this.Ion_inSystemWinionRoom = _inSystemWinionRoom;
			this.Ion_inSystemWinionRoomFilter.SetActive(_inSystemWinionRoom);
			return;
		}
		if (winion == Winion.Bo)
		{
			if (_inSystemWinionRoom)
			{
				SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SettingUnderGrident("#989898");
			}
			else
			{
				SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SettingUnderGrident("#FFFFFF");
				SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SetUIGradient("#FECEFD", "#FFFFFF");
			}
			this.Bo_inSystemWinionRoom = _inSystemWinionRoom;
			this.Bo_inSystemWinionRoomFilter.SetActive(_inSystemWinionRoom);
			return;
		}
		if (winion == Winion.Grid)
		{
			if (_inSystemWinionRoom)
			{
				SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Grid).GetComponent<WinionFace>().SettingUnderGrident("#989898");
			}
			else
			{
				SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Grid).GetComponent<WinionFace>().SettingUnderGrident("#FFFFFF");
				SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Grid).GetComponent<WinionFace>().SetUIGradient("#55BE7C", "#FFFFFF");
			}
			this.Grid_inSystemWinionRoom = _inSystemWinionRoom;
			this.Grid_inSystemWinionRoomFilter.SetActive(_inSystemWinionRoom);
			return;
		}
		if (winion == Winion.Fix)
		{
			if (_inSystemWinionRoom)
			{
				SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Fix).GetComponent<WinionFace>().SettingUnderGrident("#989898");
			}
			else
			{
				SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Fix).GetComponent<WinionFace>().SettingUnderGrident("#FFFFFF");
			}
			this.Fix_inSystemWinionRoom = _inSystemWinionRoom;
			this.Fix_inSystemWinionRoomFilter.SetActive(_inSystemWinionRoom);
			return;
		}
		if (winion == Winion.Debug)
		{
			if (_inSystemWinionRoom)
			{
				SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SettingUnderGrident("#989898");
			}
			else
			{
				SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SettingUnderGrident("#FFFFFF");
			}
			this.Debug_inSystemWinionRoom = _inSystemWinionRoom;
			this.Debug_inSystemWinionRoomFilter.SetActive(_inSystemWinionRoom);
		}
	}

	// Token: 0x06001ABA RID: 6842 RVA: 0x000C5740 File Offset: 0x000C3940
	public void ReSettingFace(Winion winion)
	{
		switch (winion)
		{
		case Winion.Ion:
		{
			Sprite sprite = this.Ion_Setting_Sprite("[아이온-무표정]");
			if (!DBManager.instance.is3DScene)
			{
				GameManager.instance.gameData.ION.winionAnimator.FaceName = "[아이온-무표정]";
				GameManager.instance.gameData.ION.winionAnimator.ResetCurAnimation();
			}
			SingletoneBehaviour<IconManager>.Instance.SetWinionFace(Icon.Face_Ion, sprite, false, "");
			return;
		}
		case Winion.Bo:
		{
			Sprite sprite = this.Bo_Setting_Sprite("[보-무표정]");
			if (!DBManager.instance.is3DScene)
			{
				GameManager.instance.gameData.Bo.winionAnimator.FaceName = "[보-무표정]";
				GameManager.instance.gameData.Bo.winionAnimator.ResetCurAnimation();
			}
			SingletoneBehaviour<IconManager>.Instance.SetWinionFace(Icon.Face_Bo, sprite, false, "");
			return;
		}
		case Winion.Grid:
		{
			Sprite sprite = this.Grid_Setting_Sprite("[그리드-무표정]");
			if (!DBManager.instance.is3DScene)
			{
				GameManager.instance.gameData.Grid.winionAnimator.FaceName = "[그리드-무표정]";
				GameManager.instance.gameData.Grid.winionAnimator.ResetCurAnimation();
			}
			SingletoneBehaviour<IconManager>.Instance.SetWinionFace(Icon.Face_Grid, sprite, false, "");
			return;
		}
		case Winion.Fix:
		{
			Sprite sprite = this.Fix_Setting_Sprite("[픽스-무표정]");
			if (!DBManager.instance.is3DScene)
			{
				GameManager.instance.gameData.Fix.winionAnimator.FaceName = "[픽스-무표정]";
				GameManager.instance.gameData.Fix.winionAnimator.ResetCurAnimation();
			}
			SingletoneBehaviour<IconManager>.Instance.SetWinionFace(Icon.Face_Fix, sprite, false, "");
			return;
		}
		case Winion.Debug:
		{
			Sprite sprite = this.Debug_Setting_Sprite("[디버그-무표정]");
			if (!DBManager.instance.is3DScene)
			{
				GameManager.instance.gameData.Debug.winionAnimator.FaceName = "[디버그-무표정]";
				GameManager.instance.gameData.Debug.winionAnimator.ResetCurAnimation();
			}
			SingletoneBehaviour<IconManager>.Instance.SetWinionFace(Icon.Face_Debug, sprite, false, "");
			return;
		}
		default:
			return;
		}
	}

	// Token: 0x06001ABB RID: 6843 RVA: 0x000C5958 File Offset: 0x000C3B58
	public void SettingFaceWindow(string emotionString)
	{
		Winion winion = this.CheckString(emotionString);
		emotionString = emotionString.Trim();
		switch (winion)
		{
		case Winion.Ion:
		{
			Sprite sprite = this.Ion_Setting_Sprite(emotionString);
			SingletoneBehaviour<IconManager>.Instance.SetWinionFace(Icon.Face_Ion, sprite, false, "");
			if (!DBManager.instance.is3DScene)
			{
				GameManager.instance.gameData.ION.winionAnimator.FaceName = emotionString;
				GameManager.instance.gameData.ION.winionAnimator.PlayCurAnimation();
				return;
			}
			break;
		}
		case Winion.Bo:
		{
			Sprite sprite = this.Bo_Setting_Sprite(emotionString);
			SingletoneBehaviour<IconManager>.Instance.SetWinionFace(Icon.Face_Bo, sprite, false, "");
			if (!DBManager.instance.is3DScene)
			{
				GameManager.instance.gameData.Bo.winionAnimator.FaceName = emotionString;
				GameManager.instance.gameData.Bo.winionAnimator.PlayCurAnimation();
				return;
			}
			break;
		}
		case Winion.Grid:
		{
			Sprite sprite = this.Grid_Setting_Sprite(emotionString);
			SingletoneBehaviour<IconManager>.Instance.SetWinionFace(Icon.Face_Grid, sprite, false, "");
			if (!DBManager.instance.is3DScene)
			{
				GameManager.instance.gameData.Grid.winionAnimator.FaceName = emotionString;
				GameManager.instance.gameData.Grid.winionAnimator.PlayCurAnimation();
				return;
			}
			break;
		}
		case Winion.Fix:
			if (!this.fixReSetAnim)
			{
				this.fixReSetAnim = true;
			}
			if (emotionString == "[픽스-유리조각_광기 눈깔 애니]")
			{
				Sprite sprite = this.Fix_Setting_Sprite(emotionString);
				SingletoneBehaviour<IconManager>.Instance.SetWinionFace(Icon.Face_Fix, sprite, false, emotionString);
			}
			else
			{
				Sprite sprite = this.Fix_Setting_Sprite(emotionString);
				SingletoneBehaviour<IconManager>.Instance.SetWinionFace(Icon.Face_Fix, sprite, false, "");
			}
			if (!DBManager.instance.is3DScene)
			{
				GameManager.instance.gameData.Fix.winionAnimator.FaceName = emotionString;
				GameManager.instance.gameData.Fix.winionAnimator.PlayCurAnimation();
				return;
			}
			break;
		case Winion.Debug:
		{
			Sprite sprite = this.Debug_Setting_Sprite(emotionString);
			SingletoneBehaviour<IconManager>.Instance.SetWinionFace(Icon.Face_Debug, sprite, false, "");
			if (!DBManager.instance.is3DScene)
			{
				GameManager.instance.gameData.Debug.winionAnimator.FaceName = emotionString;
				GameManager.instance.gameData.Debug.winionAnimator.PlayCurAnimation();
			}
			break;
		}
		default:
			return;
		}
	}

	// Token: 0x06001ABC RID: 6844 RVA: 0x000C5B9C File Offset: 0x000C3D9C
	public static Texture2D textureFromSprite(Sprite sprite)
	{
		if (sprite.rect.width != (float)sprite.texture.width)
		{
			Texture2D texture2D = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
			Color[] pixels = sprite.texture.GetPixels((int)sprite.textureRect.x, (int)sprite.textureRect.y, (int)sprite.textureRect.width, (int)sprite.textureRect.height);
			texture2D.SetPixels(pixels);
			texture2D.Apply();
			return texture2D;
		}
		return sprite.texture;
	}

	// Token: 0x06001ABD RID: 6845 RVA: 0x000C5C44 File Offset: 0x000C3E44
	public void SettingFaceWindow_3DGame(MeshRenderer _image, string emotionString)
	{
		Sprite sprite = null;
		Winion winion = this.CheckString(emotionString);
		emotionString = emotionString.Trim();
		switch (winion)
		{
		case Winion.Bo:
			sprite = this.Bo_Setting_Sprite(emotionString);
			break;
		case Winion.Grid:
			sprite = this.Grid_Setting_Sprite(emotionString);
			break;
		case Winion.Debug:
			sprite = this.Debug_Setting_Sprite(emotionString);
			break;
		}
		_image.material.mainTexture = WinionFaceInfo.textureFromSprite(sprite);
	}

	// Token: 0x06001ABE RID: 6846 RVA: 0x000C5CAC File Offset: 0x000C3EAC
	public Sprite Ion_Setting_Sprite(string emotionString)
	{
		Sprite sprite = this.IonFaceSprites[0];
		uint num = <PrivateImplementationDetails>.ComputeStringHash(emotionString);
		if (num <= 1941629204U)
		{
			if (num <= 1090515110U)
			{
				if (num <= 413253463U)
				{
					if (num <= 293654250U)
					{
						if (num != 34415898U)
						{
							if (num == 293654250U)
							{
								if (emotionString == "[아이온-머리채]")
								{
									SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#FF8159", "#000000");
									sprite = this.IonFaceSprites[12];
									goto IL_0E51;
								}
							}
						}
						else if (emotionString == "[아이온-맞음2]")
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#00146D", "#848484");
							sprite = this.IonFaceSprites[8];
							goto IL_0E51;
						}
					}
					else if (num != 382595016U)
					{
						if (num != 401476537U)
						{
							if (num == 413253463U)
							{
								if (emotionString == "[아이온- 공허 걱정 손모음]")
								{
									SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#79491D", "#2F2F2F");
									sprite = this.IonFaceSprites[23];
									goto IL_0E51;
								}
							}
						}
						else if (emotionString == "[아이온- 공허 눈물]")
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#79491D", "#000000");
							sprite = this.IonFaceSprites[19];
							goto IL_0E51;
						}
					}
					else if (emotionString == "[아이온-다침]")
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#FF8159", "#000000");
						sprite = this.IonFaceSprites[14];
						goto IL_0E51;
					}
				}
				else if (num <= 779379525U)
				{
					if (num != 490532168U)
					{
						if (num == 779379525U)
						{
							if (emotionString == "[아이온-머리뜯김]")
							{
								sprite = this.IonFaceSprites[13];
								goto IL_0E51;
							}
						}
					}
					else if (emotionString == "[아이온-울음]")
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#90781D", "#FFFFFF");
						sprite = this.IonFaceSprites[5];
						goto IL_0E51;
					}
				}
				else if (num != 911609960U)
				{
					if (num != 955039230U)
					{
						if (num == 1090515110U)
						{
							if (emotionString == "[아이온-눈찡긋]")
							{
								SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#FEED8C", "#FFFFFF");
								sprite = this.IonFaceSprites[11];
								goto IL_0E51;
							}
						}
					}
					else if (emotionString == "[아이온-다침_공포]")
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#004299", "#000000");
						sprite = this.IonFaceSprites[16];
						goto IL_0E51;
					}
				}
				else if (emotionString == "[비지트- 눈깔음]")
				{
					SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#58A9A2", "#FFFFFF");
					sprite = this.IonFaceSprites[27];
					goto IL_0E51;
				}
			}
			else if (num <= 1447747712U)
			{
				if (num <= 1259945317U)
				{
					if (num != 1122863828U)
					{
						if (num == 1259945317U)
						{
							if (emotionString == "[비지트-눈물 광기]")
							{
								SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#710F00", "#000000");
								sprite = this.IonFaceSprites[40];
								goto IL_0E51;
							}
						}
					}
					else if (emotionString == "[비지트-눈 감음]")
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#005671", "#000000");
						sprite = this.IonFaceSprites[39];
						goto IL_0E51;
					}
				}
				else if (num != 1263751283U)
				{
					if (num != 1411817176U)
					{
						if (num == 1447747712U)
						{
							if (emotionString == "[비지트-눈물고임]")
							{
								SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#005671", "#FFFFFF");
								sprite = this.IonFaceSprites[36];
								goto IL_0E51;
							}
						}
					}
					else if (emotionString == "[아이온-안타까움]")
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#ECD96D", "#FFFFFF");
						sprite = this.IonFaceSprites[4];
						goto IL_0E51;
					}
				}
				else if (emotionString == "[비지트-눈물고임_2]")
				{
					SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#005671", "#000000");
					sprite = this.IonFaceSprites[37];
					goto IL_0E51;
				}
			}
			else if (num <= 1766801077U)
			{
				if (num != 1558529031U)
				{
					if (num != 1722276922U)
					{
						if (num == 1766801077U)
						{
							if (emotionString == "[아이온-맞음2공포]")
							{
								sprite = this.IonFaceSprites[9];
								goto IL_0E51;
							}
						}
					}
					else if (emotionString == "[아이온-맞음]")
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#00146D", "#848484");
						sprite = this.IonFaceSprites[7];
						goto IL_0E51;
					}
				}
				else if (emotionString == "[비지트-무표정]")
				{
					SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#76E6DD", "#FFFFFF");
					sprite = this.IonFaceSprites[26];
					goto IL_0E51;
				}
			}
			else if (num != 1810660204U)
			{
				if (num != 1918296435U)
				{
					if (num == 1941629204U)
					{
						if (emotionString == "[아이온-화냄]")
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#FF603E", "#FFFFFF");
							sprite = this.IonFaceSprites[3];
							goto IL_0E51;
						}
					}
				}
				else if (emotionString == "[비지트-광기]")
				{
					SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#710F00", "#000000");
					sprite = this.IonFaceSprites[41];
					goto IL_0E51;
				}
			}
			else if (emotionString == "[비지트-화남]")
			{
				SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#30FFEE", "#000000");
				sprite = this.IonFaceSprites[34];
				goto IL_0E51;
			}
		}
		else if (num <= 2900794582U)
		{
			if (num <= 2241088671U)
			{
				if (num <= 2108498250U)
				{
					if (num != 1966585292U)
					{
						if (num == 2108498250U)
						{
							if (emotionString == "[아이온-충격]")
							{
								sprite = this.IonFaceSprites[25];
								goto IL_0E51;
							}
						}
					}
					else if (emotionString == "[아이온-다침_무표정]")
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#FF8159", "#000000");
						sprite = this.IonFaceSprites[17];
						goto IL_0E51;
					}
				}
				else if (num != 2133805773U)
				{
					if (num != 2228638371U)
					{
						if (num == 2241088671U)
						{
							if (emotionString == "[아이온-맞음2_눈물]")
							{
								sprite = this.IonFaceSprites[10];
								goto IL_0E51;
							}
						}
					}
					else if (emotionString == "[아이온- 공허 걱정]")
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#79491D", "#2F2F2F");
						sprite = this.IonFaceSprites[20];
						goto IL_0E51;
					}
				}
				else if (emotionString == "[비지트-웃음]")
				{
					SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#76E6DD", "#FFFFFF");
					sprite = this.IonFaceSprites[28];
					goto IL_0E51;
				}
			}
			else if (num <= 2455554206U)
			{
				if (num != 2313267977U)
				{
					if (num == 2455554206U)
					{
						if (emotionString == "[아이온-호기심]")
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#FEED8C", "#FFFFFF");
							sprite = this.IonFaceSprites[2];
							goto IL_0E51;
						}
					}
				}
				else if (emotionString == "[아이온-웃음]")
				{
					if (DBManager.instance.dialogueData.curEventNum == 9 && this.Returned_Ion)
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#FEED8C", "#FFFFFF");
					}
					else if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && DBManager.instance.dialogueData.curEventNum >= 5 && DBManager.instance.dialogueData.curEventNum <= 9)
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#004299", "#2F2F2F");
					}
					else
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#FEED8C", "#FFFFFF");
					}
					sprite = this.IonFaceSprites[1];
					goto IL_0E51;
				}
			}
			else if (num != 2497620790U)
			{
				if (num != 2500731148U)
				{
					if (num == 2900794582U)
					{
						if (emotionString == "[비지트-아쉬움]")
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#58A9A2", "#FFFFFF");
							sprite = this.IonFaceSprites[29];
							goto IL_0E51;
						}
					}
				}
				else if (emotionString == "[비지트-시선회피]")
				{
					SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#005671", "#FFFFFF");
					sprite = this.IonFaceSprites[35];
					goto IL_0E51;
				}
			}
			else if (emotionString == "[비지트-충격]")
			{
				SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#30FFEE", "#000000");
				sprite = this.IonFaceSprites[32];
				goto IL_0E51;
			}
		}
		else if (num <= 3426673660U)
		{
			if (num <= 3275600228U)
			{
				if (num != 3215730581U)
				{
					if (num == 3275600228U)
					{
						if (emotionString == "[비지트-귀여운충격]")
						{
							sprite = this.IonFaceSprites[30];
							goto IL_0E51;
						}
					}
				}
				else if (emotionString == "[아이온-다침_눈물]")
				{
					SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#FF8159", "#000000");
					sprite = this.IonFaceSprites[15];
					goto IL_0E51;
				}
			}
			else if (num != 3328517432U)
			{
				if (num != 3377504123U)
				{
					if (num == 3426673660U)
					{
						if (emotionString == "[아이온- 공허 무표정 손모음]")
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#79491D", "#2F2F2F");
							sprite = this.IonFaceSprites[21];
							goto IL_0E51;
						}
					}
				}
				else if (emotionString == "[아이온-무표정]")
				{
					SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#FEED8C", "#FFFFFF");
					sprite = this.IonFaceSprites[0];
					goto IL_0E51;
				}
			}
			else if (emotionString == "[아이온- 공허 무표정]")
			{
				SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#79491D", "#2F2F2F");
				sprite = this.IonFaceSprites[18];
				goto IL_0E51;
			}
		}
		else if (num <= 3825581691U)
		{
			if (num != 3461521562U)
			{
				if (num != 3673560469U)
				{
					if (num == 3825581691U)
					{
						if (emotionString == "[아이온-하얗게 질림]")
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#695222", "#FFFFFF");
							sprite = this.IonFaceSprites[6];
							goto IL_0E51;
						}
					}
				}
				else if (emotionString == "[아이온- 공허 눈물 손모음]")
				{
					SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#79491D", "#000000");
					sprite = this.IonFaceSprites[22];
					goto IL_0E51;
				}
			}
			else if (emotionString == "[아이온-공허웃음]")
			{
				SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#79491D", "#2F2F2F");
				sprite = this.IonFaceSprites[24];
				goto IL_0E51;
			}
		}
		else if (num != 3975164550U)
		{
			if (num != 4088207036U)
			{
				if (num == 4111464026U)
				{
					if (emotionString == "[비지트-호기심]")
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#30FFEE", "#FFFFFF");
						sprite = this.IonFaceSprites[31];
						goto IL_0E51;
					}
				}
			}
			else if (emotionString == "[비지트-눈물줄줄]")
			{
				SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#005671", "#000000");
				sprite = this.IonFaceSprites[38];
				goto IL_0E51;
			}
		}
		else if (emotionString == "[비지트-경악]")
		{
			sprite = this.IonFaceSprites[33];
			goto IL_0E51;
		}
		sprite = this.IonFaceSprites[0];
		IL_0E51:
		if (this.Ion_inRecycleBin)
		{
			SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#737A7C", "#3F3F3F");
		}
		if (this.Ion_inSystemWinionRoom)
		{
			SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Ion).GetComponent<WinionFace>().SetUIGradient("#643750", "#1E1616");
		}
		return sprite;
	}

	// Token: 0x06001ABF RID: 6847 RVA: 0x000C6B5C File Offset: 0x000C4D5C
	public Sprite Bo_Setting_Sprite(string emotionString)
	{
		Sprite sprite = this.BoFaceSprites[0];
		uint num = <PrivateImplementationDetails>.ComputeStringHash(emotionString);
		if (num <= 1872271854U)
		{
			if (num <= 821222068U)
			{
				if (num != 139354055U)
				{
					if (num != 528643784U)
					{
						if (num == 821222068U)
						{
							if (emotionString == "[보-안타까움]")
							{
								if (!DBManager.instance.is3DScene)
								{
									if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && (DBManager.instance.dialogueData.curEventNum == 5 || DBManager.instance.dialogueData.curEventNum == 11 || DBManager.instance.dialogueData.curEventNum == 26 || DBManager.instance.dialogueData.curEventNum == 27))
									{
										SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SetUIGradient("#D7A9D6", "#000000");
									}
									else if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter03 && (DBManager.instance.dialogueData.curEventNum == 0 || DBManager.instance.dialogueData.curEventNum == 1))
									{
										SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SetUIGradient("#D7A9D6", "#000000");
									}
									else
									{
										SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SetUIGradient("#D8AAFE", "#FFFFFF");
									}
								}
								sprite = this.BoFaceSprites[4];
								goto IL_0978;
							}
						}
					}
					else if (emotionString == "[보-눈물고임]")
					{
						if (!DBManager.instance.is3DScene)
						{
							if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && (DBManager.instance.dialogueData.curEventNum == 5 || DBManager.instance.dialogueData.curEventNum == 11 || DBManager.instance.dialogueData.curEventNum == 26 || DBManager.instance.dialogueData.curEventNum == 27))
							{
								SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SetUIGradient("#D7A9D6", "#000000");
							}
							else if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter03 && (DBManager.instance.dialogueData.curEventNum == 0 || DBManager.instance.dialogueData.curEventNum == 1))
							{
								SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SetUIGradient("#D7A9D6", "#000000");
							}
							else
							{
								SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SetUIGradient("#8C6FA5", "#373737");
							}
						}
						sprite = this.BoFaceSprites[5];
						goto IL_0978;
					}
				}
				else if (emotionString == "[보-하얗게 질림]")
				{
					if (!DBManager.instance.is3DScene)
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SetUIGradient("#726EA5", "#373737");
					}
					sprite = this.BoFaceSprites[7];
					goto IL_0978;
				}
			}
			else if (num <= 1253372810U)
			{
				if (num != 1234029278U)
				{
					if (num == 1253372810U)
					{
						if (emotionString == "[감시위니언-맞음]")
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SetUIGradient("#000000", "#7C0000");
							sprite = this.BoFaceSprites[13];
							goto IL_0978;
						}
					}
				}
				else if (emotionString == "[보-충격]")
				{
					if (!DBManager.instance.is3DScene)
					{
						sprite = this.BoFaceSprites[9];
						goto IL_0978;
					}
					goto IL_0978;
				}
			}
			else if (num != 1714410639U)
			{
				if (num == 1872271854U)
				{
					if (emotionString == "[보-뺨 맞음]")
					{
						if (!DBManager.instance.is3DScene)
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SetUIGradient("#8C6FA5", "#373737");
						}
						sprite = this.BoFaceSprites[8];
						goto IL_0978;
					}
				}
			}
			else if (emotionString == "[감시위니언-비웃음]")
			{
				SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SetUIGradient("#000000", "#7C0000");
				sprite = this.BoFaceSprites[12];
				goto IL_0978;
			}
		}
		else if (num <= 3164493252U)
		{
			if (num != 2187869541U)
			{
				if (num != 3082489000U)
				{
					if (num == 3164493252U)
					{
						if (emotionString == "[보-울음]")
						{
							if (!DBManager.instance.is3DScene)
							{
								if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && (DBManager.instance.dialogueData.curEventNum == 5 || DBManager.instance.dialogueData.curEventNum == 11 || DBManager.instance.dialogueData.curEventNum == 26 || DBManager.instance.dialogueData.curEventNum == 27))
								{
									SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SetUIGradient("#D7A9D6", "#000000");
								}
								else if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter03 && (DBManager.instance.dialogueData.curEventNum == 0 || DBManager.instance.dialogueData.curEventNum == 1))
								{
									SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SetUIGradient("#D7A9D6", "#000000");
								}
								else
								{
									SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SetUIGradient("#8C6FA5", "#373737");
								}
							}
							sprite = this.BoFaceSprites[6];
							goto IL_0978;
						}
					}
				}
				else if (emotionString == "[보-화냄]")
				{
					if (!DBManager.instance.is3DScene)
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SetUIGradient("#C064BE", "#FFFFFF");
					}
					sprite = this.BoFaceSprites[3];
					goto IL_0978;
				}
			}
			else if (emotionString == "[보-웃음]")
			{
				if (!DBManager.instance.is3DScene)
				{
					if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && (DBManager.instance.dialogueData.curEventNum == 5 || DBManager.instance.dialogueData.curEventNum == 11 || DBManager.instance.dialogueData.curEventNum == 26 || DBManager.instance.dialogueData.curEventNum == 27))
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SetUIGradient("#D7A9D6", "#000000");
					}
					else if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter03 && (DBManager.instance.dialogueData.curEventNum == 0 || DBManager.instance.dialogueData.curEventNum == 1))
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SetUIGradient("#D7A9D6", "#000000");
					}
					else
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SetUIGradient("#FECEFD", "#FFFFFF");
					}
				}
				sprite = this.BoFaceSprites[1];
				goto IL_0978;
			}
		}
		else if (num <= 3712276943U)
		{
			if (num != 3587854379U)
			{
				if (num == 3712276943U)
				{
					if (emotionString == "[보-먹음]")
					{
						if (!DBManager.instance.is3DScene)
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SetUIGradient("#FECEFD", "#FFFFFF");
						}
						sprite = this.BoFaceSprites[2];
						goto IL_0978;
					}
				}
			}
			else if (emotionString == "[감시위니언-무표정]")
			{
				SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SetUIGradient("#000000", "#7C0000");
				sprite = this.BoFaceSprites[11];
				goto IL_0978;
			}
		}
		else if (num != 3848150700U)
		{
			if (num == 3891343567U)
			{
				if (emotionString == "[보-무표정]")
				{
					if (!DBManager.instance.is3DScene)
					{
						if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && (DBManager.instance.dialogueData.curEventNum == 5 || DBManager.instance.dialogueData.curEventNum == 11 || DBManager.instance.dialogueData.curEventNum == 26 || DBManager.instance.dialogueData.curEventNum == 27))
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SetUIGradient("#D7A9D6", "#000000");
						}
						else if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter03 && (DBManager.instance.dialogueData.curEventNum == 0 || DBManager.instance.dialogueData.curEventNum == 1))
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SetUIGradient("#D7A9D6", "#000000");
						}
						else
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SetUIGradient("#FECEFD", "#FFFFFF");
						}
					}
					sprite = this.BoFaceSprites[0];
					goto IL_0978;
				}
			}
		}
		else if (emotionString == "[보-하얗게 질림_눈물]")
		{
			if (!DBManager.instance.is3DScene)
			{
				SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SetUIGradient("#726EA5", "#373737");
			}
			sprite = this.BoFaceSprites[10];
			goto IL_0978;
		}
		sprite = this.BoFaceSprites[0];
		IL_0978:
		if (this.Bo_inRecycleBin)
		{
			SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SetUIGradient("#737A7C", "#3F3F3F");
		}
		if (this.Bo_inSystemWinionRoom)
		{
			SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Bo).GetComponent<WinionFace>().SetUIGradient("#643750", "#1E1616");
		}
		return sprite;
	}

	// Token: 0x06001AC0 RID: 6848 RVA: 0x000C7534 File Offset: 0x000C5734
	public Sprite Grid_Setting_Sprite(string emotionString)
	{
		Sprite sprite = this.GridFaceSprites[0];
		uint num = <PrivateImplementationDetails>.ComputeStringHash(emotionString);
		if (num <= 1002855899U)
		{
			if (num <= 276674932U)
			{
				if (num != 11985199U)
				{
					if (num != 249653631U)
					{
						if (num == 276674932U)
						{
							if (emotionString == "[그리드-아픔]")
							{
								if (!DBManager.instance.is3DScene)
								{
									SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Grid).GetComponent<WinionFace>().SetUIGradient("#55BE7C", "#000000");
								}
								sprite = this.GridFaceSprites[11];
								goto IL_0648;
							}
						}
					}
					else if (emotionString == "[그리드-하얗게 질리고 눈물]")
					{
						sprite = this.GridFaceSprites[5];
						goto IL_0648;
					}
				}
				else if (emotionString == "[그리드-붉은얼굴]")
				{
					if (!DBManager.instance.is3DScene)
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Grid).GetComponent<WinionFace>().SetUIGradient("#E2FF41", "#FFFFFF");
					}
					sprite = this.GridFaceSprites[10];
					goto IL_0648;
				}
			}
			else if (num != 772946923U)
			{
				if (num != 917050587U)
				{
					if (num == 1002855899U)
					{
						if (emotionString == "[그리드-하얗게 질림]")
						{
							sprite = this.GridFaceSprites[4];
							goto IL_0648;
						}
					}
				}
				else if (emotionString == "[그리드-무표정]")
				{
					if (!DBManager.instance.is3DScene)
					{
						if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && (DBManager.instance.dialogueData.curEventNum == 5 || DBManager.instance.dialogueData.curEventNum == 26 || DBManager.instance.dialogueData.curEventNum == 27))
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Grid).GetComponent<WinionFace>().SetUIGradient("#55BE7C", "#000000");
						}
						else if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter03 && (DBManager.instance.dialogueData.curEventNum == 0 || DBManager.instance.dialogueData.curEventNum == 1))
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Grid).GetComponent<WinionFace>().SetUIGradient("#55BE7C", "#000000");
						}
						else
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Grid).GetComponent<WinionFace>().SetUIGradient("#55BE7C", "#FFFFFF");
						}
					}
					sprite = this.GridFaceSprites[0];
					goto IL_0648;
				}
			}
			else if (emotionString == "[그리드-하얗게 질리고 눈물흘림]")
			{
				sprite = this.GridFaceSprites[7];
				goto IL_0648;
			}
		}
		else if (num <= 2337514679U)
		{
			if (num != 1040031103U)
			{
				if (num != 1604861699U)
				{
					if (num == 2337514679U)
					{
						if (emotionString == "[그리드-하얗게 질리고 눈깔기]")
						{
							sprite = this.GridFaceSprites[6];
							goto IL_0648;
						}
					}
				}
				else if (emotionString == "[그리드-홍조]")
				{
					if (!DBManager.instance.is3DScene)
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Grid).GetComponent<WinionFace>().SetUIGradient("#E2FF41", "#FFFFFF");
					}
					sprite = this.GridFaceSprites[2];
					goto IL_0648;
				}
			}
			else if (emotionString == "[그리드-홍조놀람]")
			{
				if (!DBManager.instance.is3DScene)
				{
					SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Grid).GetComponent<WinionFace>().SetUIGradient("#E2FF41", "#FFFFFF");
				}
				sprite = this.GridFaceSprites[9];
				goto IL_0648;
			}
		}
		else if (num != 2395291488U)
		{
			if (num != 3425339906U)
			{
				if (num == 4136517539U)
				{
					if (emotionString == "[그리드-찡그림]")
					{
						if (!DBManager.instance.is3DScene)
						{
							if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && (DBManager.instance.dialogueData.curEventNum == 5 || DBManager.instance.dialogueData.curEventNum == 26 || DBManager.instance.dialogueData.curEventNum == 27))
							{
								SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Grid).GetComponent<WinionFace>().SetUIGradient("#55BE7C", "#000000");
							}
							else if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter03 && (DBManager.instance.dialogueData.curEventNum == 0 || DBManager.instance.dialogueData.curEventNum == 1))
							{
								SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Grid).GetComponent<WinionFace>().SetUIGradient("#55BE7C", "#000000");
							}
							else
							{
								SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Grid).GetComponent<WinionFace>().SetUIGradient("#38925A", "#FFFFFF");
							}
						}
						sprite = this.GridFaceSprites[1];
						goto IL_0648;
					}
				}
			}
			else if (emotionString == "[그리드-눈깔고 생각]")
			{
				if (!DBManager.instance.is3DScene)
				{
					if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter02 && (DBManager.instance.dialogueData.curEventNum == 5 || DBManager.instance.dialogueData.curEventNum == 26 || DBManager.instance.dialogueData.curEventNum == 27))
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Grid).GetComponent<WinionFace>().SetUIGradient("#55BE7C", "#000000");
					}
					else if (GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter03 && (DBManager.instance.dialogueData.curEventNum == 0 || DBManager.instance.dialogueData.curEventNum == 1))
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Grid).GetComponent<WinionFace>().SetUIGradient("#55BE7C", "#000000");
					}
					else
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Grid).GetComponent<WinionFace>().SetUIGradient("#55BE7C", "#FFFFFF");
					}
				}
				sprite = this.GridFaceSprites[3];
				goto IL_0648;
			}
		}
		else if (emotionString == "[그리드-홍조정면]")
		{
			if (!DBManager.instance.is3DScene)
			{
				SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Grid).GetComponent<WinionFace>().SetUIGradient("#E2FF41", "#FFFFFF");
			}
			sprite = this.GridFaceSprites[8];
			goto IL_0648;
		}
		sprite = this.GridFaceSprites[0];
		IL_0648:
		if (this.Grid_inRecycleBin)
		{
			SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Grid).GetComponent<WinionFace>().SetUIGradient("#737A7C", "#3F3F3F");
		}
		if (this.Grid_inSystemWinionRoom)
		{
			SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Grid).GetComponent<WinionFace>().SetUIGradient("#643750", "#1E1616");
		}
		return sprite;
	}

	// Token: 0x06001AC1 RID: 6849 RVA: 0x000C7BDC File Offset: 0x000C5DDC
	public Sprite Fix_Setting_Sprite(string emotionString)
	{
		Sprite sprite = this.FixFaceSprites[0];
		uint num = <PrivateImplementationDetails>.ComputeStringHash(emotionString);
		if (num <= 1826730914U)
		{
			if (num <= 1023016835U)
			{
				if (num <= 25227239U)
				{
					if (num != 19178671U)
					{
						if (num == 25227239U)
						{
							if (emotionString == "[픽스-충격]")
							{
								sprite = this.FixFaceSprites[9];
								goto IL_09FB;
							}
						}
					}
					else if (emotionString == "[픽스-웃음홍조]")
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Fix).GetComponent<WinionFace>().SetUIGradient("#FEB6B6", "#FFFFFF");
						sprite = this.FixFaceSprites[19];
						goto IL_09FB;
					}
				}
				else if (num != 729890205U)
				{
					if (num != 940363561U)
					{
						if (num == 1023016835U)
						{
							if (emotionString == "[픽스-걱정]")
							{
								if (!GameManager.instance.gameData.Fix.winionStatus.winionInfo.hasVirus)
								{
									SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Fix).GetComponent<WinionFace>().SetUIGradient("#BA7A7A", "#FFFFFF");
								}
								else if (GameManager.instance.gameData.Fix.winionStatus.winionInfo.hasVirus)
								{
									SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Fix).GetComponent<WinionFace>().SetUIGradient("#666666", "#390000");
								}
								sprite = this.FixFaceSprites[5];
								goto IL_09FB;
							}
						}
					}
					else if (emotionString == "[픽스-유리조각_광기 눈깔 축소]")
					{
						sprite = this.FixFaceSprites[17];
						goto IL_09FB;
					}
				}
				else if (emotionString == "[픽스-혼란눈물]")
				{
					sprite = this.FixFaceSprites[13];
					goto IL_09FB;
				}
			}
			else if (num <= 1587265944U)
			{
				if (num != 1551991155U)
				{
					if (num != 1585988062U)
					{
						if (num == 1587265944U)
						{
							if (emotionString == "[픽스-웃음]")
							{
								if (!GameManager.instance.gameData.Fix.winionStatus.winionInfo.hasVirus)
								{
									SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Fix).GetComponent<WinionFace>().SetUIGradient("#FEB6B6", "#FFFFFF");
								}
								else if (GameManager.instance.gameData.Fix.winionStatus.winionInfo.hasVirus)
								{
									SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Fix).GetComponent<WinionFace>().SetUIGradient("#8C3939", "#390000");
								}
								sprite = this.FixFaceSprites[1];
								goto IL_09FB;
							}
						}
					}
					else if (emotionString == "[픽스-유리조각_광기 눈깔 애니]")
					{
						sprite = this.FixFaceSprites[15];
						goto IL_09FB;
					}
				}
				else if (emotionString == "[픽스-충격_경악_눈물]")
				{
					sprite = this.FixFaceSprites[21];
					goto IL_09FB;
				}
			}
			else if (num != 1593001395U)
			{
				if (num != 1680544369U)
				{
					if (num == 1826730914U)
					{
						if (emotionString == "[픽스-유리조각_광기]")
						{
							sprite = this.FixFaceSprites[16];
							goto IL_09FB;
						}
					}
				}
				else if (emotionString == "[픽스-아련]")
				{
					SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Fix).GetComponent<WinionFace>().SetUIGradient("#B35764", "#FFFFFF");
					sprite = this.FixFaceSprites[7];
					goto IL_09FB;
				}
			}
			else if (emotionString == "[픽스-화남2]")
			{
				if (!GameManager.instance.gameData.Fix.winionStatus.winionInfo.hasVirus)
				{
					SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Fix).GetComponent<WinionFace>().SetUIGradient("#8C3939", "#FFFFFF");
				}
				else if (GameManager.instance.gameData.Fix.winionStatus.winionInfo.hasVirus)
				{
					SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Fix).GetComponent<WinionFace>().SetUIGradient("#8C3939", "#390000");
				}
				sprite = this.FixFaceSprites[4];
				goto IL_09FB;
			}
		}
		else if (num <= 3215172010U)
		{
			if (num <= 2508628781U)
			{
				if (num != 1830238168U)
				{
					if (num != 2330105825U)
					{
						if (num == 2508628781U)
						{
							if (emotionString == "[픽스-괴로운 눈물]")
							{
								SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Fix).GetComponent<WinionFace>().SetUIGradient("#7075A8", "#000000");
								sprite = this.FixFaceSprites[18];
								goto IL_09FB;
							}
						}
					}
					else if (emotionString == "[픽스-화남]")
					{
						if (!GameManager.instance.gameData.Fix.winionStatus.winionInfo.hasVirus)
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Fix).GetComponent<WinionFace>().SetUIGradient("#8C3939", "#FFFFFF");
						}
						else if (GameManager.instance.gameData.Fix.winionStatus.winionInfo.hasVirus)
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Fix).GetComponent<WinionFace>().SetUIGradient("#8C3939", "#390000");
						}
						sprite = this.FixFaceSprites[3];
						goto IL_09FB;
					}
				}
				else if (emotionString == "[픽스-무표정]")
				{
					if (!GameManager.instance.gameData.Fix.winionStatus.winionInfo.hasVirus)
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Fix).GetComponent<WinionFace>().SetUIGradient("#FEB6B6", "#FFFFFF");
					}
					else if (GameManager.instance.gameData.Fix.winionStatus.winionInfo.hasVirus)
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Fix).GetComponent<WinionFace>().SetUIGradient("#8C3939", "#390000");
					}
					sprite = this.FixFaceSprites[0];
					goto IL_09FB;
				}
			}
			else if (num != 2653462287U)
			{
				if (num != 3135601170U)
				{
					if (num == 3215172010U)
					{
						if (emotionString == "[픽스-광기2]")
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Fix).GetComponent<WinionFace>().SetUIGradient("#FF0022", "#000000");
							sprite = this.FixFaceSprites[11];
							goto IL_09FB;
						}
					}
				}
				else if (emotionString == "[픽스-폭소]")
				{
					if (!GameManager.instance.gameData.Fix.winionStatus.winionInfo.hasVirus)
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Fix).GetComponent<WinionFace>().SetUIGradient("#FEB6B6", "#FFFFFF");
					}
					else if (GameManager.instance.gameData.Fix.winionStatus.winionInfo.hasVirus)
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Fix).GetComponent<WinionFace>().SetUIGradient("#8C3939", "#390000");
					}
					sprite = this.FixFaceSprites[2];
					goto IL_09FB;
				}
			}
			else if (emotionString == "[픽스- 눈 아래 생각]")
			{
				if (!GameManager.instance.gameData.Fix.winionStatus.winionInfo.hasVirus)
				{
					SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Fix).GetComponent<WinionFace>().SetUIGradient("#620F0F", "#FFFFFF");
				}
				else if (GameManager.instance.gameData.Fix.winionStatus.winionInfo.hasVirus)
				{
					SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Fix).GetComponent<WinionFace>().SetUIGradient("#620F0F", "#000000");
				}
				sprite = this.FixFaceSprites[22];
				goto IL_09FB;
			}
		}
		else if (num <= 3575106182U)
		{
			if (num != 3483643722U)
			{
				if (num != 3508044758U)
				{
					if (num == 3575106182U)
					{
						if (emotionString == "[픽스-악마]")
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Fix).GetComponent<WinionFace>().SetUIGradient("#000000", "#AD362E");
							sprite = this.FixFaceSprites[12];
							goto IL_09FB;
						}
					}
				}
				else if (emotionString == "[픽스-충격_경악]")
				{
					sprite = this.FixFaceSprites[20];
					goto IL_09FB;
				}
			}
			else if (emotionString == "[픽스-아이온먹음]")
			{
				sprite = this.FixFaceSprites[14];
				goto IL_09FB;
			}
		}
		else if (num != 3785066250U)
		{
			if (num != 4105975413U)
			{
				if (num == 4137184139U)
				{
					if (emotionString == "[픽스-머리 복잡]")
					{
						if (!GameManager.instance.gameData.Fix.winionStatus.winionInfo.hasVirus)
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Fix).GetComponent<WinionFace>().SetUIGradient("#8C3939", "#FFFFFF");
						}
						if (GameManager.instance.gameData.Fix.winionStatus.winionInfo.hasVirus)
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Fix).GetComponent<WinionFace>().SetUIGradient("#666666", "#5D397B");
						}
						sprite = this.FixFaceSprites[6];
						goto IL_09FB;
					}
				}
			}
			else if (emotionString == "[픽스-아픔]")
			{
				SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Fix).GetComponent<WinionFace>().SetUIGradient("#002378", "#5E0000");
				sprite = this.FixFaceSprites[8];
				goto IL_09FB;
			}
		}
		else if (emotionString == "[픽스-광기]")
		{
			SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Fix).GetComponent<WinionFace>().SetUIGradient("#9F0015", "#000000");
			sprite = this.FixFaceSprites[10];
			goto IL_09FB;
		}
		sprite = this.FixFaceSprites[0];
		IL_09FB:
		if (this.Fix_inSystemWinionRoom)
		{
			SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Fix).GetComponent<WinionFace>().SetUIGradient("#643750", "#1E1616");
		}
		return sprite;
	}

	// Token: 0x06001AC2 RID: 6850 RVA: 0x000C8610 File Offset: 0x000C6810
	public Sprite Debug_Setting_Sprite(string emotionString)
	{
		Sprite sprite = this.DebugFaceSprites[0];
		uint num = <PrivateImplementationDetails>.ComputeStringHash(emotionString);
		if (num <= 1807888044U)
		{
			if (num <= 845263039U)
			{
				if (num <= 179188796U)
				{
					if (num != 29045129U)
					{
						if (num != 50041931U)
						{
							if (num == 179188796U)
							{
								if (emotionString == "[디버그-작은 공포]")
								{
									if (!DBManager.instance.is3DScene)
									{
										SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SetUIGradient("#36687B", "#FFFFFF");
									}
									sprite = this.DebugFaceSprites[3];
									goto IL_0BE0;
								}
							}
						}
						else if (emotionString == "[디버그-무표정]")
						{
							if (!DBManager.instance.is3DScene)
							{
								SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SetUIGradient("#B7D9E6", "#FFFFFF");
							}
							sprite = this.DebugFaceSprites[0];
							goto IL_0BE0;
						}
					}
					else if (emotionString == "[디버그-다침_화냄]")
					{
						if (!DBManager.instance.is3DScene)
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SetUIGradient("#6D4049", "#000000");
						}
						sprite = this.DebugFaceSprites[13];
						goto IL_0BE0;
					}
				}
				else if (num <= 362710620U)
				{
					if (num != 343748131U)
					{
						if (num == 362710620U)
						{
							if (emotionString == "[디버그-귀찢어지는중_눈물]")
							{
								sprite = this.DebugFaceSprites[21];
								goto IL_0BE0;
							}
						}
					}
					else if (emotionString == "[디버그-귀찢어짐_눈물]")
					{
						sprite = this.DebugFaceSprites[24];
						goto IL_0BE0;
					}
				}
				else if (num != 770370611U)
				{
					if (num == 845263039U)
					{
						if (emotionString == "[디버그-큰 공포]")
						{
							if (!DBManager.instance.is3DScene)
							{
								SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SetUIGradient("#36687B", "#000000");
							}
							sprite = this.DebugFaceSprites[4];
							goto IL_0BE0;
						}
					}
				}
				else if (emotionString == "[디버그-귀찢어짐_화남]")
				{
					sprite = this.DebugFaceSprites[25];
					goto IL_0BE0;
				}
			}
			else if (num <= 1115247974U)
			{
				if (num <= 985399581U)
				{
					if (num != 980048825U)
					{
						if (num == 985399581U)
						{
							if (emotionString == "[디버그-자책]")
							{
								sprite = this.DebugFaceSprites[10];
								goto IL_0BE0;
							}
						}
					}
					else if (emotionString == "[디버그-다침_눈아래]")
					{
						if (!DBManager.instance.is3DScene)
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SetUIGradient("#52406D", "#000000");
						}
						sprite = this.DebugFaceSprites[16];
						goto IL_0BE0;
					}
				}
				else if (num != 1029823732U)
				{
					if (num == 1115247974U)
					{
						if (emotionString == "[디버그-귀찢어짐]")
						{
							sprite = this.DebugFaceSprites[22];
							goto IL_0BE0;
						}
					}
				}
				else if (emotionString == "[디버그-눈물고임-공허]")
				{
					if (!DBManager.instance.is3DScene)
					{
						if (GameManager.instance.gameData.curChapter == GameManager.Chapter.Tutorial || GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter00 || GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter01)
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SetUIGradient("#496D7B", "#FFFFFF");
						}
						else
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SetUIGradient("#496D7B", "#373737");
						}
					}
					sprite = this.DebugFaceSprites[29];
					goto IL_0BE0;
				}
			}
			else if (num <= 1193873594U)
			{
				if (num != 1163357271U)
				{
					if (num == 1193873594U)
					{
						if (emotionString == "[디버그-소리치며 화냄]")
						{
							if (!DBManager.instance.is3DScene)
							{
								SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SetUIGradient("#032E3D", "#C0C0C0");
							}
							sprite = this.DebugFaceSprites[7];
							goto IL_0BE0;
						}
					}
				}
				else if (emotionString == "[디버그-귀찢어지는중]")
				{
					sprite = this.DebugFaceSprites[20];
					goto IL_0BE0;
				}
			}
			else if (num != 1432812521U)
			{
				if (num == 1807888044U)
				{
					if (emotionString == "[디버그-눈물고이고 화냄]")
					{
						if (!DBManager.instance.is3DScene)
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SetUIGradient("#032E3D", "#C0C0C0");
						}
						sprite = this.DebugFaceSprites[6];
						goto IL_0BE0;
					}
				}
			}
			else if (emotionString == "[디버그-눈을 내리깐 무표정]")
			{
				if (!DBManager.instance.is3DScene)
				{
					SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SetUIGradient("#B7D9E6", "#FFFFFF");
				}
				sprite = this.DebugFaceSprites[1];
				goto IL_0BE0;
			}
		}
		else if (num <= 2697521330U)
		{
			if (num <= 2252194834U)
			{
				if (num != 1839456261U)
				{
					if (num != 1967260210U)
					{
						if (num == 2252194834U)
						{
							if (emotionString == "[디버그-웃음홍조]")
							{
								if (!DBManager.instance.is3DScene)
								{
									SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SetUIGradient("#9BFFEB", "#FFFFFF");
								}
								sprite = this.DebugFaceSprites[11];
								goto IL_0BE0;
							}
						}
					}
					else if (emotionString == "[디버그-눈 아래_공허]")
					{
						if (!DBManager.instance.is3DScene)
						{
							if (GameManager.instance.gameData.curChapter == GameManager.Chapter.Tutorial || GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter00 || GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter01)
							{
								SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SetUIGradient("#496D7B", "#FFFFFF");
							}
							else
							{
								SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SetUIGradient("#496D7B", "#373737");
							}
						}
						sprite = this.DebugFaceSprites[27];
						goto IL_0BE0;
					}
				}
				else if (emotionString == "[디버그-다침_눈물]")
				{
					if (!DBManager.instance.is3DScene)
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SetUIGradient("#52406D", "#000000");
					}
					sprite = this.DebugFaceSprites[15];
					goto IL_0BE0;
				}
			}
			else if (num <= 2362946667U)
			{
				if (num != 2362791098U)
				{
					if (num == 2362946667U)
					{
						if (emotionString == "[디버그-다침_화냄2]")
						{
							if (!DBManager.instance.is3DScene)
							{
								SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SetUIGradient("#6D4049", "#000000");
							}
							sprite = this.DebugFaceSprites[14];
							goto IL_0BE0;
						}
					}
				}
				else if (emotionString == "[디버그-경악]")
				{
					sprite = this.DebugFaceSprites[9];
					goto IL_0BE0;
				}
			}
			else if (num != 2611077145U)
			{
				if (num == 2697521330U)
				{
					if (emotionString == "[디버그-눈물_공허]")
					{
						if (!DBManager.instance.is3DScene)
						{
							if (GameManager.instance.gameData.curChapter == GameManager.Chapter.Tutorial || GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter00 || GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter01)
							{
								SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SetUIGradient("#496D7B", "#FFFFFF");
							}
							else
							{
								SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SetUIGradient("#496D7B", "#373737");
							}
						}
						sprite = this.DebugFaceSprites[28];
						goto IL_0BE0;
					}
				}
			}
			else if (emotionString == "[디버그-웃음]")
			{
				if (!DBManager.instance.is3DScene)
				{
					if (GameManager.instance.gameData.curChapter == GameManager.Chapter.Tutorial || GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter00 || GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter01)
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SetUIGradient("#496D7B", "#FFFFFF");
					}
					else
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SetUIGradient("#496D7B", "#373737");
					}
				}
				sprite = this.DebugFaceSprites[2];
				goto IL_0BE0;
			}
		}
		else if (num <= 3465276120U)
		{
			if (num <= 3256114506U)
			{
				if (num != 3198536696U)
				{
					if (num == 3256114506U)
					{
						if (emotionString == "[디버그-다침_큰공포]")
						{
							if (!DBManager.instance.is3DScene)
							{
								SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SetUIGradient("#52406D", "#000000");
							}
							sprite = this.DebugFaceSprites[17];
							goto IL_0BE0;
						}
					}
				}
				else if (emotionString == "[디버그-울음]")
				{
					if (!DBManager.instance.is3DScene)
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SetUIGradient("#032E3D", "#C0C0C0");
					}
					sprite = this.DebugFaceSprites[8];
					goto IL_0BE0;
				}
			}
			else if (num != 3370643275U)
			{
				if (num == 3465276120U)
				{
					if (emotionString == "[디버그-다침]")
					{
						if (!DBManager.instance.is3DScene)
						{
							SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SetUIGradient("#6D4049", "#000000");
						}
						sprite = this.DebugFaceSprites[12];
						goto IL_0BE0;
					}
				}
			}
			else if (emotionString == "[디버그-귀잡아당김_눈물]")
			{
				sprite = this.DebugFaceSprites[19];
				goto IL_0BE0;
			}
		}
		else if (num <= 3916677614U)
		{
			if (num != 3777758687U)
			{
				if (num == 3916677614U)
				{
					if (emotionString == "[디버그-귀잡아당김]")
					{
						sprite = this.DebugFaceSprites[18];
						goto IL_0BE0;
					}
				}
			}
			else if (emotionString == "[디버그-귀찢어짐_아래바라봄]")
			{
				sprite = this.DebugFaceSprites[23];
				goto IL_0BE0;
			}
		}
		else if (num != 3926063555U)
		{
			if (num == 3994281028U)
			{
				if (emotionString == "[디버그-눈물고임]")
				{
					if (!DBManager.instance.is3DScene)
					{
						SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SetUIGradient("#0E3341", "#FFFFFF");
					}
					sprite = this.DebugFaceSprites[5];
					goto IL_0BE0;
				}
			}
		}
		else if (emotionString == "[디버그-무표정_공허]")
		{
			if (!DBManager.instance.is3DScene)
			{
				if (GameManager.instance.gameData.curChapter == GameManager.Chapter.Tutorial || GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter00 || GameManager.instance.gameData.curChapter == GameManager.Chapter.chapter01)
				{
					SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SetUIGradient("#496D7B", "#FFFFFF");
				}
				else
				{
					SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SetUIGradient("#496D7B", "#373737");
				}
			}
			sprite = this.DebugFaceSprites[26];
			goto IL_0BE0;
		}
		sprite = this.DebugFaceSprites[0];
		IL_0BE0:
		if (this.Debug_inSystemWinionRoom)
		{
			SingletoneBehaviour<IconManager>.Instance.GetWindow(Icon.Face_Debug).GetComponent<WinionFace>().SetUIGradient("#643750", "#1E1616");
		}
		return sprite;
	}

	// Token: 0x06001AC3 RID: 6851 RVA: 0x000C9228 File Offset: 0x000C7428
	public List<string> DivideSentence(string sentence)
	{
		List<string> list = new List<string>();
		if (sentence.ToString().Contains("/"))
		{
			list = sentence.ToString().Split('/', StringSplitOptions.None).ToList<string>();
			for (int i = 0; i < list.Count; i++)
			{
				this.CheckString(list[i]);
			}
		}
		else
		{
			list.Add(sentence);
		}
		return list;
	}

	// Token: 0x06001AC4 RID: 6852 RVA: 0x000C928C File Offset: 0x000C748C
	public Winion CheckString(string sentence)
	{
		sentence = sentence.Trim();
		Winion winion = Winion.Ion;
		uint num = <PrivateImplementationDetails>.ComputeStringHash(sentence);
		if (num <= 1941629204U)
		{
			if (num <= 1090515110U)
			{
				if (num <= 413253463U)
				{
					if (num <= 293654250U)
					{
						if (num != 34415898U)
						{
							if (num != 293654250U)
							{
								goto IL_05ED;
							}
							if (!(sentence == "[아이온-머리채]"))
							{
								goto IL_05ED;
							}
						}
						else if (!(sentence == "[아이온-맞음2]"))
						{
							goto IL_05ED;
						}
					}
					else if (num != 382595016U)
					{
						if (num != 401476537U)
						{
							if (num != 413253463U)
							{
								goto IL_05ED;
							}
							if (!(sentence == "[아이온- 공허 걱정 손모음]"))
							{
								goto IL_05ED;
							}
						}
						else if (!(sentence == "[아이온- 공허 눈물]"))
						{
							goto IL_05ED;
						}
					}
					else if (!(sentence == "[아이온-다침]"))
					{
						goto IL_05ED;
					}
				}
				else if (num <= 779379525U)
				{
					if (num != 490532168U)
					{
						if (num != 779379525U)
						{
							goto IL_05ED;
						}
						if (!(sentence == "[아이온-머리뜯김]"))
						{
							goto IL_05ED;
						}
					}
					else if (!(sentence == "[아이온-울음]"))
					{
						goto IL_05ED;
					}
				}
				else if (num != 911609960U)
				{
					if (num != 955039230U)
					{
						if (num != 1090515110U)
						{
							goto IL_05ED;
						}
						if (!(sentence == "[아이온-눈찡긋]"))
						{
							goto IL_05ED;
						}
					}
					else if (!(sentence == "[아이온-다침_공포]"))
					{
						goto IL_05ED;
					}
				}
				else if (!(sentence == "[비지트- 눈깔음]"))
				{
					goto IL_05ED;
				}
			}
			else if (num <= 1447747712U)
			{
				if (num <= 1259945317U)
				{
					if (num != 1122863828U)
					{
						if (num != 1259945317U)
						{
							goto IL_05ED;
						}
						if (!(sentence == "[비지트-눈물 광기]"))
						{
							goto IL_05ED;
						}
					}
					else if (!(sentence == "[비지트-눈 감음]"))
					{
						goto IL_05ED;
					}
				}
				else if (num != 1263751283U)
				{
					if (num != 1411817176U)
					{
						if (num != 1447747712U)
						{
							goto IL_05ED;
						}
						if (!(sentence == "[비지트-눈물고임]"))
						{
							goto IL_05ED;
						}
					}
					else if (!(sentence == "[아이온-안타까움]"))
					{
						goto IL_05ED;
					}
				}
				else if (!(sentence == "[비지트-눈물고임_2]"))
				{
					goto IL_05ED;
				}
			}
			else if (num <= 1766801077U)
			{
				if (num != 1558529031U)
				{
					if (num != 1722276922U)
					{
						if (num != 1766801077U)
						{
							goto IL_05ED;
						}
						if (!(sentence == "[아이온-맞음2공포]"))
						{
							goto IL_05ED;
						}
					}
					else if (!(sentence == "[아이온-맞음]"))
					{
						goto IL_05ED;
					}
				}
				else if (!(sentence == "[비지트-무표정]"))
				{
					goto IL_05ED;
				}
			}
			else if (num != 1810660204U)
			{
				if (num != 1918296435U)
				{
					if (num != 1941629204U)
					{
						goto IL_05ED;
					}
					if (!(sentence == "[아이온-화냄]"))
					{
						goto IL_05ED;
					}
				}
				else if (!(sentence == "[비지트-광기]"))
				{
					goto IL_05ED;
				}
			}
			else if (!(sentence == "[비지트-화남]"))
			{
				goto IL_05ED;
			}
		}
		else if (num <= 2900794582U)
		{
			if (num <= 2241088671U)
			{
				if (num <= 2108498250U)
				{
					if (num != 1966585292U)
					{
						if (num != 2108498250U)
						{
							goto IL_05ED;
						}
						if (!(sentence == "[아이온-충격]"))
						{
							goto IL_05ED;
						}
					}
					else if (!(sentence == "[아이온-다침_무표정]"))
					{
						goto IL_05ED;
					}
				}
				else if (num != 2133805773U)
				{
					if (num != 2228638371U)
					{
						if (num != 2241088671U)
						{
							goto IL_05ED;
						}
						if (!(sentence == "[아이온-맞음2_눈물]"))
						{
							goto IL_05ED;
						}
					}
					else if (!(sentence == "[아이온- 공허 걱정]"))
					{
						goto IL_05ED;
					}
				}
				else if (!(sentence == "[비지트-웃음]"))
				{
					goto IL_05ED;
				}
			}
			else if (num <= 2455554206U)
			{
				if (num != 2313267977U)
				{
					if (num != 2455554206U)
					{
						goto IL_05ED;
					}
					if (!(sentence == "[아이온-호기심]"))
					{
						goto IL_05ED;
					}
				}
				else if (!(sentence == "[아이온-웃음]"))
				{
					goto IL_05ED;
				}
			}
			else if (num != 2497620790U)
			{
				if (num != 2500731148U)
				{
					if (num != 2900794582U)
					{
						goto IL_05ED;
					}
					if (!(sentence == "[비지트-아쉬움]"))
					{
						goto IL_05ED;
					}
				}
				else if (!(sentence == "[비지트-시선회피]"))
				{
					goto IL_05ED;
				}
			}
			else if (!(sentence == "[비지트-충격]"))
			{
				goto IL_05ED;
			}
		}
		else if (num <= 3426673660U)
		{
			if (num <= 3275600228U)
			{
				if (num != 3215730581U)
				{
					if (num != 3275600228U)
					{
						goto IL_05ED;
					}
					if (!(sentence == "[비지트-귀여운충격]"))
					{
						goto IL_05ED;
					}
				}
				else if (!(sentence == "[아이온-다침_눈물]"))
				{
					goto IL_05ED;
				}
			}
			else if (num != 3328517432U)
			{
				if (num != 3377504123U)
				{
					if (num != 3426673660U)
					{
						goto IL_05ED;
					}
					if (!(sentence == "[아이온- 공허 무표정 손모음]"))
					{
						goto IL_05ED;
					}
				}
				else if (!(sentence == "[아이온-무표정]"))
				{
					goto IL_05ED;
				}
			}
			else if (!(sentence == "[아이온- 공허 무표정]"))
			{
				goto IL_05ED;
			}
		}
		else if (num <= 3825581691U)
		{
			if (num != 3461521562U)
			{
				if (num != 3673560469U)
				{
					if (num != 3825581691U)
					{
						goto IL_05ED;
					}
					if (!(sentence == "[아이온-하얗게 질림]"))
					{
						goto IL_05ED;
					}
				}
				else if (!(sentence == "[아이온- 공허 눈물 손모음]"))
				{
					goto IL_05ED;
				}
			}
			else if (!(sentence == "[아이온-공허웃음]"))
			{
				goto IL_05ED;
			}
		}
		else if (num != 3975164550U)
		{
			if (num != 4088207036U)
			{
				if (num != 4111464026U)
				{
					goto IL_05ED;
				}
				if (!(sentence == "[비지트-호기심]"))
				{
					goto IL_05ED;
				}
			}
			else if (!(sentence == "[비지트-눈물줄줄]"))
			{
				goto IL_05ED;
			}
		}
		else if (!(sentence == "[비지트-경악]"))
		{
			goto IL_05ED;
		}
		bool flag = true;
		goto IL_05EF;
		IL_05ED:
		flag = false;
		IL_05EF:
		if (flag)
		{
			return Winion.Ion;
		}
		num = <PrivateImplementationDetails>.ComputeStringHash(sentence);
		if (num <= 1872271854U)
		{
			if (num <= 821222068U)
			{
				if (num != 139354055U)
				{
					if (num != 528643784U)
					{
						if (num != 821222068U)
						{
							goto IL_07C4;
						}
						if (!(sentence == "[보-안타까움]"))
						{
							goto IL_07C4;
						}
					}
					else if (!(sentence == "[보-눈물고임]"))
					{
						goto IL_07C4;
					}
				}
				else if (!(sentence == "[보-하얗게 질림]"))
				{
					goto IL_07C4;
				}
			}
			else if (num <= 1253372810U)
			{
				if (num != 1234029278U)
				{
					if (num != 1253372810U)
					{
						goto IL_07C4;
					}
					if (!(sentence == "[감시위니언-맞음]"))
					{
						goto IL_07C4;
					}
				}
				else if (!(sentence == "[보-충격]"))
				{
					goto IL_07C4;
				}
			}
			else if (num != 1714410639U)
			{
				if (num != 1872271854U)
				{
					goto IL_07C4;
				}
				if (!(sentence == "[보-뺨 맞음]"))
				{
					goto IL_07C4;
				}
			}
			else if (!(sentence == "[감시위니언-비웃음]"))
			{
				goto IL_07C4;
			}
		}
		else if (num <= 3164493252U)
		{
			if (num != 2187869541U)
			{
				if (num != 3082489000U)
				{
					if (num != 3164493252U)
					{
						goto IL_07C4;
					}
					if (!(sentence == "[보-울음]"))
					{
						goto IL_07C4;
					}
				}
				else if (!(sentence == "[보-화냄]"))
				{
					goto IL_07C4;
				}
			}
			else if (!(sentence == "[보-웃음]"))
			{
				goto IL_07C4;
			}
		}
		else if (num <= 3712276943U)
		{
			if (num != 3587854379U)
			{
				if (num != 3712276943U)
				{
					goto IL_07C4;
				}
				if (!(sentence == "[보-먹음]"))
				{
					goto IL_07C4;
				}
			}
			else if (!(sentence == "[감시위니언-무표정]"))
			{
				goto IL_07C4;
			}
		}
		else if (num != 3848150700U)
		{
			if (num != 3891343567U)
			{
				goto IL_07C4;
			}
			if (!(sentence == "[보-무표정]"))
			{
				goto IL_07C4;
			}
		}
		else if (!(sentence == "[보-하얗게 질림_눈물]"))
		{
			goto IL_07C4;
		}
		flag = true;
		goto IL_07C6;
		IL_07C4:
		flag = false;
		IL_07C6:
		if (flag)
		{
			return Winion.Bo;
		}
		num = <PrivateImplementationDetails>.ComputeStringHash(sentence);
		if (num <= 1002855899U)
		{
			if (num <= 276674932U)
			{
				if (num != 11985199U)
				{
					if (num != 249653631U)
					{
						if (num != 276674932U)
						{
							goto IL_0940;
						}
						if (!(sentence == "[그리드-아픔]"))
						{
							goto IL_0940;
						}
					}
					else if (!(sentence == "[그리드-하얗게 질리고 눈물]"))
					{
						goto IL_0940;
					}
				}
				else if (!(sentence == "[그리드-붉은얼굴]"))
				{
					goto IL_0940;
				}
			}
			else if (num != 772946923U)
			{
				if (num != 917050587U)
				{
					if (num != 1002855899U)
					{
						goto IL_0940;
					}
					if (!(sentence == "[그리드-하얗게 질림]"))
					{
						goto IL_0940;
					}
				}
				else if (!(sentence == "[그리드-무표정]"))
				{
					goto IL_0940;
				}
			}
			else if (!(sentence == "[그리드-하얗게 질리고 눈물흘림]"))
			{
				goto IL_0940;
			}
		}
		else if (num <= 2337514679U)
		{
			if (num != 1040031103U)
			{
				if (num != 1604861699U)
				{
					if (num != 2337514679U)
					{
						goto IL_0940;
					}
					if (!(sentence == "[그리드-하얗게 질리고 눈깔기]"))
					{
						goto IL_0940;
					}
				}
				else if (!(sentence == "[그리드-홍조]"))
				{
					goto IL_0940;
				}
			}
			else if (!(sentence == "[그리드-홍조놀람]"))
			{
				goto IL_0940;
			}
		}
		else if (num != 2395291488U)
		{
			if (num != 3425339906U)
			{
				if (num != 4136517539U)
				{
					goto IL_0940;
				}
				if (!(sentence == "[그리드-찡그림]"))
				{
					goto IL_0940;
				}
			}
			else if (!(sentence == "[그리드-눈깔고 생각]"))
			{
				goto IL_0940;
			}
		}
		else if (!(sentence == "[그리드-홍조정면]"))
		{
			goto IL_0940;
		}
		flag = true;
		goto IL_0942;
		IL_0940:
		flag = false;
		IL_0942:
		if (flag)
		{
			return Winion.Grid;
		}
		num = <PrivateImplementationDetails>.ComputeStringHash(sentence);
		if (num <= 1826730914U)
		{
			if (num <= 1023016835U)
			{
				if (num <= 25227239U)
				{
					if (num != 19178671U)
					{
						if (num != 25227239U)
						{
							goto IL_0C5F;
						}
						if (!(sentence == "[픽스-충격]"))
						{
							goto IL_0C5F;
						}
					}
					else if (!(sentence == "[픽스-웃음홍조]"))
					{
						goto IL_0C5F;
					}
				}
				else if (num != 729890205U)
				{
					if (num != 940363561U)
					{
						if (num != 1023016835U)
						{
							goto IL_0C5F;
						}
						if (!(sentence == "[픽스-걱정]"))
						{
							goto IL_0C5F;
						}
					}
					else if (!(sentence == "[픽스-유리조각_광기 눈깔 축소]"))
					{
						goto IL_0C5F;
					}
				}
				else if (!(sentence == "[픽스-혼란눈물]"))
				{
					goto IL_0C5F;
				}
			}
			else if (num <= 1587265944U)
			{
				if (num != 1551991155U)
				{
					if (num != 1585988062U)
					{
						if (num != 1587265944U)
						{
							goto IL_0C5F;
						}
						if (!(sentence == "[픽스-웃음]"))
						{
							goto IL_0C5F;
						}
					}
					else if (!(sentence == "[픽스-유리조각_광기 눈깔 애니]"))
					{
						goto IL_0C5F;
					}
				}
				else if (!(sentence == "[픽스-충격_경악_눈물]"))
				{
					goto IL_0C5F;
				}
			}
			else if (num != 1593001395U)
			{
				if (num != 1680544369U)
				{
					if (num != 1826730914U)
					{
						goto IL_0C5F;
					}
					if (!(sentence == "[픽스-유리조각_광기]"))
					{
						goto IL_0C5F;
					}
				}
				else if (!(sentence == "[픽스-아련]"))
				{
					goto IL_0C5F;
				}
			}
			else if (!(sentence == "[픽스-화남2]"))
			{
				goto IL_0C5F;
			}
		}
		else if (num <= 3215172010U)
		{
			if (num <= 2508628781U)
			{
				if (num != 1830238168U)
				{
					if (num != 2330105825U)
					{
						if (num != 2508628781U)
						{
							goto IL_0C5F;
						}
						if (!(sentence == "[픽스-괴로운 눈물]"))
						{
							goto IL_0C5F;
						}
					}
					else if (!(sentence == "[픽스-화남]"))
					{
						goto IL_0C5F;
					}
				}
				else if (!(sentence == "[픽스-무표정]"))
				{
					goto IL_0C5F;
				}
			}
			else if (num != 2653462287U)
			{
				if (num != 3135601170U)
				{
					if (num != 3215172010U)
					{
						goto IL_0C5F;
					}
					if (!(sentence == "[픽스-광기2]"))
					{
						goto IL_0C5F;
					}
				}
				else if (!(sentence == "[픽스-폭소]"))
				{
					goto IL_0C5F;
				}
			}
			else if (!(sentence == "[픽스- 눈 아래 생각]"))
			{
				goto IL_0C5F;
			}
		}
		else if (num <= 3575106182U)
		{
			if (num != 3483643722U)
			{
				if (num != 3508044758U)
				{
					if (num != 3575106182U)
					{
						goto IL_0C5F;
					}
					if (!(sentence == "[픽스-악마]"))
					{
						goto IL_0C5F;
					}
				}
				else if (!(sentence == "[픽스-충격_경악]"))
				{
					goto IL_0C5F;
				}
			}
			else if (!(sentence == "[픽스-아이온먹음]"))
			{
				goto IL_0C5F;
			}
		}
		else if (num != 3785066250U)
		{
			if (num != 4105975413U)
			{
				if (num != 4137184139U)
				{
					goto IL_0C5F;
				}
				if (!(sentence == "[픽스-머리 복잡]"))
				{
					goto IL_0C5F;
				}
			}
			else if (!(sentence == "[픽스-아픔]"))
			{
				goto IL_0C5F;
			}
		}
		else if (!(sentence == "[픽스-광기]"))
		{
			goto IL_0C5F;
		}
		flag = true;
		goto IL_0C61;
		IL_0C5F:
		flag = false;
		IL_0C61:
		if (flag)
		{
			return Winion.Fix;
		}
		num = <PrivateImplementationDetails>.ComputeStringHash(sentence);
		if (num <= 1807888044U)
		{
			if (num <= 845263039U)
			{
				if (num <= 179188796U)
				{
					if (num != 29045129U)
					{
						if (num != 50041931U)
						{
							if (num != 179188796U)
							{
								goto IL_10A9;
							}
							if (!(sentence == "[디버그-작은 공포]"))
							{
								goto IL_10A9;
							}
						}
						else if (!(sentence == "[디버그-무표정]"))
						{
							goto IL_10A9;
						}
					}
					else if (!(sentence == "[디버그-다침_화냄]"))
					{
						goto IL_10A9;
					}
				}
				else if (num <= 362710620U)
				{
					if (num != 343748131U)
					{
						if (num != 362710620U)
						{
							goto IL_10A9;
						}
						if (!(sentence == "[디버그-귀찢어지는중_눈물]"))
						{
							goto IL_10A9;
						}
					}
					else if (!(sentence == "[디버그-귀찢어짐_눈물]"))
					{
						goto IL_10A9;
					}
				}
				else if (num != 770370611U)
				{
					if (num != 845263039U)
					{
						goto IL_10A9;
					}
					if (!(sentence == "[디버그-큰 공포]"))
					{
						goto IL_10A9;
					}
				}
				else if (!(sentence == "[디버그-귀찢어짐_화남]"))
				{
					goto IL_10A9;
				}
			}
			else if (num <= 1115247974U)
			{
				if (num <= 985399581U)
				{
					if (num != 980048825U)
					{
						if (num != 985399581U)
						{
							goto IL_10A9;
						}
						if (!(sentence == "[디버그-자책]"))
						{
							goto IL_10A9;
						}
					}
					else if (!(sentence == "[디버그-다침_눈아래]"))
					{
						goto IL_10A9;
					}
				}
				else if (num != 1029823732U)
				{
					if (num != 1115247974U)
					{
						goto IL_10A9;
					}
					if (!(sentence == "[디버그-귀찢어짐]"))
					{
						goto IL_10A9;
					}
				}
				else if (!(sentence == "[디버그-눈물고임-공허]"))
				{
					goto IL_10A9;
				}
			}
			else if (num <= 1193873594U)
			{
				if (num != 1163357271U)
				{
					if (num != 1193873594U)
					{
						goto IL_10A9;
					}
					if (!(sentence == "[디버그-소리치며 화냄]"))
					{
						goto IL_10A9;
					}
				}
				else if (!(sentence == "[디버그-귀찢어지는중]"))
				{
					goto IL_10A9;
				}
			}
			else if (num != 1432812521U)
			{
				if (num != 1807888044U)
				{
					goto IL_10A9;
				}
				if (!(sentence == "[디버그-눈물고이고 화냄]"))
				{
					goto IL_10A9;
				}
			}
			else if (!(sentence == "[디버그-눈을 내리깐 무표정]"))
			{
				goto IL_10A9;
			}
		}
		else if (num <= 2697521330U)
		{
			if (num <= 2252194834U)
			{
				if (num != 1839456261U)
				{
					if (num != 1967260210U)
					{
						if (num != 2252194834U)
						{
							goto IL_10A9;
						}
						if (!(sentence == "[디버그-웃음홍조]"))
						{
							goto IL_10A9;
						}
					}
					else if (!(sentence == "[디버그-눈 아래_공허]"))
					{
						goto IL_10A9;
					}
				}
				else if (!(sentence == "[디버그-다침_눈물]"))
				{
					goto IL_10A9;
				}
			}
			else if (num <= 2362946667U)
			{
				if (num != 2362791098U)
				{
					if (num != 2362946667U)
					{
						goto IL_10A9;
					}
					if (!(sentence == "[디버그-다침_화냄2]"))
					{
						goto IL_10A9;
					}
				}
				else if (!(sentence == "[디버그-경악]"))
				{
					goto IL_10A9;
				}
			}
			else if (num != 2611077145U)
			{
				if (num != 2697521330U)
				{
					goto IL_10A9;
				}
				if (!(sentence == "[디버그-눈물_공허]"))
				{
					goto IL_10A9;
				}
			}
			else if (!(sentence == "[디버그-웃음]"))
			{
				goto IL_10A9;
			}
		}
		else if (num <= 3465276120U)
		{
			if (num <= 3256114506U)
			{
				if (num != 3198536696U)
				{
					if (num != 3256114506U)
					{
						goto IL_10A9;
					}
					if (!(sentence == "[디버그-다침_큰공포]"))
					{
						goto IL_10A9;
					}
				}
				else if (!(sentence == "[디버그-울음]"))
				{
					goto IL_10A9;
				}
			}
			else if (num != 3370643275U)
			{
				if (num != 3465276120U)
				{
					goto IL_10A9;
				}
				if (!(sentence == "[디버그-다침]"))
				{
					goto IL_10A9;
				}
			}
			else if (!(sentence == "[디버그-귀잡아당김_눈물]"))
			{
				goto IL_10A9;
			}
		}
		else if (num <= 3916677614U)
		{
			if (num != 3777758687U)
			{
				if (num != 3916677614U)
				{
					goto IL_10A9;
				}
				if (!(sentence == "[디버그-귀잡아당김]"))
				{
					goto IL_10A9;
				}
			}
			else if (!(sentence == "[디버그-귀찢어짐_아래바라봄]"))
			{
				goto IL_10A9;
			}
		}
		else if (num != 3926063555U)
		{
			if (num != 3994281028U)
			{
				goto IL_10A9;
			}
			if (!(sentence == "[디버그-눈물고임]"))
			{
				goto IL_10A9;
			}
		}
		else if (!(sentence == "[디버그-무표정_공허]"))
		{
			goto IL_10A9;
		}
		flag = true;
		goto IL_10AB;
		IL_10A9:
		flag = false;
		IL_10AB:
		if (flag)
		{
			return Winion.Debug;
		}
		return winion;
	}

	// Token: 0x040017BC RID: 6076
	public List<Sprite> IonFaceSprites;

	// Token: 0x040017BD RID: 6077
	public List<Sprite> BoFaceSprites;

	// Token: 0x040017BE RID: 6078
	public List<Sprite> GridFaceSprites;

	// Token: 0x040017BF RID: 6079
	public List<Sprite> FixFaceSprites;

	// Token: 0x040017C0 RID: 6080
	public List<Sprite> DebugFaceSprites;

	// Token: 0x040017C1 RID: 6081
	public bool fixReSetAnim;

	// Token: 0x040017C2 RID: 6082
	private bool Ion_inRecycleBin;

	// Token: 0x040017C3 RID: 6083
	public GameObject Ion_RecycleBinFilter;

	// Token: 0x040017C4 RID: 6084
	private bool Bo_inRecycleBin;

	// Token: 0x040017C5 RID: 6085
	public GameObject Bo_RecycleBinFilter;

	// Token: 0x040017C6 RID: 6086
	private bool Grid_inRecycleBin;

	// Token: 0x040017C7 RID: 6087
	public GameObject Grid_RecycleBinFilter;

	// Token: 0x040017C8 RID: 6088
	private bool Ion_inSystemWinionRoom;

	// Token: 0x040017C9 RID: 6089
	public GameObject Ion_inSystemWinionRoomFilter;

	// Token: 0x040017CA RID: 6090
	private bool Bo_inSystemWinionRoom;

	// Token: 0x040017CB RID: 6091
	public GameObject Bo_inSystemWinionRoomFilter;

	// Token: 0x040017CC RID: 6092
	private bool Grid_inSystemWinionRoom;

	// Token: 0x040017CD RID: 6093
	public GameObject Grid_inSystemWinionRoomFilter;

	// Token: 0x040017CE RID: 6094
	private bool Fix_inSystemWinionRoom;

	// Token: 0x040017CF RID: 6095
	public GameObject Fix_inSystemWinionRoomFilter;

	// Token: 0x040017D0 RID: 6096
	private bool Debug_inSystemWinionRoom;

	// Token: 0x040017D1 RID: 6097
	public GameObject Debug_inSystemWinionRoomFilter;

	// Token: 0x040017D2 RID: 6098
	public bool Returned_Ion;

	// Token: 0x040017D3 RID: 6099
	private bool bo_changeBackGround;
}
