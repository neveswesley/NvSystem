namespace NvSystem.Application.Services.Interfaces;

public interface IUnitOfWork
{
    Task Commit();
}