

using System.Data;

namespace RentACarCleanArchitecture.Application.Abstractions.Data;

public interface ISqlConnectionFactory
{

    IDbConnection CreateConnection();

}