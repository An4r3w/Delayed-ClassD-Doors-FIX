using System;
using Exiled.API.Interfaces;

namespace Arithfeather.DelayedClassdDoors
{
	// Token: 0x02000002 RID: 2
	public class Config : IConfig
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002048 File Offset: 0x00000248
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002050 File Offset: 0x00000250
		public bool IsEnabled { get; set; } = true;

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002059 File Offset: 0x00000259
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002061 File Offset: 0x00000261
		public float TimeUntilDoorsOpen { get; set; } = 5f;
	}
}
