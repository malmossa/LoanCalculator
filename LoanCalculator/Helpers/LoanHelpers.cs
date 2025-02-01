using LoanCalculator.Models;

namespace LoanCalculator.Helpers
{
    public class LoanHelper
    {
        public Loan GetPayments(Loan loan)
        {

            //Calculate my Monthly Payment
            loan.Payment = CalcPayment(loan.Amount, loan.Rate, loan.Term);

            //Create Loop from 1 to the term
            decimal balance = loan.Amount;
            decimal totalInterest = 0.0m;
            decimal monthlyRate = CalcMonthlyRate(loan.Rate);

            //loop over each month until we reach the term of the loan
            for (int month = 1; month <= loan.Term; month++)
            {
                decimal monthlyInterest = CalcMonthlyInterest(balance, monthlyRate);
                totalInterest += monthlyInterest;
                decimal monthlyPrincipal = loan.Payment - monthlyInterest;
                balance -= monthlyPrincipal;

                LoanPayment loanPayment = new();

                loanPayment.Month = month;
                loanPayment.Payment = loan.Payment;
                loanPayment.MonthlyPrincipal = monthlyPrincipal;
                loanPayment.MonthlyInterest = monthlyInterest;
                loanPayment.TotalInterest = totalInterest;
                loanPayment.Balance = balance;

                //push the object into the loan model
                loan.Payments.Add(loanPayment);

            }

            loan.TotalInterest = totalInterest;
            loan.TotalCost = loan.Amount + totalInterest;

            //return the loan to the view
            return loan;
        }

        private static decimal CalcPayment(decimal amount, decimal rate, int term)
        {
            decimal monthlyRate = CalcMonthlyRate(rate);
            double rateD = Convert.ToDouble(monthlyRate);
            double amountD = Convert.ToDouble(amount);

            double paymentD = (amountD * rateD) / (1 - Math.Pow(1 + rateD, -term));


            return Convert.ToDecimal(paymentD);
        }

        private static decimal CalcMonthlyRate(decimal rate)
        {
            return rate / 1200;
        }

        private static decimal CalcMonthlyInterest(decimal balance, decimal monthlyRate)
        {
            return balance * monthlyRate;
        }

    }
}
