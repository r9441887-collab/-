using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000136 RID: 310
public class DesktopController : SingletoneBehaviour<DesktopController>
{
	// Token: 0x06000767 RID: 1895 RVA: 0x0003F328 File Offset: 0x0003D528
	public void HoverTabActive()
	{
		this.HoverActive = !this.HoverActive;
		this.HoverTab_1.SetActive(this.HoverActive);
		this.HoverTab_2.SetActive(this.HoverActive);
		this.HoverTab_3.SetActive(this.HoverActive);
	}

	// Token: 0x06000768 RID: 1896 RVA: 0x00012E4F File Offset: 0x0001104F
	public int GetImage()
	{
		return Random.Range(-1, 9);
	}

	// Token: 0x06000769 RID: 1897 RVA: 0x0003F378 File Offset: 0x0003D578
	public ValueTuple<Vector2, ImageBoxAutoSize> InstantiateImage(WinionHandler handler)
	{
		ImageBoxAutoSize component = GameManager.instance.objectPoolingSystem.GetGameObjectPrefab("ImageBox", "SystemPrefabs/", this.DesktopImageCanvas, false).GetComponent<ImageBoxAutoSize>();
		return new ValueTuple<Vector2, ImageBoxAutoSize>(component.SetImage(handler), component);
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x00012E59 File Offset: 0x00011059
	public void SpawnBomb(Vector3 position)
	{
		this.PresentBomb.transform.position = position;
		this.PresentBomb.SetActive(true);
		this.PresentBomb.GetComponent<BombObject>().BombTimer(0.5f);
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x00012E8D File Offset: 0x0001108D
	private void Awake()
	{
		this.NightBlack.alpha = 0f;
		this.danceObject.GetOriginPosition();
		this.danceObject.GetColor();
		this.DesktopActionInit();
	}

	// Token: 0x0600076C RID: 1900 RVA: 0x0003F3B8 File Offset: 0x0003D5B8
	private void Start()
	{
		this.winionRoomSettings.Init();
		DesktopController.GetPicture = this.GetPictureToggle.isOn;
		DesktopController.GetPlane = this.GetPlaneToggle.isOn;
		DesktopController.GetPoop = this.GetPoopToggle.isOn;
		int num;
		DesktopController.PictureCount = (int.TryParse(this.GetPictureCount.text, out num) ? num : 0);
		this.GetPictureToggle.onValueChanged.AddListener(delegate
		{
			this.GetPictureBoolValueChange();
		});
		this.GetPictureCount.onEndEdit.AddListener(delegate
		{
			this.GetPictureCountValueChange();
		});
		this.GetPlaneToggle.onValueChanged.AddListener(delegate
		{
			DesktopController.GetPlane = this.GetPlaneToggle.isOn;
		});
		this.GetPoopToggle.onValueChanged.AddListener(delegate
		{
			DesktopController.GetPoop = this.GetPoopToggle.isOn;
		});
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x00012EBB File Offset: 0x000110BB
	public IEnumerator TurnLightEvent()
	{
		if (this.nightEvent)
		{
			yield break;
		}
		this.nightEvent = true;
		this.isNight = true;
		SystemBox.Instance.Show(new MessageConfig("자야할 시간이에요"), SystemBox.MessageType.Default, false, 4f, false, true);
		yield return TweenExtensions.WaitForCompletion(this.SettingLights(true));
		yield return new WaitForSeconds(0.5f);
		yield return TweenExtensions.WaitForCompletion(this.TurnLight());
		yield return new WaitForSeconds(0.5f);
		List<WinionHandler> handlers = GameManager.instance.GetWinionHandlers();
		for (int i = 0; i < handlers.Count; i++)
		{
			int index = i;
			float num = Random.Range(1f, 2f);
			DOVirtual.Float(0f, 1f, num, null).OnComplete(delegate
			{
				GameObject bombObject = handlers[index].winionBehaviour.BombObject;
				if (bombObject != null)
				{
					bombObject.SetActive(false);
				}
				if (handlers[index].winionBehaviour.TargetWindow != null)
				{
					handlers[index].winionBehaviour.TargetWindow.SetActive(false);
				}
				handlers[index].winionBehaviour.RemoveComponent<WinionDance>();
				handlers[index].winionBehaviour.RemoveComponent<WinionGetBackStage>();
				handlers[index].winionBehaviour.StopDesktopAction();
				handlers[index].winionBehaviour.SetCanInterrupt(false);
				handlers[index].winionBehaviour.CanArriveAction = false;
				handlers[index].winionBehaviour.isBusy = true;
				handlers[index].winionDragAndDrop.canDragAndDrop = false;
				SingletoneBehaviour<WinionDistance>.Instance.SetShakeHandState(handlers[index].GetWinionType(), false);
				handlers[index].winionMovement.SetActiveMovement(false, false, false);
				handlers[index].winionStatus.SetSleeping();
				handlers[index].ChangeCharacterState(CharacterState.Sleeping);
				handlers[index].winionAnimator.canChangeAnimation = true;
				handlers[index].winionAnimator.SetLoop(true);
				handlers[index].winionAnimator.PlayAnimation("Sleeping", false);
				handlers[index].winionAnimator.canChangeAnimation = false;
			});
		}
		yield return TweenExtensions.WaitForCompletion(this.SettingLights(false));
		yield return new WaitForSeconds(0.5f);
		yield return TweenExtensions.WaitForCompletion(this.SettingLights2(true));
		yield return TweenExtensions.WaitForCompletion(this.NightBlack.DOFade(1f, 5f));
		yield return TweenExtensions.WaitForCompletion(ShortcutExtensions.DOLocalMove(this.LightSwitch.transform, new Vector3(0f, -400f, 0f), 1f, false).SetEase(Ease.InOutBack).SetRelative(true));
		this.LightButton.interactable = true;
		this.nightEvent = false;
		yield break;
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x00012ECA File Offset: 0x000110CA
	public IEnumerator TurnLightEventEnd()
	{
		if (this.nightEvent)
		{
			yield break;
		}
		this.nightEvent = true;
		this.LightButton.interactable = false;
		yield return TweenExtensions.WaitForCompletion(this.TurnLight());
		yield return TweenExtensions.WaitForCompletion(this.SettingLights2(false));
		List<WinionHandler> handlers = GameManager.instance.GetWinionHandlers();
		for (int i = 0; i < handlers.Count; i++)
		{
			int index = i;
			float num = Random.Range(1f, 2f);
			DOVirtual.Float(0f, 1f, num, null).OnComplete(delegate
			{
				handlers[index].winionBehaviour.RemoveComponent<WinionDance>();
				handlers[index].winionBehaviour.RemoveComponent<WinionGetBackStage>();
				handlers[index].winionBehaviour.StopDesktopAction();
				handlers[index].ChangeCharacterState(CharacterState.None);
				handlers[index].winionBehaviour.isBusy = false;
				handlers[index].winionDragAndDrop.canDragAndDrop = true;
				handlers[index].winionAnimator.canChangeAnimation = true;
				handlers[index].winionAnimator.SetLoop(false);
				handlers[index].winionBehaviour.CanArriveAction = true;
				handlers[index].winionBehaviour.SetCanInterrupt(true);
				handlers[index].SetIdleByWinionStatus();
				handlers[index].winionMovement.SetActiveMovement(true, false, false);
				handlers[index].winionMovement.MoveToRandomPosition();
				handlers[index].winionBehaviour.ArriveAction();
			});
		}
		ShortcutExtensions.DOLocalMove(this.LightSwitch.transform, new Vector3(0f, 400f, 0f), 1f, false).SetEase(Ease.InOutBack).SetRelative(true);
		this.isNight = false;
		this.nightEvent = false;
		yield break;
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x00012ED9 File Offset: 0x000110D9
	public void TurnOffLight()
	{
		base.StartCoroutine("TurnLightEventEnd");
	}

	// Token: 0x06000770 RID: 1904 RVA: 0x0003F490 File Offset: 0x0003D690
	public Tween SettingLights(bool active)
	{
		if (this.LightSettingTween)
		{
			return null;
		}
		this.LightSettingTween = true;
		this.LightSetting = active;
		Tween tween;
		if (active)
		{
			tween = ShortcutExtensions.DOLocalMove(this.LightObjects.transform, new Vector3(0f, -700f, 0f), 1f, false).SetEase(Ease.InOutCubic).SetRelative(true)
				.OnComplete(delegate
				{
					this.LightSettingTween = false;
				});
		}
		else
		{
			tween = ShortcutExtensions.DOLocalMove(this.LightObjects.transform, new Vector3(0f, 700f, 0f), 1f, false).SetEase(Ease.InOutCubic).SetRelative(true)
				.OnComplete(delegate
				{
					this.LightSettingTween = false;
				});
		}
		return tween;
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x0003F550 File Offset: 0x0003D750
	public Tween SettingLights2(bool active)
	{
		if (this.PopTween2)
		{
			return null;
		}
		this.PopTween2 = true;
		this.ObjectsOn2 = active;
		Tween tween;
		if (active)
		{
			tween = ShortcutExtensions.DOLocalMove(this.LightSwitch2.transform, new Vector3(0f, -700f, 0f), 1f, false).SetEase(Ease.InOutBack).SetRelative(true)
				.OnComplete(delegate
				{
					this.PopTween2 = false;
				});
		}
		else
		{
			tween = ShortcutExtensions.DOLocalMove(this.LightSwitch2.transform, new Vector3(0f, 700f, 0f), 1f, false).SetEase(Ease.InOutBack).SetRelative(true)
				.OnComplete(delegate
				{
					this.PopTween2 = false;
				});
		}
		return tween;
	}

	// Token: 0x06000772 RID: 1906 RVA: 0x0003F610 File Offset: 0x0003D810
	public void PopLightObject(bool active)
	{
		if (this.PopTween)
		{
			return;
		}
		this.PopTween = true;
		this.ObjectsOn = active;
		if (this.ObjectsOn)
		{
			ShortcutExtensions.DOLocalMove(this.LightBulb.transform, new Vector3(0f, 670f, 0f), 1f, false).SetEase(Ease.InOutBack).SetRelative(true);
			ShortcutExtensions.DOLocalMove(this.LightSwitch.transform, new Vector3(0f, 400f, 0f), 1f, false).SetEase(Ease.InOutBack).SetRelative(true)
				.OnComplete(delegate
				{
					this.PopTween = false;
				});
			return;
		}
		ShortcutExtensions.DOLocalMove(this.LightBulb.transform, new Vector3(0f, -670f, 0f), 1f, false).SetEase(Ease.InOutBack).SetRelative(true);
		ShortcutExtensions.DOLocalMove(this.LightSwitch.transform, new Vector3(0f, -400f, 0f), 1f, false).SetEase(Ease.InOutBack).SetRelative(true)
			.OnComplete(delegate
			{
				this.PopTween = false;
			});
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x0003F744 File Offset: 0x0003D944
	public Tween TurnLight()
	{
		if (this.TurnTween)
		{
			return null;
		}
		this.TurnTween = true;
		this.LightOn = !this.LightOn;
		ShortcutExtensions.DOLocalMove(this.LightSwitch.transform, new Vector3(0f, -50f, 0f), 0.3f, false).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutBack)
			.SetRelative(true);
		Tween tween;
		if (this.LightOn)
		{
			tween = this.NightBlack.DOFade(1f, 1f).OnComplete(delegate
			{
				this.TurnTween = false;
			});
		}
		else
		{
			tween = this.NightBlack.DOFade(0f, 1f).OnComplete(delegate
			{
				this.TurnTween = false;
			});
		}
		return tween;
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x00012EE7 File Offset: 0x000110E7
	private void Update()
	{
		if (this.danceObject.MusicStart)
		{
			this.danceObject.SetColor();
		}
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x0003F80C File Offset: 0x0003DA0C
	private static bool IsLateHour()
	{
		int hour = DateTime.Now.Hour;
		return hour >= 21 || hour < 6;
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x0003F834 File Offset: 0x0003DA34
	private static bool IsOnTheHour()
	{
		return DateTime.Now.Minute == 0;
	}

	// Token: 0x06000777 RID: 1911 RVA: 0x0003F854 File Offset: 0x0003DA54
	public void StopEveryDesktopAction()
	{
		List<WinionHandler> winionHandlers = GameManager.instance.GetWinionHandlers();
		for (int i = 0; i < winionHandlers.Count; i++)
		{
			winionHandlers[i].winionBehaviour.StopDesktopAction();
		}
	}

	// Token: 0x06000778 RID: 1912 RVA: 0x0003F890 File Offset: 0x0003DA90
	public void PlayDancePary()
	{
		List<WinionHandler> winionHandlers = GameManager.instance.GetWinionHandlers();
		for (int i = 0; i < winionHandlers.Count; i++)
		{
			ComponentHolderProtocol.AddComponent<WinionDance>(winionHandlers[i]);
		}
		this.danceObject.StartMusic();
	}

	// Token: 0x06000779 RID: 1913 RVA: 0x0003F8D4 File Offset: 0x0003DAD4
	private Vector2 GetRandomPosition()
	{
		Vector2 vector;
		vector..ctor((float)Screen.width, (float)Screen.height);
		float num = vector.x * 0.1f;
		float num2 = vector.y * 0.1f;
		float num3 = Random.Range(num, vector.x - num);
		float num4 = Random.Range(num2, vector.y - num2);
		Vector2 vector2;
		vector2..ctor(num3, num4);
		Vector2 vector3;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(this.canvasRectTransform, vector2, null, ref vector3);
		return vector3;
	}

	// Token: 0x0600077A RID: 1914 RVA: 0x00012F01 File Offset: 0x00011101
	public void MoveRandomPositionEoni()
	{
		this.danceObject.PunchScaleEoni(this.GetRandomPosition());
	}

	// Token: 0x0600077B RID: 1915 RVA: 0x00012F19 File Offset: 0x00011119
	private void DesktopActionInit()
	{
		this.DesktopActionWeights = new List<int>();
		base.StartCoroutine("DesktopActionRoutine");
	}

	// Token: 0x0600077C RID: 1916 RVA: 0x00012F32 File Offset: 0x00011132
	private IEnumerator DesktopActionRoutine()
	{
		for (;;)
		{
			yield return new WaitForSeconds(this.actionTick);
			if (!DesktopController.PlayingDesktopAction)
			{
				DesktopController.DesktopAction actionByWeights = (DesktopController.DesktopAction)this.GetActionByWeights();
				Debug.Log(actionByWeights);
				switch (actionByWeights)
				{
				case DesktopController.DesktopAction.TurnOffLight:
					if (!this.isNight)
					{
						base.StartCoroutine("TurnLightEvent");
					}
					break;
				case DesktopController.DesktopAction.Plane:
					if (!this.Plane.activeSelf)
					{
						this.Plane.GetComponent<PlaneDrop>().SetAnimation(false);
						this.Plane.SetActive(true);
					}
					break;
				case DesktopController.DesktopAction.SystemWinionPlane:
					if (!this.Plane.activeSelf)
					{
						this.Plane.GetComponent<PlaneDrop>().SetAnimation(true);
						this.Plane.SetActive(true);
					}
					break;
				}
			}
		}
		yield break;
	}

	// Token: 0x0600077D RID: 1917 RVA: 0x00012F41 File Offset: 0x00011141
	public void WeightAppend(DesktopController.DesktopAction index, int value)
	{
		this.DesktopActionWeights.Add(value);
	}

	// Token: 0x0600077E RID: 1918 RVA: 0x0003F94C File Offset: 0x0003DB4C
	public int GetActionByWeights()
	{
		this.DesktopActionWeights.Clear();
		this.WeightAppend(DesktopController.DesktopAction.TurnOffLight, 0);
		this.WeightAppend(DesktopController.DesktopAction.Plane, this.CanGetPlane() ? 10 : 0);
		this.WeightAppend(DesktopController.DesktopAction.SystemWinionPlane, this.CanGetPlane() ? 2 : 0);
		int num = this.DesktopActionWeights.Sum();
		if (num == 0)
		{
			return -1;
		}
		int num2 = Random.Range(0, num);
		int num3 = 0;
		for (int i = 0; i < this.DesktopActionWeights.Count; i++)
		{
			num3 += this.DesktopActionWeights[i];
			if (num2 < num3)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x0600077F RID: 1919 RVA: 0x00012F4F File Offset: 0x0001114F
	public void GetPictureBoolValueChange()
	{
		DesktopController.GetPicture = this.GetPictureToggle.isOn;
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x0003F9DC File Offset: 0x0003DBDC
	public void GetPictureCountValueChange()
	{
		int num;
		if (int.TryParse(this.GetPictureCount.text, out num))
		{
			DesktopController.PictureCount = num;
			if (DesktopController.PictureCount < 0)
			{
				DesktopController.PictureCount = 0;
				this.GetPictureCount.text = "0";
				return;
			}
		}
		else
		{
			DesktopController.PictureCount = 0;
			this.GetPictureCount.text = "0";
		}
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x00012F61 File Offset: 0x00011161
	public bool CanGetPictures()
	{
		return DesktopController.GetPicture && (DesktopController.PictureCount == 0 || this.ImageParent.childCount < DesktopController.PictureCount);
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x00012F8A File Offset: 0x0001118A
	public bool CanGetPlane()
	{
		return DesktopController.GetPlane;
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x00012F91 File Offset: 0x00011191
	public bool CanGetPoop()
	{
		return DesktopController.GetPoop;
	}

	// Token: 0x04000856 RID: 2134
	public bool HoverActive = true;

	// Token: 0x04000857 RID: 2135
	public GameObject HoverTab_1;

	// Token: 0x04000858 RID: 2136
	public GameObject HoverTab_2;

	// Token: 0x04000859 RID: 2137
	public GameObject HoverTab_3;

	// Token: 0x0400085A RID: 2138
	public Canvas MainCanvas;

	// Token: 0x0400085B RID: 2139
	public CanvasGroup NightBlack;

	// Token: 0x0400085C RID: 2140
	public GameObject LightObjects;

	// Token: 0x0400085D RID: 2141
	public bool LightSetting;

	// Token: 0x0400085E RID: 2142
	public bool LightSettingTween;

	// Token: 0x0400085F RID: 2143
	public GameObject LightBulb;

	// Token: 0x04000860 RID: 2144
	public bool LightOn;

	// Token: 0x04000861 RID: 2145
	public bool TurnTween;

	// Token: 0x04000862 RID: 2146
	public GameObject LightSwitch;

	// Token: 0x04000863 RID: 2147
	public bool ObjectsOn;

	// Token: 0x04000864 RID: 2148
	public bool PopTween;

	// Token: 0x04000865 RID: 2149
	public Button LightButton;

	// Token: 0x04000866 RID: 2150
	public GameObject LightSwitch2;

	// Token: 0x04000867 RID: 2151
	public bool ObjectsOn2;

	// Token: 0x04000868 RID: 2152
	public bool PopTween2;

	// Token: 0x04000869 RID: 2153
	public DanceObject danceObject;

	// Token: 0x0400086A RID: 2154
	public GameObject PresentBomb;

	// Token: 0x0400086B RID: 2155
	public List<Sprite> PopUpImages;

	// Token: 0x0400086C RID: 2156
	public Transform DesktopImageCanvas;

	// Token: 0x0400086D RID: 2157
	public RectTransform LeftInPosition;

	// Token: 0x0400086E RID: 2158
	public RectTransform RightInPosition;

	// Token: 0x0400086F RID: 2159
	public Transform brokenScreen;

	// Token: 0x04000870 RID: 2160
	public WinionRoomSetting winionRoomSettings;

	// Token: 0x04000871 RID: 2161
	public bool isNight;

	// Token: 0x04000872 RID: 2162
	public bool nightEvent;

	// Token: 0x04000873 RID: 2163
	public RectTransform canvasRectTransform;

	// Token: 0x04000874 RID: 2164
	public GameObject Plane;

	// Token: 0x04000875 RID: 2165
	public static bool PlayingDesktopAction;

	// Token: 0x04000876 RID: 2166
	public float actionTick = 3f;

	// Token: 0x04000877 RID: 2167
	private List<int> DesktopActionWeights;

	// Token: 0x04000878 RID: 2168
	public Toggle GetPictureToggle;

	// Token: 0x04000879 RID: 2169
	public TMP_InputField GetPictureCount;

	// Token: 0x0400087A RID: 2170
	public Transform ImageParent;

	// Token: 0x0400087B RID: 2171
	public static bool GetPicture;

	// Token: 0x0400087C RID: 2172
	public static int PictureCount;

	// Token: 0x0400087D RID: 2173
	public Toggle GetPlaneToggle;

	// Token: 0x0400087E RID: 2174
	public static bool GetPlane;

	// Token: 0x0400087F RID: 2175
	public Toggle GetPoopToggle;

	// Token: 0x04000880 RID: 2176
	public static bool GetPoop;

	// Token: 0x02000137 RID: 311
	public enum DesktopAction
	{
		// Token: 0x04000882 RID: 2178
		DoNothing = -1,
		// Token: 0x04000883 RID: 2179
		TurnOffLight,
		// Token: 0x04000884 RID: 2180
		Plane,
		// Token: 0x04000885 RID: 2181
		SystemWinionPlane
	}
}
