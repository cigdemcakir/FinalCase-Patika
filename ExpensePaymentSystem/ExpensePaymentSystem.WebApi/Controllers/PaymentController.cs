using ExpensePaymentSystem.Business.Interfaces;
using ExpensePaymentSystem.Data.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ExpensePaymentSystem.WebApi.Controllers;

    /// <summary>
    /// Controller for managing payment-related operations.
    /// </summary>
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        /// <summary>
        /// Constructor to initialize the PaymentController with IPaymentService.
        /// </summary>
        /// <param name="paymentService">The payment service to handle payment-related operations.</param>
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        /// Get a list of all payments.
        /// </summary>
        /// <returns>A list of payment objects.</returns>
        [HttpGet]
        public async Task<IActionResult> GetPayments()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(payments);
        }

        /// <summary>
        /// Get payment by ID.
        /// </summary>
        /// <param name="paymentId">The ID of the payment to retrieve.</param>
        /// <returns>The payment object with the specified ID.</returns>
        [HttpGet("{paymentId}")]
        public async Task<IActionResult> GetPaymentById(int paymentId)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(paymentId);
            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }

        /// <summary>
        /// Create a new payment.
        /// </summary>
        /// <param name="payment">The payment object to create.</param>
        /// <returns>The created payment object.</returns>
        [HttpPost]
        public async Task<IActionResult> CreatePayment(Payment payment)
        {
            var createdPayment = await _paymentService.CreatePaymentAsync(payment);
            return CreatedAtAction(nameof(GetPaymentById), new { paymentId = createdPayment.PaymentId }, createdPayment);
        }

        /// <summary>
        /// Update an existing payment.
        /// </summary>
        /// <param name="paymentId">The ID of the payment to update.</param>
        /// <param name="payment">The updated payment object.</param>
        /// <returns>The updated payment object.</returns>
        [HttpPut("{paymentId}")]
        public async Task<IActionResult> UpdatePayment(int paymentId, Payment payment)
        {
            var updatedPayment = await _paymentService.UpdatePaymentAsync(paymentId, payment);
            if (updatedPayment == null)
            {
                return NotFound();
            }

            return Ok(updatedPayment);
        }

        /// <summary>
        /// Delete a payment by ID.
        /// </summary>
        /// <param name="paymentId">The ID of the payment to delete.</param>
        /// <returns>No content if successful, or not found if the payment doesn't exist.</returns>
        [HttpDelete("{paymentId}")]
        public async Task<IActionResult> DeletePayment(int paymentId)
        {
            var result = await _paymentService.DeletePaymentAsync(paymentId);
            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }