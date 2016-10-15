using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Domain;

namespace HiLaarIsch.BusinessLayer
{
    public static class CustomerExtensions
    {
        public static void Map(this CustomerModel source, CustomerEntity destination)
        {
            destination.Firstname = source.Firstname;
            destination.Surname = source.Surname;
            destination.GroupLevel = source.GroupLevel;
            destination.PhoneNumber = source.PhoneNumber;
            destination.EmergencyNumber = source.EmergencyNumber;
            destination.Address = source.Address;
        }
    }
}
