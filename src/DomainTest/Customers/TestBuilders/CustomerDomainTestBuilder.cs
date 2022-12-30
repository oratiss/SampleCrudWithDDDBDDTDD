using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ReflectionTools;

namespace DomainTest.Customers.TestBuilders
{
    public class CustomerDomainTestBuilder:ReflectionBuilder<CustomerDomain, CustomerDomainTestBuilder >
    {
        private CustomerDomainTestBuilder _builderInstance;

        public override CustomerDomain Build()
        {
            _builderInstance = this;
        }
    }
}
