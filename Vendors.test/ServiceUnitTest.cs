using Microsoft.AspNetCore.Mvc;
using Vendors.Service;
using Assignment3.Controllers;
using Vendors.Entities;
using Assignment3.Components;

namespace Vendors.test
{
    public class ServiceUnitTest
    {
        [Fact]
        public void TotalInEntity_InvoiceLineItemView()
        {
            // Arrange
            Invoice invoice = new Invoice()
            {
                InvoiceId = 1,
                InvoiceLineItems = new List<InvoiceLineItem>()
            };

            invoice.InvoiceLineItems.Add(new InvoiceLineItem() { Amount = 10});
            invoice.InvoiceLineItems.Add(new InvoiceLineItem() { Amount = 500});
            invoice.InvoiceLineItems.Add(new InvoiceLineItem() { Amount = 800});

            // Act
            string total = (invoice.InvoiceLineItems.Sum(x => x.Amount)).ToString("C");


            // Assert
            Assert.Equal("$1,310.00", total);

        }


    }
}