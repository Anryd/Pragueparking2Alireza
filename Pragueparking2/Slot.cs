using System;
using System.Collections.Generic;
using static Pragueparking2.GlobalDef;

namespace Pragueparking2
{
	class Slot<V> where V : Vehicle
	{
		internal int Size { get => GlobalDef.MaxSlotSize; }
		internal int FreeSpace { get; private set; } = 4;
		private List<Vehicle> list = new List<Vehicle>();	// A list of Vehicle object in the slot


		/// <summary>
		/// Add a Vehicle to the list and change FreeSize accordingly
		/// </summary>
		/// <param name="vehicle">a derived class from Vehicle</param>
		public void Add(V vehicle)
		{
			list.Add(vehicle);
			FreeSpace -= vehicle.Size;
		}


		/// <summary>
		/// Get Vehicle object from received Registration number
		/// </summary>
		/// <param name="RegNo">Registration number of vehicle</param>
		/// <returns>A Vehicle ojbect and null if registration number is not found</returns>
		public Vehicle GetVehicle(string RegNo)
		{
			foreach (Vehicle veh in list)
			{
				if (veh.RegNo.ToUpper() == RegNo.ToUpper())
					return veh;
			}
			return null;
		}

		/// <summary>
		/// Search list for specified registration number
		/// </summary>
		/// <param name="RegNo">Registration number of vehicle</param>
		/// <returns>True if registration number is found otherwise False</returns>
		public bool Search(string RegNo)
		{
			foreach (Vehicle veh in list)
			{
				if (veh.RegNo.ToUpper() == RegNo.ToUpper())
					return true;
			}
			return false;
		}

		/// <summary>
		/// Remove vehicle which matches to given registration number
		/// </summary>
		/// <param name="RegNo">Registration number of vehicle</param>
		/// <returns>True if Vehicle is removed otherwise False</returns>
		public bool Remove(string RegNo)
		{
			int VehicleSize = list.Find(v => v.RegNo == RegNo).Size;
			int Success = list.RemoveAll(veh => veh.RegNo == RegNo);
			if (Success > 0)
			{
				FreeSpace += VehicleSize;
				return true;
			}
			else
				return false;
		}


		/// <summary>
		/// List all the content of this slot
		/// </summary>
		/// <returns>And IEnumerable list of Vehicles or an empty list</returns>
		public IEnumerable<Vehicle> ContentList() => list;


		/// <summary>
		/// Get a string list of existing registration number in slot
		/// </summary>
		/// <returns>Return a string list of existing registration number in slot</returns>
		public IEnumerable<String> ContentRegNo()
		{
			var RegNoList = new List<String>();
			foreach(var item in list)
			{
				RegNoList.Add(item.RegNo);
			}
			return RegNoList;
		}

		/// <summary>
		/// Count number of exist VehicleType in the slot
		/// </summary>
		/// <param name="vt">A VehicleType enum which represent type of vehicle</param>
		/// <returns>Number of vehicles which has been found</returns>
		public int GetCount(VehicleType vt)
		{
			int MyCount = 0;
			foreach (var item in list)
				if (item.Size == Convert.ToInt32(vt))
					MyCount++;

			return MyCount;
		}
	}
}
