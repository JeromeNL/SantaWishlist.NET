using System.Linq.Expressions;

namespace SantaWishlistApp.Utils;

public static class ReflectionHelper
{
    public static string PropertyName<T>(Expression<Func<T, object>> property) where T : class
    {
        var memberExpression = property.Body switch
        {
            UnaryExpression unaryExpression => unaryExpression.Operand as MemberExpression,
            MemberExpression member => member,
            _ => throw new ArgumentException("The provided expression is not a member access expression.",
                nameof(property))
        };

        if (memberExpression == null)
            throw new ArgumentException("The provided expression is not a member access expression.", nameof(property));

        return memberExpression.Member.Name;
    }
}