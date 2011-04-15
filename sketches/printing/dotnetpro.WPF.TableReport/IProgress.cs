using System;

namespace dotnetpro.WPF.TableReport
{
    public interface IProgressContext
    {
        void Init(string text, int MaxPages);
        void UpdateProgress(int Page);
        void Finish();
        bool Canceled { get; }
    }
}
