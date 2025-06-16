namespace Application.Helpers
{
    public static class ConvertHelper
    {
        public static double CentsToDollars(this int cents)
            => cents / 100;
        
    }
}
