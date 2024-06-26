﻿namespace Avalonia.Diagnostics;

/// <summary>
/// Defines diagnostic extensions on <see cref="StyledElement"/>s.
/// </summary>
public static class StyledElementExtensions
{
    /// <summary>
    /// Gets a style diagnostics for a <see cref="StyledElement"/>.
    /// </summary>
    /// <param name="styledElement">The element.</param>
    public static ValueStoreDiagnostic GetValueStoreDiagnostic(this StyledElement styledElement)
    {
        return styledElement.GetValueStore().GetStoreDiagnostic();
    }
}
