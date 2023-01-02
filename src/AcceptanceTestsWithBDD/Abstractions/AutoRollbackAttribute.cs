﻿using System.Reflection;
using System.Transactions;
using Xunit.Sdk;

namespace AcceptanceTestsWithBDD.Abstractions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AutoRollbackAttribute : BeforeAfterTestAttribute
    {
        private TransactionScope _scope = null!;
        public TransactionScopeAsyncFlowOption AsyncFlowOption { get; set; } = TransactionScopeAsyncFlowOption.Enabled;
        public IsolationLevel IsolationLevel { get; set; } = IsolationLevel.ReadCommitted;
        public TransactionScopeOption ScopeOption { get; set; } = TransactionScopeOption.Required;
        public long TimeoutInMs { get; set; } = -1;

        public override void Before(MethodInfo methodUnderTest)
        {
            var options = new TransactionOptions { IsolationLevel = IsolationLevel };
            if (TimeoutInMs > 0)
                options.Timeout = TimeSpan.FromMilliseconds(TimeoutInMs);

            _scope = new TransactionScope(ScopeOption, options, AsyncFlowOption);
        }

        public override void After(MethodInfo methodUnderTest)
        {
            _scope.Dispose();
        }
    }
}
