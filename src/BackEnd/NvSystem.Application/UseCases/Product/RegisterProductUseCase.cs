using NvSystem.Application.Services.Interfaces;
using NvSystem.Communications.Requests;
using NvSystem.Communications.Responses;
using NvSystem.Domain.Interfaces;
using NvSystem.Exceptions.ExceptionsBase;

namespace NvSystem.Application.UseCases.Product;

public class RegisterProductUseCase
{
    
    public ResponseRegisterProductJson Execute(RequestRegisterProductJson request)
    {
        Validate(request);

        var barcode = BarCodeGenerate();
       

        var response = new ResponseRegisterProductJson()
        {
            Name = request.Name,
            Barcode = barcode,
            CategoryId = request.CategoryId,
            SalePrice = request.SalePrice,
            StockQuantity = request.StockQuantity
        };
        
        

        // Mapeamento da entidade
        // Salvar no banco

        return response;
    }

    private void Validate(RequestRegisterProductJson request)
    {
        var validator = new RegisterProductValidator();

        var result = validator.Validate(request);
        if (result.IsValid == false)

        {
            var errorMessage = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessage);
        }
    }

    private string BarCodeGenerate()
    {
        return Guid.NewGuid()
            .ToString("N")
            .Substring(0, 12)
            .ToUpper();
    }
}