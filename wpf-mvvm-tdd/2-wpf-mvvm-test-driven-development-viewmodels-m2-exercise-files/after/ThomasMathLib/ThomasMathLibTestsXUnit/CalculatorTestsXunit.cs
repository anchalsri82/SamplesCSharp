using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThomasMathLib;
using Xunit;

namespace ThomasMathLibTestsXUnit
{
  public class CalculatorTestsXunit
  {
    [Theory]
    [InlineData(5, 2, 3)]
    [InlineData(10, 4, 6)]
    [InlineData(2, 5, -3)]
    public void AddShouldReturnSumOfValues(
      int expectedResult,
      int val1,
      int val2)
    {
      var calculator = new Calculator();
      var result = calculator.Add(val1, val2);
      Assert.Equal(expectedResult, result);
    }
  }
}
