﻿using Microsoft.Windows.ApplicationModel.Resources;

namespace Calendar_Redesign.Helpers;

internal static class ResourceExtensions
{
    private static readonly ResourceLoader _resourceLoader = new();

    public static string GetLocalized(this string resourceKey) => _resourceLoader.GetString(resourceKey);
}