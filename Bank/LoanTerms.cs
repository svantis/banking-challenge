namespace Bank
{
    public static class LoanTerms
    {
        public static double Interest { get; set; } = 0.05;

        public static int PaymentPeriodsPerYear { get; set; } = 12;

        public static int AdminFeeFixed { get; set; } = 10000;

        public static double AdminFeeVariable { get; set; } = 0.01;
    }
}
