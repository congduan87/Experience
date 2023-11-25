using Common.GiamKichSan.BaseModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.GiamKichSan
{
	public class CommonFile
	{
		public static bool Create(string path)
		{
			if (string.IsNullOrWhiteSpace(path))
				return false;
			if (File.Exists(path))
				return true;
			else
			{
				File.Create(path);
				return true;
			}
		}
		public static bool Rename(string pathOld, string pathNew)
		{
			if (string.IsNullOrWhiteSpace(pathOld) || string.IsNullOrWhiteSpace(pathNew))
				return false;

			if (!File.Exists(pathOld))
				return false;

			if (File.Exists(pathNew))
				return false;

			File.Move(pathOld, pathNew);
			return true;
		}
		public static bool Delete(string path)
		{
			if (string.IsNullOrWhiteSpace(path))
				return false;
			if (!File.Exists(path))
				return false;
			else
			{
				File.Delete(path);
				return true;
			}
		}
		public static List<ItemCollectionEntity> GetFiles(string path, int level = 0)
		{
			var output = new List<ItemCollectionEntity>();
			if (string.IsNullOrWhiteSpace(path)) return output;
			if (!Directory.Exists(path))
			{
				var directory = Directory.CreateDirectory(path);
				output.Add(new ItemCollectionEntity() { Name = directory.Name });
				return output;
			}

			var directions = Directory.GetDirectories(path);
			if (directions == null || directions.Length == 0)
				return output;

			foreach (var direction in directions)
			{
				output.Add(new ItemCollectionEntity() { Name = direction.Split('\\')[7] });
			}

			for (int indexLevel = 0; indexLevel < level; indexLevel++)
			{

			}

			return output;
		}
	}
}
