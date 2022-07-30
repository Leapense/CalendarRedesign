using System;

namespace Calendar_Redesign.Contracts.Services;

public interface IPageService
{
    Type GetPageType(string key);
}
