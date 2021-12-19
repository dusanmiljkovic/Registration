using Registration.Domain.Interfaces;

namespace Registration.Api.Services;
public class BaseService
{
    public BaseService(Serilog.ILogger logger, IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    protected internal IUnitOfWork UnitOfWork { get; set; }
}
