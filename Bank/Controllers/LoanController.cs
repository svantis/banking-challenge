using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Bank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
        [HttpPost]
        [Route("CalculateLoanSummary")]
        public Dictionary<String, double> CalculateLoanSummary(int amount, int years)
        {
            double periodInterest = LoanTerms.Interest / LoanTerms.PaymentPeriodsPerYear;
            int periods = years * LoanTerms.PaymentPeriodsPerYear;

            double EAR = (Math.Pow(1 + periodInterest, LoanTerms.PaymentPeriodsPerYear) - 1) * 100;
            double monthlypayment = Math.Round(amount * (periodInterest / (1 - Math.Pow(1 + periodInterest, -periods))), 2);
            double interestAmount = monthlypayment * periods - amount;

            Dictionary<String, double> summary = new();
            summary.Add("Effective Annual Rate", EAR);
            summary.Add("Monthly payment", monthlypayment);
            summary.Add("Total Interest Amount", interestAmount);
            summary.Add("Administration fee", AdminFee(amount));

            return summary;
        }
        [HttpPost]
        [Route("SetInterest")]
        public void SetInterest(double interest)
        {
            LoanTerms.Interest = interest;
        }
        [HttpPost]
        [Route("SetPeriods")]
        public void SetPeriods(int paymentPeriodsPerYear)
        {
            LoanTerms.PaymentPeriodsPerYear = paymentPeriodsPerYear;
        }
        [HttpPost]
        [Route("SetFixedAdminFee")]
        public void SetFixedAdminFee(int fee)
        {
            LoanTerms.AdminFeeFixed = fee;
        }
        [HttpPost]
        [Route("SetVariableAdminFee")]
        public void SetVariableAdminFee(double rate)
        {
            LoanTerms.AdminFeeVariable = rate;
        }

        private static double AdminFee(int amount)
        {
            return Math.Min(LoanTerms.AdminFeeVariable * amount, LoanTerms.AdminFeeFixed);
        }
    }
}
