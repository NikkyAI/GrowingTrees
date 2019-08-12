using System;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;

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
			api.Register<BranchPlacementBehavior>();
			api.Register<LeavesBreakingBehavior>();
		}
	}
	
	public static class Extensions
	{
		public static void Register<T>(this ICoreAPI api)
		{
			var name = (string)typeof(T).GetProperty("NAME").GetValue(null);
			if (typeof(BlockBehavior).IsAssignableFrom(typeof(T)))
				api.RegisterBlockBehaviorClass(name, typeof(T));
			else if (typeof(EntityBehavior).IsAssignableFrom(typeof(T)))
				api.RegisterEntityBehaviorClass(name, typeof(T));
			else throw new ArgumentException("T is not a block or entity behavior", nameof(T));
		}
	}
}
