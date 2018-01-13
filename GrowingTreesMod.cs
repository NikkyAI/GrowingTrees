using System;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace GrowingTrees
{
	public class GrowingTreesMod : ModBase
	{
		public static GrowingTreesMod INSTANCE { get; private set; }
		
		public static ModInfo MOD_INFO { get; } = new ModInfo {
			Name        = "GrowingTrees",
			Description = "Adds trees which grow dynamically",
			Website     = "https://github.com/copygirl/GrowingTrees",
			Author      = "copygirl",
			Version     = "0.1.0",
		};
		
		
		public override ModInfo GetModInfo() { return MOD_INFO; }
		
		public override void Start(ICoreAPI api)
		{
			base.Start(api);
			INSTANCE = this;
			
			api.RegisterBlockBehavior(BlockBehaviorPlacement.NAME, typeof(BlockBehaviorPlacement));
		}
	}
}
