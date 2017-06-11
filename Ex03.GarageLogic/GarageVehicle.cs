namespace Ex03.GarageLogic
{
    internal class GarageVehicle
    {
        internal const string k_OwnerNameKey = "Owner Name";
        internal const string k_OwnerPhoneNumberKey = "Owner Phone Number";
        private eVehicleState m_VehicleState;
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private Vehicle m_OwnerVehicle;

        internal eVehicleState VehicleState
        {
            get { return m_VehicleState; }
            set { m_VehicleState = value; }
        }

        internal Vehicle OwnerVehicle
        {
            get { return m_OwnerVehicle; }
        }

        internal string OwnerName
        {
            get { return m_OwnerName; }
        }

        internal string OwnerPhoneNumber
        {
            get { return m_OwnerPhoneNumber; }
        }

        public GarageVehicle(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_OwnerVehicle)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_OwnerVehicle = i_OwnerVehicle;
            m_VehicleState = eVehicleState.InRepair;
        }

        internal enum eVehicleState
        {
            InRepair,
            RepairDone,
            PaidFor       
        }
    }
}
