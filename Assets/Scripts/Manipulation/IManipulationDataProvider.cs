using UnityEngine;

namespace HoloAppTestSample
{
    public interface IManipulationDataProvider
    {
        bool IsManipulating { get; }
        Vector3 SmoothVelocity { get; }
    }
}