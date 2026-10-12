using RentACarCleanArchitecture.Application.Abstractions.Messaging;

namespace RentACarCleanArchitecture.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string Email, 
    string Nombre, 
    string Apellidos, 
    string Password) : ICommand<Guid>;