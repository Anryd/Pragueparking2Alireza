using System;
using System.Collections.Generic;
using System.IO;
using static Pragueparking2.GlobalDef;

namespace Pragueparking2
{
	public class ParkingLot
	{
		Slot<Vehicle>[] parkingslot = new Slot<Vehicle>[MaxParkingslot];

		// Constructor to init array
		public ParkingLot()
		{
			for (int i = 0; i < MaxParkingslot; i++)
			{
				parkingslot[i] = new Slot<Vehicle>();
			}

		}


		/// <summary>
		/// Add vehicle to array and returns Index of new vehicle
		/// </summary>
		/// <param name="RegNo">Registration number</param>
		/// <param name="CarType">Type of vehicle with help of VehicleType enum</param>
		/// <returns>Index of added vehicle or NotFoundOrInvalid{-1} if operation is not successful</returns>
		public int Add(string RegNo, VehicleType CarType)
		{
			int FreePlaceIndex = NotFoundOrInvalid;

			switch (CarType)
			{
				case VehicleType.Car:
					Car car = new Car(RegNo, DateTime.Now);
					FreePlaceIndex = GetFreeSlot(car.Size);
					if (FreePlaceIndex != NotFoundOrInvalid)
						parkingslot[FreePlaceIndex].Add(car);
					break;
				case VehicleType.MC:
					MC mc = new MC(RegNo, DateTime.Now);
					FreePlaceIndex = GetFreeSlot(mc.Size);
					if (FreePlaceIndex != NotFoundOrInvalid)
						parkingslot[FreePlaceIndex].Add(mc);
					break;
				case VehicleType.Trike:
					Tricke tricke = new Tricke(RegNo, DateTime.Now);
					FreePlaceIndex = GetFreeSlot(tricke.Size);
					if (FreePlaceIndex != NotFoundOrInvalid)
						parkingslot[FreePlaceIndex].Add(tricke);
					break;
				case VehicleType.Bike:
					Bike bike = new Bike(RegNo, DateTime.Now);
					FreePlaceIndex = GetFreeSlot(bike.Size);
					if (FreePlaceIndex != NotFoundOrInvalid)
						parkingslot[FreePlaceIndex].Add(bike);
					break;
				default:
					break;
			}
			return FreePlaceIndex;
		}


		/// <summary>
		/// Get the best free place for vehicle to have maximum space in parking
		/// </summary>
		/// <param name="Size">Size of new vehicle</param>
		/// <param name="MaxFreeSize">Optional value, between 1 to 4 (default), to return space limited to max</param>
		/// <returns>New suitable free space or NotFoundOrInvalid{-1} when a free space is not found</returns>
		private int GetFreeSlot(int Size, int MaxFreeSize = GlobalDef.MaxSlotSize)
		{
			for (int s = Size; s <= MaxFreeSize; s++)
			{
				for (int i = 0; i < GlobalDef.MaxParkingslot; i++)
				{
					if (parkingslot[i].FreeSpace == s)
						return i;
				}
			}
			return NotFoundOrInvalid;
		}

		/// <summary>
		/// Removes vehicle from array according to Vehicle object
		/// </summary>
		/// <param name="vehicle">An object derived from Vehicle class</param>
		/// <returns>Index of deleted item or NotFoundOrInvalid{-1} when could not find vehicle or operation is unsuccessful</returns>
		internal int Remove(Vehicle vehicle)
		{
			int Index = Search(vehicle);
			if (Index != NotFoundOrInvalid)
			{
				parkingslot[Index].Remove(vehicle.RegNo);
				return Index;
			}
			return NotFoundOrInvalid;
		}


		/// <summary>
		/// Removes vehicle from array according to Registration number
		/// </summary>
		/// <param name="RegNo">Registration number of vehicle to be removed</param>
		/// <returns>Index of deleted item or NotFoundOrInvalid{-1} when could not find vehicle or operation is unsuccessful</returns>
		internal int Remove(string RegNo)
		{
			int Index = Search(RegNo);
			if (Index != NotFoundOrInvalid)
			{
				parkingslot[Index].Remove(RegNo);
				return Index;
			}
			return NotFoundOrInvalid;
		}


		/// <summary>
		/// Search array for specific Registration number
		/// </summary>
		/// <param name="RegNo">Registration number of vehicle to be searched</param>
		/// <returns>Index of item or NotFoundOrInvalid{-1} when could not find vehicle</returns>
		internal int Search(String RegNo)
		{
			for (int i = 0; i < MaxParkingslot; i++)
				if (parkingslot[i].Search(RegNo))
					return i;
			return NotFoundOrInvalid;
		}


		/// <summary>
		/// Search array for specific Vehicle object
		/// </summary>
		/// <param name="vehicle">Object derived from Vehicle class to be searched</param>
		/// <returns>Index of item or NotFoundOrInvalid{-1} when could not find vehicle</returns>
		internal int Search(Vehicle vehicle)
		{
			for (int i = 0; i < MaxParkingslot; i++)
				if (parkingslot[i].Search(vehicle.RegNo))
					return i;
			return NotFoundOrInvalid;
		}

	
		/// <summary>
		/// Move a vehicle to a new position
		/// </summary>
		/// <param name="vehicle">Object derived from Vehicle class to be moved</param>
		/// <param name="NewIndex"></param>
		/// <returns>Old index of vehicle or NotFoundOrInvalid{-1} when operation is unsuccessful</returns>
		internal int Move(Vehicle vehicle, int NewIndex)
		{
			if (Search(vehicle) != NotFoundOrInvalid)
			{
				if (parkingslot[NewIndex].FreeSpace >= vehicle.Size)
				{
					Remove(vehicle);
					parkingslot[NewIndex].Add(vehicle);
					return NewIndex;
				}
			}
			return NotFoundOrInvalid;
		}


		/// <summary>
		/// Move vehicle of specified Registration number to new place
		/// </summary>
		/// <param name="RegNo">Registration number of vehicle to be moved</param>
		/// <param name="NewIndex">New index or slot which vehicle will be moved</param>
		/// <returns>Old index of vehicle or NotFoundOrInvalid{-1} when operation is unsuccessful</returns>
		internal int Move(string RegNo, int NewIndex)
		{
			int OldIndex = Search(RegNo);
			if (OldIndex != NotFoundOrInvalid)
			{
				Vehicle v = GetVehicle(RegNo);
				if (parkingslot[NewIndex].FreeSpace >= v.Size)
				{
					Remove(v);
					parkingslot[NewIndex].Add(v);
					return OldIndex;
				}
			}
			return NotFoundOrInvalid;
		}

		/// <summary>
		/// Move all vehicles of old index to a new position with specific index
		/// </summary>
		/// <param name="OldIndex">Old index of vehicles</param>
		/// <param name="NewIndex">New intended index for vehicles</param>
		/// <returns>Number of vehicles has been moved or 0 when nothing moved</returns>
		internal int MoveAllVehicle(int OldIndex, int NewIndex)
		{
			int Counter = 0;
			foreach (String RegNO in parkingslot[OldIndex].ContentRegNo())
			{
				Vehicle v = GetVehicle(RegNO);
				Remove(RegNO);
				parkingslot[NewIndex].Add(v);
				Counter++;
			}
			return Counter;
		}


		/// <summary>
		/// Return an IEnumerable list of all vehicles in specific place
		/// </summary>
		/// <param name="Index">Index or slot of place to get list of all vehicles</param>
		/// <returns>An IEnumerable list of all vehicles or an empty list</returns>
		internal IEnumerable<Vehicle> Content(int Index)
		{
			return parkingslot[Index].ContentList();
		}


		/// <summary>
		/// Move all movable vehicles to a new place to achieve maximum possible free space
		/// </summary>
		/// <returns>Number of vehicles which has been moved</returns>
		internal int Optimize()
		{
			int Counter = 0;

			for (int i = GlobalDef.MaxParkingslot - 1; i > 0; i--)
			{
				if (parkingslot[i].FreeSpace == 1 || parkingslot[i].FreeSpace == 2 || parkingslot[i].FreeSpace == 3)
				{
					for (int j = 0; j < i; j++)
					{
						if (parkingslot[j].FreeSpace == 1 || parkingslot[j].FreeSpace == 2 || parkingslot[j].FreeSpace == 3)
						{
							int FreeSpace = GetFreeSlot(parkingslot[i].Size - parkingslot[i].FreeSpace, 3);
							if (FreeSpace != NotFoundOrInvalid & i != FreeSpace)
							{
								Counter += MoveAllVehicle(i, FreeSpace);
								break;
							}
						}
					}
				}
			}

			return Counter;
		}


		/// <summary>
		/// Initi Array with random registration number for demo and test
		/// </summary>
		internal void ParkingInitArrayRandom()
		{
			Random random = new Random();
			int iNumVehicle = 100;
			for (int i = 0; i < iNumVehicle; i++)
			{
				int Type = random.Next(1, 5);
				int iLen = random.Next(4, 7);

				string strRandRegNo = Path.GetRandomFileName();
				strRandRegNo = strRandRegNo.Replace(".", "");  // remove '.' from file extension
				strRandRegNo = strRandRegNo.Substring(0, iLen);
				if (Add(strRandRegNo.ToUpper(), (VehicleType)Type) == NotFoundOrInvalid)
					return;

			}
		}


		/// <summary>
		/// Return a Vehicle object (a derived class of Vehicle) according to Registration number
		/// </summary>
		/// <param name="RegNo">Registration number of vehicle</param>
		/// <returns>A Vehicle obect or an object which derived from Vehicle or null when vehicle is not found</returns>
		internal Vehicle GetVehicle(string RegNo)
		{
			for (int i = 0; i < MaxParkingslot; i++)
				if (parkingslot[i].Search(RegNo))
					return parkingslot[i].GetVehicle(RegNo);
			return null;
		}


		/// <summary>
		/// Count number of parked vehicles for a specific type of vehicle
		/// </summary>
		/// <param name="Type">Type of vehicle from VehicleType enum</param>
		/// <returns>Number of vehicles which exist</returns>
		internal int GetCount(VehicleType Type)
		{
			int Count = 0;
			foreach (var item in parkingslot)
			{
				Count += item.GetCount(Type);
			}

			return Count;
		}


		/// <summary>
		/// Count the number of free space for specific vehicle type
		/// </summary>
		/// <param name="Type">Type of vehicle from VehicleType enum</param>
		/// <returns>Number of free space in Parking of given VehicleType</returns>
		internal int CountFreeSpace(VehicleType Type)
		{
			int Count = 0;
			foreach (var item in parkingslot)
			{
				Count += item.FreeSpace / Convert.ToInt32(Type);
			}

			return Count;
		}
	}
}
