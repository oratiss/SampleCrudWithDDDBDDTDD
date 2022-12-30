using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainTest.Customers.TestBuilders;

namespace DomainTest.Customers
{
    public class CustomerTest
    {
        [Fact]
        public async Task FirstName_Should_Not_Be_NullORWhiteSpace()
        {
            //arrange
            var customer = new CustomerDomainTestBuilder();
            //.With(x => x.FirstName, string.Empty)
            //.Build();
        }
    }
}
