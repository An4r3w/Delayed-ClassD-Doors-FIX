using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Exiled.API.Features;
using Exiled.Events;
using Exiled.Events.Handlers;
using Interactables.Interobjects.DoorUtils;
using MEC;
using Player = Exiled.Events.Handlers.Player;
using Server = Exiled.Events.Handlers.Server;


namespace Arithfeather.DelayedClassdDoors
{
	// Token: 0x02000003 RID: 3
	public class DelayedClassdDoors : Plugin<Config>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002085 File Offset: 0x00000285
		public override string Author
		{
			get
			{
				return "Arith";
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000208C File Offset: 0x0000028C
		public override void OnDisabled()
		{
			Server.WaitingForPlayers -= new Events.CustomEventHandler(this.Server_WaitingForPlayers);
			Server.RoundStarted -= new Events.CustomEventHandler(this.Server_RoundStarted);
			base.OnDisabled();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020BA File Offset: 0x000002BA
		public override void OnEnabled()
		{
			base.OnEnabled();
			Server.WaitingForPlayers += new Events.CustomEventHandler(this.Server_WaitingForPlayers);
			Server.RoundStarted += new Events.CustomEventHandler(this.Server_RoundStarted);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020E8 File Offset: 0x000002E8
		private List<DoorVariant> _cachedPrisonDoors { get; } = new List<DoorVariant>();

		// Token: 0x0600000A RID: 10 RVA: 0x000020F0 File Offset: 0x000002F0
		private void Server_RoundStarted()
		{
			Timing.RunCoroutine(this.OpenDoorsAfterTime());
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020FE File Offset: 0x000002FE
		private IEnumerator<float> OpenDoorsAfterTime()
		{
			yield return Timing.WaitForSeconds(base.Config.TimeUntilDoorsOpen);
			this.OpenClassDDoors();
			yield break;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002110 File Offset: 0x00000310
		private void Server_WaitingForPlayers()
		{
			this._cachedPrisonDoors.Clear();
			ReadOnlyCollection<DoorVariant> doors = Exiled.API.Features.Map.Doors;
			int num = doors.Count<DoorVariant>();
			for (int i = 0; i < num; i++)
			{
				DoorVariant doorVariant = doors[i];
				if (doorVariant.name.StartsWith("Prison"))
				{
					doorVariant.ServerChangeLock(DoorLockReason.Regular079, true);
					this._cachedPrisonDoors.Add(doorVariant);
				}
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021B0 File Offset: 0x000003B0
		public void OpenClassDDoors()
		{
			int count = this._cachedPrisonDoors.Count;
			for (int i = 0; i < count; i++)
			{
				DoorVariant doorVariant = this._cachedPrisonDoors[i];
				doorVariant.ServerChangeLock(DoorLockReason.Regular079, false);
				doorVariant.NetworkTargetState = true;
			}
		}
	}
}
