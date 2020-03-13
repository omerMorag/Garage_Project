using System;
using System.Collections.Generic;
using System.Text;

using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageManagementUI
    {
        public enum eDisplayOption
        {
            AllVehicles = 1,
            SpecificStatus,
        }

        // $G$ CSS-003 (-5) Bad readonly members variable name (should be in the form of r_PamelCase).
        private readonly Garage m_Garage = new Garage();
        private InputUI m_InputUI = new InputUI();
        private OutputUI m_OutputUI = new OutputUI();

        public void Run()
        {
            InputUI.eUserChoice userChoice;
            bool exitProgram = false;

            while (!exitProgram)
            {
                m_OutputUI.PrintStringToScreen(Environment.NewLine);
                try
                {
                    userChoice = m_InputUI.PrintMenuAndGetChoiceFromUser();
                    switch (userChoice)
                    {
                        case InputUI.eUserChoice.InsertVehicleToGarage:
                            insertVehicleToGarage();
                            break;

                        case InputUI.eUserChoice.DisplayLicenseNumber:
                            displayAllVehiclesInGarageWithicenseNumber();
                            break;

                        case InputUI.eUserChoice.ChangeVehicleStatus:
                            changeVehicleTreatmentStatus();
                            break;

                        case InputUI.eUserChoice.InflateWheels:
                            inflateWheelsToMax();
                            break;

                        case InputUI.eUserChoice.FillFuelTank:
                            fillFuelTank();
                            break;

                        case InputUI.eUserChoice.CargeBattary:
                            CargeBattaryEngine();
                            break;

                        case InputUI.eUserChoice.DisplayFullVehicleData:
                            displayVehicleDetails();
                            break;

                        case InputUI.eUserChoice.Exit:
                            exitProgram = true;
                            break;

                        default:
                            m_OutputUI.InvalidInputTryAgainMsg();
                            break;
                    }
                }
                catch (ArgumentException ae)
                {
                    m_OutputUI.PrintStringToScreen(ae.Message);
                }
                catch (FormatException)
                {
                    m_OutputUI.InvalidInputTryAgainMsg();
                }
                catch (OverflowException)
                {
                    m_OutputUI.SomethingWrongMsg();
                }
            }
        }

        private void fillFuelTank()
        {
            string licensePlate;
            FuelSource.eFuelType fuelOptions = new FuelSource.eFuelType();
            string PartOfOptionsHeaderMsg = string.Format("fuel type");
            FuelSource.eFuelType fuelType;
            float fuelToEnter = 0;
            bool isValidInput = true;

            do
            {
                isValidInput = true;
                m_InputUI.GetVehicleLicenseNumberFromUser(m_Garage, out licensePlate);
                FuelSource gasTank = m_Garage.AllCarsInGarage[licensePlate].Vehicle.EnergySource as FuelSource;
                if (gasTank != null)
                {
                    try
                    {
                        fuelType = (FuelSource.eFuelType)m_InputUI.MakeArrayOfStringFromEnum(PartOfOptionsHeaderMsg, fuelOptions);
                        fuelToEnter = m_InputUI.GetDataFloatFromUser("the amount of fuel");
                        gasTank.AddFuel(fuelToEnter, fuelType);
                    }
                    catch (ValueOutOfRangeException)
                    {
                        m_OutputUI.PrintStringToScreen(string.Format("Out of range, the range should be between 0 to {0}", gasTank.MaxEnergy));
                        isValidInput = false;
                    }
                }
                else
                {
                    Console.WriteLine("Not fuel source!");
                }
            }
            while (!isValidInput);
        }

        private void CargeBattaryEngine()
        {
            string licensePlate;
            bool isValidInput = true; 
            float amountOfEnergy = 0;

            do
            {
                isValidInput = true;
                m_InputUI.GetVehicleLicenseNumberFromUser(m_Garage, out licensePlate);
                ElectricSource battaryEngine = m_Garage.AllCarsInGarage[licensePlate].Vehicle.EnergySource as ElectricSource;
                if (battaryEngine != null)
                {
                    try
                    {
                        amountOfEnergy = m_InputUI.GetDataFloatFromUser("minutue of electic engine");
                        battaryEngine.AddHoursToBatery(amountOfEnergy);
                    }
                    catch (ValueOutOfRangeException)
                    {
                        m_OutputUI.PrintStringToScreen(string.Format("Out of range, the range should be between 0 to {0}", battaryEngine.MaxEnergy));
                        isValidInput = false;
                    }
                }
                else
                {
                    Console.WriteLine("Not electric source!");
                }
            }
            while (!isValidInput);
        }

        private void insertVehicleToGarage()
        {
            bool isVehicleExists = true;
            string licensePlate = null, modelName, wheelManufacturer, ownerName, ownerPhone;
            Vehicle newVehicle;

            try
            {
                m_InputUI.GetVehicleLicenseNumberFromUser(m_Garage, out licensePlate);
            }
            catch (ArgumentException)
            {
                isVehicleExists = false;
            }

            if (isVehicleExists)
            {
                m_OutputUI.VehicleIsAlreadyExsistsMsg();
                m_Garage.ChangeStatus(licensePlate, VehicleTreatmentStatus.eStatus.InTreatment);
            }
            else
            {
                modelName = m_InputUI.GetVehicleModelName();
                wheelManufacturer = m_InputUI.GetWheelManufacturerFromUser();
                newVehicle = createNewVehicle(licensePlate, modelName, wheelManufacturer, out ownerName, out ownerPhone);
                m_Garage.InsertNewVehicleToGarage(newVehicle, ownerName, ownerPhone);
            }
        }


        // $G$ DSN-011 (-8) The component who responsible for creating vehicles, should be in a separate component.
        private Vehicle createNewVehicle(string i_LicensePlate, string i_ModelName, string i_WheelManufacturer, out string o_OwnerName, out string o_OwnerPhone)
        {
            Vehicle newVehicle;
            Vehicle.eTypeOfVehicle vehicleType, vehicleOptions = new Vehicle.eTypeOfVehicle();
            EnergySource.eSource vehicleEnergySource;
            EnergySource.eSource energySourceOptions = new EnergySource.eSource();
            string PartOfOptionsHeaderMsg = string.Format("vehicle type");

            o_OwnerName = m_InputUI.GetOwnerNameFromUser();
            o_OwnerPhone = m_InputUI.GetOwnerPhoneFromUser();

            vehicleType = (Vehicle.eTypeOfVehicle)m_InputUI.MakeArrayOfStringFromEnum(PartOfOptionsHeaderMsg, vehicleOptions);
            PartOfOptionsHeaderMsg = string.Format("energy source");
            if (vehicleType != Vehicle.eTypeOfVehicle.Truck)
            {
                vehicleEnergySource = (EnergySource.eSource)m_InputUI.MakeArrayOfStringFromEnum(PartOfOptionsHeaderMsg, energySourceOptions);
            }
            else
            {
                vehicleEnergySource = EnergySource.eSource.FuelSource;
            }

            newVehicle = CreateVehicle.CreateNewVehicle(vehicleType, vehicleEnergySource, i_LicensePlate, i_ModelName, i_WheelManufacturer);

            insertVehicleDetails(newVehicle);

            return newVehicle;
        }

        // $G$ DSN-002 (-10) The UI should not know Car\Truck\Motorcycle
        private void insertVehicleDetails(Vehicle i_NewVehicle)
        {
            if (i_NewVehicle is Motorcycle)
            {
                insertLicenseType((Motorcycle)i_NewVehicle);
                insertEngineCapacity((Motorcycle)i_NewVehicle);
            }
            else if (i_NewVehicle is Car)
            {
                insertColorForCar((Car)i_NewVehicle);
                insertQuantityOfDoorsForCar((Car)i_NewVehicle);
            }
            else
            {
                insertCargoFoTruck((Truck)i_NewVehicle);
                insertVolumeOfCargo((Truck)i_NewVehicle);
            }

            insertCurrrentAirPressureOfWheels(i_NewVehicle);
            insertAmountOfEnergyToAdd(i_NewVehicle);
        }

        private void insertEngineCapacity(Motorcycle i_NewMotorcycle)
        {
            int engineCapacity;

            engineCapacity = m_InputUI.GetEngineCapacityFfromUser();
            i_NewMotorcycle.EngineCapacity = engineCapacity;
        }

        // $G$ CSS-029 (-5) Bad code duplication.
        private void insertLicenseType(Motorcycle i_NewMotorcycle)
        {
            int licenseType;
            Motorcycle.eLisenceType licenseOptions = new Motorcycle.eLisenceType();
            string PartOfOptionsHeaderMsg = string.Format("license type");

            licenseType = m_InputUI.MakeArrayOfStringFromEnum(PartOfOptionsHeaderMsg, licenseOptions);
            i_NewMotorcycle.LicenseType = (Motorcycle.eLisenceType)licenseType;
        }

        private void insertQuantityOfDoorsForCar(Car i_NewCar)
        {
            int userChoice;
            Car.eNumberOfDoors amountOfDoorsOptions = new Car.eNumberOfDoors();
            string PartOfOptionsHeaderMsg = string.Format("amount of doors");

            userChoice = m_InputUI.MakeArrayOfStringFromEnum(PartOfOptionsHeaderMsg, amountOfDoorsOptions);
            i_NewCar.NumberOfDoors = (Car.eNumberOfDoors)userChoice;
        }

        private void insertColorForCar(Car i_NewCar)
        {
            int color;
            Car.eColor colorOptions = new Car.eColor();
            string PartOfOptionsHeaderMsg = string.Format("car's color");

            color = m_InputUI.MakeArrayOfStringFromEnum(PartOfOptionsHeaderMsg, colorOptions);
            i_NewCar.Color = (Car.eColor)color;
        }

        private void insertCargoFoTruck(Truck i_NewTruck)
        {
            int userChoice;
            Truck.eTruckContain cargoOptions = new Truck.eTruckContain();
            string PartOfOptionsMsg = string.Format("cargo type");

            userChoice = m_InputUI.MakeArrayOfStringFromEnum(PartOfOptionsMsg, cargoOptions);
            if (userChoice == (int)Truck.eTruckContain.ContainsHazardousSubstances)
            {
                i_NewTruck.isContainsHazardousSubstances = true;
            }
            else
            {
                i_NewTruck.isContainsHazardousSubstances = false;
            }
        }

        private void insertVolumeOfCargo(Truck i_NewTruck)
        {
            float volumeOfCargo;

            volumeOfCargo = m_InputUI.GetVolumeOfCargoFromUusr();
            i_NewTruck.CapicityCargo = volumeOfCargo;
        }

        private void insertCurrrentAirPressureOfWheels(Vehicle i_NewVehicle)
        {
            bool isValid = true;
            float airPressureToAdd;

            do
            {
                    airPressureToAdd = m_InputUI.GetDataFloatFromUser(string.Format("current air pressure"));
                    foreach (Wheel currentWheel in i_NewVehicle.Wheels)
                    {
                        isValid = currentWheel.AddAirPressure(airPressureToAdd);
                    }

                if (!isValid)
                {
                    m_OutputUI.PrintStringToScreen(string.Format(
@"Air perssure isn't in correct range,
now the pressure is {0} and at most for this vehcile is {1}",
    i_NewVehicle.Wheels[0].CurrentAirPressure,
    i_NewVehicle.Wheels[0].MaxAirPressure));
                    isValid = false;
                }
            }
            while (!isValid);
        }

        private void insertAmountOfEnergyToAdd(Vehicle i_NewVehicle)
        {
            string input, msg;
            float AmountOfEnergyToEnter;
            bool isValidInput = true;

            if(i_NewVehicle.EnergySource is FuelSource)
            {
                msg = string.Format("please enter the current fuel situation of the vehicle");
            }
            else
            {
                msg = string.Format("please enter the current battary situation of the vehicle");
            }
            
            do
            {
                m_OutputUI.PrintStringToScreen(msg);
                try
                {
                    input = Console.ReadLine();
                    AmountOfEnergyToEnter = float.Parse(input);
                    i_NewVehicle.EnergySource.UpdateEnergy(AmountOfEnergyToEnter);
                    isValidInput = true;
                }
                catch (ValueOutOfRangeException)
                {
                    m_OutputUI.PrintStringToScreen(string.Format("{0}", i_NewVehicle.EnergySource.OutOfRangMsg()));
                    isValidInput = false;
                }
                catch (FormatException)
                {
                    m_OutputUI.InvalidInputTryAgainMsg();
                    isValidInput = false;
                }
            }
            while (!isValidInput);

            i_NewVehicle.UpdateCurretEnergyPercent();
        }

        private void displayAllVehiclesInGarageWithicenseNumber()
        {
            VehicleTreatmentStatus.eStatus statusOptions = new VehicleTreatmentStatus.eStatus();
            eDisplayOption displayOption = new eDisplayOption();
            int displayChoice, userChoice = 0;
            string displayOptionMsg = string.Format("how to want to filter your search");
            string PartOfOptionsHeaderMsg = string.Format("which vehicels you want to see");
            bool displayAll = false;

            displayChoice = m_InputUI.MakeArrayOfStringFromEnum(displayOptionMsg, displayOption);
            if ((eDisplayOption)displayChoice == eDisplayOption.AllVehicles)
            {
                displayAll = true;
            }
            else
            {
                userChoice = m_InputUI.MakeArrayOfStringFromEnum(PartOfOptionsHeaderMsg, statusOptions);
            }

            m_OutputUI.PrintLicenseNumber(userChoice, m_Garage.AllCarsInGarage, displayAll);
        }

        private void changeVehicleTreatmentStatus()
        {
            int userChoice;
            string licenseNumber;
            VehicleTreatmentStatus.eStatus statusOptions = new VehicleTreatmentStatus.eStatus();
            string PartOfOptionsHeaderMsg = string.Format("to which treatment status you want to change");

            m_InputUI.GetVehicleLicenseNumberFromUser(m_Garage, out licenseNumber);
            userChoice = m_InputUI.MakeArrayOfStringFromEnum(PartOfOptionsHeaderMsg, statusOptions);

            m_Garage.AllCarsInGarage[licenseNumber].CompareStatus((VehicleTreatmentStatus.eStatus)userChoice);
            m_Garage.AllCarsInGarage[licenseNumber].Status = (VehicleTreatmentStatus.eStatus)userChoice;
        }

        private void inflateWheelsToMax()
        {
            string licenseNumber;

            m_InputUI.GetVehicleLicenseNumberFromUser(m_Garage, out licenseNumber);
            foreach (Wheel currentWheel in m_Garage.AllCarsInGarage[licenseNumber].Vehicle.Wheels)
            {
                currentWheel.AddAirPressure(currentWheel.MaxAirPressure - currentWheel.CurrentAirPressure);
            }

            m_OutputUI.PrintStringToScreen("The air in the wheels is now at the maximum quantity");
        }

        private void displayVehicleDetails()
        {
            string licenseNumber;

            m_InputUI.GetVehicleLicenseNumberFromUser(m_Garage, out licenseNumber);
            m_OutputUI.PrintStringToScreen(m_OutputUI.CreateSpaces(m_Garage.AllCarsInGarage[licenseNumber].ToString()));
        }
    }
}