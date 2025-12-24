namespace NvSystem.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Barcode { get; private set; } = string.Empty;
    public decimal SalePrice { get; private set; } = decimal.Zero;
    public int StockQuantity { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;

    protected Product()
    {
    }

    public Product(string name, string barcode, decimal salePrice, int stockQuantity, Guid categoryId)
    {
        Id = Guid.NewGuid();
        Barcode = barcode;
        SetName(name);
        SetPrice(salePrice);
        StockQuantity = stockQuantity >= 0
            ? stockQuantity
            : throw new ArgumentException("Initial stock invalid.");
        CategoryId = categoryId;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty");

        Name = name.Trim();
    }

    private void SetPrice(decimal price)
    {
        if (price <= 0)
            throw new ArgumentException("Price cannot be less or equal to zero");

        SalePrice = price;
    }

    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity cannot be less or equal to zero");

        StockQuantity += quantity;
    }

    public void DecreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity cannot be less or equal to zero");

        StockQuantity -= quantity;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }
}