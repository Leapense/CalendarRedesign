﻿using System.Threading.Tasks;

using Calendar_Redesign.Contracts.Services;
using Calendar_Redesign.Core.Helpers;

using Windows.Storage;

namespace Calendar_Redesign.Services;

public class LocalSettingsServicePackaged : ILocalSettingsService
{
    public async Task<T> ReadSettingAsync<T>(string key)
    {
        if (ApplicationData.Current.LocalSettings.Values.TryGetValue(key, out var obj))
        {
            return await Json.ToObjectAsync<T>((string)obj);
        }

        return default;
    }

    public async Task SaveSettingAsync<T>(string key, T value)
    {
        ApplicationData.Current.LocalSettings.Values[key] = await Json.StringifyAsync(value);
    }
}
