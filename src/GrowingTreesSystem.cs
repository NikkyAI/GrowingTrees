using System;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

[assembly: ModInfo("GrowingTrees",
	Description = "Adds trees which grow dynamically",
	Website     = "https://github.com/copygirl/GrowingTrees",
	Authors     = new []{ "copygirl" })]

namespace GrowingTrees
{
	public class GrowingTreesSystem : ModSystem
	{
		public static string MOD_ID = "growingtrees";
		
		public override void Start(ICoreAPI api)
		{
			api.RegisterBlockBehaviorClass(
				BranchPlacementBehavior.NAME,
				typeof(BranchPlacementBehavior));
		}
	}
}
