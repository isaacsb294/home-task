using Application.Abstractions.Messaging;

namespace Application.Chores.RemoveProduct;

public record RemoveProductCommand(Guid ChoreId, Guid ProductId) : ICommand; 