using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Commands.PaymentCommands.CreatePayment;
using ExpensePaymentSystem.Business.Commands.PaymentCommands.DeletePayment;
using ExpensePaymentSystem.Business.Commands.PaymentCommands.UpdatePayment;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Business.Interfaces;
using ExpensePaymentSystem.Business.Queries.PaymentQueries.GetAllPayments;
using ExpensePaymentSystem.Business.Queries.PaymentQueries.GetPaymentsByParameter;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpensePaymentSystem.WebApi.Controllers;

    /// <summary>
    /// Controller for managing payment-related operations.
    /// </summary>
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor to initialize the PaymentController with IPaymentService.
        /// </summary>
        /// <param name="paymentService">The payment service to handle payment-related operations.</param>
        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get a list of all payments.
        /// </summary>
        /// <returns>A list of payment objects.</returns>
        [HttpGet]
        public async Task<ApiResponse<List<PaymentResponse>>> Get()
        {
            var operation = new GetAllPaymentsQuery();
            var result = await _mediator.Send(operation);
            return result;
        }
        
           
        [HttpGet("parameter")]
        public async Task<ApiResponse<List<PaymentResponse>>> GetByParameter([FromQuery] decimal? amount, [FromQuery] DateTime? paymentDate,  [FromQuery] string? paymentMethod)
        {
            var query = new GetPaymentsByParameterQuery(amount, paymentDate, paymentMethod);
            var result = await _mediator.Send(query);

            return result;
        }

        /// <summary>
        /// Get payment by ID.
        /// </summary>
        /// <param name="paymentId">The ID of the payment to retrieve.</param>
        /// <returns>The payment object with the specified ID.</returns>
        [HttpGet("{paymentId}")]
        public async  Task<ApiResponse<PaymentResponse>> GetPaymentById(int paymentId)
        {
            var operation = new GetPaymentByIdQuery(paymentId);
            var result = await _mediator.Send(operation);
            return result;
        }

        /// <summary>
        /// Create a new payment.
        /// </summary>
        /// <param name="payment">The payment object to create.</param>
        /// <returns>The created payment object.</returns>
        [HttpPost]
        public async Task<ApiResponse<PaymentResponse>> CreatePayment([FromBody] PaymentRequest Contact)
        {
            var operation = new CreatePaymentCommand(Contact);
            var result = await _mediator.Send(operation);
            return result; }

        /// <summary>
        /// Update an existing payment.
        /// </summary>
        /// <param name="paymentId">The ID of the payment to update.</param>
        /// <param name="payment">The updated payment object.</param>
        /// <returns>The updated payment object.</returns>
        [HttpPut("{paymentId}")]
        public async Task<ApiResponse<PaymentResponse>> UpdatePayment(int paymentId, [FromBody] PaymentRequest Contact)
        {
            var operation = new UpdatePaymentCommand(paymentId, Contact);
            var result = await _mediator.Send(operation);
            return result;
        }

        /// <summary>
        /// Delete a payment by ID.
        /// </summary>
        /// <param name="paymentId">The ID of the payment to delete.</param>
        /// <returns>No content if successful, or not found if the payment doesn't exist.</returns>
        [HttpDelete("{paymentId}")]
        public async Task<ApiResponse> DeletePayment(int paymentId)
        {
            var operation = new DeletePaymentCommand(paymentId);
            var result = await _mediator.Send(operation);
            return result;
        }
    }