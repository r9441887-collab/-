using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// Token: 0x020000DC RID: 220
public class VolumeController : MonoBehaviour
{
	// Token: 0x06000554 RID: 1364 RVA: 0x00034A78 File Offset: 0x00032C78
	private void Awake()
	{
		if (VolumeController.MasterGroup == null && this._MasterGroup != null)
		{
			VolumeController.MasterGroup = this._MasterGroup;
		}
		this.MasterSlider.minValue = 0.001f;
		this.BGMVolume.minValue = 0.001f;
		this.SfxVolume.minValue = 0.001f;
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x000114DD File Offset: 0x0000F6DD
	private void OnEnable()
	{
		this.MasterSlider.value = VolumeController.LastValue;
		this.BGMVolume.value = VolumeController.LastBGMValue;
		this.SfxVolume.value = VolumeController.LastSFXValue;
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x0001150F File Offset: 0x0000F70F
	private void OnDisable()
	{
		VolumeController.LastValue = this.MasterSlider.value;
		VolumeController.LastBGMValue = this.BGMVolume.value;
		VolumeController.LastSFXValue = this.SfxVolume.value;
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x00034ADC File Offset: 0x00032CDC
	private void Update()
	{
		VolumeController.MasterGroup.SetFloat("MasterVol", Mathf.Log(this.MasterSlider.value) * 20f);
		VolumeController.MasterGroup.SetFloat("BgmVol", Mathf.Log(this.BGMVolume.value) * 20f);
		VolumeController.MasterGroup.SetFloat("SfxVol", Mathf.Log(this.SfxVolume.value) * 20f);
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x00011541 File Offset: 0x0000F741
	public void ImmediatelyVolume()
	{
		VolumeController.MasterGroup.SetFloat("MasterVol", Mathf.Log(this.MasterSlider.value) * 20f);
	}

	// Token: 0x040005D8 RID: 1496
	public Slider MasterSlider;

	// Token: 0x040005D9 RID: 1497
	public Slider BGMVolume;

	// Token: 0x040005DA RID: 1498
	public Slider SfxVolume;

	// Token: 0x040005DB RID: 1499
	public AudioMixer _MasterGroup;

	// Token: 0x040005DC RID: 1500
	public static AudioMixer MasterGroup;

	// Token: 0x040005DD RID: 1501
	public static float LastValue = 1f;

	// Token: 0x040005DE RID: 1502
	public static float LastBGMValue = 1f;

	// Token: 0x040005DF RID: 1503
	public static float LastSFXValue = 1f;
}
