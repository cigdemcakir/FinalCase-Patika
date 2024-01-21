namespace ExpensePaymentSystem.Business.Interfaces;

public interface IPaymentSimulator
{
    Task<bool> SimulatePaymentAsync(decimal amount, int recipientId);

}