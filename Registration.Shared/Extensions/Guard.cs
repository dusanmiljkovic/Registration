using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Shared.Extensions;

/// <summary>
/// Contains extension methods for checking <see langword="null"/> value of an object.
/// </summary>
public static class Guard
{
    /// <summary>
    /// If an object is <see langword="null"/> throws <see cref="ArgumentNullException"/>; returns the same object otherwise.
    /// </summary>
    /// <typeparam name="T">The type of an object.</typeparam>
    /// <param name="obj">The object which should be checked.</param>
    /// <param name="name">The name of an object which should be checked.</param>
    /// <exception cref="ArgumentNullException">Thrown when <typeparamref name="T"/> object is <see langword="null"/>.</exception>
    public static T NotNull<T>(this T obj, string name)
        where T : class
    {
        return obj ?? throw new ArgumentNullException(name);
    }
}
