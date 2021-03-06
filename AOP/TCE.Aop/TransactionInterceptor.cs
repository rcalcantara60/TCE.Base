using System;
using System.Transactions;
using TCE.Aop.Core;
using Castle.DynamicProxy;
using TCE.CrossCutting.Dto;

namespace TCE.Aop
{
    /// <summary>
    /// Intercepts the invocation target and wraps it around a <see cref="TransactionScope"/>.
    /// </summary>
    public sealed class TransactionInterceptor : InterceptorBase<WithTransactionAttribute>
    {
        protected override void Intercept(IInvocation invocation, WithTransactionAttribute marker)
        {
            bool transactional = false;

            foreach (var attr in invocation.Method.GetCustomAttributes(true))
            {
                if (attr.GetType().Name.Equals("WithTransactionAttribute"))
                    transactional = true;

            }
            if (transactional)
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = marker.IsolationLevel,
                    Timeout = TimeSpan.FromMilliseconds(marker.Timeout)
                };
                using (var scope = new TransactionScope(marker.TransactionScopeOption, transactionOptions))
                {
                    Log(marker.LoggerLevel, () => "Transaction started.");
                    try
                    {
                        invocation.Proceed();
                        if (invocation.ReturnValue is ValidationResultDto && ((ValidationResultDto)invocation.ReturnValue).IsValid)
                            scope.Complete();
                        Log(marker.LoggerLevel, () => "Transaction completed.");
                    }
                    catch (Exception e)
                    {
                        Logger.Error("Transaction cannot be completed.", e);
                        throw;
                    }
                }
            }
            else
                invocation.Proceed();
        }
    }

    /// <summary>Marks a class/method to intercept with the <see cref="TransactionInterceptor"/>.</summary>
    public sealed class WithTransactionAttribute : MarkerBaseAttribute
    {
        /// <summary>Transaction scope creation options.</summary>
        public TransactionScopeOption TransactionScopeOption { get; set; }

        /// <summary>Gets or sets the timeout period for the transaction (in milliseconds).</summary>
        public int Timeout { get; set; }

        /// <summary>Gets or sets the isolation level of the transaction.</summary>
        public IsolationLevel IsolationLevel { get; set; }
    }
}