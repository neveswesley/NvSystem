using NvSystem.Application.Services.Interfaces;

namespace NvSystem.Application.Services;

public class RandomBarCodeGenerator : IBarcodeGenerator
{
    public string Generate()
    {
        return Guid.NewGuid()
            .ToString("N")
            .Substring(0, 12)
            .ToUpper();
    }
}