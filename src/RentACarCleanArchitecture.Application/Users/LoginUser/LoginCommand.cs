using RentACarCleanArchitecture.Application.Abstractions.Messaging;

namespace RentACarCleanArchitecture.Application.Users.LoginUser;


public record LoginCommand(string Email, string Password) : ICommand<string>;