using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace App.SlnAutoBuild
{
	internal static class SlnAutoBuild
	{
		public static void Execute()
		{
			foreach (var p in GetTargetSlnFilePaths())
			{
				var process = Process.Start(
					new ProcessStartInfo
					{
						FileName = Properties.Resources.MSBuildExePath,
						Arguments = string.Format("{0} -t:rebuild -p:configuration=Debug -maxcpucount:4", p),
					});

				process?.WaitForExit();
			}
		}

		private static IEnumerable<string> GetTargetSlnFilePaths()
		{
			return Directory.EnumerateFiles(Properties.Resources.ExploreTargetDirPath, "*.sln", SearchOption.AllDirectories);
		}
	}
}
