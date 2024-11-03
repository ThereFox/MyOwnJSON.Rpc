namespace RegistrationTests.TestControllers;

public class SimpleController
{
    public void SampleActionName()
    {
        return;
    }

    public string SampleActionNameWithReturnType()
    {
        return "TestValue";
    }

    public async Task SampleActionNameAsync()
    {
        await Task.CompletedTask;
        return;
    }
    
    public async Task<string> SampleActionNameWithReturnTypeAsync()
    {
        await Task.CompletedTask;
        return "TestValue";
    }
    
}