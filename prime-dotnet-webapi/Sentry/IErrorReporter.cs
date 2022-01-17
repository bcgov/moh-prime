using System;
using System.Threading.Tasks;
public interface IErrorReporter
{
    Task CaptureAsync(Exception exception);
    Task CaptureAsync(string message);
}