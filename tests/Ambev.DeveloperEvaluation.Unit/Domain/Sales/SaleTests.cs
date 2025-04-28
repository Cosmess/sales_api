using System;
using Xunit;
using FluentAssertions;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Sales
{
    public class SaleTests
    {
        [Fact]
        public void ShouldApply10PercentDiscount_WhenQuantityIsBetween4And9()
        {
            var sale = new Sale("S-123", DateTime.UtcNow, "Customer Test", "Branch Test");
            sale.AddItem("Test Product", 5, 100);

            sale.Items[0].DiscountPercentage.Should().Be(0.10m);
        }

        [Fact]
        public void ShouldApply20PercentDiscount_WhenQuantityIsBetween10And20()
        {
            var sale = new Sale("S-124", DateTime.UtcNow, "Customer Test", "Branch Test");
            sale.AddItem("Test Product", 15, 100);

            sale.Items[0].DiscountPercentage.Should().Be(0.20m);
        }

        [Fact]
        public void ShouldThrowException_WhenQuantityIsMoreThan20()
        {
            var sale = new Sale("S-125", DateTime.UtcNow, "Customer Test", "Branch Test");

            Action act = () => sale.AddItem("Test Product", 21, 100);

            act.Should().Throw<ArgumentException>()
                .WithMessage("Cannot sell more than 20 identical items.");
        }

        [Fact]
        public void ShouldNotApplyDiscount_WhenQuantityIsLessThan4()
        {
            var sale = new Sale("S-126", DateTime.UtcNow, "Customer Test", "Branch Test");
            sale.AddItem("Test Product", 2, 100);

            sale.Items[0].DiscountPercentage.Should().Be(0.0m);
        }

        [Fact]
        public void ShouldCancelSaleSuccessfully()
        {
            var sale = new Sale("S-127", DateTime.UtcNow, "Customer Test", "Branch Test");
            sale.Cancel();

            sale.Status.Should().Be(SaleStatus.Cancelled);
        }
    }
}
