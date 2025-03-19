using FlavoursomeWeb.Data.Enums;

namespace FlavoursomeWeb.Helpers
{
    public class PluralQuanitityTypes
    {
        public static string GetPluralType(double? quantity, QuantityType? rawType)
        {
            if (rawType == null)
            {
                return "";
            }
            string quanitityType = rawType.ToString(); // Convert enum to string

            if (quantity == null || quantity == 1 || quantity < 1)
            {
                return quanitityType; // Singular form

            }

            return quanitityType + "s"; // Plural form (basic)
        }
    }
}
