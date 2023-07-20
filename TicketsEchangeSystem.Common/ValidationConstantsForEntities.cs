namespace TicketsEchangeSystem.Common
{
    public static class ValidationConstantsForEntities
    {
        public static class Ticket
        {
            public const int TitleMinLength = 10;
            public const int TitleMaxLength = 100;

            public const int CountryMinLength = 5;
            public const int CountryMaxLength = 100;


            public const int CityNameMinLength = 5;
            public const int CityNameMaxLength = 100;

            public const int AddresslineMaxLength = 255;

            public const int PlaceOfEventMaxLength = 100;

            public const int ImageUrlMaxLenght = 20148;

            public const string PricePerTicketMinValue = "0";
            public const string PricePerTicketMaxValue = "999";
        }

        public static class Status
        {
            public const int CurrentStatusTitleMaxLength = 20;
        }

        public static class Category
        {
            public const int NameMaxLength = 40;
        }
    }
}
