using System;

namespace QuackBuddy.Services;

public interface ISoundService : IDisposable
{
    void PlayQuack();
}
