namespace CRUDTests
{
	public class UnitTest1
	{
		[Fact]
		public void Test1()
		{
			//Arrange the declaration of variables and collecting the inputs
			MyMath mm = new MyMath();
			int a = 10;
			int b = 20;
			int expected_value = 30;


			//Act calling the method which method you would like to test
			int method_result = mm.add(a, b);

			//Assert comparing the expected value with actual value
			//If the expected value and actual value both
			//are same then they pass the test case otherwise not
			Assert.Equal(method_result, expected_value);
		}
	}
}