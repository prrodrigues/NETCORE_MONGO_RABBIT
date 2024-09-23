using Test.RentMotorCycles.Infrastructure;


public class Class1 : Factory
{
    public Class1()
    {
        String queueName = "subscribeVehicle";
        SetPublisher(queueName, "5");

    }
}
