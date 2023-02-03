using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.GiamKichSan.BaseModels
{
	public class ItemCollectionEntity
	{
		public string ID { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public List<ItemCollectionEntity> Parent { get; set; } = new List<ItemCollectionEntity>();
	}
}
