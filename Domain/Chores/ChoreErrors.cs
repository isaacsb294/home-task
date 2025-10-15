using Shared;

namespace Domain.Chores;

public static class ChoreErrors
{
    public static readonly Error BlankName = new(
        "Chore.BlankName", 
        "The chore name cannot be null or blank.");
    
    public static readonly Error BlankDescription = new(
        "Chore.BlankDescription", 
        "The chore description cannot be null or blank.");
    
    public static readonly Error ProductExists = new(
        "Chore.ProductExists", 
        "This product has already been added to the chore.");
    
    public static readonly Error ProductNotExists = new(
        "Chore.ProductNotExists", 
        "This product does not exist on the chore.");
    
    public static readonly Error UserExists = new(
        "Chore.UserExists", 
        "This user has already been added as a responsible person for the chore.");
    
    public static readonly Error UserNotExists = new(
        "Chore.UserNotExists", 
        "This user is not a responsible person for the chore.");

    public static readonly Error NotFound = new(
        "Chore.NotFound", 
        "The chore could not be found.");
}